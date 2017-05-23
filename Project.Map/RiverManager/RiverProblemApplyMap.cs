
/***************************************************************************
*       功能：     RMRiverProblemApply映射类
*       作者：     李伟伟
*       日期：     2016/7/24
*       描述：     河道问题申请单
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.RiverManager;

namespace Project.Map.RiverManager
{
    public class RiverProblemApplyMap : BaseMap<RiverProblemApplyEntity, int>
    {
        public RiverProblemApplyMap() : base("RM_RiverProblemApply")
        {
            this.MapPkidDefault<RiverProblemApplyEntity, int>();
            Map(p => p.ApplyManName);
            Map(p => p.ApplyManTel);
            Map(p => p.IsDeal);
            Map(p => p.Title);
            Map(p => p.Des);
            Map(p => p.ProblemType);
            Map(p => p.PicUrl1);
            Map(p => p.PicUrl2);
            Map(p => p.PicUrl3);
            Map(p => p.FinishPicUrl);

            Map(p => p.FinishPicUrl2);
            Map(p => p.FinishPicUrl3);

            Map(p => p.DepartmentCode);
            Map(p => p.DepartmentName);
            Map(p => p.RiverId);
            Map(p => p.RiverName);
            Map(p => p.UserCode);
            Map(p => p.UserName);
            Map(p => p.Coords);
            Map(p => p.State);
            Map(p => p.DepartmentRemark);
            Map(p => p.DepartmentOpTime);
            Map(p => p.TopDepartmentRemark);
            Map(p => p.TopDepartmentOpTime);
            Map(p => p.FinishOpTime);
            Map(p => p.FinishRemark);
            Map(p => p.ReturnRemark);
            Map(p => p.ReturnOpTime);
            Map(p => p.IsExposure);
            Map(p => p.ExposureLever);
            Map(p => p.IsSendMessage);
            Map(p => p.IsActive);
            Map(p => p.CreatorUserName);
            Map(p => p.CreatorUserCode);
            Map(p => p.CreationTime);
            Map(p => p.LastModifierUserName);
            Map(p => p.LastModifierUserCode);
            Map(p => p.LastModificationTime);
            Map(p => p.Remark);
            Map(p => p.DeleteRemark);
            Map(p => p.IsDeleted);
            Map(p => p.DeleteUserName);
            Map(p => p.DeleteUserCode);
            Map(p => p.DeleteTime);

            Map(p => p.IsUrgent);
            Map(p => p.UrgentRemark);
            Map(p => p.ExposureArea);


            Map(p => p.DbFinishOpTime);
            Map(p => p.DbFinishRemark);
            Map(p => p.DbReturnRemark);
            Map(p => p.DbReturnOpTime);
            Map(p => p.ZfCsUserCode);
            Map(p => p.DbCsUserCode);
            Map(p => p.DbUserCode);
            Map(p => p.ZfCsUserName);
            Map(p => p.DbCsUserName);
            Map(p => p.DbUserName); Map(p => p.DbState); Map(p => p.IsMark);


            References(p => p.RiverEntity)
                  .Not.Insert()
                  .Not.Update()
                  .Column("RiverId");

        }
    }
}




