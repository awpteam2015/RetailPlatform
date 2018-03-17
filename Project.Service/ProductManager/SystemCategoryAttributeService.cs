
 /***************************************************************************
 *       功能：     PRMSystemCategoryAttribute业务处理层
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类对应属性
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class SystemCategoryAttributeService
    {
       
       #region 构造函数
        private readonly SystemCategoryAttributeRepository  _systemCategoryAttributeRepository;
            private static readonly SystemCategoryAttributeService Instance = new SystemCategoryAttributeService();

        public SystemCategoryAttributeService()
        {
           this._systemCategoryAttributeRepository =new SystemCategoryAttributeRepository();
        }
        
         public static  SystemCategoryAttributeService GetInstance()
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
        public System.Int32 Add(SystemCategoryAttributeEntity entity)
        {
            return _systemCategoryAttributeRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _systemCategoryAttributeRepository.GetById(pkId);
            _systemCategoryAttributeRepository.Delete(entity);
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
        public bool Delete(SystemCategoryAttributeEntity entity)
        {
         try
            {
            _systemCategoryAttributeRepository.Delete(entity);
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
        public bool Update(SystemCategoryAttributeEntity entity)
        {
          try
            {
            _systemCategoryAttributeRepository.Update(entity);
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
        public SystemCategoryAttributeEntity GetModelByPk(System.Int32 pkId)
        {
            return _systemCategoryAttributeRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【系统分类对应属性】和总【系统分类对应属性】数</returns>
        public System.Tuple<IList<SystemCategoryAttributeEntity>, int> Search(SystemCategoryAttributeEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<SystemCategoryAttributeEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AttributeId))
              //  expr = expr.And(p => p.AttributeId == where.AttributeId);
              // if (!string.IsNullOrEmpty(where.SystemCategoryId))
              //  expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
              // if (!string.IsNullOrEmpty(where.IsMust))
              //  expr = expr.And(p => p.IsMust == where.IsMust);
 #endregion
            var list = _systemCategoryAttributeRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _systemCategoryAttributeRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<SystemCategoryAttributeEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<SystemCategoryAttributeEntity> GetList(SystemCategoryAttributeEntity where)
        {
               var expr = PredicateBuilder.True<SystemCategoryAttributeEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.AttributeId))
            //  expr = expr.And(p => p.AttributeId == where.AttributeId);
            if (where.SystemCategoryId>=0)
                expr = expr.And(p => p.SystemCategoryId == where.SystemCategoryId);
            // if (!string.IsNullOrEmpty(where.IsMust))
            //  expr = expr.And(p => p.IsMust == where.IsMust);
            #endregion
            var list = _systemCategoryAttributeRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

