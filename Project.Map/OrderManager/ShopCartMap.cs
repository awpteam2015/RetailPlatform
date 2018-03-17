
 /***************************************************************************
 *       功能：     OMShopCart映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     购物车
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.OrderManager;

namespace  Project.Map.OrderManager
{
    public class ShopCartMap : BaseMap<ShopCartEntity,int>
    {
        public ShopCartMap():base("OM_ShopCart")
        {
            this.MapPkidDefault<ShopCartEntity,int>();
 
            Map(p => p.OrderNo);    
            Map(p => p.ProductCategoryId);    
            Map(p => p.ProductId);    
            Map(p => p.GoodsCode);    
            Map(p => p.GoodsId);    
            Map(p => p.Price);    
            Map(p => p.PriceSubDiscount);    
            Map(p => p.TotalAmount);    
            Map(p => p.ProductWeight);    
            Map(p => p.SpecName);    
            Map(p => p.CustomerId);    
        }
    }
}

    
 

