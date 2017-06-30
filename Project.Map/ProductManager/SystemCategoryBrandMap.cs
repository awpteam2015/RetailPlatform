
 /***************************************************************************
 *       功能：     PRMSystemCategoryBrand映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类对应品牌
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class SystemCategoryBrandMap : BaseMap<SystemCategoryBrandEntity,int>
    {
        public SystemCategoryBrandMap():base("PRM_SystemCategoryBrand")
        {
            this.MapPkidDefault<SystemCategoryBrandEntity,int>();
 
            Map(p => p.BrandId);    
            Map(p => p.SystemCategoryId);    
        }
    }
}

    
 

