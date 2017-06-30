
 /***************************************************************************
 *       功能：     PRMProductSystemCategory映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class ProductMap : BaseMap<ProductEntity,int>
    {
        public ProductMap():base("PRM_Product_SystemCategory")
        {
            this.MapPkidDefault<ProductEntity,int>();
 
            Map(p => p.SystemCategoryId);    
            Map(p => p.ProductId);    
            Map(p => p.Rank1);    
        }
    }
}

    
 

