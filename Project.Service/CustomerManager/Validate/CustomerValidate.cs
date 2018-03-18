using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model.CustomerManager;

namespace Project.Service.CustomerManager.Validate
{
  public  class CustomerValidate
    {
        #region
        private static readonly CustomerValidate Instance = new CustomerValidate();
       

        private CustomerValidate()
        {
            
        }

        public static CustomerValidate GetInstance()
        {
            return Instance;
        }

        #endregion

        public Tuple<bool, string> ValidateMobilephone(string mobilephone, int pkId = 0)
        {
            var list = CustomerService.GetInstance().GetList(new CustomerEntity() { Mobilephone = mobilephone });
            if (pkId > 0 && list.Any(p => p.PkId != pkId))
            {
                return new Tuple<bool, string>(false, "该手机号已经被注册！");
            }

            if (pkId <= 0 && list.Any())
            {
                return new Tuple<bool, string>(false, "该手机号已经被注册！");
            }

            return new Tuple<bool, string>(true, "");
        }
    }
}
