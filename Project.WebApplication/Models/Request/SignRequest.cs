using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    public class SignRequest
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        public int RiverId { get; set; }

        ///// <summary>
        ///// 坐标 {"MercatorLng":13326525.399664998,"MercatorLat":3480415.5568196685,"lngNTU":11971421,"latNTU":2981986}
        ///// </summary>
        //public System.String Coords { get; set; }

        /// <summary>
        /// 精度lngNTU
        /// </summary>
        public virtual System.Decimal lngNTU { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public virtual System.Decimal latNTU { get; set; }

        /// <summary>
        /// 巡河描述
        /// </summary>
        public virtual System.String Remark { get; set; }

        public virtual System.String PicUrl1 { get; set; }

        public virtual System.String PicUrl2 { get; set; }

        public virtual System.String PicUrl3 { get; set; }

        /// <summary>
        /// 新增字段有效范围
        /// </summary>
        public virtual int EffectArea { get; set; }
    }
}