
 /***************************************************************************
 *       功能：     OMShopCart映射类
 *       作者：     李伟伟
 *       日期：     2018/3/31
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
            Map(p => p.ProductName);    
            Map(p => p.ProductId);    
            Map(p => p.ProductCode);    
            Map(p => p.GoodsCode);    
            Map(p => p.GoodsId);    
            Map(p => p.ImageUrl);    
            Map(p => p.Price);    
            Map(p => p.DiscountMember);    
            Map(p => p.DiscountPromotion);    
            Map(p => p.DiscountPoint);    
            Map(p => p.DiscountAll);    
            Map(p => p.PromotionPrice);    
            Map(p => p.LastPrice);    
            Map(p => p.TotalAmount);    
            Map(p => p.ProductWeight);    
            Map(p => p.SpecDetail);    
            Map(p => p.Num);    
            Map(p => p.IsCheck);    
            Map(p => p.CustomerId);    
            Map(p => p.RuleId);       Map(p => p.IsExpire);   
        }
    }
}

    
 

