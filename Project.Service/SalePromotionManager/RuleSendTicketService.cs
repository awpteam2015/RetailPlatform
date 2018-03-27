
 /***************************************************************************
 *       功能：     SPMRuleSendTicket业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     满足金额发券
 * *************************************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.SalePromotionManager;
using Project.Repository.SalePromotionManager;

namespace Project.Service.SalePromotionManager
{
    public class RuleSendTicketService
    {
       
       #region 构造函数
        private readonly RuleSendTicketRepository  _ruleSendTicketRepository;
            private static readonly RuleSendTicketService Instance = new RuleSendTicketService();

        public RuleSendTicketService()
        {
           this._ruleSendTicketRepository =new RuleSendTicketRepository();
        }
        
         public static  RuleSendTicketService GetInstance()
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
        public System.Int32 Add(RuleSendTicketEntity entity)
        {
            return _ruleSendTicketRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _ruleSendTicketRepository.GetById(pkId);
            _ruleSendTicketRepository.Delete(entity);
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
        public bool Delete(RuleSendTicketEntity entity)
        {
         try
            {
            _ruleSendTicketRepository.Delete(entity);
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
        public bool Update(RuleSendTicketEntity entity)
        {
          try
            {
            _ruleSendTicketRepository.Update(entity);
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
        public RuleSendTicketEntity GetModelByPk(System.Int32 pkId)
        {
            return _ruleSendTicketRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【满足金额发券】和总【满足金额发券】数</returns>
        public System.Tuple<IList<RuleSendTicketEntity>, int> Search(RuleSendTicketEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<RuleSendTicketEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.RuleId))
              //  expr = expr.And(p => p.RuleId == where.RuleId);
              // if (!string.IsNullOrEmpty(where.ActivityId))
              //  expr = expr.And(p => p.ActivityId == where.ActivityId);
              // if (!string.IsNullOrEmpty(where.StartMoney))
              //  expr = expr.And(p => p.StartMoney == where.StartMoney);
              // if (!string.IsNullOrEmpty(where.EndMoney))
              //  expr = expr.And(p => p.EndMoney == where.EndMoney);
              // if (!string.IsNullOrEmpty(where.TicketNum))
              //  expr = expr.And(p => p.TicketNum == where.TicketNum);
              // if (!string.IsNullOrEmpty(where.CardTypeIds))
              //  expr = expr.And(p => p.CardTypeIds == where.CardTypeIds);
              // if (!string.IsNullOrEmpty(where.TicketAvaildateEnd))
              //  expr = expr.And(p => p.TicketAvaildateEnd == where.TicketAvaildateEnd);
              // if (!string.IsNullOrEmpty(where.TicketAvaildateStart))
              //  expr = expr.And(p => p.TicketAvaildateStart == where.TicketAvaildateStart);
 #endregion
            var list = _ruleSendTicketRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _ruleSendTicketRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RuleSendTicketEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RuleSendTicketEntity> GetList(RuleSendTicketEntity where)
        {
               var expr = PredicateBuilder.True<RuleSendTicketEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.RuleId))
              //  expr = expr.And(p => p.RuleId == where.RuleId);
              // if (!string.IsNullOrEmpty(where.ActivityId))
              //  expr = expr.And(p => p.ActivityId == where.ActivityId);
              // if (!string.IsNullOrEmpty(where.StartMoney))
              //  expr = expr.And(p => p.StartMoney == where.StartMoney);
              // if (!string.IsNullOrEmpty(where.EndMoney))
              //  expr = expr.And(p => p.EndMoney == where.EndMoney);
              // if (!string.IsNullOrEmpty(where.TicketNum))
              //  expr = expr.And(p => p.TicketNum == where.TicketNum);
              // if (!string.IsNullOrEmpty(where.CardTypeIds))
              //  expr = expr.And(p => p.CardTypeIds == where.CardTypeIds);
              // if (!string.IsNullOrEmpty(where.TicketAvaildateEnd))
              //  expr = expr.And(p => p.TicketAvaildateEnd == where.TicketAvaildateEnd);
              // if (!string.IsNullOrEmpty(where.TicketAvaildateStart))
              //  expr = expr.And(p => p.TicketAvaildateStart == where.TicketAvaildateStart);
 #endregion
            var list = _ruleSendTicketRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

