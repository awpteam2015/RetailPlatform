
 /***************************************************************************
 *       功能：     PRMGoodsSpecValue业务处理层
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     商品-规格值关联表
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class GoodsSpecValueService
    {
       
       #region 构造函数
        private readonly GoodsSpecValueRepository  _goodsSpecValueRepository;
            private static readonly GoodsSpecValueService Instance = new GoodsSpecValueService();

        public GoodsSpecValueService()
        {
           this._goodsSpecValueRepository =new GoodsSpecValueRepository();
        }
        
         public static  GoodsSpecValueService GetInstance()
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
        public System.Int32 Add(GoodsSpecValueEntity entity)
        {
            return _goodsSpecValueRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _goodsSpecValueRepository.GetById(pkId);
            _goodsSpecValueRepository.Delete(entity);
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
        public bool Delete(GoodsSpecValueEntity entity)
        {
         try
            {
            _goodsSpecValueRepository.Delete(entity);
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
        public bool Update(GoodsSpecValueEntity entity)
        {
          try
            {
            _goodsSpecValueRepository.Update(entity);
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
        public GoodsSpecValueEntity GetModelByPk(System.Int32 pkId)
        {
            return _goodsSpecValueRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【商品-规格值关联表】和总【商品-规格值关联表】数</returns>
        public System.Tuple<IList<GoodsSpecValueEntity>, int> Search(GoodsSpecValueEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<GoodsSpecValueEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.GoodsId))
              //  expr = expr.And(p => p.GoodsId == where.GoodsId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.SpecId))
              //  expr = expr.And(p => p.SpecId == where.SpecId);
              // if (!string.IsNullOrEmpty(where.SpecName))
              //  expr = expr.And(p => p.SpecName == where.SpecName);
              // if (!string.IsNullOrEmpty(where.SpecValueId))
              //  expr = expr.And(p => p.SpecValueId == where.SpecValueId);
              // if (!string.IsNullOrEmpty(where.SpecValueName))
              //  expr = expr.And(p => p.SpecValueName == where.SpecValueName);
 #endregion
            var list = _goodsSpecValueRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _goodsSpecValueRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<GoodsSpecValueEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<GoodsSpecValueEntity> GetList(GoodsSpecValueEntity where)
        {
               var expr = PredicateBuilder.True<GoodsSpecValueEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.GoodsId))
              //  expr = expr.And(p => p.GoodsId == where.GoodsId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.SpecId))
              //  expr = expr.And(p => p.SpecId == where.SpecId);
              // if (!string.IsNullOrEmpty(where.SpecName))
              //  expr = expr.And(p => p.SpecName == where.SpecName);
              // if (!string.IsNullOrEmpty(where.SpecValueId))
              //  expr = expr.And(p => p.SpecValueId == where.SpecValueId);
              // if (!string.IsNullOrEmpty(where.SpecValueName))
              //  expr = expr.And(p => p.SpecValueName == where.SpecValueName);
 #endregion
            var list = _goodsSpecValueRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

