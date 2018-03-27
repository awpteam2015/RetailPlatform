
/***************************************************************************
*       功能：     SPMRule映射类
*       作者：     李伟伟
*       日期：     2018/3/26
*       描述：     促销规则
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SalePromotionManager;

namespace Project.Map.SalePromotionManager
{
    public class RuleMap : BaseMap<RuleEntity, int>
    {
        public RuleMap() : base("SPM_Rule")
        {
            this.MapPkidDefault<RuleEntity, int>();

            Map(p => p.ActivityId);
            Map(p => p.RuleType);
            Map(p => p.Title);

            References(p => p.RuleDiscountMoneyEntity)
             .LazyLoad()
           .Not.Insert()
           .Not.Update()
              .NotFound.Ignore()
              .PropertyRef("RuleId")
              .Column("PkId");

            References(p => p.RuleSendTicketEntity)
         .LazyLoad()
           .Not.Insert()
           .Not.Update()
           .NotFound.Ignore()
           .PropertyRef("RuleId")
              .Column("PkId");

            HasMany(p => p.RulePromotionGoodsEntityList)
.AsSet()
.LazyLoad()
.Cascade.Delete().Inverse()
.NotFound.Ignore()
.KeyColumn("RuleId");

        }
    }
}




