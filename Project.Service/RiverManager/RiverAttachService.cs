
/***************************************************************************
*       功能：     RMRiverAttach业务处理层
*       作者：     李伟伟
*       日期：     2016/7/30
*       描述：     河流水质水纹管理
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Model.RiverManager;
using Project.Repository.RiverManager;

namespace Project.Service.RiverManager
{
    public class RiverAttachService
    {

        #region 构造函数
        private readonly RiverAttachRepository _riverAttachRepository;
        private static readonly RiverAttachService Instance = new RiverAttachService();

        public RiverAttachService()
        {
            this._riverAttachRepository = new RiverAttachRepository();
        }

        public static RiverAttachService GetInstance()
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
        public System.Int32 Add(RiverAttachEntity entity)
        {
            return _riverAttachRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _riverAttachRepository.GetById(pkId);
                _riverAttachRepository.Delete(entity);
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
        public bool Delete(RiverAttachEntity entity)
        {
            try
            {
                _riverAttachRepository.Delete(entity);
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
        public bool Update(RiverAttachEntity entity)
        {
            try
            {
                _riverAttachRepository.Update(entity);
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
        public RiverAttachEntity GetModelByPk(System.Int32 pkId)
        {
            return _riverAttachRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【河流水质水纹管理】和总【河流水质水纹管理】数</returns>
        public System.Tuple<IList<RiverAttachEntity>, int> Search(RiverAttachEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<RiverAttachEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (where.RiverId > 0)
                expr = expr.And(p => p.RiverId == where.RiverId);
            if (!string.IsNullOrEmpty(where.RiverName))
                expr = expr.And(p => p.RiverName.Contains(where.RiverName));
            if (where.RecordTime != null)
                expr = expr.And(p => p.RecordTime == where.RecordTime);

            if (where.IsMainData >0)
                expr = expr.And(p => p.IsMainData ==1);


            //expr = expr.And(p => p.Day == 1);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            #endregion
            var list = _riverAttachRepository.Query().Where(expr).OrderByDescending(p => p.RecordTime).Skip(skipResults).Take(maxResults).ToList();

            list.ForEach(p =>
            {
                p.WaterQualityRank = RiverAttachService.GetInstance()
                          .GetLowestRank(p.RiverId.GetValueOrDefault(), p.RecordTime.GetValueOrDefault());

            });

            var count = _riverAttachRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RiverAttachEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RiverAttachEntity> GetList(RiverAttachEntity where)
        {
            var expr = PredicateBuilder.True<RiverAttachEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.RiverId))
            //  expr = expr.And(p => p.RiverId == where.RiverId);
            // if (!string.IsNullOrEmpty(where.RiverName))
            //  expr = expr.And(p => p.RiverName == where.RiverName);
            // if (!string.IsNullOrEmpty(where.RecordTime))
            //  expr = expr.And(p => p.RecordTime == where.RecordTime);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            #endregion
            var list = _riverAttachRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        public string GetLowestRank(int riverId, DateTime date)
        {
            var datastart = DateTimeHelper.GetTheFirstDayOfMonth(date);
            var datalast = DateTimeHelper.GetTheLastDayOfMonth(date);

            var expr = PredicateBuilder.True<RiverAttachEntity>();
            #region
            expr = expr.And(p => p.RiverId == riverId);
            expr = expr.And(p => p.RecordTime >= datastart);
            expr = expr.And(p => p.RecordTime <= datalast);
           // expr = expr.And(p => p.Day == 1);
            #endregion
            var list = _riverAttachRepository.Query().Where(expr).OrderByDescending(p => p.WaterQualityRank).ToList();
            if (list.Any())
            {
                return list.FirstOrDefault().WaterQualityRank;
            }
            else
            {
                return "I";
            }
        }


        public bool IfHasMonthRecord(int riverId, int year,int month)
        {
            var expr = PredicateBuilder.True<RiverAttachEntity>();
            #region
            expr = expr.And(p => p.RiverId == riverId);
            expr = expr.And(p => p.Year == year);
            expr = expr.And(p => p.Month == month);
            expr = expr.And(p => p.IsMainData == 1);
            #endregion
            var list = _riverAttachRepository.Query().Where(expr).OrderByDescending(p => p.WaterQualityRank).ToList();
            return list.Any();
        }


        public RiverAttachEntity GetLatestRecord(int riverId)
        {
            var expr = PredicateBuilder.True<RiverAttachEntity>();
            #region
            expr = expr.And(p => p.RiverId == riverId);
            expr = expr.And(p => p.IsMainData == 1);
            #endregion
            var list = _riverAttachRepository.Query().Where(expr).OrderByDescending(p => p.RecordTime).ToList();
            if (list.Any())
            {
                return list.FirstOrDefault();
            }
            else
            {
                return new RiverAttachEntity()
                {
                    WaterQualityRank = "",
                    WaterQualityChange = 0
                };
            }
        }

        public List<RiverAttachEntity> GetSameList(int riverId,int year,int month)
        {
            var expr = PredicateBuilder.True<RiverAttachEntity>();
            #region
            expr = expr.And(p => p.RiverId == riverId);
            expr = expr.And(p => p.Year == year);
            expr = expr.And(p => p.Month == month);
            #endregion
            var list = _riverAttachRepository.Query().Where(expr).OrderBy(p => p.RecordTime).ToList();
            return list;
        }

        #endregion
        }
}




