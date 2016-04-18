using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using FileSystemApp.Models;
using FileSystemApp.Models.ViewModels;

namespace FileSystemApp.Controllers
{
    public class ValuesController : ApiController
    {
        FileSystemContext db = new FileSystemContext();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public IHttpActionResult Get(Guid id)
        {
            Folder folder;
            if (db.Folders.Find(id) != null)
            {
                folder = db.Folders.Find(id);
            }
            else
            {
                folder = FileManager.BuildTree(db, id);
            }
            FolderViewModel folderViewModel = FileManager.CreateFolderViewModel(folder);
            return Json(folderViewModel);
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
