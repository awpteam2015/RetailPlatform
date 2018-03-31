using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project.Application.Service.OrderManager;
using Project.Application.Service.OrderManager.Request;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.WebSite.Extend;
using Project.WebSite.Models.UserCenter;

namespace Project.WebSite.Controllers
{
    public class OrderController : AuthorizeController
    {

        #region
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
            request.CustomerId = "1";
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

        #endregion


        #region

#endregion





    }
}