
 /***************************************************************************
 *       功能：     SMProvince映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SystemSetManager;

namespace  Project.Map.SystemSetManager
{
    public class ProvinceMap : BaseMap<ProvinceEntity,int>
    {
        public ProvinceMap():base("SM_Province")
        {
            this.MapPkidDefault<ProvinceEntity,int>();
 
            Map(p => p.ProvinceId);    
            Map(p => p.Province);    
        }
    }
}

    
 

