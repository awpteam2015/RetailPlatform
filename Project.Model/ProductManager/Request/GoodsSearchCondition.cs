using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;

namespace Project.Model.ProductManager.Request
{
    public class GoodsSearchCondition : ISearchCondition
    {
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public string GoodsCode { get; set; }


        public int maxResults { get; set; }


        public int skipResults { get; set; }

    }
}
