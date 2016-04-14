using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models
{
    public class Folder
    {
        public string FullName { get; set; }
        public string Path { get; set; }
        public List<FileSystemItem> FileSystemItems { get; set; }
        public NumberOfFiles NumberOfFiles { get; set; }
    }
}