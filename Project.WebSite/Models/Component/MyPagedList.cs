using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Project.WebSite.Extend
{
    public class MyPagedList : BasePagedList<Object>
    {
        public MyPagedList(int pageNumber, int pageSize, int totalItemCount) : base(pageNumber, pageSize, totalItemCount)
        {

        }
    }
}