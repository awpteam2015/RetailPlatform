
 /***************************************************************************
 *       功能：     SPMRuleDiscountMoney映射类
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     满足金额减免
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SalePromotionManager;

namespace  Project.Map.SalePromotionManager
{
    public class RuleDiscountMoneyMap : BaseMap<RuleDiscountMoneyEntity,int>
    {
        public RuleDiscountMoneyMap():base("SPM_RuleDiscountMoney")
        {
            this.MapPkidDefault<RuleDiscountMoneyEntity,int>();
 
            Map(p => p.RuleId);    
            Map(p => p.ActivityId);    
            Map(p => p.StartMoney);    
            Map(p => p.EndMoney);    
            Map(p => p.DiscountMoney);    
            Map(p => p.CardTypeIds);    
        }
    }
}

    
 

