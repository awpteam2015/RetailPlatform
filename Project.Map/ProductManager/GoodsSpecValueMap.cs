
 /***************************************************************************
 *       功能：     PRMGoodsSpecValue映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     商品-规格值关联表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class GoodsSpecValueMap : BaseMap<GoodsSpecValueEntity,int>
    {
        public GoodsSpecValueMap():base("PRM_GoodsSpecValue")
        {
            this.MapPkidDefault<GoodsSpecValueEntity,int>();
 
            Map(p => p.GoodsId);    
            Map(p => p.ProductId);    
            Map(p => p.SpecId);    
            Map(p => p.SpecName);    
            Map(p => p.SpecValueId);    
            Map(p => p.SpecValueName);    
        }
    }
}

    
 

