
 /***************************************************************************
 *       功能：     PRMProductCategory映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class ProductCategoryMap : BaseMap<ProductCategoryEntity,int>
    {
        public ProductCategoryMap():base("PRM_ProductCategory")
        {
            this.MapPkidDefault<ProductCategoryEntity,int>();
 
            Map(p => p.ProductcategoryName);    
            Map(p => p.ParentId);    
            Map(p => p.Rank);    
            Map(p => p.Sort);    
            Map(p => p.SystemCategoryId);    
            Map(p => p.SystemCategoryName);    
            Map(p => p.Route);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.LastModificationTime);    
            Map(p => p.IsDeleted);    
            Map(p => p.DeleterUserCode);    
            Map(p => p.DeletionTime);

            HasMany(p => p.children)
 .AsSet()
 .LazyLoad()
 .Cascade.All().Inverse()
 .KeyColumn("ParentId");
        }
    }
}

    
 

