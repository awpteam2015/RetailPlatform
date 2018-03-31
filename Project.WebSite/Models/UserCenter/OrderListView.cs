using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Model.OrderManager;
using Project.WebSite.Controllers;
using Project.WebSite.Extend;
using Project.WebSite.Models.Component;

namespace Project.WebSite.Models.UserCenter
{
    public class OrderListView: SearchBaseVm
    {
        public IList<OrderMainEntity> OrderList { get; set; }

    
    }
}