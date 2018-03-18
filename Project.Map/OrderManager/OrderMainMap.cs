
 /***************************************************************************
 *       功能：     OMOrderMain映射类
 *       作者：     李伟伟
 *       日期：     2018/3/18
 *       描述：     订单主表信息
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.OrderManager;

namespace  Project.Map.OrderManager
{
    public class OrderMainMap : BaseMap<OrderMainEntity,string>
    {
        public OrderMainMap():base("OM_OrderMain")
        {
            //this.MapPkidDefault<OrderMainEntity,int>();
            Id(p => p.OrderNo);
            Map(p => p.State);    
            Map(p => p.Totalamount);    
            Map(p => p.PresentPoints);    
            Map(p => p.CustomerId);    
            Map(p => p.CustomerName);    
            Map(p => p.Linkman);    
            Map(p => p.LinkmanTel);    
            Map(p => p.LinkmanMobilephone);    
            Map(p => p.LinkmanProvinceId);    
            Map(p => p.LinkmanCityId);    
            Map(p => p.LinkmanAreaId);    
            Map(p => p.LinkmanAddress);    
            Map(p => p.LinkmanAddressfull);    
            Map(p => p.LinkmanPostcode);    
            Map(p => p.LinkmanRemark);    
            Map(p => p.CustomerAddressId);    
            Map(p => p.PayTime);    
            Map(p => p.PayRemark);    
            Map(p => p.SendTime);    
            Map(p => p.SendNo);    
            Map(p => p.SendRemark);    
            Map(p => p.ReturnReason);    
            Map(p => p.ReturnNo);    
            Map(p => p.ReturnState);    
            Map(p => p.ReturnTime);    
            Map(p => p.ReturnRemark);    
            Map(p => p.ConfirmTime);    
            Map(p => p.ConfirmRemark);    
            Map(p => p.UserIp);    
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

    
 

