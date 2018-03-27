
 /***************************************************************************
 *       功能：     SPMActivity持久层
 *       作者：     李伟伟
 *       日期：     2018/3/26
 *       描述：     促销活动  目前考虑单品促销 满足金额发券 满足金额减免
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.Domain.Repositories;
using Project.Model.SalePromotionManager;

namespace Project.Repository.SalePromotionManager
{   
    /// <summary>
    /// 持久层
    /// </summary>
    public class  ActivityRepository : RepositoryBaseSql< ActivityEntity, int>
    {

    }
}




    
 

