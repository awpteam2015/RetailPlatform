using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace Project.Infrastructure.FrameworkCore.ToolKit.Extensions
{
    #region 页面枚举

    public enum EnumProductListQueryParam
    {
        Page,
        Action,
        Pid,
        OrderByKey,
        OrderByRule,
        MinPriceScreening,
        MaxPriceScreening,
        StyleScreening,
        ColorScreening,
        SeriesScreening,
        SpaceScreening,
        MaterialScreening,
        BrandScreening,
        NewScreening,
        PromotionScreening,
        PromotionNo,
        Trend,
        Characteristic
    }

    public enum EnumNewsListQueryParam { Page }

    public enum EnumJobQueryParam
    {
        Page,
        Location,
        JobDescription
    }

    public enum EnumRoomIdeas
    {
        Page,
        SpaceScreening,
        StyleScreening
    }

    public enum EnumFindStyle
    {
        StyleScreening
    }

    public enum EnumSearchPageParams
    {
        Keyword,
        Vt,
        Page,
        CategoryId,
        OrderByKey,
        OrderByValue,
        MinPrice,
        MaxPrice
    }

    public enum EnumSeriesListParams
    {
        Page
    }

    #endregion

    /// <summary>
    ///  路由帮助器类!
    /// </summary>
    public class RouteHelper
    {
        private const string FORMAT_URL_PRODUCT_LIST_1 = "/product-list-{0}-{1}/";
        private const string FORMAT_URL_PRODUCT_LIST_2 = "/product-list-{0}-{1}-{2}/";
        private const string FORMAT_URL_ARTICLE_INFO = "/article-info-{0}-{1}/";  // {0} : ID, {1} : PID
        private const string FORMAT_URL_NEWS_INFO = "/news-info-{0}-{1}-{2}/";
        private const string FORMAT_URL_NEWS_LIST = "/news-list-{0}-{1}/";
        private const string FORMAT_URL_NEWS_IMG_LIST = "/news-img-list-{0}-{1}/";
        private const string FORMAT_URL_NEWS_IMG_INFO = "/news-img-info-{0}-{1}-{2}/";
        private const string FORMAT_URL_CATALOG_SHOW = "/catalog-show-{0}/";
        private const string FORMAT_URL_JOB_LIST = "/job-list-{0}-{1}/";
        private const string FORMAT_URL_JOB_INFO = "/job-info-{0}-{1}-{2}/";
        private const string FORMAT_URL_STORE_INFO = "/store-info-{0}-{1}-{2}/";
        private const string FORMAT_URL_DESIGN_IDEAS = "/design-ideas-{0}-{0}/";
        private const string FORMAT_URL_PICTORIAL = "/pictorial-{0}-{1}-{2}/";
        private const string FORMAT_URL_ARTICLE_SHOW = "/article-show-{0}-{1}-{2}-{3}";
        private const string FORMAT_URL_FindStyle = "/findstyle-{0}-{1}-{2}/{3}";
        private const string FORMAT_URL_ROOMIDEAS = "/roomideas-{0}-{1}-{2}/";
        private const string FORMAT_URL_ROOMIDEASET = "/roomideaset-{0}-{1}-{2}-{3}/";
        private const string FORMAT_URL_SEARCHPAGE = "/product-search/";
        private const string FORMAT_URL_MENUS_LIST = "/menus-list-{0}-{1}/";
        private const string FORMAT_URL_ARTICLE_LIST = "/article-list-{0}-{1}/";


        #region 商品模块

        /// <summary>
        /// 得到商品详细页面URL
        /// </summary>
        public static string CreateProductDetailUrl(int productId, string sku = "")
        {
            var url = string.Format("/product-detail-{0}", productId);
            if (sku != "") url += "#" + sku;
            return url;
        }

        /// <summary>
        /// 得到商品列表页URL
        /// </summary>
        public static string CreateProductListUrl(int catyId, string tag, string special = "")
        {
            var url = special == "" ? string.Format(FORMAT_URL_PRODUCT_LIST_1, tag, catyId) : string.Format(FORMAT_URL_PRODUCT_LIST_2, special, tag, catyId);
            return url;
        }

        /// <summary>
        /// 得到商品列表页URL
        /// </summary>
        public static string CreateProductListUrl(int catyId, string tag, Dictionary<string, string> conditions, string special = "")
        {
            var url = special == "" ? string.Format(FORMAT_URL_PRODUCT_LIST_1, tag, catyId) : string.Format(FORMAT_URL_PRODUCT_LIST_2, special, tag, catyId);
            return CreateProductListUrl(url, conditions);
        }

        /// <summary>
        /// 得到商品列表页URL
        /// </summary>
        public static string CreateProductListUrl(string url, Dictionary<string, string> conditions)
        {
            var prms = new List<string>();
            var index = 0;
            var base64Params = new List<EnumProductListQueryParam>() {
                EnumProductListQueryParam.ColorScreening,EnumProductListQueryParam.MaterialScreening,
                EnumProductListQueryParam.SeriesScreening,EnumProductListQueryParam.SpaceScreening,EnumProductListQueryParam.StyleScreening,
                EnumProductListQueryParam.BrandScreening,EnumProductListQueryParam.Trend, EnumProductListQueryParam.Characteristic
            };
            var queryParamNames = EnumHelper.ToList<EnumProductListQueryParam>();
            for (var i = 0; i < queryParamNames.Count; i++)
            {
                var value = conditions.GetValue(queryParamNames[i].ToString());
                if (value == null)
                {
                    prms.Add("0");
                }
                else
                {
                    if (base64Params.Contains(queryParamNames[i]) && value != "0")
                    {
                        value = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(value));
                    }
                    prms.Add(value);
                    index = i + 1;
                }
            }
            return index == 0 ? url : (url + string.Join("-", prms.Take(index).ToList()));
        }

        /// <summary>
        /// 得到系列详细页URL
        /// </summary>
        public static string CreateSeriesDetailUrl(int channelId, int id)
        {
            var url = string.Format("/series-detail-{0}-{1}/", channelId, id);
            return url;
        }

        public static string CreateProductSpecialListUrl(int catyId, string tag, params string[] prms)
        {
            var url = string.Format("/product-special-{0}-{1}/", tag, catyId);
            List<string> list = new List<string>();
            for (var i = 0; i < prms.Length; i++)
            {
                list.Add(prms[i]);
            }
            return url + string.Join("-", list);
        }

        #endregion

        #region 文章模块

        public static string CreateArticleInfoUrl(int id, int pid)
        {
            return string.Format(FORMAT_URL_ARTICLE_INFO, id, pid);
        }

        public static string CreateNewsInfoUrl(string pid, int id, int newsId)
        {
            return string.Format(FORMAT_URL_NEWS_INFO, pid, id, newsId);
        }

        public static string CreateNewsListUrl(string pid, int id)
        {
            return string.Format(FORMAT_URL_NEWS_LIST, pid, id);
        }

        public static string CreateNewsImgListUrl(string pid, int id)
        {
            return string.Format(FORMAT_URL_NEWS_IMG_LIST, pid, id);
        }

        public static string CreateNewsImgInfoUrl(string pid, int id, int newsId)
        {
            return string.Format(FORMAT_URL_NEWS_IMG_INFO, pid, id, newsId);
        }

        public static string CreateCatalogShowUrl(int cid)
        {
            return string.Format(FORMAT_URL_CATALOG_SHOW, cid);
        }

        public static string CreataJobListUrl(string pid, int id)
        {
            return string.Format(FORMAT_URL_JOB_LIST, pid, id);
        }

        public static string CreataJobInfoUrl(string pid, int id, int jobid)
        {
            return string.Format(FORMAT_URL_JOB_INFO, pid, id, jobid);
        }

        public static string CreateStoreInfoUrl(string pid, int id, int storeid)
        {
            return string.Format(FORMAT_URL_STORE_INFO, pid, id, storeid);
        }

        public static string CreataDesignIdeasUrl(string pid, int id)
        {
            return string.Format(FORMAT_URL_DESIGN_IDEAS, pid, id);
        }

        public static string CreataPictorialUrl(string pid, int id, int catyId)
        {
            return string.Format(FORMAT_URL_PICTORIAL, pid, id, catyId);
        }

        public static string CreateArticleShowUrl(string space, int item, int catyId, int newsId)
        {
            return string.Format(FORMAT_URL_ARTICLE_SHOW, space, item, catyId, newsId);
        }

        public static string CreateFindStyleUrl(string space, int item, int catyId, string stylescreening)
        {
            return string.Format(FORMAT_URL_FindStyle, space, item, catyId, stylescreening);
        }

        public static string CreateRoomIdeasUrl(string space, int item, int catyId)
        {
            return string.Format(FORMAT_URL_ROOMIDEAS, space, item, catyId);
        }

        public static string CreateRoomIdeaSetUrl(string space, int item, int catyId, int setId)
        {
            return string.Format(FORMAT_URL_ROOMIDEASET, space, item, catyId, setId);
        }

        public static string CreateRoomIdeasUrl(string space, int item, int catyId, Dictionary<string, string> conditions, List<string> queryParamNames)
        {
            var url = string.Format(FORMAT_URL_ROOMIDEAS, space, item, catyId);
            return CreatePagerUrl(url, conditions, queryParamNames);
        }

        public static string CreateMenusListUrl(string pid, int id)
        {
            return string.Format(FORMAT_URL_MENUS_LIST, pid, id);
        }

        public static string CreateArticleListUrl(string pid, int id)
        {
            return string.Format(FORMAT_URL_ARTICLE_LIST, pid, id);
        }

        public static string RemoveRoomIdeasCondition(string key, string value, Dictionary<string, string> queryDic, List<string> queryParamNames, string space, int item, int catyId)
        {
            var dic = new Dictionary<string, string>();
            foreach (var qd in queryDic)
            {
                dic.Add(qd.Key, qd.Value);
            }
            var keyValue = queryDic.ContainsKey(key) ? queryDic.GetValue(key) : "";

            var arr = keyValue.Split(',').ToList();
            if (arr.Count > 0)
            {
                arr.Remove(value);
                dic.Remove(key);
                if (arr.Count > 0)
                    dic.Add(key, string.Join(",", arr));
            }
            if (dic.All(c => c.Value == "0"))
                dic.Select(c => c.Key).ToList().ForEach(c => dic.Remove(c));
            return RouteHelper.CreateRoomIdeasUrl(space, item, catyId, dic, queryParamNames);
        }


        #endregion

        #region 搜索模块

        public static string CreateSearchPageUrl(Dictionary<string, string> conditions)
        {
            var prms = new List<string>();
            var index = 0;
            var base64Params = new List<Enum>() {
                EnumSearchPageParams.Keyword
            };
            var queryParamNames = EnumHelper.ToList<EnumSearchPageParams>();
            for (var i = 0; i < queryParamNames.Count; i++)
            {
                var value = conditions.GetValue(queryParamNames[i].ToString());
                if (value == null)
                {
                    prms.Add("0");
                }
                else
                {
                    if (base64Params.Contains(queryParamNames[i]) && value != "0")
                    {
                        value = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value)).Replace("/", "%2f");
                    }
                    prms.Add(value);
                    index = i + 1;
                }
            }
            return index == 0 ? FORMAT_URL_SEARCHPAGE : (FORMAT_URL_SEARCHPAGE + string.Join("-", prms.Take(index).ToList()));
        }

        public static string CreateSearchPageUrlByKeyWord(string keyword)
        {
            var dic = new Dictionary<string, string>();
            dic.Add(EnumSearchPageParams.Keyword.ToString(), keyword);
            return CreateSearchPageUrl(dic);
        }

        #endregion

        /// <summary>
        /// 创建分页URL
        /// </summary>
        public static string CreatePagerUrl(string url, Dictionary<string, string> conditions, List<string> queryParamNames)
        {
            var prms = new List<string>();
            var index = 0;
            for (var i = 0; i < queryParamNames.Count; i++)
            {
                var value = conditions.GetValue(queryParamNames[i].ToString());
                if (value == null)
                {
                    prms.Add("0");
                }
                else
                {
                    prms.Add(value);
                    index = i + 1;
                }
            }
            return index == 0 ? url : (url + HttpUtility.UrlEncode(string.Join("-", prms.Take(index).ToList())));
        }

        /// <summary>
        /// 解析请求中的Query数据
        /// </summary>
        /// <param name="query">"-"分隔</param>
        /// <param name="queryParams">变量数组，对应请求参数</param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseReqeustQuery(RouteData routeData, string[] queryParams)
        {
            var conditions = new Dictionary<string, string>();
            if (routeData.Values["query"] != null)
            {
                var arr = routeData.Values["query"].ToString().Split('-');
                for (var i = 0; i < arr.Length; i++)
                {
                    conditions.Add(queryParams[i], arr[i]);
                }
            }
            return conditions;
        }

        /// <summary>
        /// 解析请求中的Query数据-针对列表页面
        /// </summary>
        /// <param name="query">"-"分隔</param>
        /// <param name="queryParams">变量数组，对应请求参数</param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseReqeustQueryForProductListPage(RouteData routeData, string[] queryParams)
        {
            var base64Params = new List<string>() {
                EnumProductListQueryParam.ColorScreening.ToString(),
                EnumProductListQueryParam.MaterialScreening.ToString(),
                EnumProductListQueryParam.SeriesScreening.ToString(),
                EnumProductListQueryParam.SpaceScreening.ToString(),
                EnumProductListQueryParam.StyleScreening.ToString(),
                EnumProductListQueryParam.BrandScreening.ToString(),
                EnumProductListQueryParam.Trend.ToString(),
                EnumProductListQueryParam.Characteristic.ToString()
            };
            var conditions = new Dictionary<string, string>();
            if (routeData.Values["query"] != null)
            {
                var arr = routeData.Values["query"].ToString().Split('-');
                for (var i = 0; i < arr.Length; i++)
                {
                    if (base64Params.Contains(queryParams[i]) && arr[i] != "0")
                        conditions.Add(queryParams[i], Encoding.Default.GetString(Convert.FromBase64String(arr[i])));
                    else
                        conditions.Add(queryParams[i], arr[i]);
                }
            }
            return conditions;
        }

        /// <summary>
        /// 解析请求中的Query数据-针对Search页面
        /// </summary>
        /// <param name="query">"-"分隔</param>
        /// <param name="queryParams">变量数组，对应请求参数</param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseReqeustQueryForSearchPage(RouteData routeData, string[] queryParams)
        {
            var base64Params = new List<string>()
            {
                EnumSearchPageParams.Keyword.ToString()
            };
            var conditions = new Dictionary<string, string>();
            if (routeData.Values["query"] != null)
            {
                var arr = routeData.Values["query"].ToString().Split('-');
                for (var i = 0; i < arr.Length; i++)
                {
                    if (base64Params.Contains(queryParams[i]) && arr[i] != "0")
                        conditions.Add(queryParams[i], Encoding.UTF8.GetString(Convert.FromBase64String(arr[i])));
                    else
                        conditions.Add(queryParams[i], arr[i]);
                }
            }
            return conditions;
        }

        public static void RedirectTo404()
        {
            HttpContext.Current.Response.StatusCode = 404;
            HttpContext.Current.Response.SuppressContent = false;
            HttpContext.Current.Response.TrySkipIisCustomErrors = true;
            HttpContext.Current.Response.Redirect("/Home/FourError");
            //Response.Redirect("/html/404.html");
            HttpContext.Current.Server.ClearError();
        }
    }
}