using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.CustomerManager;
using Project.Repository.CustomerManager;
using Project.Repository.ProductManager;
using Project.Repository.SystemSetManager;
using Project.Service.CustomerManager;
using Project.Service.CustomerManager.Dto;

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
        private readonly CustomerCollectionRepository _customerCollectionRepository;
        private readonly AuthCodeRepository _authCodeRepository;
        private readonly ProductRepository _productRepository;

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
            _customerCollectionRepository = new CustomerCollectionRepository();
            _productRepository = new ProductRepository();

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
        public Tuple<bool, CustomerDto> Login(string mobilephone, string password)
        {
            var customerInfo = _customerRepository.Query().Where(p => p.Mobilephone == mobilephone && p.Password == Encrypt.MD5Encrypt(password)).FirstOrDefault();

            if (customerInfo != null)
            {
                return new Tuple<bool, CustomerDto>(true, new CustomerDto()
                {
                    CustomerId = customerInfo.PkId,
                    CustomerName = customerInfo.CustomerName,
                    Mobilephone = customerInfo.Mobilephone
                });
            }
            else
            {
                return new Tuple<bool, CustomerDto>(false, null);
            }

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

        public Tuple<bool, string> ForgetPasswordStep3(string key, string newPassword)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Tuple<IList<CustomerCollectionEntity>, int> GetCollectionList(int skipResults, int maxResults, int customerId)
        {
            return CustomerCollectionService.GetInstance()
                   .Search(new CustomerCollectionEntity() { CustomerId = customerId }, skipResults, maxResults);
        }

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="pkId"></param>
        /// <param name="customerId"></param>
        public Tuple<bool, string> CancelCollection(int pkId, int customerId)
        {
            var collectionInfo = _customerCollectionRepository.Query().SingleOrDefault(p => p.CustomerId == customerId && p.PkId == pkId);
            var result = CustomerCollectionService.GetInstance().Delete(collectionInfo);
            return new Tuple<bool, string>(result, "");
        }

        /// <summary>
        /// 新增收藏
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="customerId"></param>
        public Tuple<bool, string> AddCollection(int productId, int customerId)
        {
            var productInfo = _productRepository.GetById(productId);

            var collectionInfo = new CustomerCollectionEntity();
            collectionInfo.ProductCode = productInfo.ProductCode;
            collectionInfo.ProductName = productInfo.ProductName;
            collectionInfo.ImageUrl = productInfo.ImageUrl;

            var result = _customerCollectionRepository.Save(collectionInfo);

            return new Tuple<bool, string>(result > 0, "");
        }

        #endregion


        #region 收货地址

        /// <summary>
        /// 获取收货地址列表
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IList<CustomerAddressEntity> GetCustomerAddressList(int customerId)
        {
            return _customerAddressRepository.Query().Where(p => p.CustomerId == customerId).ToList();
        }

        /// <summary>
        /// 新增送货地址
        /// </summary>
        /// <param name="entity"></param>
        public Tuple<bool, string> AddAddress(CustomerAddressEntity entity)
        {
            return CustomerAddressService.GetInstance().Add(entity);
        }


        /// <summary>
        /// 编辑送货地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        public Tuple<bool, string> UpdateAddress(CustomerAddressEntity entity)
        {
            return CustomerAddressService.GetInstance().Update(entity);
        }


        /// <summary>
        /// 设置默认地址
        /// </summary>
        /// <param name="pkId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Tuple<bool, string> SetDefaultAddress(int pkId, int customerId)
        {
            var addressInfo = _customerAddressRepository.Query().FirstOrDefault(p => p.CustomerId == customerId && p.PkId == pkId);

            try
            {
                addressInfo.IsDefault = 1;
                _customerAddressRepository.Update(addressInfo);

                var list = this._customerAddressRepository.Query().Where(p => p.CustomerId == customerId && p.PkId != pkId);
                list.ForEach(p =>
                {
                    p.IsDefault = 2;
                    _customerAddressRepository.Update(p);
                });
                return new Tuple<bool, string>(true, "");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="pkId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Tuple<bool, string> DelAddress(int pkId, int customerId)
        {
            var addressInfo = _customerAddressRepository.Query().FirstOrDefault(p => p.CustomerId == customerId && p.PkId == pkId);
            if (addressInfo != null)
            {
                var result = CustomerAddressService.GetInstance().Delete(addressInfo);
                return new Tuple<bool, string>(result, "");
            }
            return new Tuple<bool, string>(false, "");
        }


        #endregion



    }

}
