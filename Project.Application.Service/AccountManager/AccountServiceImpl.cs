using System;
using System.Linq;
using System.Net.Http;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Model.CustomerManager;
using Project.Repository.CustomerManager;
using Project.Repository.SystemSetManager;
using Project.Service.CustomerManager;

namespace Project.Application.Service.AccountManager
{
    public class AccountServiceImpl
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CardTypeRepository _cardTypeRepository;
        private readonly ProvinceRepository _provinceRepository;
        private readonly CityRepository _cityRepository;
        private readonly AreaRepository _areaRepository;
        private readonly CustomerAddressRepository _customerAddressRepository;
        private readonly AuthCodeRepository _authCodeRepository;

        public AccountServiceImpl()
        {
            _provinceRepository = new ProvinceRepository();
            this._customerRepository = new CustomerRepository();
            _provinceRepository = new ProvinceRepository();
            this._cityRepository = new CityRepository();
            this._areaRepository = new AreaRepository();
            _customerAddressRepository = new CustomerAddressRepository();
            _cardTypeRepository = new CardTypeRepository();
            _authCodeRepository = new AuthCodeRepository();

        }


        #region 注册登录
        /// <summary>
        /// 注册
        /// </summary>
        public Tuple<bool, string> Regist(string accountName, string password, string authCode)
        {
            if (string.IsNullOrWhiteSpace(authCode))
            {
                return new Tuple<bool, string>(false, "验证码错误");
            }

            if (!string.IsNullOrEmpty(authCode))
            {
                var authCodeInfo = _authCodeRepository.Query().Where(p => p.ReciviceUser == accountName && p.CreateDate <= DateTime.Now && p.EndDate >= DateTime.Now).OrderByDescending(p => p.PkId).FirstOrDefault();

                if (authCodeInfo != null && authCodeInfo.AuthCode == authCode)
                {
                }
                else
                {
                    return new Tuple<bool, string>(false, "验证码错误");
                }
            }

            return CustomerService.GetInstance().Add(new CustomerEntity() { Mobilephone = accountName, Password = password, CardTypeId = 1 });
        }

        /// <summary>
        /// 会员登录
        /// </summary>
        public void Login()
        {

        }

        /// <summary>
        /// 注销
        /// </summary>
        public void LogOut()
        {

        }
        #endregion


        #region 忘记密码

        /// <summary>
        /// 忘记密码第一步
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="authCode"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public Tuple<bool, string> ForgetPasswordStep1(string accountName, string authCode, string verifyCode)
        {
            if (string.IsNullOrWhiteSpace(accountName) || string.IsNullOrWhiteSpace(authCode))
            {
                return new Tuple<bool, string>(false, "请输入账号及验证码");
            }

            if (!verifyCode.ToLower().Equals(authCode))
            {
                return new Tuple<bool, string>(false, "请输入正确的验证码或验证码已过期");
            }

            var customerInfo = _customerRepository.Query().Where(p => p.Mobilephone == accountName).SingleOrDefault();

            if (customerInfo != null)
            {
                return new Tuple<bool, string>(true, customerInfo.PkId.ToString());
            }
            return new Tuple<bool, string>(false, "不存在该用户信息");
        }


        /// <summary>
        /// 忘记密码第二步加载页面验证
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Tuple<bool, string> ForgetPassword2Validate(string key)
        {
            if (string.IsNullOrEmpty(key))
                return new Tuple<bool, string>(false, "");

            var customerid = Encrypt.AESDecrypt(key, Encrypt.GetKeyAES16());
            if (string.IsNullOrEmpty(customerid))
                return new Tuple<bool, string>(false, "");

            var temp = _customerRepository.GetById(int.Parse(customerid));
            if (temp == null)
                return new Tuple<bool, string>(false, null);

            return new Tuple<bool, string>(true, temp.Mobilephone);
        }

        /// <summary>
        /// 忘记密码第二步加载页面验证
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Tuple<bool, string> ForgetPasswordStep2(string key, string authCode)
        {
            if (string.IsNullOrEmpty(key))
                return new Tuple<bool, string>(false, "");

            var customerid = Encrypt.AESDecrypt(key, Encrypt.GetKeyAES16());
            if (string.IsNullOrEmpty(customerid))
                return new Tuple<bool, string>(false, "");

            var temp = _customerRepository.GetById(int.Parse(customerid));
            if (temp == null)
                return new Tuple<bool, string>(false, null);

            if (string.IsNullOrWhiteSpace(authCode))
            {
                return new Tuple<bool, string>(false, "验证码错误");
            }

            if (!string.IsNullOrEmpty(authCode))
            {
                var authCodeInfo = _authCodeRepository.Query().Where(p => p.ReciviceUser == temp.Mobilephone && p.CreateDate <= DateTime.Now && p.EndDate >= DateTime.Now).OrderByDescending(p => p.PkId).FirstOrDefault();

                if (authCodeInfo != null && authCodeInfo.AuthCode == authCode)
                {
                }
                else
                {
                    return new Tuple<bool, string>(false, "验证码错误");
                }
            }
            return new Tuple<bool, string>(true, temp.PkId.ToString());
        }


        /// <summary>
        /// 忘记密码第三步加载页面验证  主要判断验证码是否过期了
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Tuple<bool, string> ForgetPassword3Validate(string key)
        {
            if (string.IsNullOrEmpty(key))
                return new Tuple<bool, string>(false, "");

            var customerid = Encrypt.AESDecrypt(key, Encrypt.GetKeyAES16());
            if (string.IsNullOrEmpty(customerid))
                return new Tuple<bool, string>(false, "");

            var temp = _customerRepository.GetById(int.Parse(customerid));
            if (temp == null)
                return new Tuple<bool, string>(false, null);

            var isNotExpire = _authCodeRepository.Query().Any(p => p.ReciviceUser == temp.Mobilephone && p.CreateDate <= DateTime.Now && p.EndDate >= DateTime.Now);

            if (isNotExpire)
            {
                return new Tuple<bool, string>(true, "");
            }
            else
            {
                return new Tuple<bool, string>(false, "验证码过期");
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>

        public Tuple<bool, string> ForgetPasswordStep3(string key,string newPassword)
        {
            if (string.IsNullOrEmpty(key))
                return new Tuple<bool, string>(false, "");

            var customerid = Encrypt.AESDecrypt(key, Encrypt.GetKeyAES16());
            if (string.IsNullOrEmpty(customerid))
                return new Tuple<bool, string>(false, "");

            var temp = _customerRepository.GetById(int.Parse(customerid));
            if (temp == null)
                return new Tuple<bool, string>(false, null);

            temp.Password = newPassword;

            try
            {
                _customerRepository.Update(temp);
                return new Tuple<bool, string>(true, "");
            }
            catch (Exception e)
            {

                return new Tuple<bool, string>(false, "");
            }
        }

        #endregion

        #region 收藏夹

        public void SearchCollectionList()
        {

        }

        public void CancelCollection()
        {

        }

        #endregion


        #region 收货地址

        public void ChangeAddress()
        {

        }

        #endregion



    }

}
