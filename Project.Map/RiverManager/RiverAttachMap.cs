
 /***************************************************************************
 *       功能：     RMRiverAttach映射类
 *       作者：     李伟伟
 *       日期：     2016/7/30
 *       描述：     河流水质水纹管理
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.RiverManager;

namespace  Project.Map.RiverManager
{
    public class RiverAttachMap : BaseMap<RiverAttachEntity,int>
    {
        public RiverAttachMap():base("RM_RiverAttach")
        {
            this.MapPkidDefault<RiverAttachEntity,int>();
 
            Map(p => p.RiverId);    
            Map(p => p.RiverName);    
            Map(p => p.RecordTime);    
            Map(p => p.Remark);

            Map(p => p.WaterQualityChange);
            Map(p => p.RiverChief);
            Map(p => p.RiverArea);
            Map(p => p.PointName);
            Map(p => p.RiverFlow);
            Map(p => p.Zb1);
            Map(p => p.Zb2);
            Map(p => p.Zb3);
            Map(p => p.WaterQualityRank);
            Map(p => p.Pointer);
            Map(p => p.Day);

            Map(p => p.Month);
            Map(p => p.Year);
            Map(p => p.IsMainData);

        }
    }
}

    
 

