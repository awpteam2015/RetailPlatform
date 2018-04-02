using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Payment.Model;

namespace Project.Infrastructure.FrameworkCore.Payment.Interfaces
{
    /// <summary>
    /// 支付接口
    /// </summary>
    public interface IPayService
    {
        /// <summary>
        /// 发送支付请求
        /// </summary>
        /// <param name="orderPay">支付实体</param>
        /// <returns></returns>
        string SubmitRequest(OrderPay orderPay);
        /// <summary>
        /// 处理支付返回信息
        /// </summary>
        /// <param name="notifyType">通知类型</param>
        /// <returns></returns>
        PayResult CheckNotifyData(NotifyEnum notifyType);
        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="dicParams">参数</param>
        /// <returns></returns>
        bool SignVerify(Dictionary<string, string> dicParams);
    }
}
