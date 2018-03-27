
 /***************************************************************************
 *       功能：     SPMTicket映射类
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     券
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SalePromotionManager;

namespace  Project.Map.SalePromotionManager
{
    public class TicketMap : BaseMap<TicketEntity,int>
    {
        public TicketMap():base("SPM_Ticket")
        {
            this.MapPkidDefault<TicketEntity,int>();
 
            Map(p => p.TicketCode);    
            Map(p => p.TickettypeId);    
            Map(p => p.Status);    
            Map(p => p.AvaildateStart);    
            Map(p => p.AvaildateEnd);    
            Map(p => p.OrderNo);    
            Map(p => p.UseDate);    
            Map(p => p.CustomerId);    
            Map(p => p.RuleId);    
            Map(p => p.ActivityId);    
        }
    }
}

    
 

