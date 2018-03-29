using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using log4net;

namespace SW.TimerTask.WinFrom.Core
{
    /// <summary>
    /// 应用上下文
    /// </summary>
    public static class AppContext
    {
        /// <summary>
        /// 任务XML
        /// </summary>
        //public const string XML = "Tasks.xml";
        /// <summary>
        /// 所有任务列表
        /// </summary>
        public static List<TimerTask> ListTimerJob = new List<TimerTask>();

        static AppContext()
        {
            LoadTimerJobFromXml(XmlHelper.GetFileXmlList());
        }

        /// <summary>
        /// 初始化任务XML
        /// </summary>
        private static void LoadTimerJobFromXml(IList<string> xmlList)
        {
            foreach (var item in xmlList)
            {
                var doc = XDocument.Load(item);
                foreach (var e in doc.Root.Elements())
                {
                    var timerTask = new TimerTask();
                    timerTask.Id = int.Parse(e.Attribute("Id").Value);
                    timerTask.Name = e.Attribute("Name").Value;
                    timerTask.InterfaceUrl = e.Element("InterfaceUrl").Value;
                    timerTask.JobName = e.Element("JobName").Value;
                    timerTask.JobGroup = Guid.NewGuid().ToString();// e.Element("JobGroup").Value
                    timerTask.TriggerGroup = Guid.NewGuid().ToString();// e.Element("TriggerGroup").Value;
                    timerTask.TriggerName = Guid.NewGuid().ToString(); //e.Element("TriggerName").Value;
                    timerTask.Method = e.Element("Method").Value;
                    timerTask.Cron = e.Element("Cron").Value;
                    timerTask.State = e.Element("State").Value;
                    timerTask.Param = e.Element("Param").Value;
                    timerTask.fileName = item;
                    ListTimerJob.Add(timerTask);
                }
            }
        }
    }
}
