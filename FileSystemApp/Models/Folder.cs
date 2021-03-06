﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileSystemApp.Models
{
    public class Folder
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string FullName { get; set; }
        public virtual List<FileSystemItem> FileSystemItems { get; set; }
        public NumberOfFiles NumberOfFiles { get; set; }
    }
}