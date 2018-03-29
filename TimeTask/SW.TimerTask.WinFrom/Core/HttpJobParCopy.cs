using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace SW.TimerTask.WinFrom.Core
{
    public class HttpJobParCopy : IJob
    {
        private ILog _logInfo = LogManager.GetLogger("InfoLogger");
        private ILog _logError = LogManager.GetLogger("ErrorLogger");

        public void Execute(IJobExecutionContext context)
        {
            GetFile(context);
        }

        #region 2701重庆星光销售数据上传

        public void GetFile(IJobExecutionContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append("2701重庆星光时代获取文件并上传开始时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
            bool isError = false;
            Tuple<bool, string> getUpdate = new Tuple<bool, string>(true, "");
            var jobName = context.JobDetail.Key.Name;
            var timerJob = AppContext.ListTimerJob.Where(c => c.JobName == jobName).FirstOrDefault();
            if (timerJob != null)
            {
                if (timerJob.Param == "true")
                {
                    getUpdate = GetIsUplode();
                }
            }

            if (getUpdate.Item1)
            {
                try
                {
                    string local = @"D:\\SalesDataInterface\";
                    string[] files = Directory.GetFiles(local); //得到文件
                    foreach (string file in files) //循环文件
                    {
                        FileInfo fi = new FileInfo(file); //建立FileInfo对象 
                        string fileName = fi.Name;

                        string exname = fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal) + 1);

                        if (exname == "DB")
                        {
                            sb.Append(fileName + "" + Environment.NewLine);
                            string msg = ResponseWindowsShared(fileName);
                            sb.Append(msg + "" + Environment.NewLine);
                            if (msg != "")
                                isError = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    isError = true;
                    sb.Append(e.Message + Environment.NewLine);
                }
            }
            else
            {
                sb.Append(getUpdate.Item2 + Environment.NewLine);
            }

            sb.Append("结束时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
            sb.Append(Environment.NewLine);
            if (isError) this._logError.Error(sb.ToString()); else this._logInfo.Info(sb.ToString());
        }


        /// <summary>
        /// 上传文件到共享目录
        /// </summary>
        /// <param name="fName"></param>
        private string ResponseWindowsShared(string fName)
        {
            string msg = "";
            string path = @"\\172.16.18.10\本地磁盘 (d)\第三方销售数据接口传输程序\三方和收银机数据\第三方销售数据接口说明\" + fName;
            string local = @"D:\\SalesDataInterface\" + fName;
            System.Net.FileWebRequest request = null;
            System.IO.Stream stream = null;
            try
            {
                //时间戳 
                string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
                Uri uri = new Uri(path);
                byte[] bytes = System.IO.File.ReadAllBytes(local);
                request = (System.Net.FileWebRequest)System.Net.FileWebRequest.Create(uri);
                request.Method = "POST";
                //设置获得响应的超时时间（300秒） 
                request.Timeout = 300000;
                request.ContentType = "multipart/form-data; boundary=" + strBoundary;
                request.ContentLength = bytes.Length;

                stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);

                stream.Close();
                stream.Dispose();

                SaveIsUplode();

            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
            return msg;
        }
        #endregion

        /// <summary>
        /// 根据配置判断是否上传
        /// </summary>
        /// <returns></returns>
        private Tuple<bool, string> GetIsUplode()
        {
            bool isUplode = true;
            string msg = "";
            string path = @"C:\HHUpdate\2701Log.xml";
            XmlDocument xml = new XmlDocument();

            try
            {
                xml.Load(path);
                var nodeName = string.Format("root/Department_2701");
                var selectSingleNode = xml.SelectSingleNode(nodeName);

                if (selectSingleNode != null)
                {
                    var lastExportDate = selectSingleNode.ChildNodes.Item(0).InnerText.Trim();
                    string[] str = lastExportDate.Split('|');
                    if (str[1] == "1")
                    {
                        isUplode = false;
                        msg = "配置不上传";
                    }
                }
            }
            catch (Exception e)
            {

                msg = e.Message;
            }

            return new Tuple<bool, string>(isUplode, msg);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void SaveIsUplode()
        {
            try
            {
                string path = @"C:\HHUpdate\2701Log.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                var nodeName = string.Format("root/Department_2701");
                var selectSingleNode = xml.SelectSingleNode(nodeName);
                if (selectSingleNode != null)
                {
                    string lastExportDate = DateTime.Now.ToString("yyyyMMdd") + "|1";
                    selectSingleNode.ChildNodes.Item(0).InnerText = lastExportDate;
                    xml.Save(path);
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}
