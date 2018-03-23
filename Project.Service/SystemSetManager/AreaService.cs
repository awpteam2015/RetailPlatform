
/***************************************************************************
*       功能：     SMArea业务处理层
*       作者：     李伟伟
*       日期：     2018/3/17
*       描述：     
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.SystemSetManager;
using Project.Repository.SystemSetManager;

namespace Project.Service.SystemSetManager
{
    public class AreaService
    {

        #region 构造函数
        private readonly AreaRepository _areaRepository;
        private static readonly AreaService Instance = new AreaService();

        public AreaService()
        {
            this._areaRepository = new AreaRepository();
        }

        public static AreaService GetInstance()
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
        public System.Int32 Add(AreaEntity entity)
        {
            return _areaRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _areaRepository.GetById(pkId);
                _areaRepository.Delete(entity);
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
        public bool Delete(AreaEntity entity)
        {
            try
            {
                _areaRepository.Delete(entity);
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
        public bool Update(AreaEntity entity)
        {
            try
            {
                _areaRepository.Merge(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public AreaEntity GetModelByPk(System.Int32 pkId)
        {
            return _areaRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<AreaEntity>, int> Search(AreaEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<AreaEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.AreaId))
            //  expr = expr.And(p => p.AreaId == where.AreaId);
            if (!string.IsNullOrEmpty(where.Area))
                expr = expr.And(p => p.Area == where.Area);
            // if (!string.IsNullOrEmpty(where.CityId))
            //  expr = expr.And(p => p.CityId == where.CityId);
            // if (!string.IsNullOrEmpty(where.FirstWeightPrice))
            //  expr = expr.And(p => p.FirstWeightPrice == where.FirstWeightPrice);
            // if (!string.IsNullOrEmpty(where.SecondWeightPrice))
            //  expr = expr.And(p => p.SecondWeightPrice == where.SecondWeightPrice);
            #endregion
            var list = _areaRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _areaRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<AreaEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<AreaEntity> GetList(AreaEntity where)
        {
            var expr = PredicateBuilder.True<AreaEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.AreaId))
            //  expr = expr.And(p => p.AreaId == where.AreaId);
            // if (!string.IsNullOrEmpty(where.Area))
            //  expr = expr.And(p => p.Area == where.Area);
            if (!string.IsNullOrEmpty(where.CityId))
                expr = expr.And(p => p.CityId == where.CityId);
            // if (!string.IsNullOrEmpty(where.FirstWeightPrice))
            //  expr = expr.And(p => p.FirstWeightPrice == where.FirstWeightPrice);
            // if (!string.IsNullOrEmpty(where.SecondWeightPrice))
            //  expr = expr.And(p => p.SecondWeightPrice == where.SecondWeightPrice);
            #endregion
            var list = _areaRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}




