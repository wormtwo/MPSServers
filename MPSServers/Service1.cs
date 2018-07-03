using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace MPSServers
{
    public partial class Service1 : ServiceBase
    {
        public bool AutoReset { get; set; } 
        public Service1()
        {
            InitializeComponent();

            timer1.Interval = 10000;//10s
            timer1.Enabled = true;
        }

        protected override void OnStart(string[] args)
        {
            timer1.Start();
            this.WriteLog("【服务启动】");
        }

        protected override void OnStop()
        {
            timer1.Stop();
            this.WriteLog("【服务停止】");
        }
        #region 时间控件定时任务
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime myTime = DateTime.Now;
           
            if ( DateTime.Now.ToString("HHmm")=="0200")
            {
                if (!AutoReset)
                {//如果第一次的时候设置执行程序
                    AutoReset = true;//执行一次后,将AutoReset设置为true
                    this.WriteLog("【执行程序】");
                    GetData.GetHtml myGetHtml = new GetData.GetHtml("admin","admin");
                    myGetHtml.Main();
                }
            }
            else
            {
                AutoReset = false;
              
            }
        }
        #endregion
       
        #region  写入日志
        private void WriteLog(string msg)
        {
            //该日志文件会存在windows服务程序目录下
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\log\";
            path += DateTime.Now.Year + "_" + DateTime.Now.Month + "_access_log.txt";
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                FileStream fs;
                fs = File.Create(path);
                fs.Close();
            }

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "   " + msg);
                }
            }
        }
        #endregion
    }
}
