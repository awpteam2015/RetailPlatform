
/***************************************************************************
*       功能：     PRMGoods持久层
*       作者：     李伟伟
*       日期：     2017/6/30
*       描述：     商品表
* *************************************************************************/

using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.Domain.Repositories;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;
using Project.Model.ProductManager;
using Project.Model.ProductManager.Request;
using Project.Model.ProductManager.Response;

namespace Project.Repository.ProductManager
{
    /// <summary>
    /// 持久层
    /// </summary>
    public class GoodsRepository : RepositoryBaseSql<GoodsEntity, int>
    {

        public Tuple<IList<GoodsSearchView>, int> Search(GoodsSearchCondition where)
        {

            var selectStr = "SELECT a.*,b.ProductCode,b.ProductName,b.ProductCategoryId,b.ProductCategoryName,b.SystemCategoryName,b.SystemCategoryId,b.IsCommand ";
            var selectStrCount = "select count(*) as num ";

            var fromStr = @" from PRM_Goods a left join PRM_Product b on a.ProductId = b.PkId ";


            var whereStr = " where 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(where.ProductCode))
            {
                whereStr += " b.ProductCode="+where.ProductCode;
            }
            if (!string.IsNullOrWhiteSpace(where.ProductName))
            {
                whereStr += " b.ProductName like '%"+ where.ProductName + "%'";
            }
            if (!string.IsNullOrWhiteSpace(where.GoodsCode))
            {
                whereStr += " b.GoodsCode=" + where.GoodsCode;
            }

            whereStr = SqlStrHelper.RemoveSqlUnsafeString(whereStr);


            var sqlStr = selectStr + fromStr + whereStr;
            var returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                     .SetFirstResult(where.skipResults)
                     .SetMaxResults(where.maxResults)
                     .SetResultTransformer(Transformers.AliasToBean(typeof(GoodsSearchView))).List<GoodsSearchView>();


            var sqlStrCount = selectStrCount + fromStr + whereStr;
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStrCount).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();
            return new Tuple<IList<GoodsSearchView>, int>(returnList, count);
        }


    }
}







