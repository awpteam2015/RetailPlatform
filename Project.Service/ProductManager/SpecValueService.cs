
 /***************************************************************************
 *       功能：     PRMSpecValue业务处理层
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     规格值
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class SpecValueService
    {
       
       #region 构造函数
        private readonly SpecValueRepository  _specValueRepository;
            private static readonly SpecValueService Instance = new SpecValueService();

        public SpecValueService()
        {
           this._specValueRepository =new SpecValueRepository();
        }
        
         public static  SpecValueService GetInstance()
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
        public System.Int32 Add(SpecValueEntity entity)
        {
            return _specValueRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _specValueRepository.GetById(pkId);
            _specValueRepository.Delete(entity);
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
        public bool Delete(SpecValueEntity entity)
        {
         try
            {
            _specValueRepository.Delete(entity);
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
        public bool Update(SpecValueEntity entity)
        {
          try
            {
            _specValueRepository.Update(entity);
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
        public SpecValueEntity GetModelByPk(System.Int32 pkId)
        {
            return _specValueRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【规格值】和总【规格值】数</returns>
        public System.Tuple<IList<SpecValueEntity>, int> Search(SpecValueEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<SpecValueEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.SpecId))
              //  expr = expr.And(p => p.SpecId == where.SpecId);
              // if (!string.IsNullOrEmpty(where.SpecValueName))
              //  expr = expr.And(p => p.SpecValueName == where.SpecValueName);
              // if (!string.IsNullOrEmpty(where.Sort))
              //  expr = expr.And(p => p.Sort == where.Sort);
              // if (!string.IsNullOrEmpty(where.ImagePath))
              //  expr = expr.And(p => p.ImagePath == where.ImagePath);
 #endregion
            var list = _specValueRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _specValueRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<SpecValueEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<SpecValueEntity> GetList(SpecValueEntity where)
        {
               var expr = PredicateBuilder.True<SpecValueEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.SpecId))
              //  expr = expr.And(p => p.SpecId == where.SpecId);
              // if (!string.IsNullOrEmpty(where.SpecValueName))
              //  expr = expr.And(p => p.SpecValueName == where.SpecValueName);
              // if (!string.IsNullOrEmpty(where.Sort))
              //  expr = expr.And(p => p.Sort == where.Sort);
              // if (!string.IsNullOrEmpty(where.ImagePath))
              //  expr = expr.And(p => p.ImagePath == where.ImagePath);
 #endregion
            var list = _specValueRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

