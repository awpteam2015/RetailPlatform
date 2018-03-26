
/***************************************************************************
*       功能：     CMCardType映射类
*       作者：     李伟伟
*       日期：     2018/3/17
*       描述：     会员卡类型
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.CustomerManager;

namespace Project.Map.CustomerManager
{
    public class CardTypeMap : BaseMap<CardTypeEntity, int>
    {
        public CardTypeMap() : base("CM_CardType")
        {
            this.MapPkidDefault<CardTypeEntity, int>();

            Map(p => p.CardtypeName);
            Map(p => p.Discount);
            Map(p => p.NeedTotalAmount);
        }
    }
}




