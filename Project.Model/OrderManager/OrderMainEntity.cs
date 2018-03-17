

 /***************************************************************************
 *       功能：     OMOrderMain实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     订单主表信息
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.OrderManager
{
    public class OrderMainEntity: Entity
    {
        #region 属性

        public virtual System.String OrderNo { get; set; }

        /// <summary>
        /// 订单状态(作废:-1;未确认:0;确认:1;先退货审核:T;子订单部分为确认:2)
        /// </summary>
        public virtual System.String State{get; set;}
        /// <summary>
        /// 订单总价,包括赠品_decimal_
        /// </summary>
        public virtual System.Decimal? Totalamount{get; set;}
        /// <summary>
        /// 赠送积分
        /// </summary>
        public virtual System.Int32? PresentPoints{get; set;}
        /// <summary>
        /// 会员Id
        /// </summary>
        public virtual System.String CustomerId{get; set;}
        /// <summary>
        /// 会员姓名
        /// </summary>
        public virtual System.String CustomerName{get; set;}
        /// <summary>
        /// 联系人（改）
        /// </summary>
        public virtual System.String Linkman{get; set;}
        /// <summary>
        /// 联系人电话
        /// </summary>
        public virtual System.String LinkmanTel{get; set;}
        /// <summary>
        /// 联系人手机
        /// </summary>
        public virtual System.String LinkmanMobilephone{get; set;}
        /// <summary>
        /// 联系人省份
        /// </summary>
        public virtual System.String LinkmanProvinceId{get; set;}
        /// <summary>
        /// 联系人城市
        /// </summary>
        public virtual System.String LinkmanCityId{get; set;}
        /// <summary>
        /// 联系人区域(新增)
        /// </summary>
        public virtual System.String LinkmanAreaId{get; set;}
        /// <summary>
        /// 联系人配送地址（改）
        /// </summary>
        public virtual System.String LinkmanAddress{get; set;}
        /// <summary>
        /// 联系人配送地址全（改2012.11.2）
        /// </summary>
        public virtual System.String LinkmanAddressfull{get; set;}
        /// <summary>
        /// 联系人邮政编码（改）
        /// </summary>
        public virtual System.String LinkmanPostcode{get; set;}
        /// <summary>
        /// 联系人送货备注（改）
        /// </summary>
        public virtual System.String LinkmanRemark{get; set;}
        /// <summary>
        /// 送货地址id（改2012.11.20）
        /// </summary>
        public virtual System.Int32? CustomerAddressId{get; set;}
        /// <summary>
        /// 支付时间
        /// </summary>
        public virtual System.DateTime? PayTime{get; set;}
        /// <summary>
        /// 支付备注
        /// </summary>
        public virtual System.String PayRemark{get; set;}
        /// <summary>
        /// 发货时间
        /// </summary>
        public virtual System.DateTime? SendTime{get; set;}
        /// <summary>
        /// 快递单号
        /// </summary>
        public virtual System.String SendNo{get; set;}
        /// <summary>
        /// 发货备注
        /// </summary>
        public virtual System.String SendRemark{get; set;}
        /// <summary>
        /// 退货时间
        /// </summary>
        public virtual System.DateTime? ReturnTime{get; set;}
        /// <summary>
        /// 退货备注
        /// </summary>
        public virtual System.String ReturnRemark{get; set;}
        /// <summary>
        /// 客户确认时间
        /// </summary>
        public virtual System.DateTime? ConfirmTime{get; set;}
        /// <summary>
        /// 客户确认备注
        /// </summary>
        public virtual System.String ConfirmRemark{get; set;}
        /// <summary>
        /// 创建人ip
        /// </summary>
        public virtual System.String UserIp{get; set;}
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime{get; set;}
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode{get; set;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime{get; set;}
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual System.Int32? IsDeleted{get; set;}
        /// <summary>
        /// 删除人
        /// </summary>
        public virtual System.String DeleterUserCode{get; set;}
        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual System.DateTime? DeletionTime{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

