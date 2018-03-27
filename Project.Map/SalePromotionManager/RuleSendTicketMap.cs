
 /***************************************************************************
 *       功能：     SPMRuleSendTicket映射类
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     满足金额发券
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SalePromotionManager;

namespace  Project.Map.SalePromotionManager
{
    public class RuleSendTicketMap : BaseMap<RuleSendTicketEntity,int>
    {
        public RuleSendTicketMap():base("SPM_RuleSendTicket")
        {
            this.MapPkidDefault<RuleSendTicketEntity,int>();
 
            Map(p => p.RuleId);    
            Map(p => p.ActivityId);    
            Map(p => p.StartMoney);    
            Map(p => p.EndMoney);    
            Map(p => p.TicketNum);    
            Map(p => p.CardTypeIds);    
            Map(p => p.TicketAvaildateEnd);    
            Map(p => p.TicketAvaildateStart);    
        }
    }
}

    
 

