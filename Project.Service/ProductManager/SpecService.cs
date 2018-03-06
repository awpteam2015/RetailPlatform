
 /***************************************************************************
 *       功能：     PRMSpec业务处理层
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     规格表
 * *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager
{
    public class SpecService
    {
       
       #region 构造函数
        private readonly SpecRepository  _specRepository;
        private readonly SpecValueRepository _specValueRepository;
        private static readonly SpecService Instance = new SpecService();

        public SpecService()
        {
            _specValueRepository = new SpecValueRepository();
            this._specRepository =new SpecRepository();
        }

        public static  SpecService GetInstance()
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
        public System.Int32 Add(SpecEntity entity)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var pkId= _specRepository.Save(entity);
                    entity.SpecValueEntityList.ToList().ForEach(p =>
                    {
                        p.SpecId = pkId;
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
            var entity= _specRepository.GetById(pkId);
            _specRepository.Delete(entity);
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
        public bool Delete(SpecEntity entity)
        {
         try
            {
            _specRepository.Delete(entity);
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
        public bool Update(SpecEntity entity)
        {
            var oldEntity = this.GetModelByPk(entity.PkId);
            var date = DateTime.Now;
            entity.SpecValueEntityList.ToList().ForEach(p =>
            {
                //if (p.PkId <= 0)
                //{
                //}
                //else
                //{
                //    var oldRowEntity = oldEntity.SpecValueEntityList.SingleOrDefault(x => x.PkId == p.PkId);
                //}
                p.SpecId = entity.PkId;
                //p.ModuleId = entity.ModuleId;
                //p.LastModificationTime = date;
                //p.LastModifierUserCode = "";
            });

            var deleteList = oldEntity.SpecValueEntityList.Where( p => entity.SpecValueEntityList.All(x => x.PkId != p.PkId)).ToList();

            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    _specRepository.Merge(entity);

                    deleteList.ForEach(p => { _specValueRepository.Delete(p); });
                    tx.Commit();
                    return true;
                }
                catch(Exception e)
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
        public SpecEntity GetModelByPk(System.Int32 pkId)
        {
            return _specRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【规格表】和总【规格表】数</returns>
        public System.Tuple<IList<SpecEntity>, int> Search(SpecEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<SpecEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.SpecName))
                expr = expr.And(p => p.SpecName == where.SpecName);
            if (!string.IsNullOrEmpty(where.Remark))
                expr = expr.And(p => p.Remark == where.Remark);
            if (where.SpecType!=0)
                expr = expr.And(p => p.SpecType == where.SpecType);
            if (where.ShowType!=0)
                expr = expr.And(p => p.ShowType == where.ShowType);
            #endregion
            var list = _specRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _specRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<SpecEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<SpecEntity> GetList(SpecEntity where)
        {
               var expr = PredicateBuilder.True<SpecEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.SpecName))
              //  expr = expr.And(p => p.SpecName == where.SpecName);
              // if (!string.IsNullOrEmpty(where.Memo))
              //  expr = expr.And(p => p.Memo == where.Memo);
              // if (!string.IsNullOrEmpty(where.SpecType))
              //  expr = expr.And(p => p.SpecType == where.SpecType);
              // if (!string.IsNullOrEmpty(where.ShowType))
              //  expr = expr.And(p => p.ShowType == where.ShowType);
 #endregion
            var list = _specRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();

            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

