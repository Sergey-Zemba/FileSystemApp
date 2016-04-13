using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileSystemApp.Models;

namespace FileSystemApp.Controllers
{
    public class HomeController : Controller
    {
        FileSystemContext db = new FileSystemContext();
        public ActionResult Index()
        {
            return View(db.FileSystemItems);
        }
    }
}
