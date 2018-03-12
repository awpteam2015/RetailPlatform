
/***************************************************************************
*       功能：     PRMExtAttribute映射类
*       作者：     李伟伟
*       日期：     2017/6/30
*       描述：     扩展属性表
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace Project.Map.ProductManager
{
    public class ExtAttributeMap : BaseMap<ExtAttributeEntity, int>
    {
        public ExtAttributeMap() : base("PRM_ExtAttribute")
        {
            this.MapPkidDefault<ExtAttributeEntity, int>();

            Map(p => p.AttributeName);
           // Map(p => p.OtherName);
            Map(p => p.ShowType);
            Map(p => p.ShowTypeName);
            // Map(p => p.AttributeValues);


            HasMany(p => p.AttributeValueList)
         .AsSet()
         .LazyLoad()
         .Cascade.All().Inverse()
         .KeyColumn("AttributeId");
        }
    }
}




