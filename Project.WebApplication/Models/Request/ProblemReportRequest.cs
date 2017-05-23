using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    public class ProblemReportRequest
    {
        public int RiverId { get; set; }
        public string ApplyManName { get; set; }
        public string ApplyManTel { get; set; }
        public string Title { get; set; }
        public string Des { get; set; }

        /// <summary>
        /// 问题类型 1日常巡河 2问题上报 3群众举报
        /// </summary>
        public string ProblemType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PicUrl1 { get; set; }
        public string PicUrl2 { get; set; }
        public string PicUrl3 { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public decimal lngNTU { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public decimal latNTU { get; set; }

        /// <summary>
        ///  坐标 格式如{"MercatorLng":13320639.748488095,"MercatorLat":3482517.5750971343,"lngNTU":11966134,"latNTU":2983624}
        /// </summary>
        //public string Coords { get; set; }


    }
}