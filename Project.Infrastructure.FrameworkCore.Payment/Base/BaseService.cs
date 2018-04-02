using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Project.Infrastructure.FrameworkCore.Payment.Base
{
    /// <summary>
    /// 支付服务基类
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// 主机域名（正式需配置成域名）
        /// </summary>
        protected readonly string HostDomain;

        /// <summary>
        /// 初始化
        /// </summary>
        public BaseService()
        {
            HostDomain = ConfigurationManager.AppSettings["PaymentHost"];
            if (string.IsNullOrWhiteSpace(HostDomain))
            {
                throw new NullReferenceException("网银支付主机未配置。");
            }
        } 
    }
}
