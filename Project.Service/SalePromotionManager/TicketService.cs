
 /***************************************************************************
 *       功能：     SPMTicket业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     券
 * *************************************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.SalePromotionManager;
using Project.Repository.SalePromotionManager;

namespace Project.Service.SalePromotionManager
{
    public class TicketService
    {
       
       #region 构造函数
        private readonly TicketRepository  _ticketRepository;
            private static readonly TicketService Instance = new TicketService();

        public TicketService()
        {
           this._ticketRepository =new TicketRepository();
        }
        
         public static  TicketService GetInstance()
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
        public System.Int32 Add(TicketEntity entity)
        {
            return _ticketRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _ticketRepository.GetById(pkId);
            _ticketRepository.Delete(entity);
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
        public bool Delete(TicketEntity entity)
        {
         try
            {
            _ticketRepository.Delete(entity);
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
        public bool Update(TicketEntity entity)
        {
          try
            {
            _ticketRepository.Update(entity);
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
        public TicketEntity GetModelByPk(System.Int32 pkId)
        {
            return _ticketRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【券】和总【券】数</returns>
        public System.Tuple<IList<TicketEntity>, int> Search(TicketEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<TicketEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.TicketCode))
              //  expr = expr.And(p => p.TicketCode == where.TicketCode);
              // if (!string.IsNullOrEmpty(where.TickettypeId))
              //  expr = expr.And(p => p.TickettypeId == where.TickettypeId);
              // if (!string.IsNullOrEmpty(where.Status))
              //  expr = expr.And(p => p.Status == where.Status);
              // if (!string.IsNullOrEmpty(where.AvaildateStart))
              //  expr = expr.And(p => p.AvaildateStart == where.AvaildateStart);
              // if (!string.IsNullOrEmpty(where.AvaildateEnd))
              //  expr = expr.And(p => p.AvaildateEnd == where.AvaildateEnd);
              // if (!string.IsNullOrEmpty(where.OrderNo))
              //  expr = expr.And(p => p.OrderNo == where.OrderNo);
              // if (!string.IsNullOrEmpty(where.UseDate))
              //  expr = expr.And(p => p.UseDate == where.UseDate);
              // if (!string.IsNullOrEmpty(where.CustomerId))
              //  expr = expr.And(p => p.CustomerId == where.CustomerId);
              // if (!string.IsNullOrEmpty(where.RuleId))
              //  expr = expr.And(p => p.RuleId == where.RuleId);
              // if (!string.IsNullOrEmpty(where.ActivityId))
              //  expr = expr.And(p => p.ActivityId == where.ActivityId);
 #endregion
            var list = _ticketRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _ticketRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<TicketEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<TicketEntity> GetList(TicketEntity where)
        {
               var expr = PredicateBuilder.True<TicketEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.TicketCode))
              //  expr = expr.And(p => p.TicketCode == where.TicketCode);
              // if (!string.IsNullOrEmpty(where.TickettypeId))
              //  expr = expr.And(p => p.TickettypeId == where.TickettypeId);
              // if (!string.IsNullOrEmpty(where.Status))
              //  expr = expr.And(p => p.Status == where.Status);
              // if (!string.IsNullOrEmpty(where.AvaildateStart))
              //  expr = expr.And(p => p.AvaildateStart == where.AvaildateStart);
              // if (!string.IsNullOrEmpty(where.AvaildateEnd))
              //  expr = expr.And(p => p.AvaildateEnd == where.AvaildateEnd);
              // if (!string.IsNullOrEmpty(where.OrderNo))
              //  expr = expr.And(p => p.OrderNo == where.OrderNo);
              // if (!string.IsNullOrEmpty(where.UseDate))
              //  expr = expr.And(p => p.UseDate == where.UseDate);
              // if (!string.IsNullOrEmpty(where.CustomerId))
              //  expr = expr.And(p => p.CustomerId == where.CustomerId);
              // if (!string.IsNullOrEmpty(where.RuleId))
              //  expr = expr.And(p => p.RuleId == where.RuleId);
              // if (!string.IsNullOrEmpty(where.ActivityId))
              //  expr = expr.And(p => p.ActivityId == where.ActivityId);
 #endregion
            var list = _ticketRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

