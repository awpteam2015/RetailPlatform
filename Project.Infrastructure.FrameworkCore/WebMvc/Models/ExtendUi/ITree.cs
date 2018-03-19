namespace Project.Infrastructure.FrameworkCore.WebMvc.Models.ExtendUi
{
    public interface ITree
    {
        string _parentId { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
         string id { get;  }


        string text { get;  }

    }
}
