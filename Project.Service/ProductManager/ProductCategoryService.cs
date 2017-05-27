
 /***************************************************************************
 *       功能：     PRMProductCategory业务处理层
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     商品分类
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class ProductCategoryService
    {
       
       #region 构造函数
        private readonly ProductCategoryRepository  _productCategoryRepository;
            private static readonly ProductCategoryService Instance = new ProductCategoryService();

        public ProductCategoryService()
        {
           this._productCategoryRepository =new ProductCategoryRepository();
        }
        
         public static  ProductCategoryService GetInstance()
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
        public System.Int32 Add(ProductCategoryEntity entity)
        {
            return _productCategoryRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _productCategoryRepository.GetById(pkId);
            _productCategoryRepository.Delete(entity);
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
        public bool Delete(ProductCategoryEntity entity)
        {
         try
            {
            _productCategoryRepository.Delete(entity);
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
        public bool Update(ProductCategoryEntity entity)
        {
          try
            {
            _productCategoryRepository.Update(entity);
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
        public ProductCategoryEntity GetModelByPk(System.Int32 pkId)
        {
            return _productCategoryRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【商品分类】和总【商品分类】数</returns>
        public System.Tuple<IList<ProductCategoryEntity>, int> Search(ProductCategoryEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ProductCategoryEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ProductCategoryName))
              //  expr = expr.And(p => p.ProductCategoryName == where.ProductCategoryName);
              // if (!string.IsNullOrEmpty(where.ParentProductCategoryId))
              //  expr = expr.And(p => p.ParentProductCategoryId == where.ParentProductCategoryId);
              // if (!string.IsNullOrEmpty(where.CategoryRoute))
              //  expr = expr.And(p => p.CategoryRoute == where.CategoryRoute);
              // if (!string.IsNullOrEmpty(where.Rank))
              //  expr = expr.And(p => p.Rank == where.Rank);
 #endregion
            var list = _productCategoryRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _productCategoryRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ProductCategoryEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ProductCategoryEntity> GetList(ProductCategoryEntity where)
        {
               var expr = PredicateBuilder.True<ProductCategoryEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ProductCategoryName))
              //  expr = expr.And(p => p.ProductCategoryName == where.ProductCategoryName);
              // if (!string.IsNullOrEmpty(where.ParentProductCategoryId))
              //  expr = expr.And(p => p.ParentProductCategoryId == where.ParentProductCategoryId);
              // if (!string.IsNullOrEmpty(where.CategoryRoute))
              //  expr = expr.And(p => p.CategoryRoute == where.CategoryRoute);
              // if (!string.IsNullOrEmpty(where.Rank))
              //  expr = expr.And(p => p.Rank == where.Rank);
 #endregion
            var list = _productCategoryRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

