
/***************************************************************************
*       功能：     OMOrderMain映射类
*       作者：     李伟伟
*       日期：     2018/3/21
*       描述：     订单主表信息
* *************************************************************************/

using NHibernate.Type;
using Project.Config.OrderEnum;
using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.OrderManager;

namespace Project.Map.OrderManager
{
    public class OrderMainMap : BaseMap<OrderMainEntity, int>
    {
        public OrderMainMap() : base("OM_OrderMain")
        {
            this.MapPkidDefault<OrderMainEntity, int>();

            Map(p => p.OrderNo);
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
            Map(p => p.PayType);
            Map(p => p.PaySerialNumber);
            Map(p => p.PayTime);
            Map(p => p.BeginPayTime);
            Map(p => p.PayRemark);
            Map(p => p.SendTime);
            Map(p => p.SendRemark);
            Map(p => p.SendNo);
            Map(p => p.ReturnState); Map(p => p.ReturnAuditState);
            Map(p => p.ReturnPayNoSendReason);
            Map(p => p.ReturnPayNoSendRemark);
            Map(p => p.ReturnPayNoSendTime);
            Map(p => p.ReturnPayNoSendConfirmRemark);
            Map(p => p.ReturnPayNoSendConfirmTime);
            Map(p => p.ReturnPayNoSendSerialNumber);
            Map(p => p.ReturnPayNoSendPayType);
            Map(p => p.ReturnPrdAfterSendReason);
            Map(p => p.ReturnPrdAfterSendRemark);
            Map(p => p.ReturnPrdAfterSendTime);
            Map(p => p.ReturnPrdAfterSendAuditTime);
            Map(p => p.ReturnPrdAfterSendAuditRemark);
            Map(p => p.ReturnPrdSendNo);
            Map(p => p.ReturnPrdSendTime);
            Map(p => p.ReturnPrdSendRemak);
            Map(p => p.ReturnPrdSendConfirmTime);
            Map(p => p.ReturnPrdSendConfirmRemak);
            Map(p => p.ReturnPayAfterSendSerialNumber);
            Map(p => p.ReturnPayAfterSendRemark);
            Map(p => p.ReturnPayAfterSendTime);
            Map(p => p.ReturnPayAfterSendPayType);
            Map(p => p.ConfirmTime);
            Map(p => p.ConfirmRemark);
            Map(p => p.CancelTime);
            Map(p => p.CancelRemark);
            Map(p => p.UserIp);
            Map(p => p.CreatorUserCode);
            Map(p => p.CreationTime);
            Map(p => p.LastModifierUserCode);
            Map(p => p.LastModificationTime);

            HasMany(p => p.OrderMainDetailEntityList)
      .AsSet()
      .LazyLoad()
      .Cascade.Delete().Inverse()
      .NotFound.Ignore()
      .PropertyRef("OrderNo")
      .KeyColumn("OrderNo");

            References(p => p.OrderInvoiceEntity)
               .Not.Insert()
               .Not.Update()
               .NotFound.Ignore()
                .PropertyRef("OrderNo")
               .Column("OrderNo");

        }
    }
}




