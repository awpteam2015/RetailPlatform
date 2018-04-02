using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project.Application.Service.OrderManager;
using Project.Application.Service.OrderManager.Request;
using Project.Infrastructure.FrameworkCore.Payment.Configs;
using Project.Infrastructure.FrameworkCore.Payment.Factory;
using Project.Infrastructure.FrameworkCore.Payment.Model;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.WebSite.Extend;
using Project.WebSite.Models.OrderProcess;
using Project.WebSite.Models.UserCenter;

namespace Project.WebSite.Controllers
{
    public class OrderController : AuthorizeController
    {

        #region 视图
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {

            var pageIndex = RequestHelper.GetInt("page") == 0 ? 1 : RequestHelper.GetInt("page");

            var request = new SearchOrderListRequest();
            request.OrderNo = RequestHelper.GetString("OrderNo");
            request.CreateEnd = RequestHelper.GetString("CreateEnd");
            request.CreateStart = RequestHelper.GetString("CreateStart");
            request.State = RequestHelper.GetInt("State");
            request.maxResults = 2;
            request.CustomerId = 1;
            request.skipResults = (pageIndex - 1) * request.maxResults;

            //var data = CloudResourceDatasource.GetAll()
            // .OrderBy(p => p.Id).ToPagedList(page, pagesize);


            var searchList = new OrderServiceImpl().SearchOrderList(request);


            var viewModel = new OrderListView();
            viewModel.OrderList = searchList.Item1;
            viewModel.PageInfo = new MyPagedList(pageIndex, request.maxResults, searchList.Item2);
            viewModel.SearchOrderListRequest = request;


            return View(viewModel);
        }


        public ActionResult Error()
        {
            return View();
        }


        public ActionResult Pay(string orderNo)
        {
            if (string.IsNullOrEmpty(orderNo))
                return RedirectToAction("List", "Order");

            var dto = new OrderServiceImpl().GetOrderInfo(orderNo, CustomerDto.CustomerId);

            if (dto == null)
                return RedirectToAction("List", "Order");

            var model = new PayVm()
            {
                OrderNo = dto.OrderNo,
                Totalamount = dto.Totalamount
            };

            //var json = JsonConvert.SerializeObject(temp);
            //ViewBag.Json = json;
            return View(model);
        }
        #endregion


        #region 操作

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrder(AddOrderRequest request)
        {
            var registResult = new OrderServiceImpl().AddOrder(request);

            var result = new AjaxResponse<object>()
            {
                success = registResult.Item1,
                error = new ErrorInfo(registResult.Item2)
            };
            return new AbpJsonResult(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateOrder()
        {
            return new AbpJsonResult();
        }


        [HttpPost]
        public ActionResult UpdateOrderPay()
        {
            return new AbpJsonResult();
        }


        //[HttpPost]
        //public ActionResult ConfirmPay()
        //{
        //    return new AbpJsonResult();
        //}


        /// <summary>
        /// 检查支付
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckPay(string orderNo, string payCode)
        {
            var registResult = new OrderServiceImpl().CheckPay(orderNo,payCode,CustomerDto.CustomerId);

            var result = new AjaxResponse<object>()
            {
                success = registResult.Item1,
                result = registResult.Item1? registResult.Item2:"",
                error = new ErrorInfo(registResult.Item2)
            };
            return new AbpJsonResult(result);
        }


        /// <summary>
        /// 确认支付
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="payCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmPay(string orderNo, string payCode)
        {
            if (string.IsNullOrEmpty(orderNo) || string.IsNullOrEmpty(payCode))
                return RedirectToAction("List", "Order");

            //无效的请求
            if (Request.HttpMethod.ToUpper() == "GET")
                return RedirectToAction("List", "Order");

            var order = new OrderServiceImpl().GetOrderInfo(orderNo, CustomerDto.CustomerId);
            if (order == null)
                return RedirectToAction("List", "Order");

            if (order.State != 1)
                return RedirectToAction("List", "Order");

#if DEBUG
            order.Totalamount = 0.01m;
#endif

            var payment = new OrderPay
            {
                OrderNo = orderNo,
                TotalAmount = order.Totalamount,
                PayCode = payCode,
                ReceiveName = order.Linkman,
                ReceivePhone = order.LinkmanTel,
                ReceiveMobile = order.LinkmanMobilephone,
                ReceiveZip = order.LinkmanPostcode,
                ReceiveAddress = order.LinkmanAddressfull
            };

            var requestFrom = new PayFactory().SubmitRequest(payment);
            if (string.IsNullOrEmpty(requestFrom))
            {
                //无效的支付方式
                return RedirectToAction("Error", "Order");
            }
            if (payCode == NetPayConfig.TenpayCode)
            {
                return Redirect(requestFrom);
            }
            return Content(requestFrom);
        }


        #endregion





    }
}