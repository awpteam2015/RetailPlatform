using Project.Application.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Application.Service.OrderManager.Request
{
    public class SearchOrderListRequest:RequestBase, ISearchCondition
    {
        /// <summary>
        /// 
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateStart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int State { get; set; }

        #region Implementation of ISearchCondition

        /// <summary>
        /// 起始行号
        /// </summary>
        public int skipResults { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int maxResults { get; set; }

        #endregion
    }
}
