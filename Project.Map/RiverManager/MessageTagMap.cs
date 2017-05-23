
 /***************************************************************************
 *       功能：     RMMessageTag映射类
 *       作者：     李伟伟
 *       日期：     2016/9/11
 *       描述：     消息是否已读标记
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.RiverManager;

namespace  Project.Map.RiverManager
{
    public class MessageTagMap : BaseMap<MessageTagEntity,int>
    {
        public MessageTagMap():base("RM_MessageTag")
        {
            this.MapPkidDefault<MessageTagEntity,int>();
 
            Map(p => p.Kind);    
            Map(p => p.UserCode);    
            Map(p => p.UserName);    
            Map(p => p.MessageId);    
        }
    }
}

    
 

