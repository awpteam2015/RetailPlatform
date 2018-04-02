//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using CMBCHINALib;
//
//using Project.Infrastructure.FrameworkCore.Payment.Base;
//using Project.Infrastructure.FrameworkCore.Payment.Interfaces;
//using Project.Infrastructure.FrameworkCore.Payment.Model;

//namespace Project.Infrastructure.FrameworkCore.Payment.Services
//{
//    /// <summary>
//    /// 招商银行网银支付
//    /// </summary>
//    internal class CmbBankService : BaseService, IPayService
//    {
//        private readonly CmbBankModel _cmbBankModel;
//        private readonly FirmClientClass _cmbBankService = new FirmClientClass();

//        /// <summary>
//        /// 初始化
//        /// </summary>
//        public CmbBankService()
//        {
//            _cmbBankModel = new CmbBankModel
//            {
//                CmbKey = ConfigurationManager.AppSettings["CmbKey"],
//                CmbCoNo = ConfigurationManager.AppSettings["CmbCoNo"],
//                CmbGateway = ConfigurationManager.AppSettings["CmbGateway"],
//                CmbBranchId = ConfigurationManager.AppSettings["CmbBranchId"],
//                CmbReturnUrl = HostDomain + ConfigurationManager.AppSettings["CmbReturnUrl"]
//            };
//        }

//        /// <summary>
//        /// 发送支付请求
//        /// </summary>
//        /// <param name="orderPay">订单支付信息</param>
//        /// <returns></returns>
//        public string SubmitRequest(OrderPay orderPay)
//        {
//            var time = DateTime.Now.ToString("yyyyMMdd");
//            var builder = new StringBuilder();
//            builder.AppendFormat("<form name=\"form\" action=\"{0}\" method=\"post\">",
//                _cmbBankModel.CmbGateway);
//            builder.AppendFormat(" <input type=\"hidden\" name=\"BranchID\" value=\"{0}\" />",
//                _cmbBankModel.CmbBranchId);
//            builder.AppendFormat(" <input type=\"hidden\" name=\"CoNo\" value=\"{0}\" />",
//                _cmbBankModel.CmbCoNo);
           
//            builder.AppendFormat(" <input type=\"hidden\" name=\"BillNo\" value=\"{0}\" />",
//                orderPay.OrderNo);
//            builder.AppendFormat(" <input type=\"hidden\" name=\"Amount\" value=\"{0}\" />",
//                orderPay.TotalAmount.ToString("f2"));
//            builder.AppendFormat(" <input type=\"hidden\" name=\"Date\" value=\"{0}\" />",
//                time);
//            builder.AppendFormat(" <input type=\"hidden\" name=\"MerchantUrl\" value=\"{0}\" />",
//                _cmbBankModel.CmbReturnUrl);
//            builder.AppendFormat(" <input type=\"hidden\" name=\"MerchantReturnUrl\" value=\"{0}\" />",
//                _cmbBankModel.CmbReturnUrl); //可以不需要，银行默认同上

//            var merchantCode =
//            _cmbBankService.exGenMerchantCode("", time, _cmbBankModel.CmbBranchId,
//                _cmbBankModel.CmbCoNo,
//                orderPay.OrderNo, orderPay.TotalAmount.ToString("f2"), "", _cmbBankModel.CmbReturnUrl,"","","","","");
//            builder.AppendFormat(" <input type=\"hidden\" name=\"MerchantCode\" value=\"{0}\" />",
//                merchantCode);
//            builder.Append(" </form>");
//            builder.Append("<script type=\"text/javascript\">document.form.submit();</script>");

//            LoggerHelper.InfoFormat(LogType.PaymentLogger, "招行支付请求：订单号：{0}\r\n", orderPay.OrderNo);
//            return builder.ToString();
//        }

//        /// <summary>
//        /// 处理支付返回信息
//        /// </summary>
//        /// <param name="notifyType"></param>
//        /// <returns></returns>
//        public PayResult CheckNotifyData(NotifyEnum notifyType)
//        {
//            var result = new PayResult
//            {
//                PayEnum = PayEnum.CmbBank,
//                MerchantAccount = _cmbBankModel.CmbBranchId + "-" + _cmbBankModel.CmbCoNo
//            };
//            var succeed = HttpContext.Current.Request.QueryString["Succeed"];
//            var msg = HttpContext.Current.Request.QueryString["Msg"];
//            var queryString = HttpContext.Current.Request.QueryString.ToString();
//            var returnMsg = HttpContext.Current.Server.UrlDecode(queryString);
//            result.OrderNo = HttpContext.Current.Request.QueryString["BillNo"];
//            result.TotalAmount = HttpContext.Current.Request.QueryString["Amount"];

//            var dicParams = new Dictionary<string, string>
//            {
//                {"key", _cmbBankModel.CmbKey},
//                {"msg", returnMsg}
//            };
//            //签名验证
//            var verifyResult = SignVerify(dicParams);
//            if (verifyResult)
//            {
//                result.Status = succeed == "Y";
//                result.PayDate = msg.Substring(10, 8);
//                result.SerialNumber = msg.Substring(18);
//                result.MerchantAccount = string.Format("{0}-{1}", msg.Substring(0, 4)
//                    , msg.Substring(4, 6)); 
//            }
//            result.Message = verifyResult ? "验证成功" : "验证失败";
//            LoggerHelper.InfoFormat(LogType.PaymentLogger, "招商银行支付通知，{0}。\r\n", result.Message);
//            LoggerHelper.InfoFormat(LogType.PaymentLogger, "招商银行支付通知，{0}。\r\n", result.ToString());
//            return result;
//        }

//        /// <summary>
//        /// 签名验证
//        /// </summary>
//        /// <param name="dicParams">参数</param>
//        /// <returns></returns>
//        public bool SignVerify(Dictionary<string, string> dicParams)
//        {
//            try
//            {
//                var isNo = _cmbBankService.exCheckInfoFromBank(dicParams["key"], dicParams["msg"]);
//                if (isNo != 0)
//                {
//                    var errMsg = _cmbBankService.exGetLastErr(isNo);
//                    LoggerHelper.ErrorFormat(LogType.PaymentLogger, "招商银行\r\n通知商户验签失败,异常信息{0}。\r\n", errMsg);
//                    return false;
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {
//                LoggerHelper.ErrorFormat(LogType.PaymentLogger, "招商银行\r\n通知商户验签失败,错误信息{0}。\r\n", ex.Message);
//                return false;
//            }
//        }
//    }
//}
