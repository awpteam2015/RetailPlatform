
 /***************************************************************************
 *       功能：     PRMSpec映射类
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     规格
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
        }
    }
}

    
 

