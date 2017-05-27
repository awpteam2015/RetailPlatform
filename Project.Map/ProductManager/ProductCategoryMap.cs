
 /***************************************************************************
 *       功能：     PRMProductCategory映射类
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     商品分类
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
 
            Map(p => p.ProductCategoryName);    
            Map(p => p.ParentProductCategoryId);    
            Map(p => p.CategoryRoute);    
            Map(p => p.Rank);    
        }
    }
}

    
 

