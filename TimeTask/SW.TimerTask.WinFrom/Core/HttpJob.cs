using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SW.TimerTask.WinFrom.Core
{
    public class HttpJob : IJob
    {
        private ILog _logInfo = LogManager.GetLogger("InfoLogger");
        private ILog _logError = LogManager.GetLogger("ErrorLogger");
        public void Execute(IJobExecutionContext context)
        {
            bool isError = false;
            var jobName = context.JobDetail.Key.Name;
            var timerJob = AppContext.ListTimerJob.Where(c => c.JobName == jobName).FirstOrDefault();
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append(timerJob.ToString() + Environment.NewLine);
            sb.Append("开始时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
            try
            {
                if (timerJob != null)
                {
                    var res = HttpRequestHelper.Execute(timerJob.InterfaceUrl, timerJob.Method, timerJob.Param);
                    this.AppendResponseInfo(sb, res, timerJob, ref isError);
                }
            }
            catch (Exception e)
            {
                isError = true;
                var ex = e;
                while (ex != null)
                {
                    sb.Append("定时任务异常信息:" + ex.Message + Environment.NewLine);
                    ex = ex.InnerException;
                }
            }
            sb.Append("结束时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
            sb.Append(Environment.NewLine);
            if (isError) this._logError.Error(sb.ToString()); else this._logInfo.Info(sb.ToString());
        }

        private void AppendResponseInfo(StringBuilder sb, string res, TimerTask timerJob, ref bool isError)
        {
            if (string.IsNullOrEmpty(res))
            {
                sb.Append("日志:执行成功!" + Environment.NewLine);
            }
            else
            {
                var arr = XmlHelper.ParseWebResponse(res, timerJob.InterfaceUrl.Contains(".asmx"));
                if (arr[0] == "-1")
                {
                    isError = true;
                    sb.Append("WebService异常信息:" + arr[1] + Environment.NewLine);
                }
                else if (arr[0] == "0")
                {
                    sb.Append("日志:" + arr[1] + Environment.NewLine);
                }
                else
                {
                    sb.Append("日志:" + res + Environment.NewLine);
                }
            }
        }
    }
}
