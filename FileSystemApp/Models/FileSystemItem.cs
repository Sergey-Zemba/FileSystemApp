using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models
{
    public class FileSystemItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string ParentName { get; set; }
        public FileSystemItemType FileSystemItemType { get; set; }
    }
}