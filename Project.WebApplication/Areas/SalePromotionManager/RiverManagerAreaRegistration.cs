using System.Web.Mvc;

namespace Project.WebApplication.Areas.SalePromotionManager
{
    public class SalePromotionManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SalePromotionManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SalePromotionManager_default",
                "SalePromotionManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}