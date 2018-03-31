using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WebSite.Controllers
{
    public class UserCenterController : AuthorizeController
    {
        // GET: UserCenter
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CollectionList()
        {
            return View();
        }


        public ActionResult TicketList()
        {
            return View();
        }


        public ActionResult AddressList()
        {
            return View();
        }



        public ActionResult AccountInfo()
        {
            return View();
        }


        public ActionResult MessageList()
        {
            return View();
        }



    }
}