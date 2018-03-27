
 /***************************************************************************
 *       功能：     SPMActivity业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     促销活动  目前考虑单品促销 满足金额发券 满足金额减免
 * *************************************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.SalePromotionManager;
using Project.Repository.SalePromotionManager;

namespace Project.Service.SalePromotionManager
{
    public class ActivityService
    {
       
       #region 构造函数
        private readonly ActivityRepository  _activityRepository;
            private static readonly ActivityService Instance = new ActivityService();

        public ActivityService()
        {
           this._activityRepository =new ActivityRepository();
        }
        
         public static  ActivityService GetInstance()
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
        public System.Int32 Add(ActivityEntity entity)
        {
            return _activityRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _activityRepository.GetById(pkId);
            _activityRepository.Delete(entity);
             return true;
        }
        catch(Exception e)
        {
         return false;
        }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(ActivityEntity entity)
        {
         try
            {
            _activityRepository.Delete(entity);
             return true;
        }
         catch(Exception e)
        {
         return false;
        }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(ActivityEntity entity)
        {
          try
            {
            _activityRepository.Merge(entity);
         return true;
        }
         catch(Exception e)
        {
         return false;
        }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public ActivityEntity GetModelByPk(System.Int32 pkId)
        {
            return _activityRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【促销活动  目前考虑单品促销 满足金额发券 满足金额减免】和总【促销活动  目前考虑单品促销 满足金额发券 满足金额减免】数</returns>
        public System.Tuple<IList<ActivityEntity>, int> Search(ActivityEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ActivityEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.Title))
                expr = expr.And(p => p.Title .Contains(where.Title) );
            // if (!string.IsNullOrEmpty(where.StartDate))
            //  expr = expr.And(p => p.StartDate == where.StartDate);
            // if (!string.IsNullOrEmpty(where.EndDate))
            //  expr = expr.And(p => p.EndDate == where.EndDate);
            if (where.State>0)
                expr = expr.And(p => p.State == where.State);
            // if (!string.IsNullOrEmpty(where.BriefDescription))
            //  expr = expr.And(p => p.BriefDescription == where.BriefDescription);
            #endregion
            var list = _activityRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _activityRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ActivityEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ActivityEntity> GetList(ActivityEntity where)
        {
               var expr = PredicateBuilder.True<ActivityEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.Title))
              //  expr = expr.And(p => p.Title == where.Title);
              // if (!string.IsNullOrEmpty(where.StartDate))
              //  expr = expr.And(p => p.StartDate == where.StartDate);
              // if (!string.IsNullOrEmpty(where.EndDate))
              //  expr = expr.And(p => p.EndDate == where.EndDate);
              // if (!string.IsNullOrEmpty(where.State))
              //  expr = expr.And(p => p.State == where.State);
              // if (!string.IsNullOrEmpty(where.BriefDescription))
              //  expr = expr.And(p => p.BriefDescription == where.BriefDescription);
 #endregion
            var list = _activityRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

