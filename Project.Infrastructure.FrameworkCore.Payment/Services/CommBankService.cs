using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.Payment.Base;
using Project.Infrastructure.FrameworkCore.Payment.Interfaces;
using Project.Infrastructure.FrameworkCore.Payment.Model;

namespace Project.Infrastructure.FrameworkCore.Payment.Services
{
    /// <summary>
    /// 交通银行网银支付
    /// </summary>
    internal class CommBankService : BaseService, IPayService
    {
        private readonly CommBankModel _commBankModel;

        /// <summary>
        /// 初始化
        /// </summary>
        public CommBankService()
        {
            _commBankModel = new CommBankModel
            {
                Ip = ConfigurationManager.AppSettings["CommIp"],
                Port = Int32.Parse(ConfigurationManager.AppSettings["CommPort"]),
                NotifyType = ConfigurationManager.AppSettings["CommNotifyType"],
                MerId = ConfigurationManager.AppSettings["CommMerchantId"],
                MerUrl = HostDomain + ConfigurationManager.AppSettings["CommReturnUrl"],
                GoodsUrl = HostDomain + ConfigurationManager.AppSettings["CommReturnUrl"]
            };
        }

        /// <summary>
        /// 发送支付请求
        /// </summary>
        /// <param name="orderPay">订单支付信息</param>
        /// <returns></returns>
        public string SubmitRequest(OrderPay orderPay)
        {
            var dt = DateTime.Now;
            _commBankModel.Orderid = orderPay.OrderNo;
            _commBankModel.OrderDate = dt.ToString("yyyyMMdd");
            _commBankModel.OrderTime = dt.ToString("HHmmss");
            _commBankModel.Amount = orderPay.TotalAmount.ToString("f2");
            var sourceMsg = GetSourceMsg();
            var resultMsg = GetResponseData(sourceMsg);
            var builder = new StringBuilder();
            builder.AppendFormat("<form name=\"form\" method=\"post\" action =\"{0}\">", resultMsg.Item1);
            builder.AppendFormat("<input type = \"hidden\" name = \"interfaceVersion\" value =\"{0}\"/>",
                _commBankModel.InterfaceVersion);
            builder.AppendFormat("<input type = \"hidden\" name = \"merID\" value =\"{0}\"/>", _commBankModel.MerId);
            builder.AppendFormat("<input type = \"hidden\" name = \"orderid\" value =\"{0}\"/>",
                _commBankModel.Orderid);
            builder.AppendFormat("<input type = \"hidden\" name = \"orderDate\" value = \"{0}\"/>",
                _commBankModel.OrderDate);
            builder.AppendFormat("<input type = \"hidden\" name = \"orderTime\" value = \"{0}\"/>",
                _commBankModel.OrderTime);
            builder.AppendFormat("<input type = \"hidden\" name = \"tranType\" value =\"{0}\"/>",
                _commBankModel.TranType);
            builder.AppendFormat("<input type = \"hidden\" name = \"amount\" value = \"{0}\"/>",
                _commBankModel.Amount);
            builder.AppendFormat("<input type = \"hidden\" name = \"curType\" value =\"{0}\"/>",
                _commBankModel.CurType);
            builder.AppendFormat("<input type = \"hidden\" name = \"orderContent\" value = \"{0}\"/>",
                _commBankModel.OrderContent);
            builder.AppendFormat("<input type = \"hidden\" name = \"orderMono\" value = \"{0}\"/>",
                _commBankModel.OrderMono);
            builder.AppendFormat("<input type = \"hidden\" name = \"phdFlag\" value = \"{0}\"/>",
                _commBankModel.PhdFlag);
            builder.AppendFormat("<input type = \"hidden\" name = \"notifyType\" value = \"{0}\"/>",
                _commBankModel.NotifyType);
            builder.AppendFormat("<input type = \"hidden\" name = \"merURL\" value = \"{0}\"/>",
                _commBankModel.MerUrl);
            builder.AppendFormat("<input type = \"hidden\" name = \"goodsURL\" value = \"{0}\"/>",
                _commBankModel.GoodsUrl);
            builder.AppendFormat("<input type = \"hidden\" name = \"jumpSeconds\" value =\"{0}\"/>",
                _commBankModel.JumpSeconds);
            builder.AppendFormat("<input type = \"hidden\" name = \"payBatchNo\" value = \"{0}\"/>",
                _commBankModel.PayBatchNo);
            builder.AppendFormat("<input type = \"hidden\" name = \"proxyMerName\" value = \"{0}\"/>",
                _commBankModel.ProxyMerName);
            builder.AppendFormat("<input type = \"hidden\" name = \"proxyMerType\" value = \"{0}\"/>",
                _commBankModel.ProxyMerType);
            builder.AppendFormat("<input type = \"hidden\" name = \"proxyMerCredentials\" value =\"{0}\"/>",
                _commBankModel.ProxyMercredentials);
            builder.AppendFormat("<input type = \"hidden\" name = \"netType\" value = \"{0}\"/>",
                _commBankModel.NetType);
            builder.AppendFormat("<input type = \"hidden\" name = \"merSignMsg\" value = \"{0}\"/>", resultMsg.Item2);
            builder.AppendFormat("<input type = \"hidden\" name = \"issBankNo\" value = \"{0}\"/>",
                _commBankModel.IssBankNo);
            builder.AppendFormat("</form>");
            builder.Append("<script type=\"text/javascript\">document.form.submit();</script>");

            LoggerHelper.InfoFormat(LogType.PaymentLogger, "交行支付请求：订单号：{0}\r\n", orderPay.OrderNo);
            return builder.ToString();
        }

        /// <summary>
        /// 处理支付返回信息
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        public PayResult CheckNotifyData(NotifyEnum notifyType)
        {
            var result = new PayResult
            {
                PayEnum = PayEnum.CommBank,
                MerchantAccount = _commBankModel.MerId
            };
            try
            {
                var notifyMsg = HttpContext.Current.Request.Params["notifyMsg"];
                var sendMsg = new StringBuilder();
                //sendMsg.Append("<?xml version='1.0' encoding='UTF-8'?>")
                //组织申请报文
                sendMsg.Append("<Message>")
                    .Append("<TranCode>").Append("cb2200_verify").Append("</TranCode>")
                    .Append("<MsgContent>")
                    .Append(notifyMsg)
                    .Append("</MsgContent></Message>");

                TcpClient client = new TcpClient(_commBankModel.Ip, _commBankModel.Port);
                NetworkStream stream = client.GetStream();

                Byte[] data = Encoding.UTF8.GetBytes(sendMsg.ToString());
                stream.Write(data, 0, data.Length);
                data = new Byte[50 * 1024];
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.UTF8.GetString(data, 0, bytes);
                stream.Close();
                client.Close();

                //解析返回报文
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseData);
                XmlNodeList list = xmlDoc.GetElementsByTagName("retCode");
                string retCode = list.Item(0).InnerText.Trim();
                list = xmlDoc.GetElementsByTagName("errMsg");
                string errMsg = list.Item(0).InnerText.Trim();

                if (!retCode.Equals("0"))
                {
                    result.Status = false;
                    LoggerHelper.ErrorFormat(LogType.PaymentLogger, "交通银行返回商户验签失败,返回码:{0},错误信息:{1},返回详细信息:{2}。\r\n", retCode, errMsg, notifyMsg);
                }
                else
                {
                    var strs = notifyMsg.Split('|');
                    result.OrderNo = strs[1]; //订单号
                    result.SerialNumber = strs[8]; //交易流水号
                    result.PayDate = strs[6] + " " + strs[7];
                    result.TotalAmount = strs[2];
                    result.Status = true;
                }
                LoggerHelper.InfoFormat(LogType.PaymentLogger, "交通银行支付通知:{0},银行返回信息{1}。\r\n", result.OrderNo, result.ToString());
                return result;
            }
            catch (Exception ex)
            {
                LoggerHelper.ErrorFormat(LogType.PaymentLogger, "交通银行通知,错误异常信息{0}。\r\n", ex.Message);
                return result;
            }
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
        /// 获取交通银行数据信息
        /// </summary>
        private string GetSourceMsg()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}", _commBankModel.InterfaceVersion);
            builder.AppendFormat("|{0}", _commBankModel.MerId);
            builder.AppendFormat("|{0}", _commBankModel.Orderid);
            builder.AppendFormat("|{0}", _commBankModel.OrderDate);
            builder.AppendFormat("|{0}", _commBankModel.OrderTime);
            builder.AppendFormat("|{0}", _commBankModel.TranType);
            builder.AppendFormat("|{0}", _commBankModel.Amount);
            builder.AppendFormat("|{0}", _commBankModel.CurType);
            builder.AppendFormat("|{0}", _commBankModel.OrderContent);
            builder.AppendFormat("|{0}", _commBankModel.OrderMono);
            builder.AppendFormat("|{0}", _commBankModel.PhdFlag);
            builder.AppendFormat("|{0}", _commBankModel.NotifyType);
            builder.AppendFormat("|{0}", _commBankModel.MerUrl);
            builder.AppendFormat("|{0}", _commBankModel.GoodsUrl);
            builder.AppendFormat("|{0}", _commBankModel.JumpSeconds);
            builder.AppendFormat("|{0}", _commBankModel.PayBatchNo);
            builder.AppendFormat("|{0}", _commBankModel.ProxyMerName);
            builder.AppendFormat("|{0}", _commBankModel.ProxyMerType);
            builder.AppendFormat("|{0}", _commBankModel.ProxyMercredentials);
            builder.AppendFormat("|{0}", _commBankModel.NetType);
            return builder.ToString();
        }

        /// <summary>
        /// 通过socket与交通银行链接，获取返回信息(orderUrl,signMsg)
        /// </summary>
        /// <param name="sourceMsg"></param>
        /// <returns></returns>
        private Tuple<string, string> GetResponseData(string sourceMsg)
        {
            var sendMsg = new StringBuilder();
            //组织申请报文
            sendMsg.Append("<Message>")
                   .Append("<TranCode>").Append("cb2200_sign").Append("</TranCode>")
                   .Append("<MsgContent>")
                   .Append(sourceMsg)
                   .Append("</MsgContent></Message>");
            TcpClient client = new TcpClient(_commBankModel.Ip, _commBankModel.Port);
            NetworkStream stream = client.GetStream();
            Byte[] data = Encoding.UTF8.GetBytes(sendMsg.ToString());
            stream.Write(data, 0, data.Length);
            data = new Byte[50 * 1024];
            String responseData = String.Empty;
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = Encoding.UTF8.GetString(data, 0, bytes);
            stream.Close();
            client.Close();

            //解析返回报文
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseData);
            XmlNodeList list = xmlDoc.GetElementsByTagName("retCode");
            string retCode = list.Item(0).InnerText.Trim();
            //errMsg
            list = xmlDoc.GetElementsByTagName("errMsg");
            string errMsg = list.Item(0).InnerText.Trim();
            //signMsg
            list = xmlDoc.GetElementsByTagName("signMsg");
            string signMsg = list.Item(0).InnerText.Trim();
            //orderUrl
            list = xmlDoc.GetElementsByTagName("orderUrl");
            string orderUrl = list.Item(0).InnerText.Trim();

            if (!retCode.Equals("0"))
            {
                LoggerHelper.ErrorFormat(LogType.PaymentLogger, "交通银行链接,返回码:{0},错误信息:{1},详细信息:{2}\r\n", retCode, errMsg, sourceMsg);
            }
            return new Tuple<string, string>(orderUrl, signMsg);
        }
    }
}
