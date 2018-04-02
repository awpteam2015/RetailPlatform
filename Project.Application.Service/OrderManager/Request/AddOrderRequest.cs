using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Application.Service.Common;
using Project.Model.OrderManager;

namespace Project.Application.Service.OrderManager.Request
{
   public class AddOrderRequest:RequestBase
    {

        /// <summary>
        /// 送货地址
        /// </summary>
       public virtual  int CustomerAddressId { get; set; }

        /// <summary>
        /// 订单行项目信息
        /// </summary>
        public virtual ISet<OrderMainDetailEntity> OrderMainDetailEntityList { get; set; }

        /// <summary>
        /// 发票信息
        /// </summary>
        public virtual OrderInvoiceEntity OrderInvoiceEntity { get; set; }

    }
}
