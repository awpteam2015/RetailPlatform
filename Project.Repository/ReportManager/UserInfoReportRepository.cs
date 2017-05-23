using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Transform;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Model.ReportManager;

namespace Project.Repository.ReportManager
{
    public class UserInfoReportRepository
    {
        /// <summary>
        /// 通讯录
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public Tuple<IList<UserInfoViewEntity>, int> GerAddressList(UserInfoViewEntity where, int skipResults,
            int maxResults)
        {
            string whereStr = " where 1=1 ";
            if (!string.IsNullOrWhiteSpace(where.UserName))
            {
                whereStr += " and a.UserName like '%" + where.UserName + "%'";
            }
            if (!string.IsNullOrWhiteSpace(where.Mobile))
            {
                whereStr += " and a.Mobile like '%" + where.Mobile + "%'";
            }
            if (!string.IsNullOrWhiteSpace(where.DepartmentName))
            {
                whereStr += " and a.DepartmentName like '%" + where.DepartmentName + "%'";
            }
            if (!string.IsNullOrWhiteSpace(where.RiverName))
            {
                whereStr += " and a.RiverName like '%" + where.RiverName + "%'";
            }

            string sqlStr = @" SELECT distinct 
 a.UserCode, a.UserName, a.Mobile, a.Duty,a.Lever
from ( SELECT * FROM v_UserView as a " + whereStr + ") as a";
            string countStr = "select count(*) as num from (" + sqlStr + ") as a ";
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();

            var returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                 .SetFirstResult(skipResults)
                 .SetMaxResults(maxResults)
                 .SetResultTransformer(Transformers.AliasToBean(typeof(UserInfoViewEntity))).List<UserInfoViewEntity>();
            return new Tuple<IList<UserInfoViewEntity>, int>(returnList, count);
        }



      
    }
}
