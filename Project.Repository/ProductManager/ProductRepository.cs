
/***************************************************************************
*       功能：     PRMProduct持久层
*       作者：     李伟伟
*       日期：     2017/6/30
*       描述：     产品表
* *************************************************************************/

using System.Collections.Generic;
using NHibernate.Transform;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.Domain.Repositories;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;
using Project.Model.ProductManager;
using Project.Model.ProductManager.Request;
using Project.Model.ReportManager;

namespace Project.Repository.ProductManager
{
    /// <summary>
    /// 持久层
    /// </summary>
    public class ProductRepository : RepositoryBaseSql<ProductEntity, int>
    {

        /// <summary>
        /// 前台搜索
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<ProductEntity> Search(ProductSearchCondition where)
        {

            var sqlStr = "select a.*,b.ImageUrl as ImageUrl  from PRM_Product a " +
                         "left join PRM_ProductImage  b on a.PkId=b.ProductId ";

            var whereStr = " where b.IsDefault = 1";

            if (!string.IsNullOrWhiteSpace(where.ProductCode))
            {
                whereStr += " and a.ProductCode=" + where.ProductCode;
            }


            if (where.AttributeValue1 > 0)
            {
                sqlStr += " left join PRM_ProductAttributeValue attr1 on a.PkId = attr1.ProductId";

                whereStr += " and attr1.AttributeValueId=" + where.AttributeValue1;
            }

            if (where.AttributeValue2 > 0)
            {
                sqlStr += " left join PRM_ProductAttributeValue attr2 on a.PkId = attr2.ProductId";

                whereStr += " and attr2.AttributeValueId=" + where.AttributeValue1;
            }

            if (where.AttributeValue3 > 0)
            {
                sqlStr += " left join PRM_ProductAttributeValue attr3 on a.PkId = attr3.ProductId";

                whereStr += " and attr3.AttributeValueId=" + where.AttributeValue1;
            }

            sqlStr = sqlStr + SqlStrHelper.RemoveSqlUnsafeString(whereStr);

            //  sqlStr = SqlStrHelper.RemoveSqlUnsafeString(sqlStr);

            var returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                     .SetFirstResult(where.skipResults)
                     .SetMaxResults(where.maxResults)
                     .SetResultTransformer(Transformers.AliasToBean(typeof(ProductEntity))).List<ProductEntity>();

            return returnList;
        }

    }
}







