using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model.ProductManager;

namespace Project.Service.ProductManager.Validate
{
   public class ProductValidate
    {
       public Tuple<bool, string> ProductPublishValidate(ProductEntity entity)
       {
            if (!entity.GoodsEntityList.Any())
            {
                return new Tuple<bool, string>(false, "请选择产品规格来生成组合商品。");
            }

            if (entity.GoodsEntityList.Any(p => p.GoodsPrice == 0))
            {
                return new Tuple<bool, string>(false, "SKU表的商品价格必须大于0。");
            }

            if (entity.GoodsEntityList.Any(p => p.GoodsStock == 0))
            {
                return new Tuple<bool, string>(false, "SKU表的商品数量必须大于0。");
            }

           if (!entity.ProductImageEntityList.Any(p=>p.IsDefault==1))
           {
                return new Tuple<bool, string>(false, "商品第一张图片必须上传。");
            }

            if (entity.GoodsEntityList.Any(p =>string.IsNullOrWhiteSpace(p.GoodsCode) ))
            {
                return new Tuple<bool, string>(false, "SKU表的商品编码必填。");
            }

            return new Tuple<bool, string>(true, "");
        }
    }
}
