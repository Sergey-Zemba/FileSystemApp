using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models
{
    public class FileSystemDbInitializer : DropCreateDatabaseAlways<FileSystemContext>
    {
        protected override void Seed(FileSystemContext context)
        {
            base.Seed(context);
        }
    }
}