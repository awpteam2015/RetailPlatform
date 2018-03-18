
 /***************************************************************************
 *       功能：     CNMOfflineActivity业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ContentManager;
using Project.Repository.ContentManager;

namespace Project.Service.ContentManager
{
    public class OfflineActivityService
    {
       
       #region 构造函数
        private readonly OfflineActivityRepository  _offlineActivityRepository;
            private static readonly OfflineActivityService Instance = new OfflineActivityService();

        public OfflineActivityService()
        {
           this._offlineActivityRepository =new OfflineActivityRepository();
        }
        
         public static  OfflineActivityService GetInstance()
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
        public System.Int32 Add(OfflineActivityEntity entity)
        {
            return _offlineActivityRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _offlineActivityRepository.GetById(pkId);
            _offlineActivityRepository.Delete(entity);
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
        public bool Delete(OfflineActivityEntity entity)
        {
         try
            {
            _offlineActivityRepository.Delete(entity);
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
        public bool Update(OfflineActivityEntity entity)
        {
          try
            {
            _offlineActivityRepository.Merge(entity);
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
        public OfflineActivityEntity GetModelByPk(System.Int32 pkId)
        {
            return _offlineActivityRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<OfflineActivityEntity>, int> Search(OfflineActivityEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<OfflineActivityEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.Tttle))
                expr = expr.And(p => p.Tttle == where.Tttle);
            // if (!string.IsNullOrEmpty(where.OfflineActivityAddress))
            //  expr = expr.And(p => p.OfflineActivityAddress == where.OfflineActivityAddress);
            if (where.StartDate!=null)
                expr = expr.And(p => p.StartDate >= where.StartDate);
            if (where.EndDate!=null)
                expr = expr.And(p => p.EndDate <= where.EndDate);
            // if (!string.IsNullOrEmpty(where.ImageUrl))
            //  expr = expr.And(p => p.ImageUrl == where.ImageUrl);
            // if (!string.IsNullOrEmpty(where.BriefDescription))
            //  expr = expr.And(p => p.BriefDescription == where.BriefDescription);
            // if (!string.IsNullOrEmpty(where.State))
            //  expr = expr.And(p => p.State == where.State);
            // if (!string.IsNullOrEmpty(where.DeletionTime))
            //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
            // if (!string.IsNullOrEmpty(where.DeleterUserCode))
            //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            #endregion
            var list = _offlineActivityRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _offlineActivityRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<OfflineActivityEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<OfflineActivityEntity> GetList(OfflineActivityEntity where)
        {
               var expr = PredicateBuilder.True<OfflineActivityEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.Tttle))
              //  expr = expr.And(p => p.Tttle == where.Tttle);
              // if (!string.IsNullOrEmpty(where.OfflineActivityAddress))
              //  expr = expr.And(p => p.OfflineActivityAddress == where.OfflineActivityAddress);
              // if (!string.IsNullOrEmpty(where.StartDate))
              //  expr = expr.And(p => p.StartDate == where.StartDate);
              // if (!string.IsNullOrEmpty(where.EndDate))
              //  expr = expr.And(p => p.EndDate == where.EndDate);
              // if (!string.IsNullOrEmpty(where.ImageUrl))
              //  expr = expr.And(p => p.ImageUrl == where.ImageUrl);
              // if (!string.IsNullOrEmpty(where.BriefDescription))
              //  expr = expr.And(p => p.BriefDescription == where.BriefDescription);
              // if (!string.IsNullOrEmpty(where.State))
              //  expr = expr.And(p => p.State == where.State);
              // if (!string.IsNullOrEmpty(where.DeletionTime))
              //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
              // if (!string.IsNullOrEmpty(where.DeleterUserCode))
              //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
              // if (!string.IsNullOrEmpty(where.IsDeleted))
              //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
              // if (!string.IsNullOrEmpty(where.LastModificationTime))
              //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
              // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
              //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
              // if (!string.IsNullOrEmpty(where.CreationTime))
              //  expr = expr.And(p => p.CreationTime == where.CreationTime);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
 #endregion
            var list = _offlineActivityRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

