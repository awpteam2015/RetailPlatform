
 /***************************************************************************
 *       功能：     SPMActivity映射类
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     促销活动  目前考虑单品促销 满足金额发券 满足金额减免
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SalePromotionManager;

namespace  Project.Map.SalePromotionManager
{
    public class ActivityMap : BaseMap<ActivityEntity,int>
    {
        public ActivityMap():base("SPM_Activity")
        {
            this.MapPkidDefault<ActivityEntity,int>();
 
            Map(p => p.Title);    
            Map(p => p.StartDate);    
            Map(p => p.EndDate);    
            Map(p => p.State);    
            Map(p => p.BriefDescription);

            HasMany(p => p.RuleEntityList)
 .AsSet()
 .LazyLoad()
 .Cascade.Delete().Inverse()
 .NotFound.Ignore()
 .KeyColumn("ActivityId");

        }
    }
}

    
 

