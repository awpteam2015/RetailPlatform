using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Model.ProductManager;

namespace Project.WebApplication.Areas.ProductManager.Models
{
    public class ProductHdVm
    {
        public ProductHdVm()
        {
            SpecVmList=new List<SpecVm>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<SpecVm> SpecVmList { get; set; }

        //public IList<SystemCategorySpecEntity> SystemCategorySpecEntityList { get; set; }
    }


    public class SpecVm
    {
        /// <summary>
        ///规格编号
        /// </summary>
        public virtual System.Int32 SpecId { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        public virtual System.String SpecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public virtual System.Int32 IsCheck { get; set; }

        /// <summary>
        /// 规格值
        /// </summary>
        public virtual IList<SpecValueVm> SpecValueList { get; set; }
    }


    public class SpecValueVm
    {
        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecValueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? SpecId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SpecValueName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Int32? Sort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String ImagePath { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public virtual int IsCheck { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public virtual int OtherSpecValueName { get; set; }

        #endregion
    }


    //public class GoodVm
    //{
    //    public string CombineId { get; set; }


    //}


}