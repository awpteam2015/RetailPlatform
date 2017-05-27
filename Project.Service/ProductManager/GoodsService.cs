
 /***************************************************************************
 *       功能：     PRMGoods业务处理层
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     商品表
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class GoodsService
    {
       
       #region 构造函数
        private readonly GoodsRepository  _goodsRepository;
            private static readonly GoodsService Instance = new GoodsService();

        public GoodsService()
        {
           this._goodsRepository =new GoodsRepository();
        }
        
         public static  GoodsService GetInstance()
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
        public System.Int32 Add(GoodsEntity entity)
        {
            return _goodsRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _goodsRepository.GetById(pkId);
            _goodsRepository.Delete(entity);
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
        public bool Delete(GoodsEntity entity)
        {
         try
            {
            _goodsRepository.Delete(entity);
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
        public bool Update(GoodsEntity entity)
        {
          try
            {
            _goodsRepository.Update(entity);
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
        public GoodsEntity GetModelByPk(System.Int32 pkId)
        {
            return _goodsRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【商品表】和总【商品表】数</returns>
        public System.Tuple<IList<GoodsEntity>, int> Search(GoodsEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<GoodsEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.GoodsCode))
              //  expr = expr.And(p => p.GoodsCode == where.GoodsCode);
              // if (!string.IsNullOrEmpty(where.GoodsName))
              //  expr = expr.And(p => p.GoodsName == where.GoodsName);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.SpecValue1))
              //  expr = expr.And(p => p.SpecValue1 == where.SpecValue1);
              // if (!string.IsNullOrEmpty(where.SpecValue2))
              //  expr = expr.And(p => p.SpecValue2 == where.SpecValue2);
              // if (!string.IsNullOrEmpty(where.SpecValue3))
              //  expr = expr.And(p => p.SpecValue3 == where.SpecValue3);
 #endregion
            var list = _goodsRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _goodsRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<GoodsEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<GoodsEntity> GetList(GoodsEntity where)
        {
               var expr = PredicateBuilder.True<GoodsEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.GoodsCode))
              //  expr = expr.And(p => p.GoodsCode == where.GoodsCode);
              // if (!string.IsNullOrEmpty(where.GoodsName))
              //  expr = expr.And(p => p.GoodsName == where.GoodsName);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.SpecValue1))
              //  expr = expr.And(p => p.SpecValue1 == where.SpecValue1);
              // if (!string.IsNullOrEmpty(where.SpecValue2))
              //  expr = expr.And(p => p.SpecValue2 == where.SpecValue2);
              // if (!string.IsNullOrEmpty(where.SpecValue3))
              //  expr = expr.And(p => p.SpecValue3 == where.SpecValue3);
 #endregion
            var list = _goodsRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

