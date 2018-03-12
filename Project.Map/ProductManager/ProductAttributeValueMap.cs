
 /***************************************************************************
 *       功能：     PRMProductAttributeValue映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     产品--属性值关联表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class ProductAttributeValueMap : BaseMap<ProductAttributeValueEntity,int>
    {
        public ProductAttributeValueMap():base("PRM_ProductAttributeValue")
        {
            this.MapPkidDefault<ProductAttributeValueEntity,int>();
 
            Map(p => p.AttributeValueId);    
            Map(p => p.AttributeValueName);    
            Map(p => p.AttributeId);    
            Map(p => p.ProductId);    
            Map(p => p.ValueContent);    


        }
    }
}

    
 

