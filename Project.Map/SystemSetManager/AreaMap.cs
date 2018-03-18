
 /***************************************************************************
 *       功能：     SMArea映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SystemSetManager;

namespace  Project.Map.SystemSetManager
{
    public class AreaMap : BaseMap<AreaEntity,int>
    {
        public AreaMap():base("SM_Area")
        {
            this.MapPkidDefault<AreaEntity,int>();
 
            Map(p => p.AreaId);    
            Map(p => p.Area);    
            Map(p => p.CityId);    
            Map(p => p.FirstWeightPrice);    
            Map(p => p.SecondWeightPrice);    
        }
    }
}

    
 

