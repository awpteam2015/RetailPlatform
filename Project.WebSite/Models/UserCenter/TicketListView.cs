using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Model.SalePromotionManager;
using Project.WebSite.Models.Component;

namespace Project.WebSite.Models.UserCenter
{
    public class TicketListView: SearchBaseVm
    {
        public List<TicketEntity> TicketList { get; set; }
    }
}