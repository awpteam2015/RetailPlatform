using log4net;
using Quartz;
using Quartz.Impl;
using SW.TimerTask.WinFrom.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SW.TimerTask.WinFrom
{
    public partial class fmMain : Form
    {
        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

        public fmMain()
        {
            InitializeComponent();

            // 初始化并启动任务调度器
            StartScheduler();

            #region 表格数据初始化

            dgTaskList.AutoGenerateColumns = false;
            dtExecutingTask.AutoGenerateColumns = false;
            if (AppContext.ListTimerJob.Count > 0)
            {
                dgTaskList.DataSource = AppContext.ListTimerJob;
                dgTaskList.ClearSelection();
            }

            var xmllist = XmlHelper.GetFileXmlList();
            cbXml.Items.Add("全部");
            foreach (var item in xmllist)
            {
                cbXml.Items.Add(item);
            }

            #endregion

        }

        #region 窗体设置

        private void SetFullScreen()
        {
            if (this.WindowState == FormWindowState.Maximized)//如果当前的窗体是最大化
            {
                this.WindowState = FormWindowState.Normal;//把当前窗体还原默认大小
            }
            else
            {
                //this.FormBorderStyle = FormBorderStyle.None;//将该窗体的边框设置为无,也就是没有标题栏以及窗口边框的
                this.WindowState = FormWindowState.Maximized;//将该窗体设置为最大化
            }
        }

        private void fmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); //或者是this.Visible = false;
                this.notifyIcon1.Visible = true;
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出程序吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                notifyIcon1.Visible = false;
                this.Close();
                this.Dispose();
                Application.Exit();
            }
        }

        private void hideMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void showMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        #endregion

        #region 任务核心功能

        private void StartScheduler()
        {
            scheduler.Start();

            AppContext.ListTimerJob.Where(c => c.State == "启动").ToList().ForEach(task =>
            {
                IJobDetail tempJob = null;
                if (string.IsNullOrWhiteSpace(task.InterfaceUrl))
                {
                    tempJob = JobBuilder.Create<HttpJobCopy>().WithIdentity(task.JobName, task.JobGroup).Build();
                }
                else if (task.InterfaceUrl == "http://172.16.18.10")
                {
                    tempJob = JobBuilder.Create<HttpJobParCopy>().WithIdentity(task.JobName, task.JobGroup).Build();
                }
                else
                {
                    tempJob = JobBuilder.Create<HttpJob>().WithIdentity(task.JobName, task.JobGroup).Build();
                }

                ITrigger tempTrigger = TriggerBuilder.Create().WithIdentity(task.TriggerName, task.TriggerGroup).WithCronSchedule(task.Cron).Build();
                scheduler.ScheduleJob(tempJob, tempTrigger);
            });

            timerExecutingTask.Start();
        }

        public void AddJob(Core.TimerTask model)
        {
            IJobDetail tempJob = null;
            if (string.IsNullOrWhiteSpace(model.InterfaceUrl))
            {
                tempJob = JobBuilder.Create<HttpJobCopy>().WithIdentity(model.JobName, model.JobGroup).Build();
            }
            else if (model.InterfaceUrl == "172.16.18.10")
            {
                tempJob = JobBuilder.Create<HttpJobParCopy>().WithIdentity(model.JobName, model.JobGroup).Build();
            }
            else
            {
                tempJob = JobBuilder.Create<HttpJob>().WithIdentity(model.JobName, model.JobGroup).Build();
            }

            ITrigger tempTrigger = TriggerBuilder.Create().WithIdentity(model.TriggerName, model.TriggerGroup).StartNow().WithCronSchedule(model.Cron).Build();
            scheduler.ScheduleJob(tempJob, tempTrigger);
        }

        public void ReStartJob(int taskId)
        {
            var task = AppContext.ListTimerJob.First(c => c.Id == taskId);
            //this.scheduler.ResumeJob(new JobKey(task.JobName, task.JobGroup));
            this.AddJob(task);
            task.State = "启动";
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            XmlHelper.SaveTask(task);
        }

        public void PauseJob(int taskId)
        {
            var task = AppContext.ListTimerJob.First(c => c.Id == taskId);
            this.scheduler.DeleteJob(new JobKey(task.JobName, task.JobGroup));
            task.State = "停止";
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            XmlHelper.SaveTask(task);
        }

        #endregion

        #region 事件

        private void fmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            scheduler.Shutdown();
        }

        private void timerExecutingTask_Tick(object sender, EventArgs e)
        {
            ReLoadExecutingTask();
            //ReLoadAllTask();
        }

        private void dgTaskList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = int.Parse(this.dgTaskList["Id", this.dgTaskList.CurrentCell.RowIndex].Value.ToString());
            var m = AppContext.ListTimerJob.Where(c => c.Id == id).First();
            this.tbxTaskName.Text = m.Name;
            this.tbxCron.Text = m.Cron;
            this.tbxInterfaceUrl.Text = m.InterfaceUrl;
            this.tbxId.Text = m.Id.ToString();
            this.tbxMethod.Text = m.Method;
            this.tbxParam.Text = m.Param;
            if (m.State == "启动") { this.btnStart.Enabled = false; this.btnPause.Enabled = true; } else { this.btnStart.Enabled = true; this.btnPause.Enabled = false; }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.tbxTaskName.Text = "";
            this.tbxCron.Text = "";
            this.tbxInterfaceUrl.Text = "";
            this.tbxId.Text = "";
            this.tbxMethod.Text = "";
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            var id = int.Parse(this.dgTaskList["Id", this.dgTaskList.CurrentCell.RowIndex].Value.ToString());
            this.PauseJob(id);
            MessageBox.Show("停止成功!");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var id = int.Parse(this.dgTaskList["Id", this.dgTaskList.CurrentCell.RowIndex].Value.ToString());
            this.ReStartJob(id);
            MessageBox.Show("启动成功!");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var id = tbxId.Text;
            TimerTask.WinFrom.Core.TimerTask model = new Core.TimerTask();
            if (id != "")
            {
                model = AppContext.ListTimerJob.First(c => c.Id == int.Parse(id));
                if (model.State == "启动") { MessageBox.Show("编辑状态下: 保存前请先停止任务!"); return; }
            }
            FillModel(model);
            if (id == "")
            {
                model.Id = AppContext.ListTimerJob.Count == 0 ? 1 : AppContext.ListTimerJob.Max(c => c.Id) + 1;
                model.JobName = "Job" + model.Id;
                model.JobGroup = "JobGroup" + model.Id;
                model.TriggerName = "Trigger" + model.Id;
                model.TriggerGroup = "TriggerGroup" + model.Id;
                model.State = "启动";
                AppContext.ListTimerJob.Add(model);
                IJobDetail tempJob = JobBuilder.Create<HttpJob>().WithIdentity(model.JobName, model.JobGroup).Build();
                ITrigger tempTrigger = TriggerBuilder.Create().WithIdentity(model.TriggerName, model.TriggerGroup).WithCronSchedule(model.Cron).Build();
                scheduler.ScheduleJob(tempJob, tempTrigger);
            }
            XmlHelper.SaveTask(model);
            MessageBox.Show("保存成功!");
        }

        private void dgTaskList_Enter(object sender, EventArgs e)
        {
            if (dgTaskList.RowCount != 0)
            {
                dgTaskList.Rows[0].Selected = false;
            }
        }

        private void btnCron_Click(object sender, EventArgs e)
        {
            new fmCronDetail().Show();
        }

        /// <summary>
        /// 更改控件大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMain_Resize(object sender, EventArgs e)
        {
            int height = this.Height;
            int width = this.Width;
            groupBox1.Height = height - groupBox2.Height - 60;
            groupBox2.Width = (int)((width - 40) * 0.4);
            groupBox3.Width = (int)((width - 40) * 0.6);
        }

        /// <summary>
        /// 查找Test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectList();
        }

        /// <summary>
        /// 改变搜索数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbXml_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectList();
        }


        #endregion

        #region 辅助方法

        private void ReLoadExecutingTask()
        {
            var list = scheduler.GetCurrentlyExecutingJobs();
            var res = list.Select(c => new
            {
                TaskName = AppContext.ListTimerJob.First(d => d.JobName == c.JobDetail.Key.Name).Name
            }).ToList();
            dtExecutingTask.DataSource = res;
        }

        private void ReLoadAllTask()
        {
            base.DoubleBuffered = true;
            if (AppContext.ListTimerJob.Count > 0)
            {
                dgTaskList.DataSource = null;
                dgTaskList.DataSource = AppContext.ListTimerJob;
            }
        }

        private void FillModel(TimerTask.WinFrom.Core.TimerTask model)
        {
            model.InterfaceUrl = tbxInterfaceUrl.Text.Trim();
            model.Method = tbxMethod.Text.Trim().ToUpper();
            model.Name = tbxTaskName.Text.Trim();
            model.Cron = tbxCron.Text.Trim();
            model.Param = tbxParam.Text.Trim();
        }

        /// <summary>
        /// 搜索数据
        /// </summary>
        private void SelectList()
        {
            base.DoubleBuffered = true;
            string testName = this.txtTestName.Text.Trim();
            dgTaskList.DataSource = null;
            var list = AppContext.ListTimerJob;
            if (!string.IsNullOrWhiteSpace(testName))
            {
                list = list.Where(n => n.Name.Contains(testName)).ToList();
            }

            if (cbXml.SelectedIndex > 0)
            {
                list = list.Where(n => n.fileName == cbXml.Text).ToList();
            }

            dgTaskList.DataSource = list;
        }

        #endregion
    }
}
