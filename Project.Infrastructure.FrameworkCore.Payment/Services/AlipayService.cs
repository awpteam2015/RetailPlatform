using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.Payment.Alipay;
using Project.Infrastructure.FrameworkCore.Payment.Base;
using Project.Infrastructure.FrameworkCore.Payment.Interfaces;
using Project.Infrastructure.FrameworkCore.Payment.Model;

namespace Project.Infrastructure.FrameworkCore.Payment.Services
{
    /// <summary>
    /// 支付宝付款服务
    /// </summary>
    internal class AlipayService : BaseService, IPayService
    {
        private readonly AlipayModel _alipayModel;

        /// <summary>
        /// 初始化
        /// </summary>
        public AlipayService()
        {
            _alipayModel = new AlipayModel
            {
                Key = ConfigurationManager.AppSettings["AlipayKey"],
                Partner = ConfigurationManager.AppSettings["AlipayPartner"],
                NotifyUrl = HostDomain + ConfigurationManager.AppSettings["AlipayNotifyUrl"],
                ReturnUrl = HostDomain + ConfigurationManager.AppSettings["AlipayReturnUrl"],
                SellerEmail = ConfigurationManager.AppSettings["AlipaySellerEmail"],
                Remark = ConfigurationManager.AppSettings["PaymentRemark"]
            };
        }

        /// <summary>
        /// 发送支付请求
        /// </summary>
        /// <param name="orderPay">订单支付信息</param>
        /// <returns></returns>
        public string SubmitRequest(OrderPay orderPay)
        {
            //把请求参数打包成数组 
            var sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user"); //即时交易
            sParaTemp.Add("payment_type", "1"); //支付类型 不能修改
            //服务器异步通知页面路径
            sParaTemp.Add("notify_url", _alipayModel.NotifyUrl);
            //页面跳转同步通知页面路径
            sParaTemp.Add("return_url", _alipayModel.ReturnUrl);
            sParaTemp.Add("seller_email", _alipayModel.SellerEmail); //卖家支付宝帐户
            sParaTemp.Add("out_trade_no", orderPay.OrderNo); //商户订单号
            sParaTemp.Add("subject", _alipayModel.Remark); //订单名称
            sParaTemp.Add("price", orderPay.TotalAmount.ToString("f2")); //付款金额
            //商品数量 建议默认为1，不改变值，把一次交易看成是一次下订单而非购买一件商品
            sParaTemp.Add("quantity", "1");
            sParaTemp.Add("it_b_pay", "3d");
            sParaTemp.Add("logistics_fee", "0.00"); // 物流费用
            //物流类型 三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）
            sParaTemp.Add("logistics_type", "EXPRESS");
            //物流支付方式 两个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）
            sParaTemp.Add("logistics_payment", "SELLER_PAY");
            sParaTemp.Add("body", _alipayModel.Remark);
            sParaTemp.Add("show_url", HostDomain);
            sParaTemp.Add("receive_name", orderPay.ReceiveName);
            sParaTemp.Add("receive_address", orderPay.ReceiveAddress);
            sParaTemp.Add("receive_zip", orderPay.ReceiveZip);
            sParaTemp.Add("receive_phone", orderPay.ReceivePhone);
            sParaTemp.Add("receive_mobile", orderPay.ReceiveMobile);
            //建立请求
            var sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");

            LoggerHelper.InfoFormat(LogType.PaymentLogger, "支付宝支付请求：订单号：{0}\r\n", orderPay.OrderNo);
            return sHtmlText;
        }

        /// <summary>
        /// 处理支付返回信息
        /// </summary>
        /// <param name="notifyType">通知类型</param>
        /// <returns></returns>
        public PayResult CheckNotifyData(NotifyEnum notifyType)
        {
            PayResult result;
            switch (notifyType)
            {
                case NotifyEnum.GET:
                    result = CheckRequestGet();
                    break;
                case NotifyEnum.POST:
                    result = CheckRequestPost();
                    break;
                default:
                    throw new InvalidOperationException("无效的通知类型，支付宝支付异常。");
            }
            return result;
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <returns></returns>
        public bool SignVerify(Dictionary<string, string> dicParams)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 支付宝同步通知处理
        /// </summary>
        /// <returns></returns>
        private PayResult CheckRequestGet()
        {
            var result = new PayResult
            {
                MerchantAccount = _alipayModel.SellerEmail,
                PayEnum = PayEnum.Alipay
            };
            var getParams = GetRequestGet();
            //判断是否有带返回参数
            if (getParams.Count > 0)
            {
                //签名验证
                var aliNotify = new Notify();
                var notifyId = HttpContext.Current.Request.QueryString["notify_id"];
                var sign = HttpContext.Current.Request.QueryString["sign"];
                var verifyResult = aliNotify.Verify(getParams, notifyId, sign);
                if (verifyResult) //验证成功
                {                 
                    result.Message = "验证成功";
                    result.OrderNo = HttpContext.Current.Request.QueryString["out_trade_no"];//商户订单号
                    result.MerchantAccount = HttpContext.Current.Request.QueryString["seller_email"];
                    result.SerialNumber = HttpContext.Current.Request.QueryString["trade_no"];//支付宝交易号
                    result.PayDate = HttpContext.Current.Request.QueryString["gmt_payment"];
                    result.TotalAmount = HttpContext.Current.Request.QueryString["price"];

                    var tradeStatus = HttpContext.Current.Request.QueryString["trade_status"];//交易状态
                    if (tradeStatus == "WAIT_SELLER_SEND_GOODS" || tradeStatus == "TRADE_FINISHED" ||
                        tradeStatus == "TRADE_SUCCESS")
                    {
                        result.Status = true;
                    }
                }
                else
                {
                    result.Message = "验证失败";
                }
            }
            else
            {
                result.Message = "无返回参数";
            }
            LoggerHelper.InfoFormat(LogType.PaymentLogger, "支付宝同步通知，{0}。\r\n", result.ToString());
            return result;
        }

        /// <summary>
        /// 支付宝异步通知处理
        /// </summary>
        /// <returns></returns>
        private PayResult CheckRequestPost()
        {
            var result = new PayResult
            {
                MerchantAccount = _alipayModel.SellerEmail,
                PayEnum = PayEnum.Alipay
            };
            var sPara = GetRequestPost();
            if (sPara.Count > 0) //判断是否有带返回参数
            {
                var aliNotify = new Notify();
                var sign = HttpContext.Current.Request.Form["sign"];
                var notifyId = HttpContext.Current.Request.Form["notify_id"];
                var verifyResult = aliNotify.Verify(sPara, notifyId, sign);
                if (verifyResult) //验证成功
                {
                    result.Message = "success";
                    result.OrderNo = HttpContext.Current.Request.Form["out_trade_no"];//商户订单号
                    result.SerialNumber = HttpContext.Current.Request.Form["trade_no"];//支付宝交易号
                    result.MerchantAccount = HttpContext.Current.Request.Form["seller_email"];
                    result.PayDate = HttpContext.Current.Request.Form["gmt_payment"];
                    result.TotalAmount = HttpContext.Current.Request.Form["price"];

                    var tradeStatus = HttpContext.Current.Request.Form["trade_status"];//交易状态
                    if (tradeStatus == "WAIT_SELLER_SEND_GOODS" || tradeStatus == "TRADE_FINISHED" ||
                        tradeStatus == "TRADE_SUCCESS")
                    {
                        result.Status = true;
                    }
                }
                else
                {
                    result.Message = "fail";
                }
            }
            else
            {
                result.Message = "无通知参数";
            }
            LoggerHelper.InfoFormat(LogType.PaymentLogger, "支付宝异步通知，{0}。\r\n", result.ToString());
            return result;
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = HttpContext.Current.Request.Form;
            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
            }
            return sArray;
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = HttpContext.Current.Request.QueryString;
            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpContext.Current.Request.QueryString[requestItem[i]]);
            }
            return sArray;
        }
    }
}
