
 /***************************************************************************
 *       功能：     CNMPageContent映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ContentManager;

namespace  Project.Map.ContentManager
{
    public class PageContentMap : BaseMap<PageContentEntity,int>
    {
        public PageContentMap():base("CNM_PageContent")
        {
            this.MapPkidDefault<PageContentEntity,int>();

            Map(p => p.PageContentCategoryId);
            Map(p => p.PageContentCategoryName);

            Map(p => p.Title1);    
            Map(p => p.Title2);    
            Map(p => p.Title3);    
            Map(p => p.Description1);    
            Map(p => p.Description2);    
            Map(p => p.Description3);    
            Map(p => p.ImageUrl1);    
            Map(p => p.ImageUrl2);    
            Map(p => p.ImageUrl3);    
            Map(p => p.DeletionTime);    
            Map(p => p.DeleterUserCode);    
            Map(p => p.IsDeleted);    
            Map(p => p.LastModificationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.CreatorUserCode);      Map(p => p.BrowseCount);    
        }
    }
}

    
 

