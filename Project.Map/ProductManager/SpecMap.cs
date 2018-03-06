
 /***************************************************************************
 *       功能：     PRMSpec映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     规格表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class SpecMap : BaseMap<SpecEntity,int>
    {
        public SpecMap():base("PRM_Spec")
        {
            this.MapPkidDefault<SpecEntity,int>();
 
            Map(p => p.SpecName);    
            Map(p => p.Remark);    
            Map(p => p.SpecType);    
            Map(p => p.ShowType);

            Map(p => p.SpecTypeName);
            Map(p => p.ShowTypeName);

            HasMany(p => p.SpecValueEntityList)
            .AsSet()
            .LazyLoad()
            .Cascade.All().Inverse()
            .KeyColumn("SpecId");
        }
    }
}

    
 

