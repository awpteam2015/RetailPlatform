
 /***************************************************************************
 *       功能：     PRMProductImage映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     产品图片表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class ProductImageMap : BaseMap<ProductImageEntity,int>
    {
        public ProductImageMap():base("PRM_ProductImage")
        {
            this.MapPkidDefault<ProductImageEntity,int>();
 
            Map(p => p.ProductId);    
            Map(p => p.ImageUrl);    
            Map(p => p.IsDefault);    
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

    
 

