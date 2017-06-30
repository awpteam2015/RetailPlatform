
 /***************************************************************************
 *       功能：     PRMSystemCategorySpec映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类--规格关联表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class SystemCategorySpecMap : BaseMap<SystemCategorySpecEntity,int>
    {
        public SystemCategorySpecMap():base("PRM_SystemCategorySpec")
        {
            this.MapPkidDefault<SystemCategorySpecEntity,int>();
 
            Map(p => p.SpecId);    
            Map(p => p.SystemCategoryId);    
        }
    }
}

    
 

