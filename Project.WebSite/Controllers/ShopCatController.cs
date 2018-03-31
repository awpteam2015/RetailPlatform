using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Application.Service.AccountManager;
using Project.Application.Service.OrderManager;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;

namespace Project.WebSite.Controllers
{
    public class ShopCatController : AuthorizeController
    {
        #region
        // GET: ShopCat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }
        #endregion



        #region

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCart(int goodsId, int num)
        {
            var registResult = new OrderServiceImpl().AddCat(goodsId, num,CustomerDto.CustomerId);

            var result = new AjaxResponse<object>()
            {
                success = registResult.Item1,
                error = new ErrorInfo(registResult.Item2)
            };

        
            return new AbpJsonResult(result);
        }

        #endregion


    }
}