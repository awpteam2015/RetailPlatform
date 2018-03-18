using System.Web.Mvc;

namespace Project.WebApplication.Areas.CustomerManager
{
    public class CustomerManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CustomerManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CustomerManager_default",
                "CustomerManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}