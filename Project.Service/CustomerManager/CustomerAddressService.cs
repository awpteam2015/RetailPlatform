
/***************************************************************************
*       功能：     CMCustomerAddress业务处理层
*       作者：     李伟伟
*       日期：     2018/3/17
*       描述：     送货地址簿
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.CustomerManager;
using Project.Repository.CustomerManager;
using Project.Service.CustomerManager.Help;

namespace Project.Service.CustomerManager
{
    public class CustomerAddressService
    {

        #region 构造函数
        private readonly CustomerAddressRepository _customerAddressRepository;
        private static readonly CustomerAddressService Instance = new CustomerAddressService();

        public CustomerAddressService()
        {
            this._customerAddressRepository = new CustomerAddressRepository();
        }

        public static CustomerAddressService GetInstance()
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
        public Tuple<bool, string> Add(CustomerAddressEntity entity)
        {
            entity.AddressFull = CustomerHelp.GetInstance()
                 .CombineCustomerAddress(entity.ProvinceId, entity.CityId, entity.AreaId, entity.Address);

            if (entity.IsDefault == 1)
            {
                var list = this._customerAddressRepository.Query().Where(p => p.CustomerId == entity.CustomerId);
                list.ForEach(p =>
                {
                    p.IsDefault = 2;
                    _customerAddressRepository.Update(p);
                });
            }
            var pkId = _customerAddressRepository.Save(entity);

            return new Tuple<bool, string>(pkId > 0, "");
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _customerAddressRepository.GetById(pkId);
                _customerAddressRepository.Delete(entity);
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
        public bool Delete(CustomerAddressEntity entity)
        {
            try
            {
                _customerAddressRepository.Delete(entity);
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
        public Tuple<bool, string> Update(CustomerAddressEntity entity)
        {
            try
            {
                entity.AddressFull = CustomerHelp.GetInstance()
              .CombineCustomerAddress(entity.ProvinceId, entity.CityId, entity.AreaId, entity.Address);
                _customerAddressRepository.Merge(entity);

                if (entity.IsDefault == 1)
                {
                    var list = this._customerAddressRepository.Query().Where(p => p.CustomerId == entity.CustomerId && p.PkId != entity.PkId);
                    list.ForEach(p =>
                    {
                        p.IsDefault = 2;
                        _customerAddressRepository.Update(p);
                    });
                }

                return new Tuple<bool, string>(true,"");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public CustomerAddressEntity GetModelByPk(System.Int32 pkId)
        {
            return _customerAddressRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【送货地址簿】和总【送货地址簿】数</returns>
        public System.Tuple<IList<CustomerAddressEntity>, int> Search(CustomerAddressEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<CustomerAddressEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (where.CustomerId > 0)
                expr = expr.And(p => p.CustomerId == where.CustomerId);
            // if (!string.IsNullOrEmpty(where.Province))
            //  expr = expr.And(p => p.Province == where.Province);
            // if (!string.IsNullOrEmpty(where.CityId))
            //  expr = expr.And(p => p.CityId == where.CityId);
            // if (!string.IsNullOrEmpty(where.CountryId))
            //  expr = expr.And(p => p.CountryId == where.CountryId);
            // if (!string.IsNullOrEmpty(where.Address))
            //  expr = expr.And(p => p.Address == where.Address);
            // if (!string.IsNullOrEmpty(where.IsDefault))
            //  expr = expr.And(p => p.IsDefault == where.IsDefault);
            // if (!string.IsNullOrEmpty(where.Remarks))
            //  expr = expr.And(p => p.Remarks == where.Remarks);
            // if (!string.IsNullOrEmpty(where.ReceiverName))
            //  expr = expr.And(p => p.ReceiverName == where.ReceiverName);
            // if (!string.IsNullOrEmpty(where.FamilyTelephone))
            //  expr = expr.And(p => p.FamilyTelephone == where.FamilyTelephone);
            // if (!string.IsNullOrEmpty(where.PostCode))
            //  expr = expr.And(p => p.PostCode == where.PostCode);
            // if (!string.IsNullOrEmpty(where.Mobilephone))
            //  expr = expr.And(p => p.Mobilephone == where.Mobilephone);
            #endregion
            var list = _customerAddressRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _customerAddressRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<CustomerAddressEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<CustomerAddressEntity> GetList(CustomerAddressEntity where)
        {
            var expr = PredicateBuilder.True<CustomerAddressEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.CustomerId))
            //  expr = expr.And(p => p.CustomerId == where.CustomerId);
            // if (!string.IsNullOrEmpty(where.Province))
            //  expr = expr.And(p => p.Province == where.Province);
            // if (!string.IsNullOrEmpty(where.CityId))
            //  expr = expr.And(p => p.CityId == where.CityId);
            // if (!string.IsNullOrEmpty(where.CountryId))
            //  expr = expr.And(p => p.CountryId == where.CountryId);
            // if (!string.IsNullOrEmpty(where.Address))
            //  expr = expr.And(p => p.Address == where.Address);
            // if (!string.IsNullOrEmpty(where.IsDefault))
            //  expr = expr.And(p => p.IsDefault == where.IsDefault);
            // if (!string.IsNullOrEmpty(where.Remarks))
            //  expr = expr.And(p => p.Remarks == where.Remarks);
            // if (!string.IsNullOrEmpty(where.ReceiverName))
            //  expr = expr.And(p => p.ReceiverName == where.ReceiverName);
            // if (!string.IsNullOrEmpty(where.FamilyTelephone))
            //  expr = expr.And(p => p.FamilyTelephone == where.FamilyTelephone);
            // if (!string.IsNullOrEmpty(where.PostCode))
            //  expr = expr.And(p => p.PostCode == where.PostCode);
            // if (!string.IsNullOrEmpty(where.Mobilephone))
            //  expr = expr.And(p => p.Mobilephone == where.Mobilephone);
            #endregion
            var list = _customerAddressRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}




