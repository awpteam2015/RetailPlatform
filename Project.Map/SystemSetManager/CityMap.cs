
 /***************************************************************************
 *       功能：     SMCity映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SystemSetManager;

namespace  Project.Map.SystemSetManager
{
    public class CityMap : BaseMap<CityEntity,int>
    {
        public CityMap():base("SM_City")
        {
            this.MapPkidDefault<CityEntity,int>();
 
            Map(p => p.CityId);    
            Map(p => p.City);    
            Map(p => p.ProvinceId);    
        }
    }
}

    
 

