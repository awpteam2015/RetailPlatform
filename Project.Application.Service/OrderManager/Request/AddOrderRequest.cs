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
        /// 联系人（改）
        /// </summary>
        public virtual System.String Linkman { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public virtual System.String LinkmanTel { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public virtual System.String LinkmanMobilephone { get; set; }
        /// <summary>
        /// 联系人省份
        /// </summary>
        public virtual System.String LinkmanProvinceId { get; set; }
        /// <summary>
        /// 联系人城市
        /// </summary>
        public virtual System.String LinkmanCityId { get; set; }
        /// <summary>
        /// 联系人区域(新增)
        /// </summary>
        public virtual System.String LinkmanAreaId { get; set; }
        /// <summary>
        /// 联系人配送地址（改）
        /// </summary>
        public virtual System.String LinkmanAddress { get; set; }
        /// <summary>
        /// 联系人配送地址全（改2012.11.2）
        /// </summary>
        public virtual System.String LinkmanAddressfull { get; set; }
        /// <summary>
        /// 联系人邮政编码（改）
        /// </summary>
        public virtual System.String LinkmanPostcode { get; set; }
        /// <summary>
        /// 联系人送货备注（改）
        /// </summary>
        public virtual System.String LinkmanRemark { get; set; }

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
