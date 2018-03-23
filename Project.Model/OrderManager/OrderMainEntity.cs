

/***************************************************************************
*       功能：     OMOrderMain实体类
*       作者：     李伟伟
*       日期：     2018/3/21
*       描述：     订单主表信息
* *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Config.OrderEnum;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.OrderManager
{
    public class OrderMainEntity : Entity
    {

        public OrderMainEntity()
        {
            OrderMainDetailEntityList = new HashSet<OrderMainDetailEntity>();
            OrderInvoiceEntity = new OrderInvoiceEntity();
        }

        #region 属性
        /// <summary>
        /// 订单号
        /// </summary>
        public virtual System.String OrderNo { get; set; }
        /// <summary>
        /// 订单状态(作废:-1;未确认:0;确认:1;先退货审核:T;子订单部分为确认:2)
        /// </summary>
        public virtual System.Int32? State { get; set; }
        /// <summary>
        /// 订单总价,包括赠品_decimal_
        /// </summary>
        public virtual System.Decimal? Totalamount { get; set; }
        /// <summary>
        /// 赠送积分
        /// </summary>
        public virtual System.Int32? PresentPoints { get; set; }
        /// <summary>
        /// 会员Id
        /// </summary>
        public virtual System.String CustomerId { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public virtual System.String CustomerName { get; set; }
        /// <summary>
        /// 联系人（改）
        /// </summary>
        public virtual System.String Linkman { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public virtual System.String LinkmanTel { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public virtual System.String LinkmanMobilephone { get; set; }
        /// <summary>
        /// 联系人省份
        /// </summary>
        public virtual System.String LinkmanProvinceId { get; set; }
        /// <summary>
        /// 联系人城市
        /// </summary>
        public virtual System.String LinkmanCityId { get; set; }
        /// <summary>
        /// 联系人区域(新增)
        /// </summary>
        public virtual System.String LinkmanAreaId { get; set; }
        /// <summary>
        /// 联系人配送地址（改）
        /// </summary>
        public virtual System.String LinkmanAddress { get; set; }
        /// <summary>
        /// 联系人配送地址全（改2012.11.2）
        /// </summary>
        public virtual System.String LinkmanAddressfull { get; set; }
        /// <summary>
        /// 联系人邮政编码（改）
        /// </summary>
        public virtual System.String LinkmanPostcode { get; set; }
        /// <summary>
        /// 联系人送货备注（改）
        /// </summary>
        public virtual System.String LinkmanRemark { get; set; }
        /// <summary>
        /// 送货地址id（改2012.11.20）
        /// </summary>
        public virtual System.Int32? CustomerAddressId { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual System.Int32? PayType { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        public virtual System.String PaySerialNumber { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public virtual System.DateTime? PayTime { get; set; }
        /// <summary>
        /// 支付备注
        /// </summary>
        public virtual System.String PayRemark { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public virtual System.DateTime? SendTime { get; set; }
        /// <summary>
        /// 发货备注
        /// </summary>
        public virtual System.String SendRemark { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public virtual System.String SendNo { get; set; }
        /// <summary>
        /// 退货状态
        /// </summary>
        public virtual System.Int32 ReturnState { get; set; }


        public virtual System.Int32 ReturnAuditState { get; set; }

        /// <summary>
        /// 未发货退款申请原因
        /// </summary>
        public virtual System.String ReturnPayNoSendReason { get; set; }
        /// <summary>
        /// 未发货退款申请备注
        /// </summary>
        public virtual System.String ReturnPayNoSendRemark { get; set; }
        /// <summary>
        /// 未发货退款申请时间
        /// </summary>
        public virtual System.DateTime? ReturnPayNoSendTime { get; set; }
        /// <summary>
        /// 未发货退款申确认备注
        /// </summary>
        public virtual System.String ReturnPayNoSendConfirmRemark { get; set; }
        /// <summary>
        /// 未发货退款确认时间
        /// </summary>
        public virtual System.DateTime? ReturnPayNoSendConfirmTime { get; set; }
        /// <summary>
        /// 未发货退款财务流水
        /// </summary>
        public virtual System.String ReturnPayNoSendSerialNumber { get; set; }
        /// <summary>
        /// 未发货退款支付方式
        /// </summary>
        public virtual System.String ReturnPayNoSendPayType { get; set; }
        /// <summary>
        /// 已发货退货申请原因
        /// </summary>
        public virtual System.String ReturnPrdAfterSendReason { get; set; }
        /// <summary>
        /// 已发货退货申请备注
        /// </summary>
        public virtual System.String ReturnPrdAfterSendRemark { get; set; }
        /// <summary>
        /// 已发货退货申请时间
        /// </summary>
        public virtual System.DateTime? ReturnPrdAfterSendTime { get; set; }
        /// <summary>
        /// 已发货退货审核时间
        /// </summary>
        public virtual System.DateTime? ReturnPrdAfterSendAuditTime { get; set; }
        /// <summary>
        /// 已发货退货审核备注
        /// </summary>
        public virtual System.String ReturnPrdAfterSendAuditRemark { get; set; }
        /// <summary>
        /// 退货商品快递单号
        /// </summary>
        public virtual System.String ReturnPrdSendNo { get; set; }
        /// <summary>
        /// 退货商品发出时间
        /// </summary>
        public virtual System.DateTime? ReturnPrdSendTime { get; set; }
        /// <summary>
        /// 退货商品备注
        /// </summary>
        public virtual System.String ReturnPrdSendRemak { get; set; }
        /// <summary>
        /// 商家确认退货商品发出时间
        /// </summary>
        public virtual System.DateTime? ReturnPrdSendConfirmTime { get; set; }
        /// <summary>
        /// 商家确认退货商品备注
        /// </summary>
        public virtual System.String ReturnPrdSendConfirmRemak { get; set; }
        /// <summary>
        /// 退款财务流水
        /// </summary>
        public virtual System.String ReturnPayAfterSendSerialNumber { get; set; }
        /// <summary>
        /// 退款备注
        /// </summary>
        public virtual System.String ReturnPayAfterSendRemark { get; set; }
        /// <summary>
        /// 退款时间
        /// </summary>
        public virtual System.DateTime? ReturnPayAfterSendTime { get; set; }
        /// <summary>
        /// 退款支付方式
        /// </summary>
        public virtual System.String ReturnPayAfterSendPayType { get; set; }
        /// <summary>
        /// 客户确认时间
        /// </summary>
        public virtual System.DateTime? ConfirmTime { get; set; }
        /// <summary>
        /// 客户确认备注
        /// </summary>
        public virtual System.String ConfirmRemark { get; set; }
        /// <summary>
        /// 取消订单时间
        /// </summary>
        public virtual System.DateTime? CancelTime { get; set; }
        /// <summary>
        /// 取消订单原因
        /// </summary>
        public virtual System.String CancelRemark { get; set; }
        /// <summary>
        /// 创建人ip
        /// </summary>
        public virtual System.String UserIp { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        #endregion


        #region 新增属性

        /// <summary>
        /// 订单行项目信息
        /// </summary>
        public virtual ISet<OrderMainDetailEntity> OrderMainDetailEntityList { get; set; }

        /// <summary>
        /// 发票信息
        /// </summary>
        public virtual OrderInvoiceEntity OrderInvoiceEntity { get; set; }


        public virtual string Attr_State {
            get { return ((OrderStateEnum)State).ToString(); }
        }
        public virtual string Attr_ReturnState
        {
            get { return ((OrderReturnStateEnum)ReturnState).ToString(); }
        }
        #endregion
    }
}




