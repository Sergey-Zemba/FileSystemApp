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
            NumberOfFiles numberOfFiles  = new NumberOfFiles();
            List<Folder> items = new List<Folder>();
            foreach (var driver in drives)
            {
                Folder folder = new Folder {
                    FullName = driver.RootDirectory.FullName,
                    Path = "MyComputer"
                };
                items.Add(folder);
                var number = FileManager.GetFilesNumber(driver.RootDirectory, new NumberOfFiles());
                numberOfFiles.SmallSize += number.SmallSize;
                numberOfFiles.MiddleSize += number.MiddleSize;
                numberOfFiles.LargeSize += number.LargeSize;
            }
            ViewBag.NumberOfFiles = numberOfFiles;
            return View(items);
        }
    }
}
