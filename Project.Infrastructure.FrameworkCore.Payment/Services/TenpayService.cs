using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.Payment.Base;
using Project.Infrastructure.FrameworkCore.Payment.Interfaces;
using Project.Infrastructure.FrameworkCore.Payment.Model;
using tenpayApp;

namespace Project.Infrastructure.FrameworkCore.Payment.Services
{
    /// <summary>
    /// 财付通支付
    /// </summary>
    internal class TenpayService : BaseService, IPayService
    {
        private readonly TenpayModel _payModel;
        private readonly HttpContext _httpContext;

        public TenpayService()
        {
            _payModel = new TenpayModel
            {
                Partner = TenpayUtil.bargainor_id,
                Key = TenpayUtil.tenpay_key,
                NotifyUrl = HostDomain + TenpayUtil.tenpay_notify,
                ReturnUrl = HostDomain + TenpayUtil.tenpay_return,
                Remark = ConfigurationManager.AppSettings["PaymentRemark"]
            };
            _httpContext = HttpContext.Current;
        }

        /// <summary>
        /// 发送支付请求
        /// </summary>
        /// <param name="orderPay"></param>
        /// <returns></returns>
        public string SubmitRequest(OrderPay orderPay)
        {
            var total = (double)orderPay.TotalAmount * 100;
            var payDate = DateTime.Now;
            //创建RequestHandler实例
            var reqHandler = new RequestHandler(_httpContext);
            //初始化
            reqHandler.init();
            //设置密钥
            reqHandler.setKey(TenpayUtil.tenpay_key);
            reqHandler.setGateUrl("https://gw.tenpay.com/gateway/pay.htm");

            //设置支付参数
            reqHandler.setParameter("partner", _payModel.Partner);//商户号
            reqHandler.setParameter("out_trade_no", orderPay.OrderNo);//商家订单号
            reqHandler.setParameter("total_fee", total.ToString());//商品金额,以分为单位
            reqHandler.setParameter("return_url", _payModel.ReturnUrl);//交易完成后跳转的URL
            reqHandler.setParameter("notify_url", _payModel.NotifyUrl);//接收财付通通知的URL
            reqHandler.setParameter("body", _payModel.Remark);//商品描述
            reqHandler.setParameter("bank_type", "DEFAULT");//银行类型(中介担保时此参数无效)
            //用户的公网ip，不是商户服务器IP
            reqHandler.setParameter("spbill_create_ip", HttpContext.Current.Request.UserHostAddress);
            reqHandler.setParameter("fee_type", "1");//币种，1人民币
            reqHandler.setParameter("subject", _payModel.Remark);//商品名称(中介交易时必填)

            //系统可选参数
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("service_version", "1.0");
            reqHandler.setParameter("input_charset", "UTF-8");
            reqHandler.setParameter("sign_key_index", "1");

            //业务可选参数
            reqHandler.setParameter("attach", "");//附加数据，原样返回
            //商品费用，必须保证transport_fee + product_fee=total_fee
            reqHandler.setParameter("product_fee", "0");
            //物流费用，必须保证transport_fee + product_fee=total_fee
            reqHandler.setParameter("transport_fee", "0");
            //订单生成时间，格式为yyyymmddhhmmss
            reqHandler.setParameter("time_start", payDate.ToString("yyyyMMddHHmmss"));
            //订单失效时间，格式为yyyymmddhhmmss
            reqHandler.setParameter("time_expire", payDate.AddDays(3).ToString("yyyyMMddHHmmss"));
            reqHandler.setParameter("buyer_id", ""); //买方财付通账号
            reqHandler.setParameter("goods_tag", "");  //商品标记
            //交易模式，1即时到账(默认)，2中介担保，3后台选择（买家进支付中心列表选择）
            reqHandler.setParameter("trade_mode", "1");
            reqHandler.setParameter("transport_desc", "");//物流说明
            reqHandler.setParameter("trans_type", "1"); //交易类型，1实物交易，2虚拟交易
            reqHandler.setParameter("agentid", ""); //平台ID
            //代理模式，0无代理(默认)，1表示卡易售模式，2表示网店模式
            reqHandler.setParameter("agent_type", "");
            //卖家商户号，为空则等同于partner
            reqHandler.setParameter("seller_id", "");

            //获取请求带参数的url
            var requestUrl = reqHandler.getRequestURL();
            //var debuginfo = reqHandler.getDebugInfo();       
            LoggerHelper.InfoFormat(LogType.PaymentLogger, "财付通支付请求：订单号：{0}\r\n", orderPay.OrderNo);

            return requestUrl;
        }

        /// <summary>
        /// 处理支付返回信息
        /// </summary>
        /// <param name="notifyType"></param>
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
        /// GET
        /// </summary>
        /// <returns></returns>
        private PayResult CheckRequestGet()
        {
            var result = new PayResult
            {
                MerchantAccount = _payModel.Partner,
                PayEnum = PayEnum.Tenpay
            };
            //创建ResponseHandler实例
            var resHandler = new ResponseHandler(_httpContext);
            resHandler.setKey(TenpayUtil.tenpay_key);
            //判断签名
            if (resHandler.isTenpaySign())
            {
                //支付结果
                var tradeState = resHandler.getParameter("trade_state");
                //交易模式，1即时到账，2中介担保
                var tradeMode = resHandler.getParameter("trade_mode");

                result.OrderNo = resHandler.getParameter("out_trade_no");
                result.SerialNumber = resHandler.getParameter("transaction_id");
                var total = decimal.Parse(resHandler.getParameter("total_fee"));
                result.TotalAmount = (total / 100).ToString();
                result.PayDate = resHandler.getParameter("time_end");

                if ("1".Equals(tradeMode)) //即时到账 
                {
                    if ("0".Equals(tradeState))
                    {
                        result.Status = true;
                        result.Message = "即时到帐付款成功";
                    }
                    else
                    {
                        result.Message = "即时到账支付失败";
                    }
                }
                else if ("2".Equals(tradeMode)) //中介担保
                {
                    if ("0".Equals(tradeState))
                    {
                        result.Status = true;
                        result.Message = "中介担保付款成功";
                    }
                    else
                    {
                        result.Message = "中介担保付款失败" + tradeState;
                    }
                }
            }
            else
            {
                result.Message = "认证签名失败";
            }
            //获取debug信息,建议把debug信息写入日志，方便定位问题
            //string debuginfo = resHandler.getDebugInfo();
            LoggerHelper.InfoFormat(LogType.PaymentLogger, "财付通同步通知，{0}。\r\n", result.ToString());
            return result;
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <returns></returns>
        private PayResult CheckRequestPost()
        {
            var result = new PayResult
            {
                MerchantAccount = _payModel.Partner,
                PayEnum = PayEnum.Tenpay
            };
            //创建ResponseHandler实例
            var resHandler = new ResponseHandler(_httpContext);
            resHandler.setKey(TenpayUtil.tenpay_key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                //通知id
                var notifyId = resHandler.getParameter("notify_id");
                //通过通知ID查询，确保通知来至财付通
                //创建查询请求
                var queryReq = new RequestHandler(_httpContext);
                queryReq.init();
                queryReq.setKey(TenpayUtil.tenpay_key);
                queryReq.setGateUrl("https://gw.tenpay.com/gateway/simpleverifynotifyid.xml");
                queryReq.setParameter("partner", TenpayUtil.bargainor_id);
                queryReq.setParameter("notify_id", notifyId);

                //通信对象
                var httpClient = new TenpayHttpClient();
                httpClient.setTimeOut(5);
                //设置请求内容
                httpClient.setReqContent(queryReq.getRequestURL());

                //后台调用
                if (httpClient.call())
                {
                    //设置结果参数
                    var queryRes = new ClientResponseHandler();
                    queryRes.setContent(httpClient.getResContent());
                    queryRes.setKey(TenpayUtil.tenpay_key);
                    //判断签名及结果
                    //只有签名正确,retcode为0，trade_state为0才是支付成功
                    if (queryRes.isTenpaySign())
                    {
                        //支付结果
                        var tradeState = resHandler.getParameter("trade_state");
                        //交易模式，1即时到帐 2中介担保
                        var tradeMode = resHandler.getParameter("trade_mode");

                        result.OrderNo = queryRes.getParameter("out_trade_no");
                        result.SerialNumber = queryRes.getParameter("transaction_id");
                        var total = decimal.Parse(queryRes.getParameter("total_fee"));
                        result.TotalAmount = (total / 100).ToString();
                        result.PayDate = queryRes.getParameter("time_end");


                        //判断签名及结果
                        if ("0".Equals(queryRes.getParameter("retcode")))
                        {
                            if ("1".Equals(tradeMode)) //即时到账 
                            {
                                if ("0".Equals(tradeState))
                                {
                                    //给财付通系统发送成功信息
                                    result.Status = true;
                                    result.Message = "success";
                                }
                                else
                                {
                                    result.Message = "即时到账支付失败";
                                }
                            }
                            else if ("2".Equals(tradeMode))//中介担保
                            {
                                if ("0".Equals(tradeState))
                                {
                                    //给财付通系统发送成功信息
                                    result.Status = true;
                                    result.Message = "success";

                                }
                                else
                                {
                                    result.Message = "中介担保付款失败";
                                }
                            }
                        }
                        else
                        {
                            //错误时，返回结果可能没有签名，写日志trade_state、retcode、retmsg看失败详情。
                            //通知财付通处理失败，需要重新通知
                            result.Message = "查询验证签名失败或id验证失败";
                        }
                    }
                    else
                    {
                        result.Message = "通知ID查询签名验证失败";
                    }
                }
                else
                {
                    //通知财付通处理失败，需要重新通知
                    result.Message = "后台调用通信失败";
                    //写错误日志
                    //Response.Write("call err:" + httpClient.getErrInfo() + "<br>" + httpClient.getResponseCode() + "<br>"); 
                }
            }
            else
            {
                result.Message = "签名验证失败";
            }
            LoggerHelper.InfoFormat(LogType.PaymentLogger, "财付通异步通知，{0}。\r\n", result.ToString());
            return result;
        }

        public bool SignVerify(Dictionary<string, string> dicParams)
        {
            throw new NotImplementedException();
        }


    }
}
