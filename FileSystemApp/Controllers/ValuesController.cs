using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using FileSystemApp.Models;
using Microsoft.Ajax.Utilities;

namespace FileSystemApp.Controllers
{
    public class ValuesController : ApiController
    {
        FileSystemContext db  = new FileSystemContext();
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            FileSystemItem folder = db.FileSystemItems.Find(id);
            DirectoryInfo directory = new DirectoryInfo(folder.Path);
            IEnumerable<DirectoryInfo> directories = directory.GetDirectories().ToList();
            foreach (var dir in directories)
            {
                if (db.FileSystemItems.Where(f => f.Path == dir.FullName).Count() == 0)
                {
                    FileSystemItem item = new FileSystemItem
                    {
                        Name = dir.Name,
                        Path = dir.FullName,
                        ParentName = folder.Name,
                        FileSystemItemType = FileSystemItemType.Folder
                    };
                    db.FileSystemItems.Add(item);
                }
            }
            IEnumerable<FileInfo> files = directory.GetFiles().ToList();
            foreach (var file in files)
            {
                if (db.FileSystemItems.Where(f => f.Path == file.FullName).Count() == 0)
                {
                    FileSystemItem item = new FileSystemItem
                    {
                        Name = file.Name,
                        Path = file.FullName,
                        ParentName = folder.Name,
                        FileSystemItemType = FileSystemItemType.File
                    };
                    db.FileSystemItems.Add(item);
                }
            }
            db.SaveChanges();
            return Json(db.FileSystemItems.Where(d => d.ParentName == folder.Name));
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
