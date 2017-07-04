
 /***************************************************************************
 *       功能：     PRMParameterGroup映射类
 *       作者：     李伟伟
 *       日期：     2017/7/4
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class ParameterGroupMap : BaseMap<ParameterGroupEntity,int>
    {
        public ParameterGroupMap():base("PRM_ParameterGroup")
        {
            this.MapPkidDefault<ParameterGroupEntity,int>();
 
            Map(p => p.ParameterGroupName);    
            Map(p => p.SystemCategoryId);    
        }
    }
}

    
 

