namespace FileSystemApp.Models
{
    public class FileSystemItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public FileSystemItemType FileSystemItemType { get; set; }
        public int FolderId { get; set; }
        //public virtual Folder Folder { get; set; }
    }
}