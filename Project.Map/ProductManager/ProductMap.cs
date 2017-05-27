
 /***************************************************************************
 *       功能：     PRMProduct映射类
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     产品表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class ProductMap : BaseMap<ProductEntity,int>
    {
        public ProductMap():base("PRM_Product")
        {
            this.MapPkidDefault<ProductEntity,int>();
 
            Map(p => p.ProductName);    
            Map(p => p.ProductCode);    
            Map(p => p.Price);    
            Map(p => p.ProductCategoryId);    
            Map(p => p.SpecId1);    
            Map(p => p.SpecId2);    
            Map(p => p.SpecId3);    
            Map(p => p.SpecName1);    
            Map(p => p.SpecName2);    
            Map(p => p.SpecName3);    
            Map(p => p.Attribute1);    
            Map(p => p.Attribute2);    
            Map(p => p.Attribute3);    
            Map(p => p.PicUrl1);    
            Map(p => p.PicUrl2);    
            Map(p => p.PicUrl3);    
        }
    }
}

    
 

