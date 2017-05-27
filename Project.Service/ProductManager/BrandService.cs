
 /***************************************************************************
 *       功能：     PRMBrand业务处理层
 *       作者：     李伟伟
 *       日期：     2017/5/27
 *       描述：     品牌
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class BrandService
    {
       
       #region 构造函数
        private readonly BrandRepository  _brandRepository;
            private static readonly BrandService Instance = new BrandService();

        public BrandService()
        {
           this._brandRepository =new BrandRepository();
        }
        
         public static  BrandService GetInstance()
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
        public System.Int32 Add(BrandEntity entity)
        {
            return _brandRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _brandRepository.GetById(pkId);
            _brandRepository.Delete(entity);
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
        public bool Delete(BrandEntity entity)
        {
         try
            {
            _brandRepository.Delete(entity);
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
        public bool Update(BrandEntity entity)
        {
          try
            {
            _brandRepository.Update(entity);
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
        public BrandEntity GetModelByPk(System.Int32 pkId)
        {
            return _brandRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【品牌】和总【品牌】数</returns>
        public System.Tuple<IList<BrandEntity>, int> Search(BrandEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<BrandEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.BrandName))
              //  expr = expr.And(p => p.BrandName == where.BrandName);
              // if (!string.IsNullOrEmpty(where.BrandDes))
              //  expr = expr.And(p => p.BrandDes == where.BrandDes);
 #endregion
            var list = _brandRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _brandRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<BrandEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<BrandEntity> GetList(BrandEntity where)
        {
               var expr = PredicateBuilder.True<BrandEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.BrandName))
              //  expr = expr.And(p => p.BrandName == where.BrandName);
              // if (!string.IsNullOrEmpty(where.BrandDes))
              //  expr = expr.And(p => p.BrandDes == where.BrandDes);
 #endregion
            var list = _brandRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

