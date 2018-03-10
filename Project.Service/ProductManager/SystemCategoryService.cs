
/***************************************************************************
*       功能：     PRMSystemCategory业务处理层
*       作者：     李伟伟
*       日期：     2017/6/30
*       描述：     系统分类
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class SystemCategoryService
    {

        #region 构造函数
        private readonly SystemCategoryRepository _systemCategoryRepository;

        private readonly SystemCategoryAttributeRepository _systemCategoryAttributeRepository;

        private readonly SystemCategoryBrandRepository _systemCategoryBrandRepository;

        private readonly SystemCategorySpecRepository _systemCategorySpecRepository;

        private static readonly SystemCategoryService Instance = new SystemCategoryService();

        public SystemCategoryService()
        {
            this._systemCategoryRepository = new SystemCategoryRepository();

            this._systemCategoryAttributeRepository = new SystemCategoryAttributeRepository();
            this._systemCategoryBrandRepository = new SystemCategoryBrandRepository();
            this._systemCategorySpecRepository = new SystemCategorySpecRepository();

        }

        public static SystemCategoryService GetInstance()
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
        public System.Int32 Add(SystemCategoryEntity entity)
        {

            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var pkId = _systemCategoryRepository.Save(entity);
                    entity.SystemCategoryAttributeList.ToList().ForEach(p =>
                    {
                        p.SystemCategoryId = pkId;
                    });

                    entity.SystemCategorySpecList.ToList().ForEach(p =>
                    {
                        p.SystemCategoryId = pkId;
                    });

                    entity.SystemCategoryBrandList.ToList().ForEach(p =>
                    {
                        p.SystemCategoryId = pkId;
                    });

                    tx.Commit();
                    return pkId;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _systemCategoryRepository.GetById(pkId);
                _systemCategoryRepository.Delete(entity);
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
        public bool Delete(SystemCategoryEntity entity)
        {
            try
            {
                _systemCategoryRepository.Delete(entity);
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
        public bool Update(SystemCategoryEntity entity)
        {
            var oldEntity = this.GetModelByPk(entity.PkId);

            entity.SystemCategoryAttributeList.ToList().ForEach(p =>
            {
                p.SystemCategoryId = entity.PkId;
            });
            var deleteAttributeList = oldEntity.SystemCategoryAttributeList.Where(p => entity.SystemCategoryAttributeList.All(x => x.PkId != p.PkId)).ToList();

            entity.SystemCategorySpecList.ToList().ForEach(p =>
            {
                p.SystemCategoryId = entity.PkId;
            });
            var deleteSpecList = oldEntity.SystemCategorySpecList.Where(p => entity.SystemCategorySpecList.All(x => x.PkId != p.PkId)).ToList();

            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    _systemCategoryRepository.Merge(entity);

                    deleteAttributeList.ForEach(p => { _systemCategoryAttributeRepository.Delete(p); });

                    deleteSpecList.ForEach(p => { _systemCategorySpecRepository.Delete(p); });

                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public SystemCategoryEntity GetModelByPk(System.Int32 pkId)
        {
            return _systemCategoryRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【系统分类】和总【系统分类】数</returns>
        public System.Tuple<IList<SystemCategoryEntity>, int> Search(SystemCategoryEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<SystemCategoryEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.SystemCategoryName))
                expr = expr.And(p => p.SystemCategoryName == where.SystemCategoryName);
            // if (!string.IsNullOrEmpty(where.Sort))
            //  expr = expr.And(p => p.Sort == where.Sort);
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
            var list = _systemCategoryRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _systemCategoryRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<SystemCategoryEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<SystemCategoryEntity> GetList(SystemCategoryEntity where)
        {
            var expr = PredicateBuilder.True<SystemCategoryEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.SystemCategoryName))
            //  expr = expr.And(p => p.SystemCategoryName == where.SystemCategoryName);
            // if (!string.IsNullOrEmpty(where.Sort))
            //  expr = expr.And(p => p.Sort == where.Sort);
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
            var list = _systemCategoryRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}




