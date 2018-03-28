
/***************************************************************************
*       功能：     SPMRule业务处理层
*       作者：     李伟伟
*       日期：     2018/3/26
*       描述：     促销规则
* *************************************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Util;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.SalePromotionManager;
using Project.Repository.ProductManager;
using Project.Repository.SalePromotionManager;

namespace Project.Service.SalePromotionManager
{
    public class RuleService
    {

        #region 构造函数
        private readonly RuleRepository _ruleRepository;
        private readonly RuleDiscountMoneyRepository _ruleDiscountMoneyRepository;
        private readonly RuleSendTicketRepository _ruleSendTicketRepository;
        private readonly RulePromotionGoodsRepository _rulePromotionGoodsRepository;
        private readonly TicketRepository _ticketRepository;
        private readonly ProductRepository _productRepository;
        private readonly GoodsRepository _goodsRepository;

        private static readonly RuleService Instance = new RuleService();



        public RuleService()
        {
            this._ruleRepository = new RuleRepository();
            _ruleDiscountMoneyRepository = new RuleDiscountMoneyRepository();
            _ruleSendTicketRepository = new RuleSendTicketRepository();
            _rulePromotionGoodsRepository = new RulePromotionGoodsRepository();
            _ticketRepository = new TicketRepository();
            _productRepository=new ProductRepository();
            _goodsRepository=new GoodsRepository();
        }

        public static RuleService GetInstance()
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
        public System.Int32 RuleRaAdd(RuleEntity entity)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var pkId = _ruleRepository.Save(entity);

                    entity.RulePromotionGoodsEntityList.ForEach(p =>
                    {
                        p.RuleId = pkId;
                        p.ActivityId = entity.ActivityId;

                        var productInfo = _productRepository.GetById(p.ProductId);
                        if (productInfo!=null)
                        {
                            p.ProductName = productInfo.ProductName;
                            p.ProductCode = productInfo.ProductCode;
                        }

                        var goodsInfo = _goodsRepository.Query().FirstOrDefault(x => x.GoodsCode == p.GoodsCode);
                        if (goodsInfo!=null)
                        {
                            p.GoodsId = goodsInfo.PkId;
                            p.Price = goodsInfo.GoodsPrice;
                            p.SpecDetail = goodsInfo.SpecDetail;
                        }

                        _rulePromotionGoodsRepository.Save(p);
                    });

                    tx.Commit();
                    return pkId;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public bool RuleRaEdit(RuleEntity entity)
        {
            var orgInfo = _ruleRepository.GetById(entity.PkId);
            orgInfo.Title = entity.Title;


            #region 
            var addEntityList = entity.RulePromotionGoodsEntityList.Where(p => orgInfo.RulePromotionGoodsEntityList.All(x => x.PkId != p.PkId)).ToList();
            var updateEntityList = orgInfo.RulePromotionGoodsEntityList.Where(p => entity.RulePromotionGoodsEntityList.Any(x => x.PkId == p.PkId)).ToList();
            var deleteEntityList = orgInfo.RulePromotionGoodsEntityList.Where(p => entity.RulePromotionGoodsEntityList.All(x => x.PkId != p.PkId)).ToList();

            addEntityList.ForEach(p =>
            {
                orgInfo.RulePromotionGoodsEntityList.Add(p);
            });

            updateEntityList.ForEach(p =>
            {
                var newEntity = entity.RulePromotionGoodsEntityList.SingleOrDefault(x => x.PkId == p.PkId );
                p.GoodsCode = newEntity.GoodsCode;
                p.PromotionPrice = newEntity.PromotionPrice;
            });
            #endregion

            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    _ruleRepository.Update(orgInfo);

                    orgInfo.RulePromotionGoodsEntityList = new HashSet<RulePromotionGoodsEntity>(orgInfo.RulePromotionGoodsEntityList.Where(p => deleteEntityList.All(x => x.PkId != p.PkId)).ToList());

                    orgInfo.RulePromotionGoodsEntityList.ForEach(p =>
                    {
                        p.RuleId = orgInfo.PkId;
                        p.ActivityId = entity.ActivityId;

                        var productInfo = _productRepository.GetById(p.ProductId);
                        if (productInfo != null)
                        {
                            p.ProductName = productInfo.ProductName;
                            p.ProductCode = productInfo.ProductCode;
                        }

                        var goodsInfo = _goodsRepository.Query().FirstOrDefault(x => x.GoodsCode == p.GoodsCode);
                        if (goodsInfo != null)
                        {
                            p.GoodsId = goodsInfo.PkId;
                            p.Price = goodsInfo.GoodsPrice;
                            p.SpecDetail = goodsInfo.SpecDetail;
                        }
                        _rulePromotionGoodsRepository.SaveOrUpdate(p);
                    });

                    deleteEntityList.ForEach(p =>
                    {
                        _rulePromotionGoodsRepository.Delete(p);
                    });

                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }



        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public System.Int32 RuleRbAdd(RuleEntity entity)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var pkId = _ruleRepository.Save(entity);

                    entity.RuleSendTicketEntity.RuleId = pkId;
                    entity.RuleSendTicketEntity.ActivityId = entity.ActivityId;

                    _ruleSendTicketRepository.Save(entity.RuleSendTicketEntity);
                    tx.Commit();
                    return pkId;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public bool RuleRbEdit(RuleEntity entity)
        {
            var orgInfo = _ruleRepository.GetById(entity.PkId);
            orgInfo.Title = entity.Title;
            orgInfo.RuleSendTicketEntity.StartMoney = entity.RuleSendTicketEntity.StartMoney;
            orgInfo.RuleSendTicketEntity.EndMoney = entity.RuleSendTicketEntity.EndMoney;
            orgInfo.RuleSendTicketEntity.TicketNum = entity.RuleSendTicketEntity.TicketNum;
            orgInfo.RuleSendTicketEntity.TicketAvaildateStart = entity.RuleSendTicketEntity.TicketAvaildateStart;
            orgInfo.RuleSendTicketEntity.TicketAvaildateEnd = entity.RuleSendTicketEntity.TicketAvaildateEnd;

            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    _ruleRepository.Update(orgInfo);
                    _ruleSendTicketRepository.Update(orgInfo.RuleSendTicketEntity);
                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public System.Int32 RuleRcAdd(RuleEntity entity)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var pkId = _ruleRepository.Save(entity);

                    entity.RuleDiscountMoneyEntity.RuleId = pkId;
                    entity.RuleDiscountMoneyEntity.ActivityId = entity.ActivityId;

                    _ruleDiscountMoneyRepository.Save(entity.RuleDiscountMoneyEntity);
                    tx.Commit();
                    return pkId;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public bool RuleRcEdit(RuleEntity entity)
        {
            var orgInfo = _ruleRepository.GetById(entity.PkId);
            orgInfo.Title = entity.Title;
            orgInfo.RuleDiscountMoneyEntity.StartMoney = entity.RuleDiscountMoneyEntity.StartMoney;
            orgInfo.RuleDiscountMoneyEntity.EndMoney = entity.RuleDiscountMoneyEntity.EndMoney;
            orgInfo.RuleDiscountMoneyEntity.DiscountMoney = entity.RuleDiscountMoneyEntity.DiscountMoney;

            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    _ruleRepository.Update(orgInfo);
                    _ruleDiscountMoneyRepository.Update(orgInfo.RuleDiscountMoneyEntity);
                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }




        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {

                try
                {
                    var entity = _ruleRepository.GetById(pkId);
                    _ruleRepository.Delete(entity);

                    if (entity.RuleDiscountMoneyEntity != null)
                    {
                        _ruleDiscountMoneyRepository.Delete(entity.RuleDiscountMoneyEntity);
                    }

                    if (entity.RuleSendTicketEntity != null)
                    {
                        _ruleSendTicketRepository.Delete(entity.RuleSendTicketEntity);
                    }

                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(RuleEntity entity)
        {
            try
            {
                _ruleRepository.Delete(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public RuleEntity GetModelByPk(System.Int32 pkId)
        {
            return _ruleRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【促销规则】和总【促销规则】数</returns>
        public System.Tuple<IList<RuleEntity>, int> Search(RuleEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<RuleEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (where.ActivityId > 0)
                expr = expr.And(p => p.ActivityId == where.ActivityId);
            // if (!string.IsNullOrEmpty(where.RuleType))
            //  expr = expr.And(p => p.RuleType == where.RuleType);
            // if (!string.IsNullOrEmpty(where.Title))
            //  expr = expr.And(p => p.Title == where.Title);
            #endregion
            var list = _ruleRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _ruleRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RuleEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RuleEntity> GetList(RuleEntity where)
        {
            var expr = PredicateBuilder.True<RuleEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (where.ActivityId > 0)
                expr = expr.And(p => p.ActivityId == where.ActivityId);
            // if (!string.IsNullOrEmpty(where.RuleType))
            //  expr = expr.And(p => p.RuleType == where.RuleType);
            // if (!string.IsNullOrEmpty(where.Title))
            //  expr = expr.And(p => p.Title == where.Title);
            #endregion
            var list = _ruleRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}




