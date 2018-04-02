//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.Xml;
//using ICBCEBANKUTILLib;
//
//using Project.Infrastructure.FrameworkCore.Payment.Base;
//using Project.Infrastructure.FrameworkCore.Payment.Interfaces;
//using Project.Infrastructure.FrameworkCore.Payment.Model;

//namespace Project.Infrastructure.FrameworkCore.Payment.Services
//{
//    /// <summary>
//    /// 工商银行网银支付
//    /// </summary>
//    internal class IcbcBankService : BaseService, IPayService
//    {
//        private readonly IcbcBankModel _icbcBankModel;
//        private readonly B2CUtilClass _icbcB2CService = new B2CUtilClass();

//        /// <summary>
//        /// 初始化
//        /// </summary>
//        public IcbcBankService()
//        {
//            IcbcInit();
//            _icbcBankModel = new IcbcBankModel
//            {
//                MerAcct = ConfigurationManager.AppSettings["IcbcMerchantAcct"],
//                MerId = ConfigurationManager.AppSettings["IcbcMerchantId"],
//                IcbcGateway = ConfigurationManager.AppSettings["IcbcBankUrl"],
//                MerReference = HostDomain.Replace("http://", ""),//MerReference不能加 http
//                MerUrl = HostDomain + ConfigurationManager.AppSettings["IcbcReturnUrl"]
//            };
//        }

//        /// <summary>
//        /// 初始化证书
//        /// </summary>
//        /// <returns></returns>
//        public void IcbcInit()
//        {
//            try
//            {
//                var certFnPath = ConfigurationManager.AppSettings["IcbcCertFNPath"];
//                var certFnmPath = ConfigurationManager.AppSettings["IcbcCertFNMPath"];
//                var keyPath = ConfigurationManager.AppSettings["IcbcKeyFNPath"];
//                var keyValue = ConfigurationManager.AppSettings["IcbcKeyValue"];
//                var icbcInitResult = _icbcB2CService.init(certFnPath, certFnmPath, keyPath, keyValue);
//                if (icbcInitResult != 0)
//                {
//                    LoggerHelper.ErrorFormat(LogType.PaymentLogger, "工商银行初始化异常,错误码{0}。\r\n", icbcInitResult);
//                    throw new Exception("工商银行初始化失败。");
//                }
//            }
//            catch (Exception ex)
//            {
//                LoggerHelper.ErrorFormat(LogType.PaymentLogger, "工商银行初始化失败,错误信息：\r\n", ex.Message);
//                throw new Exception("工商银行初始化失败。");
//            }
//        }

//        /// <summary>
//        /// 发送支付请求
//        /// </summary>
//        /// <param name="orderPay">订单支付信息</param>
//        /// <returns></returns>
//        public string SubmitRequest(OrderPay orderPay)
//        {
//            var builder = new StringBuilder();
//            var tranDataXml = GetTranDataXml(orderPay);
//            var merSignMessage = GetSignMessage(tranDataXml);
//            var merCert = GetMerCert();
//            var tranData = Convert.ToBase64String(Encoding.GetEncoding("GBK").GetBytes(tranDataXml));

//            builder.AppendFormat("<form id=\"form\" action=\"{0}\" method=\"post\">",
//            _icbcBankModel.IcbcGateway);
//            builder.AppendFormat("<input name=\"interfaceName\" type=\"hidden\"  value=\"{0}\" />",
//                _icbcBankModel.InterfaceName);
//            builder.AppendFormat("<input name=\"interfaceVersion\" type=\"hidden\"  value=\"{0}\" />",
//               _icbcBankModel.InterfaceVersion);
//            builder.AppendFormat("<input name=\"tranData\" type=\"hidden\"  value=\"{0}\" />",
//               tranData);
//            builder.AppendFormat("<input name=\"merSignMsg\" type=\"hidden\"  value=\"{0}\" />",
//               merSignMessage);
//            builder.AppendFormat("<input  name=\"merCert\" type=\"hidden\" value=\"{0}\" />",
//               merCert);
//            builder.Append("</form>");
//            builder.Append("<script type=\"text/javascript\">document.getElementById(\"form\").submit();</script>");

//            //LoggerHelper.InfoFormat(LogType.PaymentLogger, "工行支付请求：\r\n{0}\r\n", tranDataXml);
//            LoggerHelper.InfoFormat(LogType.PaymentLogger, "工行支付请求：订单号：{0}\r\n", orderPay.OrderNo);
//            return builder.ToString();
//        }

//        /// <summary>
//        /// 处理支付返回信息
//        /// </summary>
//        /// <param name="notifyType"></param>
//        /// <returns></returns>
//        public PayResult CheckNotifyData(NotifyEnum notifyType)
//        {
//            var result = new PayResult { PayEnum = PayEnum.IcbcBank };
//            try
//            {
//                var merVar = HttpContext.Current.Request.Params["merVAR"];
//                var signMsg = HttpContext.Current.Request.Params["signMsg"];
//                var notifyData = HttpContext.Current.Request.Params["notifyData"];
//                //解析返回Xml
//                var xmlDoc = new XmlDocument();
//                var notifyDataXml = Encoding.GetEncoding("GBK").GetString(Convert.FromBase64String(notifyData));
//                xmlDoc.LoadXml(notifyDataXml);
//                var tranStatXml = xmlDoc.GetElementsByTagName("tranStat");
//                var tranStat = tranStatXml.Item(0).InnerText.Trim();
//                var orderXml = xmlDoc.GetElementsByTagName("orderid");
//                var amountXml = xmlDoc.GetElementsByTagName("amount");
//                var tradeXml = xmlDoc.GetElementsByTagName("tranSerialNo");
//                var merAcctXml = xmlDoc.GetElementsByTagName("merAcct");
//                var orderDateXml = xmlDoc.GetElementsByTagName("orderDate");

//                result.OrderNo = orderXml.Item(0).InnerText.Trim();
//                result.TotalAmount = amountXml.Item(0).InnerText.Trim();
//                result.SerialNumber = tradeXml.Item(0).InnerText.Trim();
//                result.MerchantAccount = merAcctXml.Item(0).InnerText.Trim();
//                result.PayDate = orderDateXml.Item(0).InnerText.Trim();

//                //订单处理状态 1:交易成功，已清算;2:交易失败;3:交易可疑.
//                if (tranStat == "1")
//                {
//                    var dicParams = new Dictionary<string, string>
//                    {
//                        {"notifyDataXml", notifyDataXml},
//                        {"signMsg", signMsg},
//                    };
//                    //对银行返回给商户的参数进行验签
//                    var isSingn = SignVerify(dicParams);
//                    if (isSingn)
//                    {
//                        result.Status = true;
//                        result.Message = "支付成功";
//                    }
//                }
//                LoggerHelper.InfoFormat(LogType.PaymentLogger, "工商银行支付通知，{0}。\r\n", result.ToString());
//                return result;
//            }
//            catch (Exception ex)
//            {
//                LoggerHelper.ErrorFormat(LogType.PaymentLogger, "工商银行验证银行返回信息,错误异常信息{0}。\r\n", ex.Message);
//                return result;
//            }
//        }

//        /// <summary>
//        /// 签名验证
//        /// </summary>
//        /// <returns></returns>
//        public bool SignVerify(Dictionary<string, string> dicParams)
//        {
//            var result = true;
//            var signMsg = dicParams["signMsg"];
//            var notifyDataXml = dicParams["notifyDataXml"];
//            var singnCode = _icbcB2CService.verifySignC(notifyDataXml, notifyDataXml.Length, signMsg, signMsg.Length);
//            if (singnCode != 0)
//            {
//                result = false;
//                LoggerHelper.ErrorFormat(LogType.PaymentLogger, "工商银行\r\n通知商户验签失败,错误码{0},详细信息{1}。\r\n", notifyDataXml);
//            }
//            return result;
//        }

//        /// <summary>
//        /// 获取工行网银提交页面
//        /// </summary>
//        /// <returns></returns>
//        private string GetTranDataXml(OrderPay orderPay)
//        {
//            var builder = new StringBuilder();
//            //银行金额的单位是分，网站订单的金额的单位是元，所以要转换
//            var amount = Convert.ToString(Convert.ToInt64(orderPay.TotalAmount * 100));
//            var date = DateTime.Now.ToString("yyyyMMddHHmmss");
//            builder.Append("<?xml version=\"1.0\" encoding=\"GBK\" standalone=\"no\"?>");
//            builder.Append("<B2CReq>");
//            builder.AppendFormat("<interfaceName>ICBC_PERBANK_B2C</interfaceName>");
//            builder.AppendFormat("<interfaceVersion>1.0.0.11</interfaceVersion>");
//            builder.Append("<orderInfo>");
//            builder.AppendFormat("<orderDate>{0}</orderDate>", date);
//            builder.AppendFormat("<curType>{0}</curType>", _icbcBankModel.CurType);
//            builder.AppendFormat("<merID>{0}</merID>", _icbcBankModel.MerId);
//            builder.Append("<subOrderInfoList>");
//            builder.Append("<subOrderInfo>");
//            builder.AppendFormat("<orderid>{0}</orderid>", orderPay.OrderNo);
//            builder.AppendFormat("<amount>{0}</amount>", amount);
//            builder.AppendFormat("<installmentTimes>{0}</installmentTimes>", _icbcBankModel.InstallmentTimes);
//            builder.AppendFormat("<merAcct>{0}</merAcct>", _icbcBankModel.MerAcct);
//            builder.Append("<goodsID></goodsID>");
//            builder.Append("<goodsName>harborhousehome</goodsName>");
//            builder.Append("<goodsNum></goodsNum>");
//            builder.Append("<carriageAmt>0</carriageAmt>");
//            builder.Append("</subOrderInfo>");
//            builder.Append("</subOrderInfoList>");
//            builder.Append("</orderInfo>");
//            builder.Append("<custom>");
//            builder.AppendFormat("<verifyJoinFlag>{0}</verifyJoinFlag>", _icbcBankModel.VerifyJoinFlag);
//            builder.AppendFormat("<Language>{0}</Language>", _icbcBankModel.Language);
//            builder.Append("</custom>");
//            builder.Append("<message>");
//            builder.AppendFormat("<creditType>{0}</creditType>", _icbcBankModel.CreditType);
//            builder.AppendFormat("<notifyType>{0}</notifyType>", _icbcBankModel.NotifyType);
//            builder.AppendFormat("<resultType>{0}</resultType>", _icbcBankModel.ResultType);
//            builder.AppendFormat("<merReference>{0}</merReference>", _icbcBankModel.MerReference);
//            builder.AppendFormat("<merCustomIp></merCustomIp>");
//            builder.Append("<goodsType>1</goodsType>");
//            builder.Append("<merCustomID></merCustomID>");
//            builder.Append("<merCustomPhone></merCustomPhone>");
//            builder.Append("<goodsAddress></goodsAddress>");
//            builder.Append("<merOrderRemark></merOrderRemark>");
//            builder.Append("<merHint></merHint>");
//            builder.Append("<remark1></remark1>");
//            builder.Append("<remark2></remark2>");
//            builder.AppendFormat("<merURL>{0}</merURL>", _icbcBankModel.MerUrl);
//            builder.Append("<merVAR></merVAR>");
//            builder.Append("</message>");
//            builder.Append("</B2CReq>");
//            return builder.ToString();
//        }

//        /// <summary>
//        /// 获取商城证书公钥
//        /// </summary>
//        /// <returns></returns>
//        private string GetMerCert()
//        {
//            return _icbcB2CService.getCert(1);
//        }

//        /// <summary>
//        /// 获取商户提交给银行的签名串
//        /// </summary>
//        /// <param name="tranDataXml"></param>
//        /// <returns></returns>
//        private string GetSignMessage(string tranDataXml)
//        {
//            var merSignMessage = _icbcB2CService.signC(tranDataXml, tranDataXml.Length);
//            return merSignMessage;
//        }
//    }
//}
