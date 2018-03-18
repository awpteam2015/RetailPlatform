
 /***************************************************************************
 *       功能：     CMCustomerAddress映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     送货地址簿
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.CustomerManager;

namespace  Project.Map.CustomerManager
{
    public class CustomerAddressMap : BaseMap<CustomerAddressEntity,int>
    {
        public CustomerAddressMap():base("CM_CustomerAddress")
        {
            this.MapPkidDefault<CustomerAddressEntity,int>();
 
            Map(p => p.CustomerId);    
            Map(p => p.ProvinceId);    
            Map(p => p.CityId);    
            Map(p => p.AreaId);    
            Map(p => p.Address);
            Map(p => p.AddressFull);   
            Map(p => p.IsDefault);    
            Map(p => p.Remarks);    
            Map(p => p.ReceiverName);    
            Map(p => p.FamilyTelephone);    
            Map(p => p.PostCode);    
            Map(p => p.Mobilephone);    
        }
    }
}

    
 

