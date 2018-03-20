
/***************************************************************************
*       功能：     PRMProduct业务处理层
*       作者：     李伟伟
*       日期：     2017/6/30
*       描述：     产品表
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.ProductManager;
using Project.Model.ProductManager.Request;
using Project.Repository.ProductManager;
using Project.Service.ProductManager.Help;

namespace Project.Service.ProductManager
{
    public class ProductService
    {

        #region 构造函数
        private readonly ProductRepository _productRepository;
        private readonly GoodsRepository _goodsRepository;
        private readonly GoodsSpecValueRepository _goodsSpecValueRepository;
        private readonly ProductAttributeValueRepository _productAttributeValueRepository;
        private readonly ProductImageRepository _productImageRepository;

        private static readonly ProductService Instance = new ProductService();

        public ProductService()
        {
            this._productRepository = new ProductRepository();
            this._goodsRepository = new GoodsRepository();
            _goodsSpecValueRepository = new GoodsSpecValueRepository();
            _productAttributeValueRepository = new ProductAttributeValueRepository();
            _productImageRepository = new ProductImageRepository();
        }

        public static ProductService GetInstance()
        {
            return Instance;
        }
        #endregion


        #region 基础方法 
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Tuple<bool, string> Add(ProductEntity entity)
        {
            if (!entity.GoodsEntityList.Any())
            {
                return new Tuple<bool, string>(false, "请选择产品规格来生成组合商品。");
            }
            ProductHelp.GetInstance().CombineProductInfo(entity);

            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var pkId = _productRepository.Save(entity);
                    entity.GoodsEntityList.ToList().ForEach(p =>
                    {
                        p.ProductId = pkId;

                        p.GoodsSpecValueList.ToList().ForEach(x =>
                        {
                            x.GoodsId = p.PkId;
                            x.ProductId = p.ProductId;
                        });

                    });

                    entity.ProductAttributeValueEntityList.ToList().ForEach(p =>
                    {
                        p.ProductId = pkId;
                    });

                    entity.ProductImageEntityList.ToList().ForEach(p =>
                    {
                        p.ProductId = pkId;
                    });


                    tx.Commit();
                    return new Tuple<bool, string>(true, pkId.ToString());
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }



        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _productRepository.GetById(pkId);
                _productRepository.Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(ProductEntity entity)
        {
            try
            {
                _productRepository.Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public Tuple<bool, string> Update(ProductEntity entity)
        {
            if (!entity.GoodsEntityList.Any())
            {
                return new Tuple<bool, string>(false, "请选择产品规格来生成组合商品。");
            }

            var newInfo = entity;
            ProductHelp.GetInstance().CombineProductInfo(newInfo);

            var orgInfo = ProductService.GetInstance().GetModelByPk(entity.PkId);

            orgInfo.SellPrice = newInfo.SellPrice;
            orgInfo.StockNum = newInfo.StockNum;
            orgInfo.ProductCode = newInfo.ProductCode;
            orgInfo.ProductName = newInfo.ProductName;
            orgInfo.BriefDescription = newInfo.BriefDescription;
            orgInfo.Description = newInfo.Description;
            orgInfo.ProductCategoryId = newInfo.ProductCategoryId;
            orgInfo.ProductCategoryName = newInfo.ProductCategoryName;
            orgInfo.SystemCategoryName = newInfo.SystemCategoryName;


            #region 产品属性处理
            var addProductAttributeValueEntityList = entity.ProductAttributeValueEntityList.Where(p => orgInfo.ProductAttributeValueEntityList.All(x => x.AttributeValueId != p.AttributeValueId && x.AttributeId != p.AttributeId)).ToList();
            var updateProductAttributeValueEntityList = orgInfo.ProductAttributeValueEntityList.Where(p => entity.ProductAttributeValueEntityList.Any(x => x.AttributeValueId == p.AttributeValueId && x.AttributeId == p.AttributeId)).ToList();
            var deleteProductAttributeValueEntityList = orgInfo.ProductAttributeValueEntityList.Where(p => entity.ProductAttributeValueEntityList.All(x => x.AttributeValueId != p.AttributeValueId && x.AttributeId != p.AttributeId)).ToList();

            addProductAttributeValueEntityList.ForEach(p =>
            {
                orgInfo.ProductAttributeValueEntityList.Add(p);
            });

            updateProductAttributeValueEntityList.ForEach(p =>
            {
                var newEntity = entity.ProductAttributeValueEntityList.SingleOrDefault(x => x.AttributeValueId == p.AttributeValueId && x.AttributeId == p.AttributeId);
                //p.GoodsPrice = newEntity.GoodsPrice;
                //p.GoodsCode = newEntity.GoodsCode;
                //p.GoodsStock = newEntity.GoodsStock;
                //p.GoodsPrice = newEntity.GoodsPrice;
                //p.SkuCode = newEntity.SkuCode;
            });
            #endregion

            #region sku列表处理
            var addGoodsEntityList = entity.GoodsEntityList.Where(p => orgInfo.GoodsEntityList.All(x => x.CombineId != p.CombineId)).ToList();
            var updateGoodsEntityList = orgInfo.GoodsEntityList.Where(p => entity.GoodsEntityList.Any(x => x.CombineId == p.CombineId)).ToList();
            var deleteGoodsEntityList = orgInfo.GoodsEntityList.Where(p => entity.GoodsEntityList.All(x => x.CombineId != p.CombineId)).ToList();

            addGoodsEntityList.ForEach(p =>
            {
                orgInfo.GoodsEntityList.Add(p);
            });

            updateGoodsEntityList.ForEach(p =>
            {
                var newEntity = entity.GoodsEntityList.SingleOrDefault(x => x.CombineId == p.CombineId);
                p.GoodsPrice = newEntity.GoodsPrice;
                p.GoodsCode = newEntity.GoodsCode;
                p.GoodsStock = newEntity.GoodsStock;
                p.GoodsPrice = newEntity.GoodsPrice;
                p.SkuCode = newEntity.SkuCode;
            });
            #endregion

            #region 商品图片处理
            var addProductImageEntityList = entity.ProductImageEntityList.Where(p => orgInfo.ProductImageEntityList.All(x => x.ImageUrl != p.ImageUrl)).ToList();
            var updateProductImageEntityList = orgInfo.ProductImageEntityList.Where(p => entity.ProductImageEntityList.Any(x => x.ImageUrl == p.ImageUrl)).ToList();
            var deleteProductImageEntityList = orgInfo.ProductImageEntityList.Where(p => entity.ProductImageEntityList.All(x => x.ImageUrl != p.ImageUrl)).ToList();

            addProductImageEntityList.ForEach(p =>
            {
                orgInfo.ProductImageEntityList.Add(p);
            });

            //updateProductImageEntityList.ForEach(p =>
            //{
            //    var newEntity = entity.GoodsEntityList.SingleOrDefault(x => x.CombineId == p.CombineId);
            //    p.GoodsPrice = newEntity.GoodsPrice;
            //    p.GoodsCode = newEntity.GoodsCode;
            //    p.GoodsStock = newEntity.GoodsStock;
            //    p.GoodsPrice = newEntity.GoodsPrice;
            //    p.SkuCode = newEntity.SkuCode;
            //});
            #endregion


            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    _productRepository.Update(orgInfo);

                    #region 产品属性处理
                    orgInfo.ProductAttributeValueEntityList = new HashSet<ProductAttributeValueEntity>(orgInfo.ProductAttributeValueEntityList.Where(p => deleteProductAttributeValueEntityList.All(x => x.PkId != p.PkId)).ToList());

                    orgInfo.ProductAttributeValueEntityList.ForEach(p =>
                    {
                        p.ProductId = orgInfo.PkId;
                    });

                    deleteProductAttributeValueEntityList.ForEach(p =>
                    {
                        _productAttributeValueRepository.Delete(p);
                    });
                    #endregion

                    #region Sku列表处理
                    orgInfo.GoodsEntityList = new HashSet<GoodsEntity>(orgInfo.GoodsEntityList.Where(p => deleteGoodsEntityList.All(x => x.PkId != p.PkId)).ToList());

                    orgInfo.GoodsEntityList.ForEach(p =>
                    {
                        p.ProductId = orgInfo.PkId;
                        _goodsRepository.SaveOrUpdate(p);

                        p.GoodsSpecValueList.ForEach(x =>
                        {
                            x.GoodsId = p.PkId;
                            x.ProductId = p.ProductId;
                        });
                    });

                    deleteGoodsEntityList.ForEach(p =>
                    {
                        _goodsRepository.Delete(p);
                    });
                    #endregion

                    #region 商品处理列表
                    orgInfo.ProductImageEntityList = new HashSet<ProductImageEntity>(orgInfo.ProductImageEntityList.Where(p => deleteProductImageEntityList.All(x => x.PkId != p.PkId)).ToList());

                    orgInfo.ProductImageEntityList.ForEach(p =>
                    {
                        p.ProductId = orgInfo.PkId;
                    });

                    deleteProductImageEntityList.ForEach(p =>
                    {
                        _productImageRepository.Delete(p);
                    });
                    #endregion

                    tx.Commit();
                    return new Tuple<bool, string>(true, "");
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public ProductEntity GetModelByPk(System.Int32 pkId)
        {
            return _productRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【产品表】和总【产品表】数</returns>
        public System.Tuple<IList<ProductEntity>, int> Search(ProductEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<ProductEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.ProductName))
                expr = expr.And(p => p.ProductName == where.ProductName);
            if (where.SystemCategoryId > 0)
                expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
            if (where.ProductCategoryId > 0)
                expr = expr.And(p => p.ProductCategoryId == where.ProductCategoryId);
            // if (!string.IsNullOrEmpty(where.ProductCategoryRoute))
            //  expr = expr.And(p => p.ProductCategoryRoute == where.ProductCategoryRoute);
            // if (!string.IsNullOrEmpty(where.BrandId))
            //  expr = expr.And(p => p.BrandId == where.BrandId);
            // if (!string.IsNullOrEmpty(where.ProductCode))
            //  expr = expr.And(p => p.ProductCode == where.ProductCode);
            // if (!string.IsNullOrEmpty(where.Unit))
            //  expr = expr.And(p => p.Unit == where.Unit);
            // if (!string.IsNullOrEmpty(where.BriefDescription))
            //  expr = expr.And(p => p.BriefDescription == where.BriefDescription);
            // if (!string.IsNullOrEmpty(where.Description))
            //  expr = expr.And(p => p.Description == where.Description);
            // if (!string.IsNullOrEmpty(where.Weight))
            //  expr = expr.And(p => p.Weight == where.Weight);
            // if (!string.IsNullOrEmpty(where.WeightUnit))
            //  expr = expr.And(p => p.WeightUnit == where.WeightUnit);
            // if (!string.IsNullOrEmpty(where.MarketPrice))
            //  expr = expr.And(p => p.MarketPrice == where.MarketPrice);
            // if (!string.IsNullOrEmpty(where.SellPrice))
            //  expr = expr.And(p => p.SellPrice == where.SellPrice);
            // if (!string.IsNullOrEmpty(where.Cost))
            //  expr = expr.And(p => p.Cost == where.Cost);
            // if (!string.IsNullOrEmpty(where.PriceUnit))
            //  expr = expr.And(p => p.PriceUnit == where.PriceUnit);
            // if (!string.IsNullOrEmpty(where.StockNum))
            //  expr = expr.And(p => p.StockNum == where.StockNum);
            // if (!string.IsNullOrEmpty(where.BuyMaxNum))
            //  expr = expr.And(p => p.BuyMaxNum == where.BuyMaxNum);
            // if (!string.IsNullOrEmpty(where.BuyMinNum))
            //  expr = expr.And(p => p.BuyMinNum == where.BuyMinNum);
            // if (!string.IsNullOrEmpty(where.ViewNum))
            //  expr = expr.And(p => p.ViewNum == where.ViewNum);
            // if (!string.IsNullOrEmpty(where.CommentNum))
            //  expr = expr.And(p => p.CommentNum == where.CommentNum);
            // if (!string.IsNullOrEmpty(where.SelledNum))
            //  expr = expr.And(p => p.SelledNum == where.SelledNum);
            // if (!string.IsNullOrEmpty(where.Title))
            //  expr = expr.And(p => p.Title == where.Title);
            // if (!string.IsNullOrEmpty(where.MetaKeywords))
            //  expr = expr.And(p => p.MetaKeywords == where.MetaKeywords);
            // if (!string.IsNullOrEmpty(where.MetaDescription))
            //  expr = expr.And(p => p.MetaDescription == where.MetaDescription);
            // if (!string.IsNullOrEmpty(where.IsShow))
            //  expr = expr.And(p => p.IsShow == where.IsShow);
            // if (!string.IsNullOrEmpty(where.IsCommand))
            //  expr = expr.And(p => p.IsCommand == where.IsCommand);
            // if (!string.IsNullOrEmpty(where.PdtDesc))
            //  expr = expr.And(p => p.PdtDesc == where.PdtDesc);
            // if (!string.IsNullOrEmpty(where.SpecDesc))
            //  expr = expr.And(p => p.SpecDesc == where.SpecDesc);
            // if (!string.IsNullOrEmpty(where.ParamsDesc))
            //  expr = expr.And(p => p.ParamsDesc == where.ParamsDesc);
            // if (!string.IsNullOrEmpty(where.TagsDesc))
            //  expr = expr.And(p => p.TagsDesc == where.TagsDesc);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.DeleterUserCode))
            //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
            // if (!string.IsNullOrEmpty(where.DeletionTime))
            //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
            // if (!string.IsNullOrEmpty(where.P1))
            //  expr = expr.And(p => p.P1 == where.P1);
            // if (!string.IsNullOrEmpty(where.P2))
            //  expr = expr.And(p => p.P2 == where.P2);
            // if (!string.IsNullOrEmpty(where.P3))
            //  expr = expr.And(p => p.P3 == where.P3);
            // if (!string.IsNullOrEmpty(where.P4))
            //  expr = expr.And(p => p.P4 == where.P4);
            // if (!string.IsNullOrEmpty(where.P5))
            //  expr = expr.And(p => p.P5 == where.P5);
            // if (!string.IsNullOrEmpty(where.P6))
            //  expr = expr.And(p => p.P6 == where.P6);
            // if (!string.IsNullOrEmpty(where.P7))
            //  expr = expr.And(p => p.P7 == where.P7);
            // if (!string.IsNullOrEmpty(where.P8))
            //  expr = expr.And(p => p.P8 == where.P8);
            // if (!string.IsNullOrEmpty(where.P9))
            //  expr = expr.And(p => p.P9 == where.P9);
            #endregion
            var list = _productRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _productRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ProductEntity>, int>(list, count);
        }


        public System.Tuple<IList<ProductEntity>, int> SearchFront(ProductSearch where)
        {
            var list = _productRepository.Search(where);
            return new Tuple<IList<ProductEntity>, int>(list, 100);
        }


        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ProductEntity> GetList(ProductEntity where)
        {
            var expr = PredicateBuilder.True<ProductEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.ProductName))
            //  expr = expr.And(p => p.ProductName == where.ProductName);
            // if (!string.IsNullOrEmpty(where.SystemCategoryId))
            //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
            // if (!string.IsNullOrEmpty(where.ProductCategoryId))
            //  expr = expr.And(p => p.ProductCategoryId == where.ProductCategoryId);
            // if (!string.IsNullOrEmpty(where.ProductCategoryRoute))
            //  expr = expr.And(p => p.ProductCategoryRoute == where.ProductCategoryRoute);
            // if (!string.IsNullOrEmpty(where.BrandId))
            //  expr = expr.And(p => p.BrandId == where.BrandId);
            // if (!string.IsNullOrEmpty(where.ProductCode))
            //  expr = expr.And(p => p.ProductCode == where.ProductCode);
            // if (!string.IsNullOrEmpty(where.Unit))
            //  expr = expr.And(p => p.Unit == where.Unit);
            // if (!string.IsNullOrEmpty(where.BriefDescription))
            //  expr = expr.And(p => p.BriefDescription == where.BriefDescription);
            // if (!string.IsNullOrEmpty(where.Description))
            //  expr = expr.And(p => p.Description == where.Description);
            // if (!string.IsNullOrEmpty(where.Weight))
            //  expr = expr.And(p => p.Weight == where.Weight);
            // if (!string.IsNullOrEmpty(where.WeightUnit))
            //  expr = expr.And(p => p.WeightUnit == where.WeightUnit);
            // if (!string.IsNullOrEmpty(where.MarketPrice))
            //  expr = expr.And(p => p.MarketPrice == where.MarketPrice);
            // if (!string.IsNullOrEmpty(where.SellPrice))
            //  expr = expr.And(p => p.SellPrice == where.SellPrice);
            // if (!string.IsNullOrEmpty(where.Cost))
            //  expr = expr.And(p => p.Cost == where.Cost);
            // if (!string.IsNullOrEmpty(where.PriceUnit))
            //  expr = expr.And(p => p.PriceUnit == where.PriceUnit);
            // if (!string.IsNullOrEmpty(where.StockNum))
            //  expr = expr.And(p => p.StockNum == where.StockNum);
            // if (!string.IsNullOrEmpty(where.BuyMaxNum))
            //  expr = expr.And(p => p.BuyMaxNum == where.BuyMaxNum);
            // if (!string.IsNullOrEmpty(where.BuyMinNum))
            //  expr = expr.And(p => p.BuyMinNum == where.BuyMinNum);
            // if (!string.IsNullOrEmpty(where.ViewNum))
            //  expr = expr.And(p => p.ViewNum == where.ViewNum);
            // if (!string.IsNullOrEmpty(where.CommentNum))
            //  expr = expr.And(p => p.CommentNum == where.CommentNum);
            // if (!string.IsNullOrEmpty(where.SelledNum))
            //  expr = expr.And(p => p.SelledNum == where.SelledNum);
            // if (!string.IsNullOrEmpty(where.Title))
            //  expr = expr.And(p => p.Title == where.Title);
            // if (!string.IsNullOrEmpty(where.MetaKeywords))
            //  expr = expr.And(p => p.MetaKeywords == where.MetaKeywords);
            // if (!string.IsNullOrEmpty(where.MetaDescription))
            //  expr = expr.And(p => p.MetaDescription == where.MetaDescription);
            // if (!string.IsNullOrEmpty(where.IsShow))
            //  expr = expr.And(p => p.IsShow == where.IsShow);
            // if (!string.IsNullOrEmpty(where.IsCommand))
            //  expr = expr.And(p => p.IsCommand == where.IsCommand);
            // if (!string.IsNullOrEmpty(where.PdtDesc))
            //  expr = expr.And(p => p.PdtDesc == where.PdtDesc);
            // if (!string.IsNullOrEmpty(where.SpecDesc))
            //  expr = expr.And(p => p.SpecDesc == where.SpecDesc);
            // if (!string.IsNullOrEmpty(where.ParamsDesc))
            //  expr = expr.And(p => p.ParamsDesc == where.ParamsDesc);
            // if (!string.IsNullOrEmpty(where.TagsDesc))
            //  expr = expr.And(p => p.TagsDesc == where.TagsDesc);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.DeleterUserCode))
            //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
            // if (!string.IsNullOrEmpty(where.DeletionTime))
            //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
            // if (!string.IsNullOrEmpty(where.P1))
            //  expr = expr.And(p => p.P1 == where.P1);
            // if (!string.IsNullOrEmpty(where.P2))
            //  expr = expr.And(p => p.P2 == where.P2);
            // if (!string.IsNullOrEmpty(where.P3))
            //  expr = expr.And(p => p.P3 == where.P3);
            // if (!string.IsNullOrEmpty(where.P4))
            //  expr = expr.And(p => p.P4 == where.P4);
            // if (!string.IsNullOrEmpty(where.P5))
            //  expr = expr.And(p => p.P5 == where.P5);
            // if (!string.IsNullOrEmpty(where.P6))
            //  expr = expr.And(p => p.P6 == where.P6);
            // if (!string.IsNullOrEmpty(where.P7))
            //  expr = expr.And(p => p.P7 == where.P7);
            // if (!string.IsNullOrEmpty(where.P8))
            //  expr = expr.And(p => p.P8 == where.P8);
            // if (!string.IsNullOrEmpty(where.P9))
            //  expr = expr.And(p => p.P9 == where.P9);
            #endregion
            var list = _productRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}




