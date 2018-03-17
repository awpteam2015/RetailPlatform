

 /***************************************************************************
 *       功能：     OMOrderInvoice实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.OrderManager
{
    public class OrderInvoiceEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 订单号
        /// </summary>
        public virtual System.String OrderNo{get; set;}
        /// <summary>
        /// 发票抬头
        /// </summary>
        public virtual System.String InvoiceTitle{get; set;}
        /// <summary>
        /// 发票内容
        /// </summary>
        public virtual System.String InvoiceContent{get; set;}
        /// <summary>
        /// 开票公司
        /// </summary>
        public virtual System.String InvoiceCompany{get; set;}
        /// <summary>
        /// 发票编码
        /// </summary>
        public virtual System.String InvoiceNo{get; set;}
        /// <summary>
        /// 开票金额
        /// </summary>
        public virtual System.Decimal? Money{get; set;}
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

