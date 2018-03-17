
 /***************************************************************************
 *       功能：     CMCustomerCollection映射类
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.CustomerManager;

namespace  Project.Map.CustomerManager
{
    public class CustomerCollectionMap : BaseMap<CustomerCollectionEntity,int>
    {
        public CustomerCollectionMap():base("CM_CustomerCollection")
        {
            this.MapPkidDefault<CustomerCollectionEntity,int>();
 
            Map(p => p.CustomerId);    
            Map(p => p.ProductId);    
            Map(p => p.ProductName);    
            Map(p => p.GoodsId);    
            Map(p => p.GoodsCode);    
            Map(p => p.ProductCode);    
            Map(p => p.ImageUrl);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.LastModificationTime);    
            Map(p => p.IsDeleted);    
            Map(p => p.DeleterUserCode);    
            Map(p => p.DeletionTime);    
        }
    }
}

    
 

