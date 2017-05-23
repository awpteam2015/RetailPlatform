using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model.ReportManager;
using Project.Repository.ReportManager;

namespace Project.Service.ReportManager
{
   public class UserInfoReportService
    {
        #region 构造函数
        private readonly UserInfoReportRepository _UserInfoReportRepository;
        private static readonly UserInfoReportService Instance = new UserInfoReportService();

        public UserInfoReportService()
        {
            this._UserInfoReportRepository = new UserInfoReportRepository();
        }

        public static UserInfoReportService GetInstance()
        {
            return Instance;
        }
        #endregion


       public Tuple<IList<UserInfoViewEntity>, int> GerAddressList(UserInfoViewEntity where, int skipResults,
           int maxResults)
       {
           return _UserInfoReportRepository.GerAddressList(where, skipResults, maxResults);
       }
    }
}
