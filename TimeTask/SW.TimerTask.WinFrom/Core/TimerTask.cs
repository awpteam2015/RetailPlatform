using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.TimerTask.WinFrom.Core
{
    /// <summary>
    /// 定时任务对象
    /// </summary>
    public class TimerTask
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobName { get; set; }
        public string JobGroup { get; set; }
        public string InterfaceUrl { get; set; }
        public string TriggerName { get; set; }
        public string TriggerGroup { get; set; }
        public string Cron { get; set; }
        public string Method { get; set; }
        public string State { get; set; }
        public string Param { get; set; }

        public string fileName { get; set; }

        public override string ToString()
        {
            return string.Format("任务:{0} \n 接口:{1}\n", this.Name, this.InterfaceUrl);
        }
    }
}
