
 /***************************************************************************
 *       功能：     PRMGoods映射类
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     商品表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class GoodsMap : BaseMap<GoodsEntity,int>
    {
        public GoodsMap():base("PRM_Goods")
        {
            this.MapPkidDefault<GoodsEntity,int>();
 
            Map(p => p.GoodsCode);    
            Map(p => p.GoodsName);    
            Map(p => p.ProductCode);    
            Map(p => p.SpecValue1);    
            Map(p => p.SpecValue2);    
            Map(p => p.SpecValue3);    
        }
    }
}

    
 

