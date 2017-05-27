
 /***************************************************************************
 *       功能：     PRMSpec业务处理层
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     规格
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class SpecService
    {
       
       #region 构造函数
        private readonly SpecRepository  _specRepository;
            private static readonly SpecService Instance = new SpecService();

        public SpecService()
        {
           this._specRepository =new SpecRepository();
        }
        
         public static  SpecService GetInstance()
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
        public System.Int32 Add(SpecEntity entity)
        {
            return _specRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _specRepository.GetById(pkId);
            _specRepository.Delete(entity);
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
        public bool Delete(SpecEntity entity)
        {
         try
            {
            _specRepository.Delete(entity);
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
        public bool Update(SpecEntity entity)
        {
          try
            {
            _specRepository.Update(entity);
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
        public SpecEntity GetModelByPk(System.Int32 pkId)
        {
            return _specRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【规格】和总【规格】数</returns>
        public System.Tuple<IList<SpecEntity>, int> Search(SpecEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<SpecEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.SpecName))
              //  expr = expr.And(p => p.SpecName == where.SpecName);
 #endregion
            var list = _specRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _specRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<SpecEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<SpecEntity> GetList(SpecEntity where)
        {
               var expr = PredicateBuilder.True<SpecEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.SpecName))
              //  expr = expr.And(p => p.SpecName == where.SpecName);
 #endregion
            var list = _specRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

