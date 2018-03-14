
 /***************************************************************************
 *       功能：     PRMSpecValue映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     规格值
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class SpecValueMap : BaseMap<SpecValueEntity,int>
    {
        public SpecValueMap():base("PRM_SpecValue")
        {
            this.MapPkidDefault<SpecValueEntity,int>();
 
            Map(p => p.SpecId);    
            Map(p => p.SpecValueName);    
            Map(p => p.Sort);    
            Map(p => p.ImageUrl);    
        }
    }
}

    
 

