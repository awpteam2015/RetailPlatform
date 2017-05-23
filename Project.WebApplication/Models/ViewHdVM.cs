using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Model.RiverManager;

namespace Project.WebApplication.Models
{
    public class ViewHdVM
    {
        public IList<RiverAttachEntity> list { get; set; }
    }
}