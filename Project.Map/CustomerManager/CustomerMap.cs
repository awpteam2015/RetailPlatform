
 /***************************************************************************
 *       功能：     CMCustomer映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     会员信息
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.CustomerManager;

namespace  Project.Map.CustomerManager
{
    public class CustomerMap : BaseMap<CustomerEntity,int>
    {
        public CustomerMap():base("CM_Customer")
        {
            this.MapPkidDefault<CustomerEntity,int>();
 
            Map(p => p.CardNo);
            Map(p => p.CardTypeId);
            Map(p => p.CardTypeName);
            Map(p => p.Password);    
            Map(p => p.CustomerName);    
            Map(p => p.Gender);    
            Map(p => p.Birthday);    
            Map(p => p.Email);    
            Map(p => p.Familytelephone);    
            Map(p => p.Postcode);    
            Map(p => p.Mobilephone);    
            Map(p => p.ProvinceId);    
            Map(p => p.CityId);    
            Map(p => p.AreaId);    
            Map(p => p.Address);     Map(p => p.AddressFull);    
            Map(p => p.Memo);    
            Map(p => p.Discount);    
            Map(p => p.Totalamount);    
            Map(p => p.Totalpoints);    
            Map(p => p.Availablepoints);    
            Map(p => p.LastModificationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.IsDeleted);    
            Map(p => p.DeleterUserCode);    
            Map(p => p.DeletionTime);     Map(p => p.State);    
        }
    }
}

    
 

