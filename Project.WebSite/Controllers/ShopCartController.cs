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
    public class ShopCartController : AuthorizeController
    {
        #region 视图
        // GET: ShopCat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            new OrderServiceImpl().UpdateCartState(CustomerDto.CustomerId);

            var list = new OrderServiceImpl().GetShopCartList(CustomerDto.CustomerId);

            return View(list);
        }
        #endregion



        #region 操作方法

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCart(int goodsId, int num)
        {
            var registResult = new OrderServiceImpl().AddCart(goodsId, num,CustomerDto.CustomerId);

            var result = new AjaxResponse<object>()
            {
                success = registResult.Item1,
                error = new ErrorInfo(registResult.Item2)
            };

        
            return new AbpJsonResult(result);
        }

      /// <summary>
      /// 删除购物车行项目
      /// </summary>
      /// <param name="pkId"></param>
      /// <returns></returns>
        [HttpPost]
        public ActionResult DelCart(int pkId)
        {
            var registResult = new OrderServiceImpl().DelCart( pkId, CustomerDto.CustomerId);

            var result = new AjaxResponse<object>()
            {
                success = registResult.Item1,
                error = new ErrorInfo(registResult.Item2)
            };

            return new AbpJsonResult(result);
        }

        /// <summary>
        /// 更新购物车数量
        /// </summary>
        /// <param name="pkId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCartNum(int pkId,int num)
        {
            var registResult = new OrderServiceImpl().UpdateCartNum( pkId, num, CustomerDto.CustomerId);

            var result = new AjaxResponse<object>()
            {
                success = registResult.Item1,
                error = new ErrorInfo(registResult.Item2)
            };

            return new AbpJsonResult(result);
        }


        /// <summary>
        /// 更新购物车数量
        /// </summary>
        /// <param name="pkId"></param>
        /// <param name="isCheck"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCartCheck(int pkId, int isCheck)
        {
            var registResult = new OrderServiceImpl().UpdateCartCheck(pkId, isCheck, CustomerDto.CustomerId);

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