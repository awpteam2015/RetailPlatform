
 /***************************************************************************
 *       功能：     PRMSystemCategorySpec业务处理层
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类--规格关联表
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class SystemCategorySpecService
    {
       
       #region 构造函数
        private readonly SystemCategorySpecRepository  _systemCategorySpecRepository;
            private static readonly SystemCategorySpecService Instance = new SystemCategorySpecService();

        public SystemCategorySpecService()
        {
           this._systemCategorySpecRepository =new SystemCategorySpecRepository();
        }
        
         public static  SystemCategorySpecService GetInstance()
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
        public System.Int32 Add(SystemCategorySpecEntity entity)
        {
            return _systemCategorySpecRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _systemCategorySpecRepository.GetById(pkId);
            _systemCategorySpecRepository.Delete(entity);
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
        public bool Delete(SystemCategorySpecEntity entity)
        {
         try
            {
            _systemCategorySpecRepository.Delete(entity);
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
        public bool Update(SystemCategorySpecEntity entity)
        {
          try
            {
            _systemCategorySpecRepository.Update(entity);
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
        public SystemCategorySpecEntity GetModelByPk(System.Int32 pkId)
        {
            return _systemCategorySpecRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【系统分类--规格关联表】和总【系统分类--规格关联表】数</returns>
        public System.Tuple<IList<SystemCategorySpecEntity>, int> Search(SystemCategorySpecEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<SystemCategorySpecEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.SpecId))
              //  expr = expr.And(p => p.SpecId == where.SpecId);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
 #endregion
            var list = _systemCategorySpecRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _systemCategorySpecRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<SystemCategorySpecEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<SystemCategorySpecEntity> GetList(SystemCategorySpecEntity where)
        {
               var expr = PredicateBuilder.True<SystemCategorySpecEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.SpecId))
              //  expr = expr.And(p => p.SpecId == where.SpecId);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
 #endregion
            var list = _systemCategorySpecRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

