

 /***************************************************************************
 *       功能：     CMCustomer实体类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     会员信息
 * *************************************************************************/
using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.CustomerManager
{
    public class CustomerEntity: Entity
    { 
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CardNo{get; set;}

        public virtual System.Int32 CardTypeId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual System.String Password{get; set;}
        /// <summary>
        /// 会员名称
        /// </summary>
        public virtual System.String CustomerName{get; set;}
        /// <summary>
        /// 性别
        /// </summary>
        public virtual System.String Gender{get; set;}
        /// <summary>
        /// 生日
        /// </summary>
        public virtual System.String Birthday{get; set;}
        /// <summary>
        /// 邮件
        /// </summary>
        public virtual System.String Email{get; set;}
        /// <summary>
        /// 家庭电话
        /// </summary>
        public virtual System.String Familytelephone{get; set;}
        /// <summary>
        /// 邮编
        /// </summary>
        public virtual System.String Postcode{get; set;}
        /// <summary>
        /// 手机
        /// </summary>
        public virtual System.String Mobilephone{get; set;}
        /// <summary>
        /// 居住地址   省
        /// </summary>
        public virtual System.String ProvinceId{get; set;}
        /// <summary>
        /// 居住地址   市
        /// </summary>
        public virtual System.String CityId{get; set;}
        /// <summary>
        /// 居住地址   区（新增）
        /// </summary>
        public virtual System.String AreaId { get; set;}
        /// <summary>
        /// 居住地址   详细地址
        /// </summary>
        public virtual System.String Address{get; set;}  public virtual System.String AddressFull { get; set;}
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Memo{get; set;}
        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual System.Int32? Discount{get; set;}
        /// <summary>
        /// 消费总金额
        /// </summary>
        public virtual System.Decimal? Totalamount{get; set;}
        /// <summary>
        /// 总积分
        /// </summary>
        public virtual System.Int32? Totalpoints{get; set;}
        /// <summary>
        /// 可用积分
        /// </summary>
        public virtual System.Int32? Availablepoints{get; set;}
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime{get; set;}
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode{get; set;}
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime{get; set;}
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode{get; set;}
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

    
 

