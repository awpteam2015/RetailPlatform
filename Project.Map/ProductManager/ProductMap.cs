
 /***************************************************************************
 *       功能：     PRMProduct映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     产品表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class ProductMap : BaseMap<ProductEntity,int>
    {
        public ProductMap():base("PRM_Product")
        {
            this.MapPkidDefault<ProductEntity,int>();
 
            Map(p => p.ProductName);    
            Map(p => p.SystemCategoryId);    
            Map(p => p.ProductCategoryId);    
            Map(p => p.ProductCategoryRoute);    
            Map(p => p.BrandId);    
            Map(p => p.ProductCode);    
            Map(p => p.Unit);    
            Map(p => p.BriefDescription);    
            Map(p => p.Description);    
            Map(p => p.Weight);    
            Map(p => p.WeightUnit);    
            Map(p => p.MarketPrice);    
            Map(p => p.SellPrice);    
            Map(p => p.Cost);    
            Map(p => p.PriceUnit);    
            Map(p => p.StockNum);    
            Map(p => p.BuyMaxNum);    
            Map(p => p.BuyMinNum);    
            Map(p => p.ViewNum);    
            Map(p => p.CommentNum);    
            Map(p => p.SelledNum);    
            Map(p => p.Title);    
            Map(p => p.Meta_Keywords);    
            Map(p => p.Meta_Description);    
            Map(p => p.IsShow);    
            Map(p => p.IsCommand);    
            Map(p => p.Pdt_desc);    
            Map(p => p.Spec_desc);    
            Map(p => p.Params_desc);    
            Map(p => p.Tags_desc);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.LastModificationTime);    
            Map(p => p.IsDeleted);    
            Map(p => p.DeleterUserCode);    
            Map(p => p.DeletionTime);    
            Map(p => p.p_1);    
            Map(p => p.p_2);    
            Map(p => p.p_3);    
            Map(p => p.p_4);    
            Map(p => p.p_5);    
            Map(p => p.p_6);    
            Map(p => p.p_7);    
            Map(p => p.p_8);    
            Map(p => p.p_9);    
        }
    }
}

    
 

