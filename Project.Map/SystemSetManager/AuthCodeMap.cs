
 /***************************************************************************
 *       功能：     SMAuthCode映射类
 *       作者：     李伟伟
 *       日期：     2018/3/20
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SystemSetManager;

namespace  Project.Map.SystemSetManager
{
    public class AuthCodeMap : BaseMap<AuthCodeEntity,int>
    {
        public AuthCodeMap():base("SM_AuthCode")
        {
            this.MapPkidDefault<AuthCodeEntity,int>();
 
            Map(p => p.AuthType);    
            Map(p => p.SendType);    
            Map(p => p.ReciviceUser);    
            Map(p => p.AuthCode);    
            Map(p => p.CreateDate);
            Map(p => p.EndDate);    
        }
    }
}

    
 

