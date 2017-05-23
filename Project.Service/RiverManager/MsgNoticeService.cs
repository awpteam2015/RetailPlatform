
 /***************************************************************************
 *       功能：     RMMsgNotice业务处理层
 *       作者：     李伟伟
 *       日期：     2016/7/30
 *       描述：     消息通知
 * *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Util;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.RiverManager;
using Project.Repository.RiverManager;

namespace Project.Service.RiverManager
{
    public class MsgNoticeService
    {
       
       #region 构造函数
        private readonly MsgNoticeRepository  _msgNoticeRepository;
            private static readonly MsgNoticeService Instance = new MsgNoticeService();

        public MsgNoticeService()
        {
           this._msgNoticeRepository =new MsgNoticeRepository();
        }
        
         public static  MsgNoticeService GetInstance()
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
        public System.Int32 Add(MsgNoticeEntity entity)
        {
            return _msgNoticeRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _msgNoticeRepository.GetById(pkId);
            _msgNoticeRepository.Delete(entity);
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
        public bool Delete(MsgNoticeEntity entity)
        {
         try
            {
            _msgNoticeRepository.Delete(entity);
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
        public bool Update(MsgNoticeEntity entity)
        {
          try
            {
            _msgNoticeRepository.Update(entity);
         return true;
        }
        catch(Exception e)
        {
         return false;
        }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public MsgNoticeEntity GetModelByPk(System.Int32 pkId)
        {
            return _msgNoticeRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【消息通知】和总【消息通知】数</returns>
        public System.Tuple<IList<MsgNoticeEntity>, int> Search(MsgNoticeEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<MsgNoticeEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.Title))
                expr = expr.And(p => p.Title.Contains(where.Title));
            // if (!string.IsNullOrEmpty(where.Des))
            //  expr = expr.And(p => p.Des == where.Des);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.IsDelete))
             expr = expr.And(p => p.IsDelete == 0);
            // if (!string.IsNullOrEmpty(where.IsSend))
            //  expr = expr.And(p => p.IsSend == where.IsSend);
            // if (!string.IsNullOrEmpty(where.SendTime))
            //  expr = expr.And(p => p.SendTime == where.SendTime);

            if (!string.IsNullOrEmpty(where.BelongCompanys))
            {
                var newx = PredicateBuilder.True<MsgNoticeEntity>();

                var arr = where.BelongCompanys.Split(',');
                arr.ForEach(x =>
                {
                    newx = newx.Or(p => p.BelongCompanys.Contains(x));
                });

                expr = expr.And(newx);
            }


            #endregion
            var list = _msgNoticeRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _msgNoticeRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<MsgNoticeEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<MsgNoticeEntity> GetList(MsgNoticeEntity where)
        {
               var expr = PredicateBuilder.True<MsgNoticeEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.Title))
              //  expr = expr.And(p => p.Title == where.Title);
              // if (!string.IsNullOrEmpty(where.Des))
              //  expr = expr.And(p => p.Des == where.Des);
              // if (!string.IsNullOrEmpty(where.CreationTime))
              //  expr = expr.And(p => p.CreationTime == where.CreationTime);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.LastModificationTime))
              //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
              // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
              //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
              // if (!string.IsNullOrEmpty(where.IsDelete))
              //  expr = expr.And(p => p.IsDelete == where.IsDelete);
              // if (!string.IsNullOrEmpty(where.IsSend))
              //  expr = expr.And(p => p.IsSend == where.IsSend);
              // if (!string.IsNullOrEmpty(where.SendTime))
              //  expr = expr.And(p => p.SendTime == where.SendTime);
 #endregion
            var list = _msgNoticeRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

