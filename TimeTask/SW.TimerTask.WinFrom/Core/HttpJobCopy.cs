using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SW.TimerTask.WinFrom.Core
{
    public class HttpJobCopy : IJob
    {
        private ILog _logInfo = LogManager.GetLogger("InfoLogger");
        private ILog _logError = LogManager.GetLogger("ErrorLogger");

        public void Execute(IJobExecutionContext context)
        {
            GetFile();
        }

        #region 3502上传

        public void GetFile()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append("3502获取文件并上传开始时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
            bool isError = false;

            try
            {
                string local = @"D:\\SalesData\";
                string[] files = Directory.GetFiles(local);//得到文件
                foreach (string file in files)//循环文件
                {
                    FileInfo fi = new FileInfo(file); //建立FileInfo对象 
                    string fileName = fi.Name;
                    int dt = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));

                    string exname = fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal) + 1);//得到后缀名
                    //获取今天和第昨天
                    if (exname == "txt" && (fileName.IndexOf(dt.ToString(), StringComparison.Ordinal) == 0 || fileName.IndexOf((dt - 1).ToString(), StringComparison.Ordinal) == 0)) //如果后缀名为.xml文件,开头为Tasks
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
            string path = @"\\192.168.20.12\SalesData\" + fName;
            string username = "Administrator";
            string password = "Password9";
            string local = @"D:\\SalesData\" + fName;
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

                System.Net.ICredentials ic = new System.Net.NetworkCredential(username, password);
                request.Credentials = ic;
                stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);

            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
            return msg;
        }
        #endregion


    }
}
