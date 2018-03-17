
 /***************************************************************************
 *       功能：     PRMBrand映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     品牌表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class BrandMap : BaseMap<BrandEntity,int>
    {
        public BrandMap():base("PRM_Brand")
        {
            this.MapPkidDefault<BrandEntity,int>();
 
            Map(p => p.BrandName);    
            Map(p => p.Sort);    
            Map(p => p.ImageUrl);    
            Map(p => p.Description);    
      
        }
    }
}

    
 

