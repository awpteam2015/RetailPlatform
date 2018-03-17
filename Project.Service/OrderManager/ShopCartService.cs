
 /***************************************************************************
 *       功能：     OMShopCart业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     购物车
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.OrderManager;
using Project.Repository.OrderManager;

namespace Project.Service.OrderManager
{
    public class ShopCartService
    {
       
       #region 构造函数
        private readonly ShopCartRepository  _shopCartRepository;
            private static readonly ShopCartService Instance = new ShopCartService();

        public ShopCartService()
        {
           this._shopCartRepository =new ShopCartRepository();
        }
        
         public static  ShopCartService GetInstance()
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
        public System.Int32 Add(ShopCartEntity entity)
        {
            return _shopCartRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _shopCartRepository.GetById(pkId);
            _shopCartRepository.Delete(entity);
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
        public bool Delete(ShopCartEntity entity)
        {
         try
            {
            _shopCartRepository.Delete(entity);
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
        public bool Update(ShopCartEntity entity)
        {
          try
            {
            _shopCartRepository.Update(entity);
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
        public ShopCartEntity GetModelByPk(System.Int32 pkId)
        {
            return _shopCartRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【购物车】和总【购物车】数</returns>
        public System.Tuple<IList<ShopCartEntity>, int> Search(ShopCartEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ShopCartEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.OrderNo))
              //  expr = expr.And(p => p.OrderNo == where.OrderNo);
              // if (!string.IsNullOrEmpty(where.ProductCategoryId))
              //  expr = expr.And(p => p.ProductCategoryId == where.ProductCategoryId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.GoodsCode))
              //  expr = expr.And(p => p.GoodsCode == where.GoodsCode);
              // if (!string.IsNullOrEmpty(where.GoodsId))
              //  expr = expr.And(p => p.GoodsId == where.GoodsId);
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
              // if (!string.IsNullOrEmpty(where.CustomerId))
              //  expr = expr.And(p => p.CustomerId == where.CustomerId);
 #endregion
            var list = _shopCartRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _shopCartRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ShopCartEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ShopCartEntity> GetList(ShopCartEntity where)
        {
               var expr = PredicateBuilder.True<ShopCartEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.OrderNo))
              //  expr = expr.And(p => p.OrderNo == where.OrderNo);
              // if (!string.IsNullOrEmpty(where.ProductCategoryId))
              //  expr = expr.And(p => p.ProductCategoryId == where.ProductCategoryId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.GoodsCode))
              //  expr = expr.And(p => p.GoodsCode == where.GoodsCode);
              // if (!string.IsNullOrEmpty(where.GoodsId))
              //  expr = expr.And(p => p.GoodsId == where.GoodsId);
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
              // if (!string.IsNullOrEmpty(where.CustomerId))
              //  expr = expr.And(p => p.CustomerId == where.CustomerId);
 #endregion
            var list = _shopCartRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

