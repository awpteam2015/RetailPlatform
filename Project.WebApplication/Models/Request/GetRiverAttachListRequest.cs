using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebApplication.Models.Request
{
    public class GetRiverAttachListRequest
    {
        public  int RiverId { get; set; }

        public int skipResults { get; set; }


        public int maxResults { get; set; }

    }
}