using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models.ViewModels
{
    public class FolderViewModel
    {
        public Guid ParentId { get; set; }
        public string FullName { get; set; }
        public NumberOfFiles NumberOfFiles { get; set; }
        public List<FileSystemItemViewModel> FileSystemItems { get; set; }
    }
}