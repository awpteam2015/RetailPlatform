
 /***************************************************************************
 *       功能：     RMMessageTag业务处理层
 *       作者：     李伟伟
 *       日期：     2016/9/11
 *       描述：     消息是否已读标记
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.RiverManager;
using Project.Repository.RiverManager;

namespace Project.Service.RiverManager
{
    public class MessageTagService
    {
       
       #region 构造函数
        private readonly MessageTagRepository  _messageTagRepository;
            private static readonly MessageTagService Instance = new MessageTagService();

        public MessageTagService()
        {
           this._messageTagRepository =new MessageTagRepository();
        }
        
         public static  MessageTagService GetInstance()
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
        public System.Int32 Add(MessageTagEntity entity)
        {
            return _messageTagRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _messageTagRepository.GetById(pkId);
            _messageTagRepository.Delete(entity);
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
        public bool Delete(MessageTagEntity entity)
        {
         try
            {
            _messageTagRepository.Delete(entity);
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
        public bool Update(MessageTagEntity entity)
        {
          try
            {
            _messageTagRepository.Update(entity);
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
        public MessageTagEntity GetModelByPk(System.Int32 pkId)
        {
            return _messageTagRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【消息是否已读标记】和总【消息是否已读标记】数</returns>
        public System.Tuple<IList<MessageTagEntity>, int> Search(MessageTagEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<MessageTagEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.Kind))
              //  expr = expr.And(p => p.Kind == where.Kind);
              // if (!string.IsNullOrEmpty(where.UserCode))
              //  expr = expr.And(p => p.UserCode == where.UserCode);
              // if (!string.IsNullOrEmpty(where.UserName))
              //  expr = expr.And(p => p.UserName == where.UserName);
              // if (!string.IsNullOrEmpty(where.MessageId))
              //  expr = expr.And(p => p.MessageId == where.MessageId);
 #endregion
            var list = _messageTagRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _messageTagRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<MessageTagEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<MessageTagEntity> GetList(MessageTagEntity where)
        {
               var expr = PredicateBuilder.True<MessageTagEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.Kind))
              //  expr = expr.And(p => p.Kind == where.Kind);
              // if (!string.IsNullOrEmpty(where.UserCode))
              //  expr = expr.And(p => p.UserCode == where.UserCode);
              // if (!string.IsNullOrEmpty(where.UserName))
              //  expr = expr.And(p => p.UserName == where.UserName);
              // if (!string.IsNullOrEmpty(where.MessageId))
              //  expr = expr.And(p => p.MessageId == where.MessageId);
 #endregion
            var list = _messageTagRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        public bool IsExist(int kind,int messageId,string userCode)
        {
            var expr = PredicateBuilder.True<MessageTagEntity>();
            expr = expr.And(p => p.Kind == kind);
            expr = expr.And(p => p.MessageId == messageId);
            expr = expr.And(p => p.UserCode == userCode);
            var list = _messageTagRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list.Any();
        }

        #endregion
    }
}

    
 

