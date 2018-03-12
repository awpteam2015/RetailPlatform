using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.AutoMapper;
using Project.Model.PermissionManager;
using Project.Model.ProductManager;
using Project.Model.ReportManager;
using Project.Model.RiverManager;
using Project.Service;
using Project.WebApplication.Areas.ProductManager.Models;
using Project.WebApplication.Models.Response;

namespace Project.WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.CreateMap<UserInfoViewEntity, UserInfoResponse>().IgnoreAllNull();
            Mapper.CreateMap<UserInfoEntity, UserInfoResponse>().IgnoreAllNull();
            Mapper.CreateMap<RiverProblemApplyEntity, ProblemResponse>().IgnoreAllNull();

            Mapper.CreateMap<RiverAttachEntity, GetRiverAttachListResponse>().IgnoreAllNull();

            Mapper.CreateMap<SpecEntity, SpecVm>().IgnoreAllNull();
            Mapper.CreateMap<SpecValueEntity, SpecValueVm>().IgnoreAllNull();

            Mapper.CreateMap<ExtAttributeEntity, AttributeVm>().IgnoreAllNull();
            Mapper.CreateMap<AttributeValueEntity, AttributeValueVm>().IgnoreAllNull();

            // Mapper.CreateMap<ProductEntity, ProductHdVm>().IgnoreAllNull();

            BootstrapperService.Init();
        }
    }
}
