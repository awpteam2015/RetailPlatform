
 /***************************************************************************
 *       功能：     PRMProductSpec业务处理层
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
    public class ProductSpecService
    {
       
       #region 构造函数
        private readonly ProductSpecRepository  _productSpecRepository;
            private static readonly ProductSpecService Instance = new ProductSpecService();

        public ProductSpecService()
        {
           this._productSpecRepository =new ProductSpecRepository();
        }
        
         public static  ProductSpecService GetInstance()
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
        public System.Int32 Add(ProductSpecEntity entity)
        {
            return _productSpecRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _productSpecRepository.GetById(pkId);
            _productSpecRepository.Delete(entity);
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
        public bool Delete(ProductSpecEntity entity)
        {
         try
            {
            _productSpecRepository.Delete(entity);
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
        public bool Update(ProductSpecEntity entity)
        {
          try
            {
            _productSpecRepository.Update(entity);
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
        public ProductSpecEntity GetModelByPk(System.Int32 pkId)
        {
            return _productSpecRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<ProductSpecEntity>, int> Search(ProductSpecEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ProductSpecEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.SpecId))
              //  expr = expr.And(p => p.SpecId == where.SpecId);
              // if (!string.IsNullOrEmpty(where.SpecType))
              //  expr = expr.And(p => p.SpecType == where.SpecType);
 #endregion
            var list = _productSpecRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _productSpecRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ProductSpecEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ProductSpecEntity> GetList(ProductSpecEntity where)
        {
               var expr = PredicateBuilder.True<ProductSpecEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.SpecId))
              //  expr = expr.And(p => p.SpecId == where.SpecId);
              // if (!string.IsNullOrEmpty(where.SpecType))
              //  expr = expr.And(p => p.SpecType == where.SpecType);
 #endregion
            var list = _productSpecRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

