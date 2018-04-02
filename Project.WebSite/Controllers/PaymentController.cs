using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Application.Service.OrderManager;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.Payment.Configs;
using Project.Infrastructure.FrameworkCore.Payment.Factory;
using Project.Infrastructure.FrameworkCore.Payment.Interfaces;
using Project.Infrastructure.FrameworkCore.Payment.Model;

namespace Project.WebSite.Controllers
{
    public class PaymentController : Controller
    {
        private static object _obj = new object();
        private readonly IPayFactory _payFactory;
        public PaymentController()
        {
            _payFactory = new PayFactory();
        }



        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        #region 网银通知处理

        /// <summary>
        /// 支付宝同步处理
        /// </summary>
        /// <returns></returns>

        public ActionResult AlipayReturn()
        {
            var result = _payFactory.CheckNotifyData(PayEnum.Alipay, NotifyEnum.GET);
            if (result.Status)
            {
                ConfirmOrder(result);
            }
            Content(result.Message);
            return View();
        }

        /// <summary>
        /// 支付宝异步处理
        /// </summary>
        /// <returns></returns>
        public ActionResult AlipayNotify()
        {
            var result = _payFactory.CheckNotifyData(PayEnum.Alipay, NotifyEnum.POST);
            if (result.Status)
            {
                ConfirmOrder(result);
            }
            return Content(result.Message);
        }

        /// <summary>
        /// 招商银行
        /// </summary>
        /// <returns></returns>
        public ActionResult CmbReturn()
        {
            var result = _payFactory.CheckNotifyData(PayEnum.CmbBank, NotifyEnum.GET);
            if (result.Status)
            {
                ConfirmOrder(result);
            }
            return View();
        }

        /// <summary>
        /// 交通银行
        /// </summary>
        /// <returns></returns>
        public ActionResult CommReturn()
        {
            var result = _payFactory.CheckNotifyData(PayEnum.CommBank, NotifyEnum.GET);
            if (result.Status)
            {
                ConfirmOrder(result);
            }
            return View();
        }

        /// <summary>
        /// 工商银行
        /// </summary>
        /// <returns></returns>
        public ActionResult IcbcReturn()
        {
            var result = _payFactory.CheckNotifyData(PayEnum.IcbcBank, NotifyEnum.GET);
            if (result.Status)
            {
                ConfirmOrder(result);
            }
            return View();
        }

        /// <summary>
        /// 财付通同步通知
        /// </summary>
        /// <returns></returns>
        public ActionResult TenpayReturn()
        {
            var result = _payFactory.CheckNotifyData(PayEnum.Tenpay, NotifyEnum.GET);
            if (result.Status)
            {
                ConfirmOrder(result);
            }
            Content(result.Message);
            return View();
        }

        /// <summary>
        /// 财付通异步通知
        /// </summary>
        /// <returns></returns>
        public ActionResult TenpayNotify()
        {
            try
            {
                LoggerHelper.InfoFormat(LogType.PaymentLogger, "财付通异步请求开始\r\n");
                var result = _payFactory.CheckNotifyData(PayEnum.Tenpay, NotifyEnum.POST);
                LoggerHelper.InfoFormat(LogType.PaymentLogger, "财付通异步请求开始：订单号：{0}状态{1}\r\n", result.OrderNo, result.Status);
                if (result.Status)
                {
                    ConfirmOrder(result);
                }
                return Content(result.Message);
            }
            catch (Exception e)
            {
                LoggerHelper.InfoFormat(LogType.PaymentLogger, "财付通异步请求异常" + e.Message);
                throw;
            }
        }

        #endregion

        #region 确认订单

        /// <summary>
        /// 确认订单
        /// </summary>
        [NonAction]
        public void ConfirmOrder(PayResult payResult)
        {
            lock (_obj)
            {
                try
                {
                    var payCode = NetPayConfig.GetPayCode(payResult.PayEnum);
                    var returnMsg = new OrderServiceImpl().ConfirmOrderPay(payResult.OrderNo,
                        payCode,
                        payResult.ToString(),
                        payResult.SerialNumber);
                    LoggerHelper.InfoFormat("OrderLogger", "订单号：{0},确认订单信息：{1}\r\n", payResult.OrderNo, returnMsg.Item2);
                }
                catch (Exception ex)
                {
                    LoggerHelper.ErrorFormat("OrderLogger", "订单号：{0},确认订单信息：{1}\r\n", payResult.OrderNo, ex.Message);
                }
            }
        }
        #endregion


    }
}