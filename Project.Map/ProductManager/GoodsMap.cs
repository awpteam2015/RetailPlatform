
 /***************************************************************************
 *       功能：     PRMGoods映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
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
 
            Map(p => p.ProductId);    
            Map(p => p.GoodsCode);    
            Map(p => p.GoodsStock);    
            Map(p => p.GoodsPrice);    
            Map(p => p.GoodsCost);    
            Map(p => p.GoodsWeight);    
            Map(p => p.GoodsWeightUnit);    
            Map(p => p.Unit);    
            Map(p => p.Title);    
            Map(p => p.IsDefault);    
        }
    }
}

    
 

