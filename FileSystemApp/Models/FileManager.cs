using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models
{
    public static class FileManager
    {
        public static NumberOfFiles GetFilesNumber(DirectoryInfo directory, NumberOfFiles numberOfFiles)
        {
            var files = directory.GetFiles();
            if (files.Length > 0)
            {
                foreach (var file in files)
                {
                    if (file.Length <= 10485760)
                    {
                        numberOfFiles.SmallSize++;
                    }
                    else if (file.Length > 10485760 && file.Length <= 104857600)
                    {
                        numberOfFiles.MiddleSize++;
                    }
                    else
                    {
                        numberOfFiles.LargeSize++;
                    }
                }
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