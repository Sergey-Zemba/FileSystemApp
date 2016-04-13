using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FileSystemApp.Models;

namespace FileSystemApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<DriveInfo> drives = DriveInfo.GetDrives().ToList().Where(d => d.IsReady);
            ViewBag.Count = drives.Count();
            List<FileSystemItem> items = new List<FileSystemItem>();
            foreach (var driver in drives)
            {
                FileSystemItem folder = new FileSystemItem { 
                    Name = driver.Name,
                    Path = driver.RootDirectory.FullName,
                    ParentName = "MyComputer",
                    ParentPath = "MyComputer",
                    FileSystemItemType = FileSystemItemType.Folder
                };
                items.Add(folder);
            }
            return View(items);
        }
    }
}
