
 /***************************************************************************
 *       功能：     SPMRulePromotionGoods映射类
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     促销商品
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SalePromotionManager;

namespace  Project.Map.SalePromotionManager
{
    public class RulePromotionGoodsMap : BaseMap<RulePromotionGoodsEntity,int>
    {
        public RulePromotionGoodsMap():base("SPM_RulePromotionGoods")
        {
            this.MapPkidDefault<RulePromotionGoodsEntity,int>();
 
            Map(p => p.ActivityId);    
            Map(p => p.RuleId);    
            Map(p => p.ProductId);    
            Map(p => p.ProductCode);    
            Map(p => p.GoodsCode);    
            Map(p => p.GoodsId);    
            Map(p => p.Price);    
            Map(p => p.PromotionPrice);
            Map(p => p.ProductName);
            Map(p => p.SpecDetail);
        }
    }
}

    
 

