

/***************************************************************************
*       功能：     PRMSpec实体类
*       作者：     李伟伟
*       日期：     2017/6/30
*       描述：     规格表
* *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class SpecEntity : Entity
    {

        public SpecEntity()
        {
            this.SpecValueEntityList = new HashSet<SpecValueEntity>();
        }

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String SpecName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.String Remark { get; set; }
        /// <summary>
        /// 0text 1image
        /// </summary>
        public virtual System.Int32? SpecType { get; set; }
        /// <summary>
        /// 0平铺 1下拉框
        /// </summary>
        public virtual System.Int32? ShowType { get; set; }

        /// <summary>
        /// 0text 1image
        /// </summary>
        public virtual string SpecTypeName { get; set; }
        /// <summary>
        /// 0平铺 1下拉框
        /// </summary>
        public virtual string ShowTypeName { get; set; }

        #endregion


        #region 新增属性
        /// <summary>
        /// 规格值
        /// </summary>
        public virtual ISet<SpecValueEntity> SpecValueEntityList { get; set; }

        #endregion


        #region 附加属性 数据库中不存在

        #endregion
    }
}




