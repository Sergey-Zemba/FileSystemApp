using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FileSystemApp.Models.ViewModels;

namespace FileSystemApp.Models
{
    public static class FileManager
    {
        public static Folder BuildTree(FileSystemContext db, Guid id)
        {
            List<FileSystemItem> items = new List<FileSystemItem>();
            Folder folder;
            if (id == Guid.Empty)
            {
                IEnumerable<DriveInfo> drives = DriveInfo.GetDrives().ToList().Where(d => d.IsReady);
                NumberOfFiles numberOfFiles = new NumberOfFiles();
                foreach (var driver in drives)
                {
                    FileSystemItem fileSystemItem = new FileSystemItem
                    {
                        Id = Guid.NewGuid(),
                        FolderId = id,
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
                    Id = Guid.NewGuid(),
                    ParentId = id,
                    FileSystemItems = items,
                    FullName = "My Computer",
                    NumberOfFiles = numberOfFiles
                };
                db.Folders.Add(folder);
                db.SaveChanges();
                return folder;
            }
            FileSystemItem item = db.FileSystemItems.Find(id);
            DirectoryInfo directory = new DirectoryInfo(item.Path);
            try
            {
                var subDirectories = directory.GetDirectories();
                if (subDirectories.Count() > 0)
                {
                    foreach (var dir in subDirectories)
                    {
                        FileSystemItem subDir = new FileSystemItem
                        {
                            Id = Guid.NewGuid(),
                            FolderId = id,
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
                            Id = Guid.NewGuid(),
                            FolderId = id,
                            Name = file.Name,
                            Path = file.DirectoryName,
                            FileSystemItemType = FileSystemItemType.File
                        };
                        items.Add(fsi);
                    }
                }
                folder = new Folder
                {
                    Id = id,
                    ParentId = item.FolderId,
                    FileSystemItems = items,
                    FullName = directory.FullName,
                    NumberOfFiles = GetFilesNumber(directory, new NumberOfFiles())
                };
            }
            catch (UnauthorizedAccessException)
            {
                folder = new Folder
                {
                    Id = id,
                    ParentId = item.FolderId,
                    FileSystemItems = items,
                    FullName = "Unauthorized",
                    NumberOfFiles = new NumberOfFiles()
                };
            }
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

        public static FolderViewModel CreateFolderViewModel(Folder folder)
        {
            List<FileSystemItemViewModel> fileSystemItems = new List<FileSystemItemViewModel>();
            foreach (var item in folder.FileSystemItems)
            {
                FileSystemItemViewModel file = new FileSystemItemViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    FileSystemItemType = item.FileSystemItemType
                };
                fileSystemItems.Add(file);
            }
            FolderViewModel folderViewModel = new FolderViewModel
            {
                ParentId = folder.ParentId,
                FullName = folder.FullName,
                NumberOfFiles = folder.NumberOfFiles,
                FileSystemItems = fileSystemItems.OrderBy(i => i.FileSystemItemType).ThenBy(i => i.Name).ToList()
            };
            return folderViewModel;
        }
    }
}
