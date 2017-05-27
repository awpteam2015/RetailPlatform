
 /***************************************************************************
 *       功能：     PRMProduct业务处理层
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     产品表
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class ProductService
    {
       
       #region 构造函数
        private readonly ProductRepository  _productRepository;
            private static readonly ProductService Instance = new ProductService();

        public ProductService()
        {
           this._productRepository =new ProductRepository();
        }
        
         public static  ProductService GetInstance()
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
        public System.Int32 Add(ProductEntity entity)
        {
            return _productRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _productRepository.GetById(pkId);
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
        public bool Update(ProductEntity entity)
        {
          try
            {
            _productRepository.Update(entity);
         return true;
        }
        catch
        {
         return false;
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
              // if (!string.IsNullOrEmpty(where.ProductName))
              //  expr = expr.And(p => p.ProductName == where.ProductName);
              // if (!string.IsNullOrEmpty(where.ProductCode))
              //  expr = expr.And(p => p.ProductCode == where.ProductCode);
              // if (!string.IsNullOrEmpty(where.Price))
              //  expr = expr.And(p => p.Price == where.Price);
              // if (!string.IsNullOrEmpty(where.ProductCategoryId))
              //  expr = expr.And(p => p.ProductCategoryId == where.ProductCategoryId);
              // if (!string.IsNullOrEmpty(where.SpecId1))
              //  expr = expr.And(p => p.SpecId1 == where.SpecId1);
              // if (!string.IsNullOrEmpty(where.SpecId2))
              //  expr = expr.And(p => p.SpecId2 == where.SpecId2);
              // if (!string.IsNullOrEmpty(where.SpecId3))
              //  expr = expr.And(p => p.SpecId3 == where.SpecId3);
              // if (!string.IsNullOrEmpty(where.SpecName1))
              //  expr = expr.And(p => p.SpecName1 == where.SpecName1);
              // if (!string.IsNullOrEmpty(where.SpecName2))
              //  expr = expr.And(p => p.SpecName2 == where.SpecName2);
              // if (!string.IsNullOrEmpty(where.SpecName3))
              //  expr = expr.And(p => p.SpecName3 == where.SpecName3);
              // if (!string.IsNullOrEmpty(where.Attribute1))
              //  expr = expr.And(p => p.Attribute1 == where.Attribute1);
              // if (!string.IsNullOrEmpty(where.Attribute2))
              //  expr = expr.And(p => p.Attribute2 == where.Attribute2);
              // if (!string.IsNullOrEmpty(where.Attribute3))
              //  expr = expr.And(p => p.Attribute3 == where.Attribute3);
              // if (!string.IsNullOrEmpty(where.PicUrl1))
              //  expr = expr.And(p => p.PicUrl1 == where.PicUrl1);
              // if (!string.IsNullOrEmpty(where.PicUrl2))
              //  expr = expr.And(p => p.PicUrl2 == where.PicUrl2);
              // if (!string.IsNullOrEmpty(where.PicUrl3))
              //  expr = expr.And(p => p.PicUrl3 == where.PicUrl3);
 #endregion
            var list = _productRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _productRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ProductEntity>, int>(list, count);
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
              // if (!string.IsNullOrEmpty(where.ProductCode))
              //  expr = expr.And(p => p.ProductCode == where.ProductCode);
              // if (!string.IsNullOrEmpty(where.Price))
              //  expr = expr.And(p => p.Price == where.Price);
              // if (!string.IsNullOrEmpty(where.ProductCategoryId))
              //  expr = expr.And(p => p.ProductCategoryId == where.ProductCategoryId);
              // if (!string.IsNullOrEmpty(where.SpecId1))
              //  expr = expr.And(p => p.SpecId1 == where.SpecId1);
              // if (!string.IsNullOrEmpty(where.SpecId2))
              //  expr = expr.And(p => p.SpecId2 == where.SpecId2);
              // if (!string.IsNullOrEmpty(where.SpecId3))
              //  expr = expr.And(p => p.SpecId3 == where.SpecId3);
              // if (!string.IsNullOrEmpty(where.SpecName1))
              //  expr = expr.And(p => p.SpecName1 == where.SpecName1);
              // if (!string.IsNullOrEmpty(where.SpecName2))
              //  expr = expr.And(p => p.SpecName2 == where.SpecName2);
              // if (!string.IsNullOrEmpty(where.SpecName3))
              //  expr = expr.And(p => p.SpecName3 == where.SpecName3);
              // if (!string.IsNullOrEmpty(where.Attribute1))
              //  expr = expr.And(p => p.Attribute1 == where.Attribute1);
              // if (!string.IsNullOrEmpty(where.Attribute2))
              //  expr = expr.And(p => p.Attribute2 == where.Attribute2);
              // if (!string.IsNullOrEmpty(where.Attribute3))
              //  expr = expr.And(p => p.Attribute3 == where.Attribute3);
              // if (!string.IsNullOrEmpty(where.PicUrl1))
              //  expr = expr.And(p => p.PicUrl1 == where.PicUrl1);
              // if (!string.IsNullOrEmpty(where.PicUrl2))
              //  expr = expr.And(p => p.PicUrl2 == where.PicUrl2);
              // if (!string.IsNullOrEmpty(where.PicUrl3))
              //  expr = expr.And(p => p.PicUrl3 == where.PicUrl3);
 #endregion
            var list = _productRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

