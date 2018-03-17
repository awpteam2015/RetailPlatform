
 /***************************************************************************
 *       功能：     CMCustomer业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     会员信息
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.CustomerManager;
using Project.Repository.CustomerManager;

namespace Project.Service.CustomerManager
{
    public class CustomerService
    {
       
       #region 构造函数
        private readonly CustomerRepository  _customerRepository;
            private static readonly CustomerService Instance = new CustomerService();

        public CustomerService()
        {
           this._customerRepository =new CustomerRepository();
        }
        
         public static  CustomerService GetInstance()
        {
            return Instance;
        }
        #endregion


        #region 基础方法 
         /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public System.Int32 Add(CustomerEntity entity)
        {
            return _customerRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _customerRepository.GetById(pkId);
            _customerRepository.Delete(entity);
             return true;
        }
        catch
        {
         return false;
        }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(CustomerEntity entity)
        {
         try
            {
            _customerRepository.Delete(entity);
             return true;
        }
        catch
        {
         return false;
        }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(CustomerEntity entity)
        {
          try
            {
            _customerRepository.Update(entity);
         return true;
        }
        catch
        {
         return false;
        }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public CustomerEntity GetModelByPk(System.Int32 pkId)
        {
            return _customerRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【会员信息】和总【会员信息】数</returns>
        public System.Tuple<IList<CustomerEntity>, int> Search(CustomerEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<CustomerEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.CardNo))
              //  expr = expr.And(p => p.CardNo == where.CardNo);
              // if (!string.IsNullOrEmpty(where.Password))
              //  expr = expr.And(p => p.Password == where.Password);
              // if (!string.IsNullOrEmpty(where.CustomerName))
              //  expr = expr.And(p => p.CustomerName == where.CustomerName);
              // if (!string.IsNullOrEmpty(where.Gender))
              //  expr = expr.And(p => p.Gender == where.Gender);
              // if (!string.IsNullOrEmpty(where.Birthday))
              //  expr = expr.And(p => p.Birthday == where.Birthday);
              // if (!string.IsNullOrEmpty(where.Email))
              //  expr = expr.And(p => p.Email == where.Email);
              // if (!string.IsNullOrEmpty(where.Familytelephone))
              //  expr = expr.And(p => p.Familytelephone == where.Familytelephone);
              // if (!string.IsNullOrEmpty(where.Postcode))
              //  expr = expr.And(p => p.Postcode == where.Postcode);
              // if (!string.IsNullOrEmpty(where.Mobilephone))
              //  expr = expr.And(p => p.Mobilephone == where.Mobilephone);
              // if (!string.IsNullOrEmpty(where.ProvinceId))
              //  expr = expr.And(p => p.ProvinceId == where.ProvinceId);
              // if (!string.IsNullOrEmpty(where.CityId))
              //  expr = expr.And(p => p.CityId == where.CityId);
              // if (!string.IsNullOrEmpty(where.CountryId))
              //  expr = expr.And(p => p.CountryId == where.CountryId);
              // if (!string.IsNullOrEmpty(where.Address))
              //  expr = expr.And(p => p.Address == where.Address);
              // if (!string.IsNullOrEmpty(where.Memo))
              //  expr = expr.And(p => p.Memo == where.Memo);
              // if (!string.IsNullOrEmpty(where.Discount))
              //  expr = expr.And(p => p.Discount == where.Discount);
              // if (!string.IsNullOrEmpty(where.Totalamount))
              //  expr = expr.And(p => p.Totalamount == where.Totalamount);
              // if (!string.IsNullOrEmpty(where.Totalpoints))
              //  expr = expr.And(p => p.Totalpoints == where.Totalpoints);
              // if (!string.IsNullOrEmpty(where.Availablepoints))
              //  expr = expr.And(p => p.Availablepoints == where.Availablepoints);
              // if (!string.IsNullOrEmpty(where.LastModificationTime))
              //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
              // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
              //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
              // if (!string.IsNullOrEmpty(where.CreationTime))
              //  expr = expr.And(p => p.CreationTime == where.CreationTime);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.IsDeleted))
              //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
              // if (!string.IsNullOrEmpty(where.DeleterUserCode))
              //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
              // if (!string.IsNullOrEmpty(where.DeletionTime))
              //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
 #endregion
            var list = _customerRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _customerRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<CustomerEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<CustomerEntity> GetList(CustomerEntity where)
        {
               var expr = PredicateBuilder.True<CustomerEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.CardNo))
              //  expr = expr.And(p => p.CardNo == where.CardNo);
              // if (!string.IsNullOrEmpty(where.Password))
              //  expr = expr.And(p => p.Password == where.Password);
              // if (!string.IsNullOrEmpty(where.CustomerName))
              //  expr = expr.And(p => p.CustomerName == where.CustomerName);
              // if (!string.IsNullOrEmpty(where.Gender))
              //  expr = expr.And(p => p.Gender == where.Gender);
              // if (!string.IsNullOrEmpty(where.Birthday))
              //  expr = expr.And(p => p.Birthday == where.Birthday);
              // if (!string.IsNullOrEmpty(where.Email))
              //  expr = expr.And(p => p.Email == where.Email);
              // if (!string.IsNullOrEmpty(where.Familytelephone))
              //  expr = expr.And(p => p.Familytelephone == where.Familytelephone);
              // if (!string.IsNullOrEmpty(where.Postcode))
              //  expr = expr.And(p => p.Postcode == where.Postcode);
              // if (!string.IsNullOrEmpty(where.Mobilephone))
              //  expr = expr.And(p => p.Mobilephone == where.Mobilephone);
              // if (!string.IsNullOrEmpty(where.ProvinceId))
              //  expr = expr.And(p => p.ProvinceId == where.ProvinceId);
              // if (!string.IsNullOrEmpty(where.CityId))
              //  expr = expr.And(p => p.CityId == where.CityId);
              // if (!string.IsNullOrEmpty(where.CountryId))
              //  expr = expr.And(p => p.CountryId == where.CountryId);
              // if (!string.IsNullOrEmpty(where.Address))
              //  expr = expr.And(p => p.Address == where.Address);
              // if (!string.IsNullOrEmpty(where.Memo))
              //  expr = expr.And(p => p.Memo == where.Memo);
              // if (!string.IsNullOrEmpty(where.Discount))
              //  expr = expr.And(p => p.Discount == where.Discount);
              // if (!string.IsNullOrEmpty(where.Totalamount))
              //  expr = expr.And(p => p.Totalamount == where.Totalamount);
              // if (!string.IsNullOrEmpty(where.Totalpoints))
              //  expr = expr.And(p => p.Totalpoints == where.Totalpoints);
              // if (!string.IsNullOrEmpty(where.Availablepoints))
              //  expr = expr.And(p => p.Availablepoints == where.Availablepoints);
              // if (!string.IsNullOrEmpty(where.LastModificationTime))
              //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
              // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
              //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
              // if (!string.IsNullOrEmpty(where.CreationTime))
              //  expr = expr.And(p => p.CreationTime == where.CreationTime);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.IsDeleted))
              //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
              // if (!string.IsNullOrEmpty(where.DeleterUserCode))
              //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
              // if (!string.IsNullOrEmpty(where.DeletionTime))
              //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
 #endregion
            var list = _customerRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

