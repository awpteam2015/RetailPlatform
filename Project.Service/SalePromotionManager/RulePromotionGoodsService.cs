
 /***************************************************************************
 *       功能：     SPMRulePromotionGoods业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     促销商品
 * *************************************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.SalePromotionManager;
using Project.Repository.SalePromotionManager;

namespace Project.Service.SalePromotionManager
{
    public class RulePromotionGoodsService
    {
       
       #region 构造函数
        private readonly RulePromotionGoodsRepository  _rulePromotionGoodsRepository;
            private static readonly RulePromotionGoodsService Instance = new RulePromotionGoodsService();

        public RulePromotionGoodsService()
        {
           this._rulePromotionGoodsRepository =new RulePromotionGoodsRepository();
        }
        
         public static  RulePromotionGoodsService GetInstance()
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
        public System.Int32 Add(RulePromotionGoodsEntity entity)
        {
            return _rulePromotionGoodsRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _rulePromotionGoodsRepository.GetById(pkId);
            _rulePromotionGoodsRepository.Delete(entity);
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
        public bool Delete(RulePromotionGoodsEntity entity)
        {
         try
            {
            _rulePromotionGoodsRepository.Delete(entity);
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
        public bool Update(RulePromotionGoodsEntity entity)
        {
          try
            {
            _rulePromotionGoodsRepository.Update(entity);
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
        public RulePromotionGoodsEntity GetModelByPk(System.Int32 pkId)
        {
            return _rulePromotionGoodsRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【促销商品】和总【促销商品】数</returns>
        public System.Tuple<IList<RulePromotionGoodsEntity>, int> Search(RulePromotionGoodsEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<RulePromotionGoodsEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ActivityId))
              //  expr = expr.And(p => p.ActivityId == where.ActivityId);
              // if (!string.IsNullOrEmpty(where.RuleId))
              //  expr = expr.And(p => p.RuleId == where.RuleId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.ProductCode))
              //  expr = expr.And(p => p.ProductCode == where.ProductCode);
              // if (!string.IsNullOrEmpty(where.GoodsCode))
              //  expr = expr.And(p => p.GoodsCode == where.GoodsCode);
              // if (!string.IsNullOrEmpty(where.GoodsId))
              //  expr = expr.And(p => p.GoodsId == where.GoodsId);
              // if (!string.IsNullOrEmpty(where.Price))
              //  expr = expr.And(p => p.Price == where.Price);
              // if (!string.IsNullOrEmpty(where.PromotionPrice))
              //  expr = expr.And(p => p.PromotionPrice == where.PromotionPrice);
 #endregion
            var list = _rulePromotionGoodsRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _rulePromotionGoodsRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RulePromotionGoodsEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RulePromotionGoodsEntity> GetList(RulePromotionGoodsEntity where)
        {
               var expr = PredicateBuilder.True<RulePromotionGoodsEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ActivityId))
              //  expr = expr.And(p => p.ActivityId == where.ActivityId);
              // if (!string.IsNullOrEmpty(where.RuleId))
              //  expr = expr.And(p => p.RuleId == where.RuleId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.ProductCode))
              //  expr = expr.And(p => p.ProductCode == where.ProductCode);
              // if (!string.IsNullOrEmpty(where.GoodsCode))
              //  expr = expr.And(p => p.GoodsCode == where.GoodsCode);
              // if (!string.IsNullOrEmpty(where.GoodsId))
              //  expr = expr.And(p => p.GoodsId == where.GoodsId);
              // if (!string.IsNullOrEmpty(where.Price))
              //  expr = expr.And(p => p.Price == where.Price);
              // if (!string.IsNullOrEmpty(where.PromotionPrice))
              //  expr = expr.And(p => p.PromotionPrice == where.PromotionPrice);
 #endregion
            var list = _rulePromotionGoodsRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

