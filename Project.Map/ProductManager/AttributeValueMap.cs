
 /***************************************************************************
 *       功能：     PRMAttributeValue映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     扩展属性值
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class AttributeValueMap : BaseMap<AttributeValueEntity,int>
    {
        public AttributeValueMap():base("PRM_AttributeValue")
        {
            this.MapPkidDefault<AttributeValueEntity,int>();
 
            Map(p => p.AttributeValueName);    
            Map(p => p.AttributeId);
            Map(p => p.Sort);
        }
    }
}

    
 

