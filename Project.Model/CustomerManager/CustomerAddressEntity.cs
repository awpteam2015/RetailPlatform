

 /***************************************************************************
 *       功能：     CMCustomerAddress实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     送货地址簿
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.CustomerManager
{
    public class CustomerAddressEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? CustomerId{get; set;}
        /// <summary>
        /// 送货地址  省
        /// </summary>
        public virtual System.String Province{get; set;}
        /// <summary>
        /// 送货地址   市
        /// </summary>
        public virtual System.String CityId{get; set;}
        /// <summary>
        /// 送货地址   区（新增）
        /// </summary>
        public virtual System.String CountryId{get; set;}
        /// <summary>
        /// 送货地址   详细地址
        /// </summary>
        public virtual System.String Address{get; set;}
        /// <summary>
        /// 是否是默认地址
        /// </summary>
        public virtual System.Int32? IsDefault{get; set;}
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remarks{get; set;}
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public virtual System.String ReceiverName{get; set;}
        /// <summary>
        /// 电话
        /// </summary>
        public virtual System.String FamilyTelephone{get; set;}
        /// <summary>
        /// 邮编
        /// </summary>
        public virtual System.String PostCode{get; set;}
        /// <summary>
        /// 手机
        /// </summary>
        public virtual System.String Mobilephone{get; set;}
		#endregion
        

        #region 新增属性
        
        #endregion
    }
}

    
 

