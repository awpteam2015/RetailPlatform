
 /***************************************************************************
 *       功能：     PRMParameterGroupDetail业务处理层
 *       作者：     李伟伟
 *       日期：     2017/7/4
 *       描述：     
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class ParameterGroupDetailService
    {
       
       #region 构造函数
        private readonly ParameterGroupDetailRepository  _parameterGroupDetailRepository;
            private static readonly ParameterGroupDetailService Instance = new ParameterGroupDetailService();

        public ParameterGroupDetailService()
        {
           this._parameterGroupDetailRepository =new ParameterGroupDetailRepository();
        }
        
         public static  ParameterGroupDetailService GetInstance()
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
        public System.Int32 Add(ParameterGroupDetailEntity entity)
        {
            return _parameterGroupDetailRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _parameterGroupDetailRepository.GetById(pkId);
            _parameterGroupDetailRepository.Delete(entity);
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
        public bool Delete(ParameterGroupDetailEntity entity)
        {
         try
            {
            _parameterGroupDetailRepository.Delete(entity);
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
        public bool Update(ParameterGroupDetailEntity entity)
        {
          try
            {
            _parameterGroupDetailRepository.Update(entity);
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
        public ParameterGroupDetailEntity GetModelByPk(System.Int32 pkId)
        {
            return _parameterGroupDetailRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<ParameterGroupDetailEntity>, int> Search(ParameterGroupDetailEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ParameterGroupDetailEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ParameterName))
              //  expr = expr.And(p => p.ParameterName == where.ParameterName);
              // if (!string.IsNullOrEmpty(where.ParameterGroupId))
              //  expr = expr.And(p => p.ParameterGroupId == where.ParameterGroupId);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
 #endregion
            var list = _parameterGroupDetailRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _parameterGroupDetailRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ParameterGroupDetailEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ParameterGroupDetailEntity> GetList(ParameterGroupDetailEntity where)
        {
               var expr = PredicateBuilder.True<ParameterGroupDetailEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ParameterName))
              //  expr = expr.And(p => p.ParameterName == where.ParameterName);
              // if (!string.IsNullOrEmpty(where.ParameterGroupId))
              //  expr = expr.And(p => p.ParameterGroupId == where.ParameterGroupId);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
 #endregion
            var list = _parameterGroupDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

