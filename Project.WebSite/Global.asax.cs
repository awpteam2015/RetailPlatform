using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.AutoMapper;
using Project.Model.ProductManager;
using Project.Model.ReportManager;
using Project.Service;
using Project.WebSite.Models;

namespace Project.WebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.CreateMap<ProductEntity,ProductVm>().IgnoreAllNull();
            Mapper.CreateMap<ExtAttributeEntity, AttributeVm>().IgnoreAllNull();
            Mapper.CreateMap<AttributeValueEntity, AttributeValueVm>().IgnoreAllNull();

            BootstrapperService.Init();
        }
    }
}
