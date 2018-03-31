using System;
using System.Collections.Generic;
using System.Linq;
using Project.Application.Service.OrderManager.Request;
using Project.Application.Service.OrderManager.Response;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.OrderManager;
using Project.Repository.OrderManager;

namespace Project.Application.Service.OrderManager
{
    public class OrderServiceImpl
    {
        #region
        private readonly OrderMainRepository _orderMainRepository;
        private readonly OrderMainDetailRepository _orderMainDetailRepository;
        private readonly OrderInvoiceRepository _orderInvoiceRepository;

        public OrderServiceImpl()
        {
            this._orderMainRepository = new OrderMainRepository();
            _orderMainDetailRepository = new OrderMainDetailRepository();
            _orderInvoiceRepository = new OrderInvoiceRepository();
        }
        #endregion


        #region 购物车相关
        public void AddCat(int goodsId, int customerId)
        {

        }

        public void DelCat()
        {

        }

        public void UpdateCat()
        {

        }
        #endregion

        #region 订单相关
        public void AddOrder()
        {

        }

        public void ConfirmOrder()
        {

        }

        public void ConfirmOrderPay()
        {

        }

        /// <summary>
        /// 订单搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Tuple<IList<OrderMainEntity>, int> SearchOrderList(SearchOrderListRequest request)
        {
            var expr = PredicateBuilder.True<OrderMainEntity>();

            expr = expr.And(p => p.CustomerId == request.CustomerId);

            if (!string.IsNullOrEmpty(request.OrderNo))
                expr = expr.And(p => p.OrderNo == request.OrderNo);

            if (request.CreateStart != null)
                expr = expr.And(p => p.CreationTime >= request.CreateStart);

            if (request.CreateEnd != null)
                expr = expr.And(p => p.CreationTime <= request.CreateEnd);

            var list = _orderMainRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(request.skipResults).Take(request.maxResults).ToList();
            var count = _orderMainRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<OrderMainEntity>, int>(list, count);
        }



        public void Cancel()
        {

        }

        public void ApplyReturnMoney()
        {

        }

        public void ApplyReturnProduct()
        {

        }

        public void OrderFinsh()
        {

        }

        public void OrderReturnInfoWrite()
        {

        }

        #endregion


        #region

        #endregion


    }
}
