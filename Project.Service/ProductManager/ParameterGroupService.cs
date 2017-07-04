
 /***************************************************************************
 *       功能：     PRMParameterGroup业务处理层
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
    public class ParameterGroupService
    {
       
       #region 构造函数
        private readonly ParameterGroupRepository  _parameterGroupRepository;
            private static readonly ParameterGroupService Instance = new ParameterGroupService();

        public ParameterGroupService()
        {
           this._parameterGroupRepository =new ParameterGroupRepository();
        }
        
         public static  ParameterGroupService GetInstance()
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
        public System.Int32 Add(ParameterGroupEntity entity)
        {
            return _parameterGroupRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _parameterGroupRepository.GetById(pkId);
            _parameterGroupRepository.Delete(entity);
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
        public bool Delete(ParameterGroupEntity entity)
        {
         try
            {
            _parameterGroupRepository.Delete(entity);
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
        public bool Update(ParameterGroupEntity entity)
        {
          try
            {
            _parameterGroupRepository.Update(entity);
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
        public ParameterGroupEntity GetModelByPk(System.Int32 pkId)
        {
            return _parameterGroupRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<ParameterGroupEntity>, int> Search(ParameterGroupEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ParameterGroupEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ParameterGroupName))
              //  expr = expr.And(p => p.ParameterGroupName == where.ParameterGroupName);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
 #endregion
            var list = _parameterGroupRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _parameterGroupRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ParameterGroupEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ParameterGroupEntity> GetList(ParameterGroupEntity where)
        {
               var expr = PredicateBuilder.True<ParameterGroupEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ParameterGroupName))
              //  expr = expr.And(p => p.ParameterGroupName == where.ParameterGroupName);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
 #endregion
            var list = _parameterGroupRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

