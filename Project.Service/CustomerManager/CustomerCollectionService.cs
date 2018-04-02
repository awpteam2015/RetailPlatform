
 /***************************************************************************
 *       功能：     CMCustomerCollection业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.CustomerManager;
using Project.Repository.CustomerManager;

namespace Project.Service.CustomerManager
{
    public class CustomerCollectionService
    {
       
       #region 构造函数
        private readonly CustomerCollectionRepository  _customerCollectionRepository;
            private static readonly CustomerCollectionService Instance = new CustomerCollectionService();

        public CustomerCollectionService()
        {
           this._customerCollectionRepository =new CustomerCollectionRepository();
        }
        
         public static  CustomerCollectionService GetInstance()
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
        public System.Int32 Add(CustomerCollectionEntity entity)
        {
            return _customerCollectionRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _customerCollectionRepository.GetById(pkId);
            _customerCollectionRepository.Delete(entity);
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
        public bool Delete(CustomerCollectionEntity entity)
        {
         try
            {
            _customerCollectionRepository.Delete(entity);
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
        public bool Update(CustomerCollectionEntity entity)
        {
          try
            {
            _customerCollectionRepository.Update(entity);
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
        public CustomerCollectionEntity GetModelByPk(System.Int32 pkId)
        {
            return _customerCollectionRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<CustomerCollectionEntity>, int> Search(CustomerCollectionEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<CustomerCollectionEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (where.CustomerId>0)
                expr = expr.And(p => p.CustomerId == where.CustomerId);
            // if (!string.IsNullOrEmpty(where.ProductId))
            //  expr = expr.And(p => p.ProductId == where.ProductId);
            // if (!string.IsNullOrEmpty(where.ProductName))
            //  expr = expr.And(p => p.ProductName == where.ProductName);
            // if (!string.IsNullOrEmpty(where.GoodsId))
            //  expr = expr.And(p => p.GoodsId == where.GoodsId);
            // if (!string.IsNullOrEmpty(where.GoodsCode))
            //  expr = expr.And(p => p.GoodsCode == where.GoodsCode);
            // if (!string.IsNullOrEmpty(where.ProductCode))
            //  expr = expr.And(p => p.ProductCode == where.ProductCode);
            // if (!string.IsNullOrEmpty(where.ImageUrl))
            //  expr = expr.And(p => p.ImageUrl == where.ImageUrl);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.DeleterUserCode))
            //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
            // if (!string.IsNullOrEmpty(where.DeletionTime))
            //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
            #endregion
            var list = _customerCollectionRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _customerCollectionRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<CustomerCollectionEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<CustomerCollectionEntity> GetList(CustomerCollectionEntity where)
        {
               var expr = PredicateBuilder.True<CustomerCollectionEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.CustomerId))
              //  expr = expr.And(p => p.CustomerId == where.CustomerId);
              // if (!string.IsNullOrEmpty(where.ProductId))
              //  expr = expr.And(p => p.ProductId == where.ProductId);
              // if (!string.IsNullOrEmpty(where.ProductName))
              //  expr = expr.And(p => p.ProductName == where.ProductName);
              // if (!string.IsNullOrEmpty(where.GoodsId))
              //  expr = expr.And(p => p.GoodsId == where.GoodsId);
              // if (!string.IsNullOrEmpty(where.GoodsCode))
              //  expr = expr.And(p => p.GoodsCode == where.GoodsCode);
              // if (!string.IsNullOrEmpty(where.ProductCode))
              //  expr = expr.And(p => p.ProductCode == where.ProductCode);
              // if (!string.IsNullOrEmpty(where.ImageUrl))
              //  expr = expr.And(p => p.ImageUrl == where.ImageUrl);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.CreationTime))
              //  expr = expr.And(p => p.CreationTime == where.CreationTime);
              // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
              //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
              // if (!string.IsNullOrEmpty(where.LastModificationTime))
              //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
              // if (!string.IsNullOrEmpty(where.IsDeleted))
              //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
              // if (!string.IsNullOrEmpty(where.DeleterUserCode))
              //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
              // if (!string.IsNullOrEmpty(where.DeletionTime))
              //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
 #endregion
            var list = _customerCollectionRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

