

/***************************************************************************
*       功能：     RMRiverProblemApply实体类
*       作者：     李伟伟
*       日期：     2016/7/24
*       描述：     河道问题申请单
* *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Interface;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.RiverManager
{
    public class RiverProblemApplyEntity : Entity, IAudited
    {
        public RiverProblemApplyEntity()
        {
            RiverEntity = new RiverEntity();
        }

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Title { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public virtual System.String ApplyManName { get; set; }

        /// <summary>
        /// 申请人电话
        /// </summary>
        public virtual System.String ApplyManTel { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        public virtual System.String Des { get; set; }
        /// <summary>
        /// 问题类型 1日常巡河 2问题上报
        /// </summary>
        public virtual System.Int32 ProblemType { get; set; }
        /// <summary>
        /// 图片地址 多个
        /// </summary>
        public virtual System.String PicUrl1 { get; set; }

        public virtual System.String PicUrl2 { get; set; }

        public virtual System.String PicUrl3 { get; set; }

        public virtual System.String FinishPicUrl { get; set; }

        public virtual System.String FinishPicUrl2 { get; set; }

        public virtual System.String FinishPicUrl3 { get; set; }


        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual System.String DepartmentCode { get; set; }

        public virtual System.String DepartmentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? RiverId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String RiverName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String UserName { get; set; }
        /// <summary>
        /// 坐标
        /// </summary>
        public virtual System.String Coords { get; set; }
        /// <summary>
        /// 问题状态 1.部门待处理 2河长待处理 3完结 4重新申请作废 5回退部门待处理 
        /// </summary>
        public virtual System.Int32? State { get; set; }
        /// <summary>
        /// 部门处理意见
        /// </summary>
        public virtual System.String DepartmentRemark { get; set; }
        /// <summary>
        /// 部门操作时间
        /// </summary>
        public virtual System.DateTime? DepartmentOpTime { get; set; }
        /// <summary>
        /// 治水办处理意见
        /// </summary>
        public virtual System.String TopDepartmentRemark { get; set; }
        /// <summary>
        /// 治水办处理意见时间
        /// </summary>
        public virtual System.DateTime? TopDepartmentOpTime { get; set; }
        /// <summary>
        /// 河长结束问题时间
        /// </summary>
        public virtual System.DateTime? FinishOpTime { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public virtual System.String FinishRemark { get; set; }
        /// <summary>
        /// 回退说明
        /// </summary>
        public virtual System.String ReturnRemark { get; set; }
        /// <summary>
        /// 河长回退问题时间
        /// </summary>
        public virtual System.DateTime? ReturnOpTime { get; set; }
        /// <summary>
        /// 是否曝光
        /// </summary>
        public virtual System.Int32? IsExposure { get; set; }
        /// <summary>
        /// 曝光等级
        /// </summary>
        public virtual System.Int32? ExposureLever { get; set; }
        /// <summary>
        /// 是否已发送短信
        /// </summary>
        public virtual System.Int32? IsSendMessage { get; set; }

        /// <summary>
        /// 是否处理
        /// </summary>
        public virtual System.Int32? IsDeal { get; set; }


        /// <summary>
        /// 是否标记
        /// </summary>
        public virtual System.Int32? IsMark { get; set; }


        public virtual System.String Attr_IsDeal
        {
            get
            {
                if (IsDeal == 2)
                {
                    return "已处理";
                }
                else
                {
                    return "未处理";
                }
            }
        }

        public virtual System.String Attr_IsMark
        {
            get
            {
                if (IsMark == 2)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual System.Int32? IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String CreatorUserName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual System.String CreatorUserCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual System.DateTime? CreationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String LastModifierUserName { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual System.String LastModifierUserCode { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual System.DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual System.String Remark { get; set; }
        /// <summary>
        /// 删除原因
        /// </summary>
        public virtual System.String DeleteRemark { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual System.Int32? IsDeleted { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        public virtual System.String DeleteUserName { get; set; }

        public virtual System.String ExposureArea { get; set; }
        /// <summary>
        /// 删除人编码
        /// </summary>
        public virtual System.String DeleteUserCode { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual System.DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 是否督办
        /// </summary>
        public virtual System.Int32 IsUrgent { get; set; }

        public virtual System.String UrgentRemark { get; set; }

        public virtual RiverEntity RiverEntity { get; set; }

        public virtual string DbFinishOpTime { get; set; }
        public virtual string DbFinishRemark { get; set; }
        public virtual string DbReturnRemark { get; set; }
        public virtual string DbReturnOpTime { get; set; }

        public virtual string ZfCsUserCode { get; set; }
        public virtual string DbCsUserCode { get; set; }
        public virtual string DbUserCode { get; set; }
        public virtual string ZfCsUserName { get; set; }
        public virtual string DbCsUserName { get; set; }
        public virtual string DbUserName { get; set; }
        public virtual int? DbState { get; set; }
        #endregion


        #region 新增属性
        public virtual string Attr_ProblemTypeStr
        {
            get
            {
                switch (ProblemType)
                {
                    case 1:
                        return "日常巡河";
                    case 2:
                        return "问题上报";
                    case 3:
                        return "群众举报";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 1.部门待处理 2河长待处理 3完结 4重新申请作废 5回退部门待处理 
        /// </summary>
        public virtual string Attr_StateStr
        {
            get
            {
                switch (State)
                {
                    case 1:
                        return "部门待处理";
                    case 2:
                        return "河长待处理";
                    case 3:
                        return "完结";
                    case 4:
                        return "重新申请作废";
                    case 5:
                        return "回退部门待处理";
                    default:
                        return "";
                }

            }
        }

        public virtual string Attr_IsUrgent
        {
            get
            {
                switch (IsUrgent)
                {
                    case 1:
                        return "是";
                    default:
                        return "否";
                }

            }
        }

        public virtual string Attr_IsExposure
        {
            get
            {
                switch (IsExposure)
                {
                    case 1:
                        return "是";
                    default:
                        return "否";
                }

            }
        }

        public virtual string Attr_DbState
        {
            get
            {
                switch (DbState)
                {
                    case 1:
                        return "已回退";
                    case 2:
                        return "已完结";
                    default:
                        return "";
                }

            }
        }
        public virtual string Attr_ExposureLever
        {
            get { return ExposureLever.ToString(); }
        }

        public virtual string Attr_IsSendMessage
        {
            get
            {
                switch (IsSendMessage)
                {
                    case 1:
                        return "是";
                    default:
                        return "否";
                }

            }
        }



        public virtual string Attr_ExpireStr
        {
            get
            {
                switch (State)
                {
                    case 1:
                    case 2:
                    case 5:
                        if (DateTime.Now.Subtract(CreationTime.GetValueOrDefault()).Days > 7)
                        {
                            return "<span style=\"color:red\">过期未处理</span>";
                        }
                        return "";
                    default:
                        return "";
                }

            }
        }


        public virtual System.DateTime? Attr_CreationTimeStart { get; set; }

        public virtual System.DateTime? Attr_CreationTimeEnd { get; set; }

        public virtual IList<string> Attr_DepartmentCode { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public virtual string S_State { get; set; }



        public virtual int IsSpecialDeal { get; set; }

        public virtual int DifferDay {
            get
            {
                if (CreationTime!=null)
                {
                    return DateTime.Now.Subtract(CreationTime.GetValueOrDefault()).Days;
                }
                return 0;
            }
        }

        //public virtual DateTime? StartDateTime { get; set; }

        //public virtual DateTime? EndDateTime { get; set; }
        #endregion
    }
}




