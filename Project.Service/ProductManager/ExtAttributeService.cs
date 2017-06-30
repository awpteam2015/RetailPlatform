
 /***************************************************************************
 *       功能：     PRMExtAttribute业务处理层
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     扩展属性表
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class ExtAttributeService
    {
       
       #region 构造函数
        private readonly ExtAttributeRepository  _extAttributeRepository;
            private static readonly ExtAttributeService Instance = new ExtAttributeService();

        public ExtAttributeService()
        {
           this._extAttributeRepository =new ExtAttributeRepository();
        }
        
         public static  ExtAttributeService GetInstance()
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
        public System.Int32 Add(ExtAttributeEntity entity)
        {
            return _extAttributeRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _extAttributeRepository.GetById(pkId);
            _extAttributeRepository.Delete(entity);
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
        public bool Delete(ExtAttributeEntity entity)
        {
         try
            {
            _extAttributeRepository.Delete(entity);
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
        public bool Update(ExtAttributeEntity entity)
        {
          try
            {
            _extAttributeRepository.Update(entity);
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
        public ExtAttributeEntity GetModelByPk(System.Int32 pkId)
        {
            return _extAttributeRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【扩展属性表】和总【扩展属性表】数</returns>
        public System.Tuple<IList<ExtAttributeEntity>, int> Search(ExtAttributeEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ExtAttributeEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AttributeName))
              //  expr = expr.And(p => p.AttributeName == where.AttributeName);
              // if (!string.IsNullOrEmpty(where.OtherName))
              //  expr = expr.And(p => p.OtherName == where.OtherName);
              // if (!string.IsNullOrEmpty(where.ShowType))
              //  expr = expr.And(p => p.ShowType == where.ShowType);
              // if (!string.IsNullOrEmpty(where.AttributeValues))
              //  expr = expr.And(p => p.AttributeValues == where.AttributeValues);
 #endregion
            var list = _extAttributeRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _extAttributeRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ExtAttributeEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ExtAttributeEntity> GetList(ExtAttributeEntity where)
        {
               var expr = PredicateBuilder.True<ExtAttributeEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AttributeName))
              //  expr = expr.And(p => p.AttributeName == where.AttributeName);
              // if (!string.IsNullOrEmpty(where.OtherName))
              //  expr = expr.And(p => p.OtherName == where.OtherName);
              // if (!string.IsNullOrEmpty(where.ShowType))
              //  expr = expr.And(p => p.ShowType == where.ShowType);
              // if (!string.IsNullOrEmpty(where.AttributeValues))
              //  expr = expr.And(p => p.AttributeValues == where.AttributeValues);
 #endregion
            var list = _extAttributeRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

