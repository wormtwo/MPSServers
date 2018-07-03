using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using WinHttp;

namespace GetData
{
    public partial class GetHtml
    {
        const string MainUrl = "http://192.168.1.2:99";
        public WinHttpRequest win = new WinHttpRequest();
        public string UserName{get;set;}
        public string UserPwd { get; set; }
        public GetHtml(string uname,string upwd)
        {
            UserName = uname;
            UserPwd = upwd;
        }
        public  void Main()
        {
            //登录
            Login();
            //点击
            Submit();
        }
        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        private void Login()
        {
            try
            {
                win.Open("GET", MainUrl + "/mylogin.aspx", false);
                win.Send();
                string strText = win.ResponseText;
                SetHeaderToHtml(MainUrl + "/mylogin.aspx", 0, strText);
            }
            catch (Exception)
            {
            }
          
            
        }
        #endregion
        #region 点击按钮
        private void Submit()
        {
            try
            {
                string strText = "";
                #region 主生产计划
                win.Open("GET", MainUrl + "/joa/dongan/class1.aspx", false);
                win.Send();
                strText = win.ResponseText;
                SetHeaderToHtml(MainUrl + "/joa/dongan/class1.aspx", 1, strText);
                SetHeaderToHtml(MainUrl + "/joa/dongan/class1.aspx", 2, strText);
                SetHeaderToHtml(MainUrl + "/joa/dongan/class1.aspx", 3, strText);
                #endregion
                #region 物料信息
                win.Open("GET", MainUrl + "/joa/dongan/class2.aspx", false);
                win.Send();
                strText = win.ResponseText;
                SetHeaderToHtml(MainUrl + "/joa/dongan/class2.aspx", 1, strText);
                SetHeaderToHtml(MainUrl + "/joa/dongan/class2.aspx", 5, strText);
                SetHeaderToHtml(MainUrl + "/joa/dongan/class2.aspx", 3, strText);
                SetHeaderToHtml(MainUrl + "/joa/dongan/class2.aspx", 4, strText);
                #endregion
                #region 工艺信息
                win.Open("GET", MainUrl + "/joa/dongan/class3.aspx", false);
                win.Send();
                strText = win.ResponseText;
                SetHeaderToHtml(MainUrl + "/joa/dongan/class3.aspx", 1, strText);
                SetHeaderToHtml(MainUrl + "/joa/dongan/class3.aspx", 2, strText);
                SetHeaderToHtml(MainUrl + "/joa/dongan/class3.aspx", 3, strText);
                #endregion
                #region 工艺装备.
                win.Open("GET", MainUrl + "/joa/dongan/class4.aspx", false);
                win.Send();
                strText = win.ResponseText;
                SetHeaderToHtml(MainUrl + "/joa/dongan/class4.aspx", 1, strText);
                SetHeaderToHtml(MainUrl + "/joa/dongan/class4.aspx", 3, strText);
                #endregion
                #region 物料结构信息表
                win.Open("GET", MainUrl + "/joa/dongan/class5.aspx", false);
                win.Send();
                strText = win.ResponseText;
                SetHeaderToHtml(MainUrl + "/joa/dongan/class5.aspx", 1, strText);
                SetHeaderToHtml(MainUrl + "/joa/dongan/class5.aspx", 3, strText);
                #endregion
            }
            catch (Exception)
            {
            }
           
        }
        #region 点击按钮操作
        private void SetHeaderToHtml(string url, int sendStrType, string str)
        {
            try
            {
                string VIEWSTATE = "", VIEWSTATEGENERATOR = "", EVENTVALIDATION = "";
                VIEWSTATE = GetASPstr(str, "__VIEWSTATE");
                VIEWSTATEGENERATOR = GetASPstr(str, "__VIEWSTATEGENERATOR");
                EVENTVALIDATION = GetASPstr(str, "__EVENTVALIDATION");
                string sendStr = "";
                switch (sendStrType)
                {
                    case 0://登录
                        sendStr = "__VIEWSTATE=" + VIEWSTATE + "&__VIEWSTATEGENERATOR=" + VIEWSTATEGENERATOR + "&__EVENTVALIDATION=" + EVENTVALIDATION + "&uname=" + UserName + "&pword=" + UserPwd + "&srceensize=1920%2C1080&cword=22&LoginButton.x=45&LoginButton.y=22";
                        break;
                    case 1://新增数据导入
                        sendStr = "__VIEWSTATE=" + VIEWSTATE + "&__VIEWSTATEGENERATOR=" + VIEWSTATEGENERATOR + "&__EVENTVALIDATION=" + EVENTVALIDATION + "&ctl00%24Jcontext%24Button_Add=%E6%96%B0%E5%A2%9E%E6%95%B0%E6%8D%AE%E5%AF%BC%E5%85%A5";
                        break;
                    case 2://修改数据导入
                        sendStr = "__VIEWSTATE=" + VIEWSTATE + "&__VIEWSTATEGENERATOR=" + VIEWSTATEGENERATOR + "&__EVENTVALIDATION=" + EVENTVALIDATION + "&ctl00%24Jcontext%24Button_Mod=%E4%BF%AE%E6%94%B9%E6%95%B0%E6%8D%AE%E5%AF%BC%E5%85%A5";
                        break;
                    case 3://删除数据导入
                        sendStr = "__VIEWSTATE=" + VIEWSTATE + "&__VIEWSTATEGENERATOR=" + VIEWSTATEGENERATOR + "&__EVENTVALIDATION=" + EVENTVALIDATION + "&ctl00%24Jcontext%24Button_Del=%E5%88%A0%E9%99%A4%E6%95%B0%E6%8D%AE%E5%AF%BC%E5%85%A5";
                        break;
                    case 4://物料信息---工艺数据导入
                        sendStr = "__VIEWSTATE=" + VIEWSTATE + "&__VIEWSTATEGENERATOR=" + VIEWSTATEGENERATOR + "&__EVENTVALIDATION=" + EVENTVALIDATION + "&ctl00%24Jcontext%24ButtonGongYi=%E5%B7%A5%E8%89%BA%E6%95%B0%E6%8D%AE%E5%AF%BC%E5%85%A5";
                        break;
                    case 5://物料信息---修改数据导入
                        sendStr = "__VIEWSTATE=" + VIEWSTATE + "&__VIEWSTATEGENERATOR=" + VIEWSTATEGENERATOR + "&__EVENTVALIDATION=" + EVENTVALIDATION + "&ctl00%24Jcontext%24Button1=%E4%BF%AE%E6%94%B9%E6%95%B0%E6%8D%AE%E5%AF%BC%E5%85%A5";
                        break;
                }
                win.Open("POST", url, false);
                win.SetRequestHeader("Accept", "image/gif, image/jpeg, image/pjpeg, application/x-ms-application, application/xaml+xml, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*");
                win.SetRequestHeader("Accept-Language", "zh-CN  User - Agent: Mozilla / 4.0(compatible; MSIE 7.0; Windows NT 10.0; WOW64; Trident / 7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729; InfoPath.3; TheWorld 7)");
                win.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                // win.SetRequestHeader("Cookie", cookie);
                //win.SetRequestHeader("Referer", MainUrl + "/mylogin.aspx");
                win.SetRequestHeader("Content-Length", sendStr.Length.ToString());
                win.SetRequestHeader("Host", "1.58.184.121:8010");
                win.SetRequestHeader("Connection", "Keep-Alive");
                win.SetRequestHeader("Pragma", "no-cache");
                win.Send(sendStr);
                string tt = win.ResponseText;
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #endregion

        #region 根据name值获取ASP参数的方法
        private string GetASPstr(string str,string name)
        {
            string rex = @"<input type=""hidden"" name=""@name"" id=""@name"" value=""(.+)"" />";
            rex = rex.Replace("@name", name);
            Regex reg = new Regex(rex);
            if (reg.IsMatch(str))
            {
                Match mt_chk = reg.Match(str);
                return HttpUtility.UrlEncode(mt_chk.Groups[1].ToString());
                //return mt_chk.Groups[1].ToString().Replace("/", "%2F").Replace("+", "%2B");
            }
            return "";
        }
        #endregion
    }
}
