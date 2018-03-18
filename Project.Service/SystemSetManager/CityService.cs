
 /***************************************************************************
 *       功能：     SMCity业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.SystemSetManager;
using Project.Repository.SystemSetManager;

namespace Project.Service.SystemSetManager
{
    public class CityService
    {
       
       #region 构造函数
        private readonly CityRepository  _cityRepository;
            private static readonly CityService Instance = new CityService();

        public CityService()
        {
           this._cityRepository =new CityRepository();
        }
        
         public static  CityService GetInstance()
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
        public System.Int32 Add(CityEntity entity)
        {
            return _cityRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _cityRepository.GetById(pkId);
            _cityRepository.Delete(entity);
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
        public bool Delete(CityEntity entity)
        {
         try
            {
            _cityRepository.Delete(entity);
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
        public bool Update(CityEntity entity)
        {
          try
            {
            _cityRepository.Update(entity);
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
        public CityEntity GetModelByPk(System.Int32 pkId)
        {
            return _cityRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<CityEntity>, int> Search(CityEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<CityEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.CityId))
              //  expr = expr.And(p => p.CityId == where.CityId);
              // if (!string.IsNullOrEmpty(where.City))
              //  expr = expr.And(p => p.City == where.City);
              // if (!string.IsNullOrEmpty(where.ProvinceId))
              //  expr = expr.And(p => p.ProvinceId == where.ProvinceId);
 #endregion
            var list = _cityRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _cityRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<CityEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<CityEntity> GetList(CityEntity where)
        {
               var expr = PredicateBuilder.True<CityEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.CityId))
                expr = expr.And(p => p.CityId == where.CityId);
            // if (!string.IsNullOrEmpty(where.City))
            //  expr = expr.And(p => p.City == where.City);
            // if (!string.IsNullOrEmpty(where.ProvinceId))
            //  expr = expr.And(p => p.ProvinceId == where.ProvinceId);
            #endregion
            var list = _cityRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

