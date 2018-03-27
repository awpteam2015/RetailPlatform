using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Config
{
    public class SiteConfigEntiy
    {
        public SiteConfigEntiy()
        {
            PicSizeConfig = new PicSizeConfig();
        }


        public PicSizeConfig PicSizeConfig { get; set; }


        public string JsParamter {
            get { return ConfigurationManager.AppSettings["JsParamter"]; }
        }

        public int IndexPagePkId
        {
      get { return int.Parse(ConfigurationManager.AppSettings["IndexPagePkId"]); }
        }


        public List<string> WaterRankList
        {
            get
            {
                return new List<string>() { "I", "II", "III", "IV", "V" };
            }
        }


    }


    public class PicSizeConfig
    {
        /// <summary>
        /// 商品图片配置
        /// </summary>
        public string GoodsPicSize {
            get
            {
                return ConfigurationManager.AppSettings["GoodsPicSize"];
            }
        }
    }


}
