using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SW.TimerTask.WinFrom.Core
{
    /// <summary>
    /// HTTP请求帮助类
    /// </summary>
    public class HttpRequestHelper
    {
        public static string DoPost(string url, string postParam)
        {
            byte[] param = Encoding.UTF8.GetBytes(postParam);
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = param.Length;
            req.KeepAlive = false;
            req.ProtocolVersion = HttpVersion.Version10;
            req.Timeout = 5*60*1000;
            using (var reqstream = req.GetRequestStream())
            {
                reqstream.Write(param, 0, param.Length);
            }
            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                var data = reader.ReadToEnd();
                return data;
            }
        }

        public static string DoGet(string url, string getParam)
        {
            HttpWebRequest req = HttpWebRequest.Create(url + "?" + getParam) as HttpWebRequest;
            req.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                var data = reader.ReadToEnd();
                return data;
            }
        }

        /// <summary>
        /// 执行请求
        /// 协议：响应格式 code|msg  例如: -1|尝试除以0 , 0|正常
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Execute(string url, string method, string param)
        {
            var info = method .ToLower()== "post" ? DoPost(url, param) : DoGet(url, param);
            return info;
        }
    }
}
