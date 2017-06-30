
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
    public class SystemCategoryMap : BaseMap<SystemCategoryEntity,int>
    {
        public SystemCategoryMap():base("PRM_SystemCategory_Spec")
        {
            this.MapPkidDefault<SystemCategoryEntity,int>();
 
            Map(p => p.SpecId);    
            Map(p => p.SystemCategoryId);    
        }
    }
}

    
 

