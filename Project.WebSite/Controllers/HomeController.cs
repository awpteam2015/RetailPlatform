using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Project.WebSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageTest()
        {
            var page =int.Parse(Request["page"]);
            var kk = Request["kk"];

            const int pagesize = 3;
            var data = CloudResourceDatasource.GetAll()
                .OrderBy(p => p.Id).ToPagedList(page, pagesize);
            return View(data);
        }

    }

    public class VirtualMachine
    {
        public int Id { set; get; }

        public string HostName { set; get; }

        public string IPAddress { set; get; }

        public string Owner { set; get; }

        public string State { set; get; }

        public DateTime ExpiredDate { set; get; }

        public string IsAzure { set; get; }
    }


    public class CloudResourceDatasource
    {
        public static IEnumerable<VirtualMachine> GetAll()
        {
            return new List<VirtualMachine>
            {
                new VirtualMachine{ Id = 1, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 2, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 3, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 4, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 5, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 6, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 7, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 8, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 9, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 10, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 11, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 12, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 13, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"},
                new VirtualMachine{ Id = 14, HostName = "Azure", IPAddress = "192.168.21.1", Owner = "justin", State = "ON", ExpiredDate = DateTime.Now, IsAzure = "true"}
            };
        }
    }



}