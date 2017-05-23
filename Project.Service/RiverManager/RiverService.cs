
/***************************************************************************
*       功能：     RMRiver业务处理层
*       作者：     李伟伟
*       日期：     2016/7/23
*       描述：     河道信息
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.RiverManager;
using Project.Repository.RiverManager;
using Project.Service.HRManager;
using Project.Service.PermissionManager;
using EnumerableExtensions = NHibernate.Util.EnumerableExtensions;

namespace Project.Service.RiverManager
{
    public class RiverService
    {

        #region 构造函数
        private readonly RiverRepository _riverRepository;
        private readonly RiverOwerRepository _riverOwerRepository;
        private static readonly RiverService Instance = new RiverService();

        public RiverService()
        {
            this._riverRepository = new RiverRepository();
            this._riverOwerRepository = new RiverOwerRepository();
        }

        public static RiverService GetInstance()
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
        public System.Int32 Add(RiverEntity entity)
        {
            return _riverRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _riverRepository.GetById(pkId);
                _riverRepository.Delete(entity);
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
        public bool Delete(RiverEntity entity)
        {
            try
            {
                _riverRepository.Delete(entity);
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
        public bool Update(RiverEntity entity)
        {
            try
            {
                _riverRepository.Update(entity);
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
        public RiverEntity GetModelByPk(System.Int32 pkId)
        {
            return _riverRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【河道信息】和总【河道信息】数</returns>
        public System.Tuple<IList<RiverEntity>, int> Search(RiverEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<RiverEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.RiverName))
                expr = expr.And(p => p.RiverName.Contains(where.RiverName));
            if (!string.IsNullOrEmpty(where.RiverRank))
                expr = expr.And(p => p.RiverRank == where.RiverRank);
            if (!string.IsNullOrEmpty(where.DepartmentCode))
                expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);

            if (where.Attr_DepartmentCodes != null && where.Attr_DepartmentCodes.Any())
                expr = expr.And(p => where.Attr_DepartmentCodes.Contains(p.DepartmentCode));


            if (!string.IsNullOrEmpty(where.DepartmentName))
                expr = expr.And(p => p.DepartmentName.Contains(where.DepartmentName));

            if (!string.IsNullOrEmpty(where.RiverStart))
                expr = expr.And(p => p.RiverStart.Contains(where.RiverStart));
            if (!string.IsNullOrEmpty(where.RiverEnd))
                expr = expr.And(p => p.RiverEnd.Contains(where.RiverEnd));

            //if (!string.IsNullOrEmpty(where.RiverArea))
            //    expr = expr.And(p => p.RiverArea == where.RiverArea);
            // if (!string.IsNullOrEmpty(where.RiverLength))
            //  expr = expr.And(p => p.RiverLength == where.RiverLength);
            if (!string.IsNullOrEmpty(where.RiverCrossArea))
                expr = expr.And(p => p.RiverCrossArea.Contains(where.RiverCrossArea));


            var expr2 = PredicateBuilder.True<RiverOwerEntity>();
            if (!string.IsNullOrEmpty(where.UserCode))
                expr2 = expr2.And(p => p.UserCode == where.UserCode);

            if (!string.IsNullOrEmpty(where.UserName))
                expr2 = expr2.And(p => p.UserName == where.UserName);

            // if (!string.IsNullOrEmpty(where.Coords))
            //  expr = expr.And(p => p.Coords == where.Coords);
            // if (!string.IsNullOrEmpty(where.IsActive))
            //  expr = expr.And(p => p.IsActive == where.IsActive);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            #endregion

            var query = _riverRepository.Query().Where(expr);
            if (!string.IsNullOrEmpty(where.UserName) || !string.IsNullOrEmpty(where.UserCode))
            {
                query = (from a in _riverRepository.Query().Where(expr)
                         from b in _riverOwerRepository.Query().Where(expr2)
                         where a.PkId == b.RiverId
                         select new RiverEntity()
                         {
                             PkId = a.PkId,
                             RiverName = a.RiverName,
                             RiverRank = a.RiverRank,
                             RiverStart = a.RiverStart,
                             RiverLength = a.RiverLength,
                             RiverCrossArea = a.RiverCrossArea,
                             Coords = a.Coords,
                             CreationTime = a.CreationTime,
                             DepartmentCode = a.DepartmentCode,
                             DepartmentName = a.DepartmentName,
                             UserCode = b.UserCode,
                             UserName = b.UserName,
                         });
            }
            var list = query.OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();

            list.ForEach(p =>
            {
                if (p.RiverOwerList != null && p.RiverOwerList.Any())
                {
                    p.RiverOwerList.ToList().ForEach(x =>
                    {
                        //p.Attr_Lever += "," +DictionaryService.GetInstance().GetModelByKeyCode("Lever", UserInfoService.GetInstance().GetUserInfo(x.UserCode).Lever).KeyName;
                        if (UserInfoService.GetInstance().GetUserInfo(x.UserCode)!=null)
                        {
                            p.Attr_Lever += "," + UserInfoService.GetInstance().GetUserInfo(x.UserCode).Lever;
                        }
                        
                    });
                    if (p.Attr_Lever!=null)
                    {
                        p.Attr_Lever = p.Attr_Lever.Substring(1, p.Attr_Lever.Length - 1);
                    }
                  
                }
            });

            var count = query.Count();
            return new System.Tuple<IList<RiverEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RiverEntity> GetList(RiverEntity where)
        {
            var expr = PredicateBuilder.True<RiverEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.RiverName))
                expr = expr.And(p => p.RiverName == where.RiverName);
            if (!string.IsNullOrEmpty(where.RiverRank))
                expr = expr.And(p => p.RiverRank == where.RiverRank);
            // if (!string.IsNullOrEmpty(where.RiverArea))
            //  expr = expr.And(p => p.RiverArea == where.RiverArea);
            // if (!string.IsNullOrEmpty(where.RiverLength))
            //  expr = expr.And(p => p.RiverLength == where.RiverLength);
            // if (!string.IsNullOrEmpty(where.RiverCrossArea))
            //  expr = expr.And(p => p.RiverCrossArea == where.RiverCrossArea);
            // if (!string.IsNullOrEmpty(where.Coords))
            //  expr = expr.And(p => p.Coords == where.Coords);
            // if (!string.IsNullOrEmpty(where.IsActive))
            //  expr = expr.And(p => p.IsActive == where.IsActive);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            #endregion
            var list = _riverRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        public bool SetRiverChief(int riverId, string userName, string userCode, int check)
        {
            try
            {
                var riverInfo = RiverService.GetInstance().GetModelByPk(riverId);

                if (check == 1)
                {
                    new RiverOwerRepository().Save(new RiverOwerEntity()
                    {
                        RiverId = riverId,
                        RiverName = riverInfo.RiverName,
                        UserName = userName,
                        UserCode = userCode
                    });
                }
                else
                {
                    var checkEntiy =
                        new RiverOwerRepository().Query().Where(p => p.UserCode == userCode && p.RiverId == riverId).FirstOrDefault();

                    new RiverOwerRepository().Delete(checkEntiy);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        #endregion
    }
}




