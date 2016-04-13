namespace FileSystemApp.Models
{
    public class FileSystemItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ParentName { get; set; }
        public string ParentPath { get; set; }
        public FileSystemItemType FileSystemItemType { get; set; }
    }
}