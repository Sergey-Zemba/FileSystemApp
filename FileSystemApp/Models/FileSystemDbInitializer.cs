using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models
{
    public class FileSystemDbInitializer : DropCreateDatabaseAlways<FileSystemContext>
    {
        protected override void Seed(FileSystemContext context)
        {
            IEnumerable<DriveInfo> drives = DriveInfo.GetDrives().ToList().Where(d => d.IsReady);
            foreach (var driver in drives)
            {
                FileSystemItem folder = new FileSystemItem { Name = driver.Name, Path = driver.Name, ParentName = "", FileSystemItemType = FileSystemItemType.Folder };
                context.FileSystemItems.Add(folder);
            }
            base.Seed(context);
        }
    }
}