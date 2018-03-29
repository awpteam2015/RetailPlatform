using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.ProductManager.Response
{
    public class GoodsSearchView
    {

        public virtual int PkId { get; set; }

        #region
        public virtual System.String ProductName { get; set; }
        /// <summary>
        /// 系统类型
        /// </summary>
        public virtual System.Int32 SystemCategoryId { get; set; }

        public virtual System.String SystemCategoryName { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public virtual System.Int32 ProductCategoryId { get; set; }

        public virtual System.String ProductCategoryName { get; set; }


        public virtual System.String ProductCode { get; set; }

        public virtual System.Int32 IsCommand { get; set; }

        #endregion

        #region
        /// <summary>
        /// 组合规格的sku编码
        /// </summary>
        public virtual string CombineId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32 ProductId { get; set; }
        /// <summary>
        /// 货号
        /// </summary>
        public virtual System.String GoodsCode { get; set; }

        /// <summary>
        /// Sku编码
        /// </summary>
        public virtual System.String SkuCode { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public virtual System.Int32 GoodsStock { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public virtual System.Decimal GoodsPrice { get; set; }

        /// <summary>
        /// 是否是默认商品
        /// </summary>
        public virtual System.Int32 IsDefault { get; set; }

        /// <summary>
        /// 规格值明细
        /// </summary>
        public virtual System.String SpecDetail { get; set; }

        public virtual System.String SpecValueJson { get; set; }
        #endregion
    }
}
