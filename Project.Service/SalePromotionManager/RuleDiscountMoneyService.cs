
 /***************************************************************************
 *       功能：     SPMRuleDiscountMoney业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     满足金额减免
 * *************************************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.SalePromotionManager;
using Project.Repository.SalePromotionManager;

namespace Project.Service.SalePromotionManager
{
    public class RuleDiscountMoneyService
    {
       
       #region 构造函数
        private readonly RuleDiscountMoneyRepository  _ruleDiscountMoneyRepository;
            private static readonly RuleDiscountMoneyService Instance = new RuleDiscountMoneyService();

        public RuleDiscountMoneyService()
        {
           this._ruleDiscountMoneyRepository =new RuleDiscountMoneyRepository();
        }
        
         public static  RuleDiscountMoneyService GetInstance()
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
        public System.Int32 Add(RuleDiscountMoneyEntity entity)
        {
            return _ruleDiscountMoneyRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _ruleDiscountMoneyRepository.GetById(pkId);
            _ruleDiscountMoneyRepository.Delete(entity);
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
        public bool Delete(RuleDiscountMoneyEntity entity)
        {
         try
            {
            _ruleDiscountMoneyRepository.Delete(entity);
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
        public bool Update(RuleDiscountMoneyEntity entity)
        {
          try
            {
            _ruleDiscountMoneyRepository.Update(entity);
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
        public RuleDiscountMoneyEntity GetModelByPk(System.Int32 pkId)
        {
            return _ruleDiscountMoneyRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【满足金额减免】和总【满足金额减免】数</returns>
        public System.Tuple<IList<RuleDiscountMoneyEntity>, int> Search(RuleDiscountMoneyEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<RuleDiscountMoneyEntity>();
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
              // if (!string.IsNullOrEmpty(where.DiscountMoney))
              //  expr = expr.And(p => p.DiscountMoney == where.DiscountMoney);
              // if (!string.IsNullOrEmpty(where.CardTypeIds))
              //  expr = expr.And(p => p.CardTypeIds == where.CardTypeIds);
 #endregion
            var list = _ruleDiscountMoneyRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _ruleDiscountMoneyRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RuleDiscountMoneyEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RuleDiscountMoneyEntity> GetList(RuleDiscountMoneyEntity where)
        {
               var expr = PredicateBuilder.True<RuleDiscountMoneyEntity>();
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
              // if (!string.IsNullOrEmpty(where.DiscountMoney))
              //  expr = expr.And(p => p.DiscountMoney == where.DiscountMoney);
              // if (!string.IsNullOrEmpty(where.CardTypeIds))
              //  expr = expr.And(p => p.CardTypeIds == where.CardTypeIds);
 #endregion
            var list = _ruleDiscountMoneyRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

