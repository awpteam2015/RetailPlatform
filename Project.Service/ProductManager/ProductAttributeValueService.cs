
 /***************************************************************************
 *       功能：     PRMProductAttributeValue业务处理层
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     产品--属性值关联表
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class ProductAttributeValueService
    {
       
       #region 构造函数
        private readonly ProductAttributeValueRepository  _productAttributeValueRepository;
            private static readonly ProductAttributeValueService Instance = new ProductAttributeValueService();

        public ProductAttributeValueService()
        {
           this._productAttributeValueRepository =new ProductAttributeValueRepository();
        }
        
         public static  ProductAttributeValueService GetInstance()
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
        public System.Int32 Add(ProductAttributeValueEntity entity)
        {
            return _productAttributeValueRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _productAttributeValueRepository.GetById(pkId);
            _productAttributeValueRepository.Delete(entity);
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
        public bool Delete(ProductAttributeValueEntity entity)
        {
         try
            {
            _productAttributeValueRepository.Delete(entity);
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
        public bool Update(ProductAttributeValueEntity entity)
        {
          try
            {
            _productAttributeValueRepository.Update(entity);
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
        public ProductAttributeValueEntity GetModelByPk(System.Int32 pkId)
        {
            return _productAttributeValueRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【产品--属性值关联表】和总【产品--属性值关联表】数</returns>
        public System.Tuple<IList<ProductAttributeValueEntity>, int> Search(ProductAttributeValueEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ProductAttributeValueEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AttributeValueId))
              //  expr = expr.And(p => p.AttributeValueId == where.AttributeValueId);
              // if (!string.IsNullOrEmpty(where.AttributeValueName))
              //  expr = expr.And(p => p.AttributeValueName == where.AttributeValueName);
              // if (!string.IsNullOrEmpty(where.AttributeId))
              //  expr = expr.And(p => p.AttributeId == where.AttributeId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.ValueContent))
              //  expr = expr.And(p => p.ValueContent == where.ValueContent);
 #endregion
            var list = _productAttributeValueRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _productAttributeValueRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ProductAttributeValueEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ProductAttributeValueEntity> GetList(ProductAttributeValueEntity where)
        {
               var expr = PredicateBuilder.True<ProductAttributeValueEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AttributeValueId))
              //  expr = expr.And(p => p.AttributeValueId == where.AttributeValueId);
              // if (!string.IsNullOrEmpty(where.AttributeValueName))
              //  expr = expr.And(p => p.AttributeValueName == where.AttributeValueName);
              // if (!string.IsNullOrEmpty(where.AttributeId))
              //  expr = expr.And(p => p.AttributeId == where.AttributeId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.ValueContent))
              //  expr = expr.And(p => p.ValueContent == where.ValueContent);
 #endregion
            var list = _productAttributeValueRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

