
 /***************************************************************************
 *       功能：     PRMAttributeValue业务处理层
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     扩展属性值
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class AttributeValueService
    {
       
       #region 构造函数
        private readonly AttributeValueRepository  _attributeValueRepository;
            private static readonly AttributeValueService Instance = new AttributeValueService();

        public AttributeValueService()
        {
           this._attributeValueRepository =new AttributeValueRepository();
        }
        
         public static  AttributeValueService GetInstance()
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
        public System.Int32 Add(AttributeValueEntity entity)
        {
            return _attributeValueRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _attributeValueRepository.GetById(pkId);
            _attributeValueRepository.Delete(entity);
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
        public bool Delete(AttributeValueEntity entity)
        {
         try
            {
            _attributeValueRepository.Delete(entity);
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
        public bool Update(AttributeValueEntity entity)
        {
          try
            {
            _attributeValueRepository.Update(entity);
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
        public AttributeValueEntity GetModelByPk(System.Int32 pkId)
        {
            return _attributeValueRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【扩展属性值】和总【扩展属性值】数</returns>
        public System.Tuple<IList<AttributeValueEntity>, int> Search(AttributeValueEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<AttributeValueEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AttributeValueName))
              //  expr = expr.And(p => p.AttributeValueName == where.AttributeValueName);
              // if (!string.IsNullOrEmpty(where.AttributeId))
              //  expr = expr.And(p => p.AttributeId == where.AttributeId);
 #endregion
            var list = _attributeValueRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _attributeValueRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<AttributeValueEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<AttributeValueEntity> GetList(AttributeValueEntity where)
        {
               var expr = PredicateBuilder.True<AttributeValueEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AttributeValueName))
              //  expr = expr.And(p => p.AttributeValueName == where.AttributeValueName);
              // if (!string.IsNullOrEmpty(where.AttributeId))
              //  expr = expr.And(p => p.AttributeId == where.AttributeId);
 #endregion
            var list = _attributeValueRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

