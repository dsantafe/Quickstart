using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Quickstart.Web.Controllers
{
    public class DocumentsController : Controller
    {
        public class Document
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public ActionResult Index()
        {
            var filePath = Server.MapPath("~") + @"\Documents\";
            var files = Directory.GetFiles(filePath);

            List<Document> documents = new List<Document>();
            for (int i = 0; i < files.Length; i++)
            {
                documents.Add(new Document
                {
                    Id = i,
                    Name = files[i].Split('\\').LastOrDefault()
                });
            }

            return View(documents);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file)
        {
            MemoryStream ms = new MemoryStream();
            file.InputStream.CopyTo(ms);
            byte[] data = ms.ToArray();
            string base64 = Convert.ToBase64String(data);

            var filePath = Server.MapPath("~") + @"\Documents\";
            var fileName = filePath + file.FileName;

            if (System.IO.File.Exists(fileName))
                System.IO.File.Delete(fileName);

            FileStream fileStream = System.IO.File.Create(fileName);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();

            return View();
        }

        [HttpGet]
        public ActionResult Download(int id)
        {
            var filePath = Server.MapPath("~") + @"\Documents\";
            var files = Directory.GetFiles(filePath);

            var path = string.Empty;
            for (int i = 0; i < files.Length; i++)
                if (i == id)
                    path = files[i];

            byte[] fileDownload = System.IO.File.ReadAllBytes(path);

            return File(fileDownload, MediaTypeNames.Application.Octet, path.Split('\\').LastOrDefault());
        }
    }
}