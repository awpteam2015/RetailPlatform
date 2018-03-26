
/***************************************************************************
*       功能：     OMOrderMain业务处理层
*       作者：     李伟伟
*       日期：     2018/3/21
*       描述：     订单主表信息
* *************************************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Util;
using Project.Config.OrderEnum;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Model.OrderManager;
using Project.Repository.OrderManager;

namespace Project.Service.OrderManager
{
    public class OrderMainService
    {

        #region 构造函数
        private readonly OrderMainRepository _orderMainRepository;
        private readonly OrderMainDetailRepository _orderMainDetailRepository;
        private readonly OrderInvoiceRepository _orderInvoiceRepository;

        private static readonly OrderMainService Instance = new OrderMainService();

        public OrderMainService()
        {
            this._orderMainRepository = new OrderMainRepository();
            _orderMainDetailRepository = new OrderMainDetailRepository();
            _orderInvoiceRepository = new OrderInvoiceRepository();
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
        public System.String Add(OrderMainEntity entity)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    entity.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + entity.PkId;
                    var pkId = _orderMainRepository.Save(entity);
                    entity.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + entity.PkId;
                    _orderMainRepository.Update(entity);

                    entity.OrderMainDetailEntityList.ForEach(p =>
                    {
                        p.OrderNo = entity.OrderNo;
                        _orderMainDetailRepository.Save(p);
                    });

                    entity.OrderInvoiceEntity.OrderNo = entity.OrderNo;
                    _orderInvoiceRepository.Save(entity.OrderInvoiceEntity);

                    tx.Commit();

                    return entity.OrderNo;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// 确认支付
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public System.String ConfirmPay(OrderMainEntity entity)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    _orderMainRepository.Update(entity);


                    tx.Commit();
                    return entity.OrderNo;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
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
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 订单付款
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Pay(OrderMainEntity entity)
        {
            var orgInfo = OrderMainService.GetInstance().GetModelByPk(entity.PkId);
            orgInfo.PayRemark = entity.PayRemark;
            orgInfo.PayTime = DateTime.Now;
            orgInfo.PayType = entity.PayType;
            orgInfo.PaySerialNumber = entity.PaySerialNumber;
            orgInfo.State = (int)OrderStateEnum.已付款;

            try
            {
                _orderMainRepository.Update(orgInfo);
                LoggerHelper.Info(LogType.OrderLogger, OrderOpEnum.付款成功.ToString() + "|" + entity.OrderNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Cancel(OrderMainEntity entity)
        {
            var t = (OrderStateEnum)1;

            var orgInfo = OrderMainService.GetInstance().GetModelByPk(entity.PkId);
            orgInfo.CancelRemark = entity.CancelRemark;
            orgInfo.CancelTime = DateTime.Now;
            orgInfo.State = (int)OrderStateEnum.取消;

            try
            {
                _orderMainRepository.Update(orgInfo);
                LoggerHelper.Info(LogType.OrderLogger, OrderOpEnum.取消订单.ToString() + "|" + entity.OrderNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Send(OrderMainEntity entity)
        {
            var orgInfo = OrderMainService.GetInstance().GetModelByPk(entity.PkId);
            orgInfo.SendNo = entity.SendNo;
            orgInfo.SendRemark = entity.SendRemark;
            orgInfo.SendTime = entity.CreationTime;
            orgInfo.State = (int)OrderStateEnum.已发货;
            try
            {
                _orderMainRepository.Update(orgInfo);
                LoggerHelper.Info(LogType.OrderLogger, OrderOpEnum.订单发货.ToString() + "|" + entity.OrderNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        #region 未发货订单
        /// <summary>
        /// 订单退款申请
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ReturnPayNoSend(OrderMainEntity entity)
        {
            var orgInfo = OrderMainService.GetInstance().GetModelByPk(entity.PkId);
            orgInfo.ReturnPayNoSendReason = entity.ReturnPayNoSendReason;
            orgInfo.ReturnState = (int)OrderReturnStateEnum.申请退款;
            orgInfo.ReturnPayNoSendRemark = entity.ReturnPayNoSendRemark;
            orgInfo.ReturnPayNoSendTime = DateTime.Now;
            try
            {
                _orderMainRepository.Update(orgInfo);
                LoggerHelper.Info(LogType.OrderLogger, OrderOpEnum.申请退款.ToString() + "|" + entity.OrderNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 订单退款确认
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ReturnPayNoSendConfirm(OrderMainEntity entity)
        {
            var orgInfo = OrderMainService.GetInstance().GetModelByPk(entity.PkId);
            orgInfo.ReturnPayNoSendConfirmRemark = entity.ReturnPayNoSendConfirmRemark;
            orgInfo.ReturnState = (int)OrderReturnStateEnum.确认退款;
            orgInfo.ReturnPayNoSendSerialNumber = entity.ReturnPayNoSendSerialNumber;
            orgInfo.ReturnPayNoSendPayType = entity.ReturnPayNoSendPayType;
            orgInfo.ReturnPayNoSendConfirmTime = DateTime.Now;
            try
            {
                _orderMainRepository.Update(orgInfo);
                LoggerHelper.Info(LogType.OrderLogger, OrderOpEnum.确认退款.ToString() + "|" + entity.OrderNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 已发货订单相关操作
        /// <summary>
        /// 已发货订单 退货
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ReturnPrdAfterSend(OrderMainEntity entity)
        {
            var orgInfo = OrderMainService.GetInstance().GetModelByPk(entity.PkId);
            orgInfo.ReturnPrdAfterSendReason = entity.ReturnPrdAfterSendReason;
            orgInfo.ReturnPrdAfterSendRemark = entity.ReturnPrdAfterSendRemark;
            orgInfo.ReturnState = (int)OrderReturnStateEnum.申请退货;
            orgInfo.ReturnPrdAfterSendTime = DateTime.Now;

            try
            {
                _orderMainRepository.Update(orgInfo);
                LoggerHelper.Info(LogType.OrderLogger, "申请退货|" + entity.OrderNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 已发货订单 退货审核
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ReturnPrdAfterSendAudit(OrderMainEntity entity)
        {
            var orgInfo = OrderMainService.GetInstance().GetModelByPk(entity.PkId);
            orgInfo.ReturnPrdAfterSendAuditRemark = entity.ReturnPrdAfterSendAuditRemark;
            orgInfo.ReturnAuditState = entity.ReturnAuditState;
            if (orgInfo.ReturnAuditState==1)
            {
                orgInfo.ReturnState= (int)OrderReturnStateEnum.退货审核通过;
            }
            else
            {
                orgInfo.ReturnState = (int)OrderReturnStateEnum.退货审核拒绝;
            }
            orgInfo.ReturnPrdAfterSendAuditTime = DateTime.Now;

            try
            {
                _orderMainRepository.Update(orgInfo);
                LoggerHelper.Info(LogType.OrderLogger, "退货审核|" + entity.OrderNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 已发货订单 客户退货
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ReturnPrdSend(OrderMainEntity entity)
        {
            var orgInfo = OrderMainService.GetInstance().GetModelByPk(entity.PkId);
            orgInfo.ReturnPrdSendNo = entity.ReturnPrdSendNo;
            orgInfo.ReturnPrdSendRemak = entity.ReturnPrdSendRemak;
            orgInfo.ReturnState = (int)OrderReturnStateEnum.客户退货;
            orgInfo.ReturnPrdSendTime = DateTime.Now;

            try
            {
                _orderMainRepository.Update(orgInfo);
                LoggerHelper.Info(LogType.OrderLogger, "客户退货|" + entity.OrderNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

 /// <summary>
        /// 已发货订单 商家确认收货
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ReturnPrdSendConfirm(OrderMainEntity entity)
        {
            var orgInfo = OrderMainService.GetInstance().GetModelByPk(entity.PkId);
            orgInfo.ReturnPrdSendConfirmRemak = entity.ReturnPrdSendConfirmRemak;
            orgInfo.ReturnState = (int)OrderReturnStateEnum.收货确认;
            orgInfo.ReturnPrdSendConfirmTime = DateTime.Now;

            try
            {
                _orderMainRepository.Update(orgInfo);
                LoggerHelper.Info(LogType.OrderLogger, "收货确认|" + entity.OrderNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

/// <summary>
        /// 已发货订单 退款
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ReturnPayAfterSend(OrderMainEntity entity)
        {
            var orgInfo = OrderMainService.GetInstance().GetModelByPk(entity.PkId);
            orgInfo.ReturnPayAfterSendPayType = entity.ReturnPayAfterSendPayType;
            orgInfo.ReturnPayAfterSendSerialNumber = entity.ReturnPayAfterSendSerialNumber;
            orgInfo.ReturnPayAfterSendRemark = entity.ReturnPayAfterSendRemark;
            orgInfo.ReturnState = (int)OrderReturnStateEnum.确认退款;
            orgInfo.ReturnPayAfterSendTime = DateTime.Now;

            try
            {
                _orderMainRepository.Update(orgInfo);
                LoggerHelper.Info(LogType.OrderLogger, "确认退款|" + entity.OrderNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion




        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public OrderMainEntity GetModelByPk(System.Int32 pkId)
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
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.OrderNo))
                expr = expr.And(p => p.OrderNo == where.OrderNo);
            if (where.State>0)
                expr = expr.And(p => p.State == where.State);
            // if (!string.IsNullOrEmpty(where.Totalamount))
            //  expr = expr.And(p => p.Totalamount == where.Totalamount);
            // if (!string.IsNullOrEmpty(where.PresentPoints))
            //  expr = expr.And(p => p.PresentPoints == where.PresentPoints);
            // if (!string.IsNullOrEmpty(where.CustomerId))
            //  expr = expr.And(p => p.CustomerId == where.CustomerId);
            if (!string.IsNullOrEmpty(where.CustomerName))
                expr = expr.And(p => p.CustomerName == where.CustomerName);
            // if (!string.IsNullOrEmpty(where.Linkman))
            //  expr = expr.And(p => p.Linkman == where.Linkman);
            // if (!string.IsNullOrEmpty(where.LinkmanTel))
            //  expr = expr.And(p => p.LinkmanTel == where.LinkmanTel);
            if (!string.IsNullOrEmpty(where.LinkmanMobilephone))
                expr = expr.And(p => p.LinkmanMobilephone == where.LinkmanMobilephone);
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
            if (!string.IsNullOrEmpty(where.SendNo))
                expr = expr.And(p => p.SendNo == where.SendNo);
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
            var list = _orderMainRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
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
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
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
            var list = _orderMainRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public OrderMainEntity GetOrderMainByOrderNo(string orderNo)
        {
            return _orderMainRepository.Query().Where(p => p.OrderNo == orderNo).FirstOrDefault();
        }

        #endregion
    }
}




