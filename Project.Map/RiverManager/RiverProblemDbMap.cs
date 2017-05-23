
 /***************************************************************************
 *       功能：     RMRiverProblemDb映射类
 *       作者：     李伟伟
 *       日期：     2016/8/13
 *       描述：     督办
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.RiverManager;

namespace  Project.Map.RiverManager
{
    public class RiverProblemDbMap : BaseMap<RiverProblemDbEntity,int>
    {
        public RiverProblemDbMap():base("RM_RiverProblemDb")
        {
            this.MapPkidDefault<RiverProblemDbEntity,int>();
 
            Map(p => p.DbRemark);    
            Map(p => p.UserCode);    
            Map(p => p.UserName);    
            Map(p => p.CreateTime);    
            Map(p => p.RiverProblemApplyId);    
        }
    }
}

    
 

