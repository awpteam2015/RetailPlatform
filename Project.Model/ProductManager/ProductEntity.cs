

 /***************************************************************************
 *       功能：     PRMProduct实体类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     产品表
 * *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class ProductEntity: Entity
    {
        public ProductEntity()
        {
            GoodsEntityList=new HashSet<GoodsEntity>();
            ProductAttributeValueEntityList=new HashSet<ProductAttributeValueEntity>();
        }

        #region 属性
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual System.String ProductName{get; set;}
        /// <summary>
        /// 系统类型
        /// </summary>
        public virtual System.Int32? SystemCategoryId{get; set;}
        /// <summary>
        /// 商品分类
        /// </summary>
        public virtual System.Int32? ProductCategoryId{get; set;}
        /// <summary>
        /// 商品分类全路由
        /// </summary>
        public virtual System.String ProductCategoryRoute{get; set;}
        /// <summary>
        /// 品牌ID
        /// </summary>
        public virtual System.Int32? BrandId{get; set;}
        /// <summary>
        /// 商品编号
        /// </summary>
        public virtual System.String ProductCode{get; set;}
        /// <summary>
        /// 计量单位
        /// </summary>
        public virtual System.Int32? Unit{get; set;}
        /// <summary>
        /// 简介
        /// </summary>
        public virtual System.String BriefDescription{get; set;}
        /// <summary>
        /// 详细描述
        /// </summary>
        public virtual System.String Description{get; set;}
        /// <summary>
        /// 重量
        /// </summary>
        public virtual System.Int32? Weight{get; set;}
        /// <summary>
        /// 重量单位
        /// </summary>
        public virtual System.String WeightUnit{get; set;}
        /// <summary>
        /// 市场价
        /// </summary>
        public virtual System.Decimal? MarketPrice{get; set;}
        /// <summary>
        /// 销售价
        /// </summary>
        public virtual System.Decimal? SellPrice{get; set;}
        /// <summary>
        /// 成本价
        /// </summary>
        public virtual System.Decimal? Cost{get; set;}
        /// <summary>
        /// 货币单位
        /// </summary>
        public virtual System.Int32? PriceUnit{get; set;}
        /// <summary>
        /// 库存数量
        /// </summary>
        public virtual System.Int32? StockNum{get; set;}
        /// <summary>
        /// 最大数量限制
        /// </summary>
        public virtual System.Int32? BuyMaxNum{get; set;}
        /// <summary>
        /// 最小数量限制
        /// </summary>
        public virtual System.Int32? BuyMinNum{get; set;}
        /// <summary>
        /// 访问次数
        /// </summary>
        public virtual System.Int32? ViewNum{get; set;}
        /// <summary>
        /// 评论次数
        /// </summary>
        public virtual System.Int32? CommentNum{get; set;}
        /// <summary>
        /// 售出数量
        /// </summary>
        public virtual System.Int32? SelledNum{get; set;}
        /// <summary>
        /// 页面标题
        /// </summary>
        public virtual System.String Title{get; set;}
        /// <summary>
        /// 页面关键字
        /// </summary>
        public virtual System.String Meta_Keywords{get; set;}
        /// <summary>
        /// 页面描述
        /// </summary>
        public virtual System.String Meta_Description{get; set;}
        /// <summary>
        /// 是否上架
        /// </summary>
        public virtual System.Byte? IsShow{get; set;}
        /// <summary>
        /// 是否推荐
        /// </summary>
        public virtual System.Byte? IsCommand{get; set;}
        /// <summary>
        /// Pdt_desc
        /// </summary>
        public virtual System.String Pdt_desc{get; set;}
        /// <summary>
        /// Spec_desc
        /// </summary>
        public virtual System.String Spec_desc{get; set;}
        /// <summary>
        /// Params_desc
        /// </summary>
        public virtual System.String Params_desc{get; set;}
        /// <summary>
        /// Tags_desc
        /// </summary>
        public virtual System.String Tags_desc{get; set;}
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime{get; set;}
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode{get; set;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime{get; set;}
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual System.Int32? IsDeleted{get; set;}
        /// <summary>
        /// 删除人
        /// </summary>
        public virtual System.String DeleterUserCode{get; set;}
        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual System.DateTime? DeletionTime{get; set;}
        /// <summary>
        /// 扩展属性1
        /// </summary>
        public virtual System.String p_1{get; set;}
        /// <summary>
        /// 扩展属性2
        /// </summary>
        public virtual System.String p_2{get; set;}
        /// <summary>
        /// 扩展属性1
        /// </summary>
        public virtual System.String p_3{get; set;}
        /// <summary>
        /// 扩展属性2
        /// </summary>
        public virtual System.String p_4{get; set;}
        /// <summary>
        /// 扩展属性1
        /// </summary>
        public virtual System.String p_5{get; set;}
        /// <summary>
        /// 扩展属性2
        /// </summary>
        public virtual System.String p_6{get; set;}
        /// <summary>
        /// 扩展属性1
        /// </summary>
        public virtual System.String p_7{get; set;}
        /// <summary>
        /// 扩展属性2
        /// </summary>
        public virtual System.String p_8{get; set;}
        /// <summary>
        /// 扩展属性1
        /// </summary>
        public virtual System.String p_9{get; set;}
		#endregion
        

        #region 新增属性

        /// <summary>
        /// 商品列表
        /// </summary>
        public virtual ISet<GoodsEntity> GoodsEntityList { get; set; }


        public virtual ISet<GoodsSpecValueEntity> GoodsSpecValueEntityList { get; set; }


        public virtual ISet<ProductAttributeValueEntity> ProductAttributeValueEntityList { get; set; }
        #endregion
    }
}

    
 

