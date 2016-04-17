using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models
{
    public class Folder
    {
        public string FullName { get; set; }
        [Key]
        public string Path { get; set; }
        public List<FileSystemItem> FileSystemItems { get; set; }
        public NumberOfFiles NumberOfFiles { get; set; }
    }
}