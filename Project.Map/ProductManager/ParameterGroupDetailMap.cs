
 /***************************************************************************
 *       功能：     PRMParameterGroupDetail映射类
 *       作者：     李伟伟
 *       日期：     2017/7/4
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class ParameterGroupDetailMap : BaseMap<ParameterGroupDetailEntity,int>
    {
        public ParameterGroupDetailMap():base("PRM_ParameterGroupDetail")
        {
            this.MapPkidDefault<ParameterGroupDetailEntity,int>();
 
            Map(p => p.ParameterName);    
            Map(p => p.ParameterGroupId);    
            Map(p => p.SystemCategoryId);    
        }
    }
}

    
 

