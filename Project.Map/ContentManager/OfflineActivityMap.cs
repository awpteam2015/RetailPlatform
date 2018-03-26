
 /***************************************************************************
 *       功能：     CNMOfflineActivity映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ContentManager;

namespace  Project.Map.ContentManager
{
    public class OfflineActivityMap : BaseMap<OfflineActivityEntity,int>
    {
        public OfflineActivityMap():base("CNM_OfflineActivity")
        {
            this.MapPkidDefault<OfflineActivityEntity,int>();
 
            Map(p => p.Tttle);    
            Map(p => p.OfflineActivityAddress);    
            Map(p => p.StartDate);    
            Map(p => p.EndDate);    
            Map(p => p.ImageUrl);    
            Map(p => p.BriefDescription);       Map(p => p.Description);       Map(p => p.BindGoodsCode);    
            Map(p => p.State);    
            Map(p => p.DeletionTime);    
            Map(p => p.DeleterUserCode);    
            Map(p => p.IsDeleted);    
            Map(p => p.LastModificationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.CreatorUserCode);    
        }
    }
}

    
 

