using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileSystemApp.Models
{
    public class FileSystemItem
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public FileSystemItemType FileSystemItemType { get; set; }
        [ForeignKey("Folder")]
        public Guid FolderId { get; set; }
        public virtual Folder Folder { get; set; }
    }
}