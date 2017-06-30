
 /***************************************************************************
 *       功能：     PRMSystemCategoryBrand业务处理层
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类对应品牌
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class SystemCategoryBrandService
    {
       
       #region 构造函数
        private readonly SystemCategoryBrandRepository  _systemCategoryBrandRepository;
            private static readonly SystemCategoryBrandService Instance = new SystemCategoryBrandService();

        public SystemCategoryBrandService()
        {
           this._systemCategoryBrandRepository =new SystemCategoryBrandRepository();
        }
        
         public static  SystemCategoryBrandService GetInstance()
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
        public System.Int32 Add(SystemCategoryBrandEntity entity)
        {
            return _systemCategoryBrandRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _systemCategoryBrandRepository.GetById(pkId);
            _systemCategoryBrandRepository.Delete(entity);
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
        public bool Delete(SystemCategoryBrandEntity entity)
        {
         try
            {
            _systemCategoryBrandRepository.Delete(entity);
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
        public bool Update(SystemCategoryBrandEntity entity)
        {
          try
            {
            _systemCategoryBrandRepository.Update(entity);
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
        public SystemCategoryBrandEntity GetModelByPk(System.Int32 pkId)
        {
            return _systemCategoryBrandRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【系统分类对应品牌】和总【系统分类对应品牌】数</returns>
        public System.Tuple<IList<SystemCategoryBrandEntity>, int> Search(SystemCategoryBrandEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<SystemCategoryBrandEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.BrandId))
              //  expr = expr.And(p => p.BrandId == where.BrandId);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
 #endregion
            var list = _systemCategoryBrandRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _systemCategoryBrandRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<SystemCategoryBrandEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<SystemCategoryBrandEntity> GetList(SystemCategoryBrandEntity where)
        {
               var expr = PredicateBuilder.True<SystemCategoryBrandEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.BrandId))
              //  expr = expr.And(p => p.BrandId == where.BrandId);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
 #endregion
            var list = _systemCategoryBrandRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

