using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models
{
    public static class FileManager
    {
        public static Folder BuildTree(FileSystemContext db, string path)
        {
            List<FileSystemItem> items = new List<FileSystemItem>();
            Folder folder;
            if (path == "MyComputer")
            {
                IEnumerable<DriveInfo> drives = DriveInfo.GetDrives().ToList().Where(d => d.IsReady);
                NumberOfFiles numberOfFiles = new NumberOfFiles();
                foreach (var driver in drives)
                {
                    FileSystemItem fileSystemItem = new FileSystemItem
                    {
                        Name = driver.RootDirectory.FullName,
                        Path = driver.RootDirectory.FullName,
                        FileSystemItemType = FileSystemItemType.Folder
                    };
                    items.Add(fileSystemItem);
                    var number = GetFilesNumber(driver.RootDirectory, new NumberOfFiles());
                    numberOfFiles.SmallSize += number.SmallSize;
                    numberOfFiles.MiddleSize += number.MiddleSize;
                    numberOfFiles.LargeSize += number.LargeSize;
                }
                folder = new Folder
                {
                    FileSystemItems = items,
                    FullName = "MyComputer",
                    NumberOfFiles = numberOfFiles,
                    Path = "MyComputer"
                };
                db.Folders.Add(folder);
                db.SaveChanges();
                return folder;
            }
            DirectoryInfo directory = new DirectoryInfo(path);
            var subDirectories = directory.GetDirectories();
            if (subDirectories.Count() > 0)
            {
                foreach (var dir in subDirectories)
                {
                    FileSystemItem subDir = new FileSystemItem
                    {
                        Name = dir.Name,
                        Path = dir.FullName,
                        FileSystemItemType = FileSystemItemType.Folder
                    };
                    items.Add(subDir);
                }
            }
            var files = directory.GetFiles();
            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    FileSystemItem fsi = new FileSystemItem
                    {
                        Name = file.Name,
                        Path = file.DirectoryName,
                        FileSystemItemType = FileSystemItemType.File
                    };
                    items.Add(fsi);
                }
            }
            folder = new Folder
            {
                FileSystemItems = items,
                FullName = directory.FullName,
                NumberOfFiles = GetFilesNumber(directory, new NumberOfFiles()),
                Path = directory.FullName
            };
            db.Folders.Add(folder);
            db.SaveChanges();
            return folder;
        }
        public static NumberOfFiles GetFilesNumber(DirectoryInfo directory, NumberOfFiles numberOfFiles)
        {
            var files = directory.GetFiles();
            if (files.Length > 0)
            {
                numberOfFiles.SmallSize += files.Count(f => f.Length <= 10485760);
                numberOfFiles.MiddleSize += files.Count(f => f.Length > 10485760 && f.Length <= 104857600);
                numberOfFiles.LargeSize += files.Count(f => f.Length > 104857600);
            }
            var subDirectories = directory.GetDirectories();
            if (subDirectories.Length > 0)
            {
                foreach (var subdir in subDirectories)
                {
                    try
                    {
                        GetFilesNumber(subdir, numberOfFiles);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        continue;
                    }
                }
            }
            return numberOfFiles;
        }
    }
}