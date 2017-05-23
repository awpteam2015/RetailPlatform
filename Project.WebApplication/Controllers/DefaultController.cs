using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using Project.Config;
using Project.Model.PermissionManager;
using Project.Service.SystemSetManager;
using Project.WebApplication.Models;
using Project.WebApplication.Models.Request;

namespace Project.WebApplication.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            var httpClient = new HttpClient();
            var responseJson2 = httpClient.GetAsync("http://localhost:8655/api/Open/GetRecords?id=1111").Result.Content.ReadAsAsync<WebAPIResponse<IList<string>>>();

            return View();
        }

        public ActionResult Index2()
        {
            var httpClient = new HttpClient();
            var t222 = JsonConvert.SerializeObject(new GetAddressListRequest() {skipResults = 1, maxResults = 10});


            var httpContent = new StringContent(JsonConvert.SerializeObject(new GetAddressListRequest() { skipResults = 1,maxResults =10 }));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var tt = httpClient.PostAsync("http://localhost:8655/api/Open/GetAddressList", httpContent).Result;


            var result = httpClient.PostAsync("http://localhost:8655/api/Open/GetAddressListRequest", httpContent).Result.Content.ReadAsAsync<WebAPIResponse<IList<UserInfoEntity>>>();
            //result.Result.Result

            return View();
        }
    }
}