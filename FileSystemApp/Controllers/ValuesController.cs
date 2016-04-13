using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using FileSystemApp.Models;

namespace FileSystemApp.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public IHttpActionResult Get(string id)
        {
            string path = id.Replace("=", "\\");
            if (path.Length == 2)
            {
                path += @"\";
            }
            if (id == "MyComputer")
            {
                return Json("MyComputer");
            }
            DirectoryInfo directory = new DirectoryInfo(path);
            string fullName = directory.FullName;
            List<FileSystemItem> items = new List<FileSystemItem>();
            IEnumerable<DirectoryInfo> directories = directory.GetDirectories().ToList();
            foreach (var dir in directories)
            {
                FileSystemItem item = new FileSystemItem
                {
                    Name = dir.Name,
                    Path = dir.FullName,
                    ParentName = fullName,
                    ParentPath = path.Length == 3 ? "MyComputer" : fullName.Substring(0, fullName.LastIndexOf("\\")),
                    FileSystemItemType = FileSystemItemType.Folder
                };
                items.Add(item);
            }
            IEnumerable<FileInfo> files = directory.GetFiles().ToList();
            foreach (var file in files)
            {
                FileSystemItem item = new FileSystemItem
                {
                    Name = file.Name,
                    Path = file.FullName,
                    ParentName = fullName,
                    ParentPath = path.Length == 3 ? "MyComputer" : fullName.Substring(0, fullName.LastIndexOf("\\")),
                    FileSystemItemType = FileSystemItemType.File
                };
                items.Add(item);
            }
            return Json(items);
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
