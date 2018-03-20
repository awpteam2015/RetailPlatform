
 /***************************************************************************
 *       功能：     SMAuthCode业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/20
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
    public class AuthCodeService
    {
       
       #region 构造函数
        private readonly AuthCodeRepository  _authCodeRepository;
            private static readonly AuthCodeService Instance = new AuthCodeService();

        public AuthCodeService()
        {
           this._authCodeRepository =new AuthCodeRepository();
        }
        
         public static  AuthCodeService GetInstance()
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
        public System.Int32 Add(AuthCodeEntity entity)
        {
            return _authCodeRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _authCodeRepository.GetById(pkId);
            _authCodeRepository.Delete(entity);
             return true;
        }
        catch(Exception e)
        {
         return false;
        }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(AuthCodeEntity entity)
        {
         try
            {
            _authCodeRepository.Delete(entity);
             return true;
        }
         catch(Exception e)
        {
         return false;
        }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(AuthCodeEntity entity)
        {
          try
            {
            _authCodeRepository.Update(entity);
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
        public AuthCodeEntity GetModelByPk(System.Int32 pkId)
        {
            return _authCodeRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<AuthCodeEntity>, int> Search(AuthCodeEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<AuthCodeEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AuthType))
              //  expr = expr.And(p => p.AuthType == where.AuthType);
              // if (!string.IsNullOrEmpty(where.SendType))
              //  expr = expr.And(p => p.SendType == where.SendType);
              // if (!string.IsNullOrEmpty(where.ReciviceUser))
              //  expr = expr.And(p => p.ReciviceUser == where.ReciviceUser);
              // if (!string.IsNullOrEmpty(where.AuthCode))
              //  expr = expr.And(p => p.AuthCode == where.AuthCode);
              // if (!string.IsNullOrEmpty(where.CreateDate))
              //  expr = expr.And(p => p.CreateDate == where.CreateDate);
 #endregion
            var list = _authCodeRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _authCodeRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<AuthCodeEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<AuthCodeEntity> GetList(AuthCodeEntity where)
        {
               var expr = PredicateBuilder.True<AuthCodeEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AuthType))
              //  expr = expr.And(p => p.AuthType == where.AuthType);
              // if (!string.IsNullOrEmpty(where.SendType))
              //  expr = expr.And(p => p.SendType == where.SendType);
              // if (!string.IsNullOrEmpty(where.ReciviceUser))
              //  expr = expr.And(p => p.ReciviceUser == where.ReciviceUser);
              // if (!string.IsNullOrEmpty(where.AuthCode))
              //  expr = expr.And(p => p.AuthCode == where.AuthCode);
              // if (!string.IsNullOrEmpty(where.CreateDate))
              //  expr = expr.And(p => p.CreateDate == where.CreateDate);
 #endregion
            var list = _authCodeRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

