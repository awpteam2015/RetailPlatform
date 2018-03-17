
 /***************************************************************************
 *       功能：     CNMPageContentCategory映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     内容分类
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ContentManager;

namespace  Project.Map.ContentManager
{
    public class PageContentCategoryMap : BaseMap<PageContentCategoryEntity,int>
    {
        public PageContentCategoryMap():base("CNM_PageContentCategory")
        {
            this.MapPkidDefault<PageContentCategoryEntity,int>();
 
            Map(p => p.PageContentCategoryName);    
            Map(p => p.ParentId);    
            Map(p => p.Rank);    
            Map(p => p.Sort);    
            Map(p => p.Route);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.LastModificationTime);    
            Map(p => p.IsDeleted);    
            Map(p => p.DeleterUserCode);    
            Map(p => p.DeletionTime);    
        }
    }
}

    
 

