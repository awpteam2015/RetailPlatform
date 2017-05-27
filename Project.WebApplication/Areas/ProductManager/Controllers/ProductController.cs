using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WebApplication.Areas.ProductManager.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductController : Controller
    {
        // GET: ProductManager/Product
        public ActionResult Index()
        {
            return View();
        }
    }
}