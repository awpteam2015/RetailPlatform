using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Project.Application.Service.OrderManager.Request;
using Project.Application.Service.OrderManager.Response;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.OrderManager;
using Project.Repository.CustomerManager;
using Project.Repository.OrderManager;
using Project.Repository.ProductManager;
using Project.Service.OrderManager;

namespace Project.Application.Service.OrderManager
{
    public class OrderServiceImpl
    {
        #region
        private readonly OrderMainRepository _orderMainRepository;
        private readonly OrderMainDetailRepository _orderMainDetailRepository;
        private readonly OrderInvoiceRepository _orderInvoiceRepository;
        private readonly ShopCartRepository _shopCartRepository;
        private readonly GoodsRepository _goodsRepository;
        private readonly ProductRepository _productRepository;
        private readonly CustomerRepository _customerRepository;

        public OrderServiceImpl()
        {
            this._orderMainRepository = new OrderMainRepository();
            _orderMainDetailRepository = new OrderMainDetailRepository();
            _orderInvoiceRepository = new OrderInvoiceRepository();
            _shopCartRepository = new ShopCartRepository();
            _goodsRepository = new GoodsRepository();
            _productRepository = new ProductRepository();
            _customerRepository = new CustomerRepository();
        }
        #endregion


        #region 购物车相关

        /// <summary>
        /// 获取购物车列表信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IList<ShopCartEntity> GetShopCartList(int customerId)
        {
            var shopCartList = _shopCartRepository.Query().Where(p => p.CustomerId == customerId).ToList();
            return shopCartList;
        }


        /// <summary>
        /// 购物车新增商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="num"></param>
        /// <param name="customerId"></param>
        public Tuple<bool, string> AddCart(int goodsId, int num, int customerId)
        {
            var customerInfo = _customerRepository.GetById(customerId);
            var goodsInfo = _goodsRepository.GetById(goodsId);
            var productInfo = _productRepository.GetById(goodsInfo.ProductId);

            var shopCartInfo = new ShopCartEntity();
            shopCartInfo.CustomerId = customerId;
            shopCartInfo.GoodsId = goodsId;
            shopCartInfo.GoodsCode = goodsInfo.GoodsCode;
            shopCartInfo.Num = num;
            shopCartInfo.SpecDetail = goodsInfo.SpecDetail;
            shopCartInfo.ProductId = productInfo.PkId;
            shopCartInfo.ProductName = productInfo.ProductName;
            shopCartInfo.ProductCode = productInfo.ProductCode;
            shopCartInfo.ImageUrl = productInfo.ImageUrl;


            //价格计算
            shopCartInfo.Price = goodsInfo.GoodsPrice;
            shopCartInfo.PromotionPrice = goodsInfo.PromotionPrice;
            shopCartInfo.RuleId = goodsInfo.RuleId;
            shopCartInfo.DiscountMember = goodsInfo.RuleId > 0 ? shopCartInfo.PromotionPrice * (100 - customerInfo.Discount) / 100 : shopCartInfo.Price * (100 - customerInfo.Discount) / 100;
            shopCartInfo.DiscountPromotion = goodsInfo.RuleId > 0 ? goodsInfo.GoodsPrice - goodsInfo.PromotionPrice : 0;
            shopCartInfo.DiscountAll = shopCartInfo.DiscountMember + shopCartInfo.DiscountPromotion;
            shopCartInfo.LastPrice = shopCartInfo.Price - shopCartInfo.DiscountAll;
            shopCartInfo.TotalAmount = shopCartInfo.LastPrice * shopCartInfo.Num;

            var pkId = _shopCartRepository.Save(shopCartInfo);
            if (pkId > 0)
            {
                return new Tuple<bool, string>(true, "");
            }
            else
            {
                return new Tuple<bool, string>(false, "");
            }
        }


        /// <summary>
        /// 删除商品行项目
        /// </summary>
        public Tuple<bool, string> DelCart( int pkId, int customerId)
        {
            var shopCartInfo = _shopCartRepository.Query().FirstOrDefault(p => p.PkId == pkId && p.CustomerId == customerId);
            try
            {
                if (shopCartInfo != null)
                {
                    _shopCartRepository.Delete(shopCartInfo);
                    return new Tuple<bool, string>(true, "");
                }
                else
                {
                    return new Tuple<bool, string>(false, "");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// 更新购物车中的商品数量
        /// </summary>
        public Tuple<bool, string> UpdateCartNum(int pkId, int num, int customerId)
        {
            var shopCartInfo = _shopCartRepository.Query().FirstOrDefault(p => p.PkId == pkId && p.CustomerId == customerId);
            try
            {
                if (shopCartInfo != null)
                {
                    shopCartInfo.Num = num;
                    shopCartInfo.TotalAmount = shopCartInfo.LastPrice * num;
                    _shopCartRepository.Update(shopCartInfo);
                    return new Tuple<bool, string>(true, "");
                }
                else
                {
                    return new Tuple<bool, string>(false, "");
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        /// <summary>
        /// 更新购物车中的商品行项目信息  有些促销过期的情况
        /// </summary>
        public Tuple<bool, string> UpdateCartState(int customerId)
        {
            var list = _shopCartRepository.Query().Where(p => p.CustomerId == customerId);

            try
            {
                list.ForEach(p =>
            {
                var goodInfo = _goodsRepository.GetById(p.GoodsId);

                if (p.Price != goodInfo.GoodsPrice || p.RuleId != goodInfo.RuleId || p.PromotionPrice != goodInfo.PromotionPrice)
                {
                    p.IsExpire = 1;
                    p.IsCheck = 2;
                }
                _shopCartRepository.Update(p);

            });
                return new Tuple<bool, string>(true, "");
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        /// <summary>
        /// 更新购物车中的是否勾选
        /// </summary>
        public Tuple<bool, string> UpdateCartCheck(int pkId, int isCheck, int customerId)
        {
            var shopCartInfo = _shopCartRepository.Query().FirstOrDefault(p => p.PkId == pkId && p.CustomerId == customerId);
            try
            {
                if (shopCartInfo != null)
                {
                    shopCartInfo.IsCheck = isCheck;
                    _shopCartRepository.Update(shopCartInfo);
                    return new Tuple<bool, string>(true, "");
                }
                else
                {
                    return new Tuple<bool, string>(false, "");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 订单相关

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public OrderMainEntity  GetOrderInfo(string orderNo,int customerId)
        {
            var orderInfo = _orderMainRepository.Query().FirstOrDefault(p => p.OrderNo == orderNo && p.CustomerId == customerId);
            return orderInfo;
        }


        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="request"></param>
        public Tuple<bool,string> AddOrder(AddOrderRequest request)
        {
            var customerInfo = _customerRepository.GetById(request.CustomerId);

            var orderMainInfo = new OrderMainEntity();
            Mapper.Map(request, orderMainInfo);

            orderMainInfo.CustomerName = orderMainInfo.CustomerName;
            // orderMainInfo.

            var result = OrderMainService.GetInstance().Add(orderMainInfo);
            return  new Tuple<bool, string>(true,"");
        }

        /// <summary>
        /// 开始订单支付
        /// </summary>
        public void UpdateOrderPay(string orderNo,string payType,int customerId)
        {
            var orderInfo = _orderMainRepository.Query().FirstOrDefault(p => p.OrderNo == orderNo && p.CustomerId == customerId);
            orderInfo.BeginPayTime=DateTime.Now;
            orderInfo.PayType = payType;
            _orderMainRepository.Update(orderInfo);
        }


        /// <summary>
        /// 确认订单支付 支付返回
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="paySerialNumber"></param>
        /// <param name="payRemark"></param>
        /// <param name="customerId"></param>
        public void ConfirmOrderPay(string orderNo, string paySerialNumber,string payRemark, int customerId)
        {
            var orderInfo = _orderMainRepository.Query().FirstOrDefault(p => p.OrderNo == orderNo && p.CustomerId == customerId);
            orderInfo.PaySerialNumber = paySerialNumber;
            orderInfo.PayRemark = payRemark;
            _orderMainRepository.Update(orderInfo);
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

            if (!string.IsNullOrEmpty(request.CreateStart))
            {
                expr = expr.And(p => p.CreationTime >= DateTime.Parse(request.CreateStart));
            }

            if (!string.IsNullOrEmpty(request.CreateEnd))
            {
                expr = expr.And(p => p.CreationTime <= DateTime.Parse(request.CreateEnd));
            }

            var list = _orderMainRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(request.skipResults).Take(request.maxResults).ToList();
            var count = _orderMainRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<OrderMainEntity>, int>(list, count);
        }


        /// <summary>
        /// 
        /// </summary>
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


        /// <summary>
        /// 订单支付检查并修改支付方式
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderNo"></param>
        /// <param name="payCode"></param>ConfirmPay
        /// <returns></returns>
        public Tuple<bool, string> CheckPay( string orderNo, string payCode,int customerId)
        {

            var orderMain = _orderMainRepository.Query().FirstOrDefault(p => p.OrderNo == orderNo && p.CustomerId == customerId);
            if (orderMain == null)
                return new Tuple<bool, string>(false, "该订单不存在");

            //if (orderMain.State == "-1")
            //    return new Tuple<bool, bool, string>(false, false, "该订单已作废");
            //if (orderMain.State == "1")
            //    return new Tuple<bool, bool, string>(false, false, "该订单已付款");
            //if (orderMain.State == "T")
            //    return new Tuple<bool, bool, string>(false, false, "该订单已退货");

           //库存检查
            var stockCheck = new StockService().StockCheck(orderMain);
            if (!stockCheck.Item1)
                return new Tuple<bool, string>(false, "库存不足，请您联系客服。");

            if (orderMain.Totalamount == 0)
            {
                return new Tuple<bool, string>(true, "noneedpay");
            }
            return new Tuple<bool, string>(true, string.Empty);
        }

        #endregion


        #region

        #endregion


    }
}
