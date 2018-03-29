using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.ToolKit.ValidateHandler;
using Project.Model.SystemSetManager;
using Project.Repository.SystemSetManager;

namespace Project.Application.Service.SystemSetManager
{
    public class SystemSetServiceImpl
    {
        private AuthCodeRepository _authCodeRepository;
        public SystemSetServiceImpl()
        {
            _authCodeRepository = new AuthCodeRepository();
        }

        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="authType">操作行为</param>
        /// <returns></returns>
        public Tuple<bool, string> SendMobileAuthCode(string mobile, string authType)
        {
            if (!ValidateRegExp.IsMobile(mobile.Trim()))
                return new Tuple<bool, string>(false, "请输入正确的手机号！");

            var historyAuthCodeList = _authCodeRepository.Query().Where(p => p.ReciviceUser == mobile && p.CreateDate >= DateTime.Today).ToList();

            if (historyAuthCodeList.Count > 20)
            {
                return new Tuple<bool, string>(false, "您今日发送的短信量已使用完");
            }

            var authCodeEntity = new AuthCodeEntity();
            authCodeEntity.CreateDate = DateTime.Now;
            authCodeEntity.EndDate = DateTime.Now.AddMinutes(2);
            authCodeEntity.ReciviceUser = mobile;
            authCodeEntity.AuthType = "";
            authCodeEntity.SendType = "mobile";
            authCodeEntity.AuthCode=new Random().Next(1000,9999).ToString();

            var pkId = _authCodeRepository.Save(authCodeEntity);

            if (pkId > 0)
            {
                //if (!string.IsNullOrWhiteSpace(mobile))
                //{
                //    string Url = "http://jx008.openmas.net:9080/OpenMasService";
                //    Sms Client = new Sms(Url);
                //    string externcode = "2"; //自定义扩展代码（模块）
                //    string ApplicationID = "8088002";
                //    string Password = "GWuFdgdAJROI";
                //    try
                //    {

                //        //发送短信
                //        var result = Client.SendMessage(new[] { tel }, Message, externcode, ApplicationID, Password);
                //    }
                //    catch (Exception)
                //    {
                //return new Tuple<bool, string>(false, "");

                //    }
                //}
            }

            return new Tuple<bool, string>(pkId>0, "");

        }

    }
}
