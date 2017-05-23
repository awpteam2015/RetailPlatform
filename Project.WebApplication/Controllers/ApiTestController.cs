using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Project.WebApplication.Controllers
{
    public class ApiTestController : Controller
    {
        // GET: ApiTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            var client = new HttpClient();
            var imageStream = Request.Files["UploadedImage"];
            var fileContent = new StreamContent(imageStream.InputStream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"UploadedImage\"",
                FileName = "\"1.jpg\""
            }; // the extra quotes are key here

            // fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            var content = new MultipartFormDataContent { fileContent };
            var t = client.PostAsync("http://localhost:8655//api/WeiXin/PostFile", content).Result.Content.ReadAsStringAsync().Result;

            return Content(t);
        }
    }
}