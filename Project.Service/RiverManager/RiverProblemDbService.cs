
 /***************************************************************************
 *       功能：     RMRiverProblemDb业务处理层
 *       作者：     李伟伟
 *       日期：     2016/8/13
 *       描述：     督办
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.RiverManager;
using Project.Repository.RiverManager;

namespace Project.Service.RiverManager
{
    public class RiverProblemDbService
    {
       
       #region 构造函数
        private readonly RiverProblemDbRepository  _riverProblemDbRepository;
            private static readonly RiverProblemDbService Instance = new RiverProblemDbService();

        public RiverProblemDbService()
        {
           this._riverProblemDbRepository =new RiverProblemDbRepository();
        }
        
         public static  RiverProblemDbService GetInstance()
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
        public System.Int32 Add(RiverProblemDbEntity entity)
        {
            return _riverProblemDbRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _riverProblemDbRepository.GetById(pkId);
            _riverProblemDbRepository.Delete(entity);
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
        public bool Delete(RiverProblemDbEntity entity)
        {
         try
            {
            _riverProblemDbRepository.Delete(entity);
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
        public bool Update(RiverProblemDbEntity entity)
        {
          try
            {
            _riverProblemDbRepository.Update(entity);
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
        public RiverProblemDbEntity GetModelByPk(System.Int32 pkId)
        {
            return _riverProblemDbRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【督办】和总【督办】数</returns>
        public System.Tuple<IList<RiverProblemDbEntity>, int> Search(RiverProblemDbEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<RiverProblemDbEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.DbRemark))
            //  expr = expr.And(p => p.DbRemark == where.DbRemark);
            // if (!string.IsNullOrEmpty(where.UserCode))
            //  expr = expr.And(p => p.UserCode == where.UserCode);
            // if (!string.IsNullOrEmpty(where.UserName))
            //  expr = expr.And(p => p.UserName == where.UserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            if (where.RiverProblemApplyId>0)
                expr = expr.And(p => p.RiverProblemApplyId == where.RiverProblemApplyId);
            #endregion
            var list = _riverProblemDbRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _riverProblemDbRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RiverProblemDbEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RiverProblemDbEntity> GetList(RiverProblemDbEntity where)
        {
               var expr = PredicateBuilder.True<RiverProblemDbEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.DbRemark))
            //  expr = expr.And(p => p.DbRemark == where.DbRemark);
            // if (!string.IsNullOrEmpty(where.UserCode))
            //  expr = expr.And(p => p.UserCode == where.UserCode);
            // if (!string.IsNullOrEmpty(where.UserName))
            //  expr = expr.And(p => p.UserName == where.UserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
     
                expr = expr.And(p => p.RiverProblemApplyId == where.RiverProblemApplyId);
            #endregion
            var list = _riverProblemDbRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

