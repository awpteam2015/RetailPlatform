using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Models.Response
{
    public class ProblemResponse
    {
        /// <summary>
        /// 问题id
        /// </summary>
        public virtual System.String PkId { get; set; }
        #region 属性
        /// <summary>
        /// 地址描述
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
        /// 问题类型 1日常巡河 2问题上报 3群众举报
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
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual System.String DepartmentName { get; set; }
        /// <summary>
        /// 河道编码
        /// </summary>
        public virtual System.Int32? RiverId { get; set; }
        /// <summary>
        /// 河道名称
        /// </summary>
        public virtual System.String RiverName { get; set; }
        /// <summary>
        /// 河长手机
        /// </summary>
        public virtual System.String UserCode { get; set; }
        /// <summary>
        /// 河长姓名
        /// </summary>
        public virtual System.String UserName { get; set; }
        /// <summary>
        /// 坐标
        /// </summary>
        public virtual System.String Coords { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public string lngNTU {
            get
            {
                if (!string.IsNullOrWhiteSpace(Coords))
                {
                    //Coords= "{\"MercatorLng\":\"\",\"MercatorLat\":\"\",\"lngNTU\":0,\"latNTU\":0}";
                    var jd = JsonConvert.DeserializeObject<Zb>(Coords);
                return (decimal.Parse(jd.lngNTU)/100000).ToString();

                }
                return "";
            }
        }

        /// <summary>
        /// 纬度
        /// </summary>
        public string latNTU {
            get
            {
                if (!string.IsNullOrWhiteSpace(Coords))
                {
                    var jd = JsonConvert.DeserializeObject<Zb>(Coords);
                    return (decimal.Parse(jd.latNTU) / 100000).ToString();
                }
                return "";
            }
        }


        /// <summary>
        /// 问题状态 1.部门待处理 2河长待处理 3完结 4重新申请作废 5回退部门待处理 
        /// </summary>
        public virtual System.Int32? State { get; set; }
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
        /// 是否处理 0代表全部 1代表未处理 2代表已处理
        /// </summary>
        public virtual System.Int32? IsDeal { get; set; }

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

        /// 是否督办
        /// </summary>
        public virtual System.Int32 IsUrgent { get; set; }

        /// <summary>
        /// 督办意见
        /// </summary>
        public virtual string UrgentRemark { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public virtual System.String FinishRemark { get; set; }
        /// <summary>
        /// 回退说明
        /// </summary>
        public virtual System.String ReturnRemark { get; set; }

        /// <summary>
        /// 部门处理意见
        /// </summary>
        public virtual System.String DepartmentRemark { get; set; }

        /// <summary>
        /// 治水办处理意见
        /// </summary>
        public virtual System.String TopDepartmentRemark { get; set; }


        public virtual bool IsRead { get; set; }

        public virtual int IsMark { get; set; }
        public virtual string ZfCsUserCode { get; set; }
        public virtual string DbCsUserCode { get; set; }
        public virtual string DbUserCode { get; set; }
        public virtual string ZfCsUserName { get; set; }
        public virtual string DbCsUserName { get; set; }
        public virtual string DbUserName { get; set; }
        public virtual int? DbState { get; set; }

        #endregion
    }
}