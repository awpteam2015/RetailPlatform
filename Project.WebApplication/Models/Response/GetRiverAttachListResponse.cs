namespace Project.WebApplication.Models.Response
{
    public class GetRiverAttachListResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? RiverId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String RiverName { get; set; }
        /// <summary>
        /// 记录月份
        /// </summary>
        public virtual System.DateTime? RecordTime { get; set; }
        /// <summary>
        /// 水纹水质
        /// </summary>
       // public virtual System.String Remark { get; set; }

        /// <summary>
        /// 水质类别
        /// </summary>
        public virtual System.String WaterQualityRank { get; set; }
        /// <summary>
        /// 2代表下降
        /// </summary>
        public virtual System.Int32 WaterQualityChange { get; set; }
    }
}