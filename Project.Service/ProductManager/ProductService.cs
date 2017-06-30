
 /***************************************************************************
 *       功能：     PRMProductSystemCategory业务处理层
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     
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
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<ProductEntity>, int> Search(ProductEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ProductEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.Id))
              //  expr = expr.And(p => p.Id == where.Id);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.Rank1))
              //  expr = expr.And(p => p.Rank1 == where.Rank1);
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
              // if (!string.IsNullOrEmpty(where.Id))
              //  expr = expr.And(p => p.Id == where.Id);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.Rank1))
              //  expr = expr.And(p => p.Rank1 == where.Rank1);
 #endregion
            var list = _productRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

