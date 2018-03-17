
 /***************************************************************************
 *       功能：     OMOrderInvoice映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.OrderManager;

namespace  Project.Map.OrderManager
{
    public class OrderInvoiceMap : BaseMap<OrderInvoiceEntity,int>
    {
        public OrderInvoiceMap():base("OM_OrderInvoice")
        {
            this.MapPkidDefault<OrderInvoiceEntity,int>();
 
            Map(p => p.OrderNo);    
            Map(p => p.InvoiceTitle);    
            Map(p => p.InvoiceContent);    
            Map(p => p.InvoiceCompany);    
            Map(p => p.InvoiceNo);    
            Map(p => p.Money);    
            Map(p => p.Remark);    
        }
    }
}

    
 

