using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models.ViewModels
{
    public class FileSystemItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FileSystemItemType FileSystemItemType { get; set; }
    }
}