
 /***************************************************************************
 *       功能：     SMProvince业务处理层
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
    public class ProvinceService
    {
       
       #region 构造函数
        private readonly ProvinceRepository  _provinceRepository;
            private static readonly ProvinceService Instance = new ProvinceService();

        public ProvinceService()
        {
           this._provinceRepository =new ProvinceRepository();
        }
        
         public static  ProvinceService GetInstance()
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
        public System.Int32 Add(ProvinceEntity entity)
        {
            return _provinceRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _provinceRepository.GetById(pkId);
            _provinceRepository.Delete(entity);
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
        public bool Delete(ProvinceEntity entity)
        {
         try
            {
            _provinceRepository.Delete(entity);
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
        public bool Update(ProvinceEntity entity)
        {
          try
            {
            _provinceRepository.Update(entity);
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
        public ProvinceEntity GetModelByPk(System.Int32 pkId)
        {
            return _provinceRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<ProvinceEntity>, int> Search(ProvinceEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ProvinceEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.ProvinceId))
                expr = expr.And(p => p.ProvinceId == where.ProvinceId);
            if (!string.IsNullOrEmpty(where.Province))
                expr = expr.And(p => p.Province == where.Province);
            #endregion
            var list = _provinceRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _provinceRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ProvinceEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ProvinceEntity> GetList(ProvinceEntity where)
        {
               var expr = PredicateBuilder.True<ProvinceEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.ProvinceId))
              //  expr = expr.And(p => p.ProvinceId == where.ProvinceId);
              // if (!string.IsNullOrEmpty(where.Province))
              //  expr = expr.And(p => p.Province == where.Province);
 #endregion
            var list = _provinceRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

