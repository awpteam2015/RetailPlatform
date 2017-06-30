﻿
 /***************************************************************************
 *       功能：     PRMProductSpec映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class ProductSpecMap : BaseMap<ProductSpecEntity,int>
    {
        public ProductSpecMap():base("PRM_ProductSpec")
        {
            this.MapPkidDefault<ProductSpecEntity,int>();
 
            Map(p => p.ProductId);    
            Map(p => p.SpecId);    
            Map(p => p.SpecType);    
        }
    }
}

    
 

