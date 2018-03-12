

/***************************************************************************
*       功能：     PRMExtAttribute实体类
*       作者：     李伟伟
*       日期：     2017/6/30
*       描述：     扩展属性表
* *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager
{
    public class ExtAttributeEntity : Entity
    {
        public ExtAttributeEntity()
        {
            AttributeValueList = new HashSet<AttributeValueEntity>();
        }

        #region 属性

        public virtual Int32 AttributeId
        {
            get { return PkId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.String AttributeName { get; set; }

        /// <summary>
        /// 表现方式 1 select 2input 3
        /// </summary>
        public virtual int ShowType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ShowTypeName { get; set; }

        #endregion


        #region 新增属性
        /// <summary>
        /// 扩展属性值
        /// </summary>
        public virtual ISet<AttributeValueEntity> AttributeValueList { get; set; }

        #endregion
    }
}




