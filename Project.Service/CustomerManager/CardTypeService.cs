
 /***************************************************************************
 *       功能：     CMCardType业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     会员卡类型
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.CustomerManager;
using Project.Repository.CustomerManager;

namespace Project.Service.CustomerManager
{
    public class CardTypeService
    {
       
       #region 构造函数
        private readonly CardTypeRepository  _cardTypeRepository;
            private static readonly CardTypeService Instance = new CardTypeService();

        public CardTypeService()
        {
           this._cardTypeRepository =new CardTypeRepository();
        }
        
         public static  CardTypeService GetInstance()
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
        public System.Int32 Add(CardTypeEntity entity)
        {
            return _cardTypeRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _cardTypeRepository.GetById(pkId);
            _cardTypeRepository.Delete(entity);
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
        public bool Delete(CardTypeEntity entity)
        {
         try
            {
            _cardTypeRepository.Delete(entity);
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
        public bool Update(CardTypeEntity entity)
        {
          try
            {
            _cardTypeRepository.Update(entity);
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
        public CardTypeEntity GetModelByPk(System.Int32 pkId)
        {
            return _cardTypeRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【会员卡类型】和总【会员卡类型】数</returns>
        public System.Tuple<IList<CardTypeEntity>, int> Search(CardTypeEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<CardTypeEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.CardtypeName))
              //  expr = expr.And(p => p.CardtypeName == where.CardtypeName);
              // if (!string.IsNullOrEmpty(where.Discount))
              //  expr = expr.And(p => p.Discount == where.Discount);
 #endregion
            var list = _cardTypeRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _cardTypeRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<CardTypeEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<CardTypeEntity> GetList(CardTypeEntity where)
        {
               var expr = PredicateBuilder.True<CardTypeEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.CardtypeName))
              //  expr = expr.And(p => p.CardtypeName == where.CardtypeName);
              // if (!string.IsNullOrEmpty(where.Discount))
              //  expr = expr.And(p => p.Discount == where.Discount);
 #endregion
            var list = _cardTypeRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

