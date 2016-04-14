namespace FileSystemApp.Models
{
    public class FileSystemItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public FileSystemItemType FileSystemItemType { get; set; }
    }
}