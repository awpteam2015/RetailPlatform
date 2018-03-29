using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities.Component
{
    /// <summary>
    ///搜索条件
    /// </summary>
   public interface ISearchCondition
    {
        /// <summary>
        /// 起始行号
        /// </summary>
        int skipResults { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        int maxResults { get; set; }
    }
}
