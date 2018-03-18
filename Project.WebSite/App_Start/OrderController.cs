using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WebSite.Controllers
{
    public class OrderController : AuthorizeController
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
    }
}