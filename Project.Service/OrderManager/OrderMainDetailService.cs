
 /***************************************************************************
 *       功能：     OMOrderMainDetail业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/21
 *       描述：     订单主表明细
 * *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.OrderManager;
using Project.Repository.OrderManager;

namespace Project.Service.OrderManager
{
    public class OrderMainDetailService
    {
       
       #region 构造函数
        private readonly OrderMainDetailRepository  _orderMainDetailRepository;
            private static readonly OrderMainDetailService Instance = new OrderMainDetailService();

        public OrderMainDetailService()
        {
           this._orderMainDetailRepository =new OrderMainDetailRepository();
        }
        
         public static  OrderMainDetailService GetInstance()
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
        public System.Int32 Add(OrderMainDetailEntity entity)
        {
            return _orderMainDetailRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _orderMainDetailRepository.GetById(pkId);
            _orderMainDetailRepository.Delete(entity);
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
        public bool Delete(OrderMainDetailEntity entity)
        {
         try
            {
            _orderMainDetailRepository.Delete(entity);
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
        public bool Update(OrderMainDetailEntity entity)
        {
          try
            {
            _orderMainDetailRepository.Update(entity);
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
        public OrderMainDetailEntity GetModelByPk(System.Int32 pkId)
        {
            return _orderMainDetailRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【订单主表明细】和总【订单主表明细】数</returns>
        public System.Tuple<IList<OrderMainDetailEntity>, int> Search(OrderMainDetailEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<OrderMainDetailEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.OrderNo))
              //  expr = expr.And(p => p.OrderNo == where.OrderNo);
              // if (!string.IsNullOrEmpty(where.ProductCategoryId))
              //  expr = expr.And(p => p.ProductCategoryId == where.ProductCategoryId);
              // if (!string.IsNullOrEmpty(where.ProductName))
              //  expr = expr.And(p => p.ProductName == where.ProductName);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.ProductCode))
              //  expr = expr.And(p => p.ProductCode == where.ProductCode);
              // if (!string.IsNullOrEmpty(where.GoodsCode))
              //  expr = expr.And(p => p.GoodsCode == where.GoodsCode);
              // if (!string.IsNullOrEmpty(where.GoodsId))
              //  expr = expr.And(p => p.GoodsId == where.GoodsId);
              // if (!string.IsNullOrEmpty(where.ImageUrl))
              //  expr = expr.And(p => p.ImageUrl == where.ImageUrl);
              // if (!string.IsNullOrEmpty(where.Price))
              //  expr = expr.And(p => p.Price == where.Price);
              // if (!string.IsNullOrEmpty(where.PriceSubDiscount))
              //  expr = expr.And(p => p.PriceSubDiscount == where.PriceSubDiscount);
              // if (!string.IsNullOrEmpty(where.TotalAmount))
              //  expr = expr.And(p => p.TotalAmount == where.TotalAmount);
              // if (!string.IsNullOrEmpty(where.ProductWeight))
              //  expr = expr.And(p => p.ProductWeight == where.ProductWeight);
              // if (!string.IsNullOrEmpty(where.SpecName))
              //  expr = expr.And(p => p.SpecName == where.SpecName);
 #endregion
            var list = _orderMainDetailRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _orderMainDetailRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<OrderMainDetailEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<OrderMainDetailEntity> GetList(OrderMainDetailEntity where)
        {
               var expr = PredicateBuilder.True<OrderMainDetailEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.OrderNo))
              //  expr = expr.And(p => p.OrderNo == where.OrderNo);
              // if (!string.IsNullOrEmpty(where.ProductCategoryId))
              //  expr = expr.And(p => p.ProductCategoryId == where.ProductCategoryId);
              // if (!string.IsNullOrEmpty(where.ProductName))
              //  expr = expr.And(p => p.ProductName == where.ProductName);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.ProductCode))
              //  expr = expr.And(p => p.ProductCode == where.ProductCode);
              // if (!string.IsNullOrEmpty(where.GoodsCode))
              //  expr = expr.And(p => p.GoodsCode == where.GoodsCode);
              // if (!string.IsNullOrEmpty(where.GoodsId))
              //  expr = expr.And(p => p.GoodsId == where.GoodsId);
              // if (!string.IsNullOrEmpty(where.ImageUrl))
              //  expr = expr.And(p => p.ImageUrl == where.ImageUrl);
              // if (!string.IsNullOrEmpty(where.Price))
              //  expr = expr.And(p => p.Price == where.Price);
              // if (!string.IsNullOrEmpty(where.PriceSubDiscount))
              //  expr = expr.And(p => p.PriceSubDiscount == where.PriceSubDiscount);
              // if (!string.IsNullOrEmpty(where.TotalAmount))
              //  expr = expr.And(p => p.TotalAmount == where.TotalAmount);
              // if (!string.IsNullOrEmpty(where.ProductWeight))
              //  expr = expr.And(p => p.ProductWeight == where.ProductWeight);
              // if (!string.IsNullOrEmpty(where.SpecName))
              //  expr = expr.And(p => p.SpecName == where.SpecName);
 #endregion
            var list = _orderMainDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

