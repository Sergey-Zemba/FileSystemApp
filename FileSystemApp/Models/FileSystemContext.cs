using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models
{
    public class FileSystemContext : DbContext
    {
        public DbSet<FileSystemItem> FileSystemItems { get; set; }
    }
}