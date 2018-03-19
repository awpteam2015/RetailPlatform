
/***************************************************************************
*       功能：     OMOrderMain业务处理层
*       作者：     李伟伟
*       日期：     2018/3/18
*       描述：     订单主表信息
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.OrderManager;
using Project.Repository.OrderManager;

namespace Project.Service.OrderManager
{
    public class OrderMainService
    {

        #region 构造函数
        private readonly OrderMainRepository _orderMainRepository;
        private static readonly OrderMainService Instance = new OrderMainService();

        public OrderMainService()
        {
            this._orderMainRepository = new OrderMainRepository();
        }

        public static OrderMainService GetInstance()
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
        public Tuple<bool, string> Add(OrderMainEntity entity)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var pkId = _orderMainRepository.Save(entity);




                    tx.Commit();
                    return new Tuple<bool, string>(true, pkId);
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
        public bool DeleteByPkId(System.String pkId)
        {
            try
            {
                var entity = _orderMainRepository.GetById(pkId);
                _orderMainRepository.Delete(entity);
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
        public bool Delete(OrderMainEntity entity)
        {
            try
            {
                _orderMainRepository.Delete(entity);
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
        public bool Update(OrderMainEntity entity)
        {
            try
            {
                _orderMainRepository.Update(entity);
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
        public OrderMainEntity GetModelByPk(System.String pkId)
        {
            return _orderMainRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【订单主表信息】和总【订单主表信息】数</returns>
        public System.Tuple<IList<OrderMainEntity>, int> Search(OrderMainEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<OrderMainEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.OrderNo))
            //  expr = expr.And(p => p.OrderNo == where.OrderNo);
            // if (!string.IsNullOrEmpty(where.State))
            //  expr = expr.And(p => p.State == where.State);
            // if (!string.IsNullOrEmpty(where.Totalamount))
            //  expr = expr.And(p => p.Totalamount == where.Totalamount);
            // if (!string.IsNullOrEmpty(where.PresentPoints))
            //  expr = expr.And(p => p.PresentPoints == where.PresentPoints);
            // if (!string.IsNullOrEmpty(where.CustomerId))
            //  expr = expr.And(p => p.CustomerId == where.CustomerId);
            // if (!string.IsNullOrEmpty(where.CustomerName))
            //  expr = expr.And(p => p.CustomerName == where.CustomerName);
            // if (!string.IsNullOrEmpty(where.Linkman))
            //  expr = expr.And(p => p.Linkman == where.Linkman);
            // if (!string.IsNullOrEmpty(where.LinkmanTel))
            //  expr = expr.And(p => p.LinkmanTel == where.LinkmanTel);
            // if (!string.IsNullOrEmpty(where.LinkmanMobilephone))
            //  expr = expr.And(p => p.LinkmanMobilephone == where.LinkmanMobilephone);
            // if (!string.IsNullOrEmpty(where.LinkmanProvinceId))
            //  expr = expr.And(p => p.LinkmanProvinceId == where.LinkmanProvinceId);
            // if (!string.IsNullOrEmpty(where.LinkmanCityId))
            //  expr = expr.And(p => p.LinkmanCityId == where.LinkmanCityId);
            // if (!string.IsNullOrEmpty(where.LinkmanAreaId))
            //  expr = expr.And(p => p.LinkmanAreaId == where.LinkmanAreaId);
            // if (!string.IsNullOrEmpty(where.LinkmanAddress))
            //  expr = expr.And(p => p.LinkmanAddress == where.LinkmanAddress);
            // if (!string.IsNullOrEmpty(where.LinkmanAddressfull))
            //  expr = expr.And(p => p.LinkmanAddressfull == where.LinkmanAddressfull);
            // if (!string.IsNullOrEmpty(where.LinkmanPostcode))
            //  expr = expr.And(p => p.LinkmanPostcode == where.LinkmanPostcode);
            // if (!string.IsNullOrEmpty(where.LinkmanRemark))
            //  expr = expr.And(p => p.LinkmanRemark == where.LinkmanRemark);
            // if (!string.IsNullOrEmpty(where.CustomerAddressId))
            //  expr = expr.And(p => p.CustomerAddressId == where.CustomerAddressId);
            // if (!string.IsNullOrEmpty(where.PayTime))
            //  expr = expr.And(p => p.PayTime == where.PayTime);
            // if (!string.IsNullOrEmpty(where.PayRemark))
            //  expr = expr.And(p => p.PayRemark == where.PayRemark);
            // if (!string.IsNullOrEmpty(where.SendTime))
            //  expr = expr.And(p => p.SendTime == where.SendTime);
            // if (!string.IsNullOrEmpty(where.SendNo))
            //  expr = expr.And(p => p.SendNo == where.SendNo);
            // if (!string.IsNullOrEmpty(where.SendRemark))
            //  expr = expr.And(p => p.SendRemark == where.SendRemark);
            // if (!string.IsNullOrEmpty(where.ReturnReason))
            //  expr = expr.And(p => p.ReturnReason == where.ReturnReason);
            // if (!string.IsNullOrEmpty(where.ReturnNo))
            //  expr = expr.And(p => p.ReturnNo == where.ReturnNo);
            // if (!string.IsNullOrEmpty(where.ReturnState))
            //  expr = expr.And(p => p.ReturnState == where.ReturnState);
            // if (!string.IsNullOrEmpty(where.ReturnTime))
            //  expr = expr.And(p => p.ReturnTime == where.ReturnTime);
            // if (!string.IsNullOrEmpty(where.ReturnRemark))
            //  expr = expr.And(p => p.ReturnRemark == where.ReturnRemark);
            // if (!string.IsNullOrEmpty(where.ConfirmTime))
            //  expr = expr.And(p => p.ConfirmTime == where.ConfirmTime);
            // if (!string.IsNullOrEmpty(where.ConfirmRemark))
            //  expr = expr.And(p => p.ConfirmRemark == where.ConfirmRemark);
            // if (!string.IsNullOrEmpty(where.UserIp))
            //  expr = expr.And(p => p.UserIp == where.UserIp);
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
            var list = _orderMainRepository.Query().Where(expr).OrderByDescending(p => p.OrderNo).Skip(skipResults).Take(maxResults).ToList();
            var count = _orderMainRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<OrderMainEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<OrderMainEntity> GetList(OrderMainEntity where)
        {
            var expr = PredicateBuilder.True<OrderMainEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.OrderNo))
            //  expr = expr.And(p => p.OrderNo == where.OrderNo);
            // if (!string.IsNullOrEmpty(where.State))
            //  expr = expr.And(p => p.State == where.State);
            // if (!string.IsNullOrEmpty(where.Totalamount))
            //  expr = expr.And(p => p.Totalamount == where.Totalamount);
            // if (!string.IsNullOrEmpty(where.PresentPoints))
            //  expr = expr.And(p => p.PresentPoints == where.PresentPoints);
            // if (!string.IsNullOrEmpty(where.CustomerId))
            //  expr = expr.And(p => p.CustomerId == where.CustomerId);
            // if (!string.IsNullOrEmpty(where.CustomerName))
            //  expr = expr.And(p => p.CustomerName == where.CustomerName);
            // if (!string.IsNullOrEmpty(where.Linkman))
            //  expr = expr.And(p => p.Linkman == where.Linkman);
            // if (!string.IsNullOrEmpty(where.LinkmanTel))
            //  expr = expr.And(p => p.LinkmanTel == where.LinkmanTel);
            // if (!string.IsNullOrEmpty(where.LinkmanMobilephone))
            //  expr = expr.And(p => p.LinkmanMobilephone == where.LinkmanMobilephone);
            // if (!string.IsNullOrEmpty(where.LinkmanProvinceId))
            //  expr = expr.And(p => p.LinkmanProvinceId == where.LinkmanProvinceId);
            // if (!string.IsNullOrEmpty(where.LinkmanCityId))
            //  expr = expr.And(p => p.LinkmanCityId == where.LinkmanCityId);
            // if (!string.IsNullOrEmpty(where.LinkmanAreaId))
            //  expr = expr.And(p => p.LinkmanAreaId == where.LinkmanAreaId);
            // if (!string.IsNullOrEmpty(where.LinkmanAddress))
            //  expr = expr.And(p => p.LinkmanAddress == where.LinkmanAddress);
            // if (!string.IsNullOrEmpty(where.LinkmanAddressfull))
            //  expr = expr.And(p => p.LinkmanAddressfull == where.LinkmanAddressfull);
            // if (!string.IsNullOrEmpty(where.LinkmanPostcode))
            //  expr = expr.And(p => p.LinkmanPostcode == where.LinkmanPostcode);
            // if (!string.IsNullOrEmpty(where.LinkmanRemark))
            //  expr = expr.And(p => p.LinkmanRemark == where.LinkmanRemark);
            // if (!string.IsNullOrEmpty(where.CustomerAddressId))
            //  expr = expr.And(p => p.CustomerAddressId == where.CustomerAddressId);
            // if (!string.IsNullOrEmpty(where.PayTime))
            //  expr = expr.And(p => p.PayTime == where.PayTime);
            // if (!string.IsNullOrEmpty(where.PayRemark))
            //  expr = expr.And(p => p.PayRemark == where.PayRemark);
            // if (!string.IsNullOrEmpty(where.SendTime))
            //  expr = expr.And(p => p.SendTime == where.SendTime);
            // if (!string.IsNullOrEmpty(where.SendNo))
            //  expr = expr.And(p => p.SendNo == where.SendNo);
            // if (!string.IsNullOrEmpty(where.SendRemark))
            //  expr = expr.And(p => p.SendRemark == where.SendRemark);
            // if (!string.IsNullOrEmpty(where.ReturnReason))
            //  expr = expr.And(p => p.ReturnReason == where.ReturnReason);
            // if (!string.IsNullOrEmpty(where.ReturnNo))
            //  expr = expr.And(p => p.ReturnNo == where.ReturnNo);
            // if (!string.IsNullOrEmpty(where.ReturnState))
            //  expr = expr.And(p => p.ReturnState == where.ReturnState);
            // if (!string.IsNullOrEmpty(where.ReturnTime))
            //  expr = expr.And(p => p.ReturnTime == where.ReturnTime);
            // if (!string.IsNullOrEmpty(where.ReturnRemark))
            //  expr = expr.And(p => p.ReturnRemark == where.ReturnRemark);
            // if (!string.IsNullOrEmpty(where.ConfirmTime))
            //  expr = expr.And(p => p.ConfirmTime == where.ConfirmTime);
            // if (!string.IsNullOrEmpty(where.ConfirmRemark))
            //  expr = expr.And(p => p.ConfirmRemark == where.ConfirmRemark);
            // if (!string.IsNullOrEmpty(where.UserIp))
            //  expr = expr.And(p => p.UserIp == where.UserIp);
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
            var list = _orderMainRepository.Query().Where(expr).OrderBy(p => p.OrderNo).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}




