using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.ProductManager.Request
{
    /// <summary>
    /// 官网前台搜索条件
    /// </summary>
    public class ProductSearchCondition: ISearchCondition
    {
        public string ProductCode { get; set; }


        public int AttributeValue1 { get; set; }

        public int AttributeValue2 { get; set; }

        public int AttributeValue3 { get; set; }


        public int skipResults { get; set; }
        public int maxResults { get; set; }

    }
}
