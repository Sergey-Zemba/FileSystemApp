using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using FileSystemApp.Models;

namespace FileSystemApp.Controllers
{
    public class HomeController : Controller
    {
        FileSystemContext db = new FileSystemContext();
        public ActionResult Index()
        {
            Folder myComputer = FileManager.BuildTree(db, "Root");
            return View(myComputer);
        }
    }
}
