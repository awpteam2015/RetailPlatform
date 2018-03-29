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
    /// XML辅助类
    /// </summary>
    public static class XmlHelper
    {
        private static ILog _logInfo = LogManager.GetLogger("InfoLogger");

        /// <summary>
        /// 保存任务
        /// </summary>
        /// <param name="task"></param>
        public static void SaveTask(TimerTask task)
        {
            var xmlList = GetFileXmlList();
            foreach (var item in xmlList)
            {
                var doc = XDocument.Load(item);
                if (doc.Root.Elements().Any(e => e.Attribute("Id").Value == task.Id.ToString()))
                {
                    // edit
                    var m = doc.Root.Elements().First(e => e.Attribute("Id").Value == task.Id.ToString());
                    m.Attribute("Name").Value = task.Name;
                    m.Element("InterfaceUrl").Value = task.InterfaceUrl;
                    m.Element("Cron").Value = task.Cron;
                    m.Element("Method").Value = task.Method;
                    m.Element("State").Value = task.State;
                    m.Element("Param").Value = task.Param;
                }
                doc.Save(item);
            }

            //else
            //{
            //    // add
            //    XElement e = new XElement("Task");
            //    e.Add(
            //            new XAttribute("Id", task.Id.ToString()),
            //            new XAttribute("Name", task.Name),
            //            new XElement("JobName", task.JobName),
            //            new XElement("JobGroup", task.JobGroup),
            //            new XElement("TriggerName", task.TriggerName),
            //            new XElement("TriggerGroup", task.TriggerGroup),
            //            new XElement("Cron", task.Cron),
            //            new XElement("State", task.State),
            //            new XElement("InterfaceUrl", task.InterfaceUrl),
            //            new XElement("Method", task.Method),
            //            new XElement("Param", task.Param)
            //         );
            //    doc.Root.Add(e);
            //}
        }

        /// <summary>
        /// 解析webservice返回结果
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>arr[0]: code (0:success, -1:error) | arr[1]: msg</returns> 
        public static string[] ParseWebResponse(string info, bool isWebService)
        {
            if (isWebService)
            {
                var doc = XDocument.Parse(info);
                return doc.Root.Value.Split('|');
            }
            else
            {
                return info.Split('|');
            }
        }

        /// <summary>
        /// 获取所有配置名称
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetFileXmlList()
        {
            IList<string> xmlList = new List<string>();

            try
            {
                string[] files = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory());//得到文件
                foreach (string file in files)//循环文件
                {
                    FileInfo fi = new FileInfo(file); //建立FileInfo对象 
                    string fileName = fi.Name;

                    string exname = fileName.Substring(fileName.LastIndexOf(".") + 1);//得到后缀名
                    int i = fileName.IndexOf("Tasks");

                    if (exname == "xml" && i == 0)//如果后缀名为.xml文件,开头为Tasks
                    {
                        xmlList.Add(fileName);
                    }
                }
            }
            catch (Exception e)
            {
                _logInfo.Info(e.Message);
            }
            return xmlList;
        }
    }
}
