
 /***************************************************************************
 *       功能：     RMMsgNotice映射类
 *       作者：     李伟伟
 *       日期：     2016/7/30
 *       描述：     消息通知
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.RiverManager;

namespace  Project.Map.RiverManager
{
    public class MsgNoticeMap : BaseMap<MsgNoticeEntity,int>
    {
        public MsgNoticeMap():base("RM_MsgNotice")
        {
            this.MapPkidDefault<MsgNoticeEntity,int>();
 
            Map(p => p.Title);    
            Map(p => p.Des);    
            Map(p => p.CreationTime);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.LastModificationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.IsDelete);    
            Map(p => p.IsSend);    
            Map(p => p.SendTime);
            Map(p => p.BelongCompanys);    
        }
    }
}

    
 

