using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FileSystemApp.Models;
using FileSystemApp.Models.ViewModels;

namespace FileSystemApp.Controllers
{
    public class HomeController : Controller
    {
        FileSystemContext db = new FileSystemContext();
        public ActionResult Index()
        {
            db.Folders.RemoveRange(db.Folders);
            db.FileSystemItems.RemoveRange(db.FileSystemItems);
            Folder myComputer = FileManager.BuildTree(db, Guid.Empty);
            FolderViewModel folderViewModel = FileManager.CreateFolderViewModel(myComputer);
            return View(folderViewModel);
        }
    }
}
