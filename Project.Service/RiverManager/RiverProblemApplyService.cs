
/***************************************************************************
*       功能：     RMRiverProblemApply业务处理层
*       作者：     李伟伟
*       日期：     2016/7/24
*       描述：     河道问题申请单
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.AccessControl;
using NHibernate.Linq;
using OpenMas;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.RiverManager;
using Project.Repository.RiverManager;
using Project.Service.PermissionManager;

namespace Project.Service.RiverManager
{
    public class RiverProblemApplyService
    {

        #region 构造函数
        private readonly RiverProblemApplyRepository _riverProblemApplyRepository;
        private static readonly RiverProblemApplyService Instance = new RiverProblemApplyService();

        public RiverProblemApplyService()
        {
            this._riverProblemApplyRepository = new RiverProblemApplyRepository();
        }

        public static RiverProblemApplyService GetInstance()
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
        public System.Int32 Add(RiverProblemApplyEntity entity)
        {
            return _riverProblemApplyRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _riverProblemApplyRepository.GetById(pkId);
                _riverProblemApplyRepository.Delete(entity);
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
        public bool Delete(RiverProblemApplyEntity entity)
        {
            try
            {
                _riverProblemApplyRepository.Delete(entity);
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
        public bool Update(RiverProblemApplyEntity entity)
        {
            SessionFactoryManager.GetCurrentSession().Clear();
            try
            {
                _riverProblemApplyRepository.Update(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public RiverProblemApplyEntity GetModelByPk(System.Int32 pkId)
        {
            return _riverProblemApplyRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【河道问题申请单】和总【河道问题申请单】数</returns>
        public System.Tuple<IList<RiverProblemApplyEntity>, int> Search(RiverProblemApplyEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<RiverProblemApplyEntity>();
            #region  

            if (where.Attr_DepartmentCode != null && where.Attr_DepartmentCode.Any())
                expr = expr.And(p => where.Attr_DepartmentCode.Contains(p.DepartmentCode));

            if (!string.IsNullOrEmpty(where.ZfCsUserCode))
                expr = expr.And(p => p.ZfCsUserCode.Contains(where.ZfCsUserCode));

            if (!string.IsNullOrEmpty(where.DbCsUserCode))
                expr = expr.And(p => p.DbCsUserCode.Contains(where.DbCsUserCode));

            if (where.Attr_CreationTimeStart != null)
                expr = expr.And(p => p.CreationTime >= where.Attr_CreationTimeStart);
            if (where.Attr_CreationTimeEnd != null)
                expr = expr.And(p => p.CreationTime < where.Attr_CreationTimeEnd);

            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.Title))
                expr = expr.And(p => p.Title.Contains(where.Title));

            if (!string.IsNullOrEmpty(where.RiverEntity.RiverRank))
                expr = expr.And(p => p.RiverEntity.RiverRank.Contains(where.RiverEntity.RiverRank));
            if (!string.IsNullOrEmpty(where.Des))
                expr = expr.And(p => p.Des.Contains(where.Des));
            if (where.ProblemType > 0)
                expr = expr.And(p => p.ProblemType == where.ProblemType);
            // if (!string.IsNullOrEmpty(where.PicUrl))
            //  expr = expr.And(p => p.PicUrl == where.PicUrl);
            if (!string.IsNullOrEmpty(where.DepartmentCode) && where.DepartmentCode != "0")
                expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.RiverId))
            //  expr = expr.And(p => p.RiverId == where.RiverId);
            if (!string.IsNullOrEmpty(where.RiverName))
                expr = expr.And(p => p.RiverName == where.RiverName);

            if (!string.IsNullOrEmpty(where.ApplyManName))
                expr = expr.And(p => p.ApplyManName == where.ApplyManName);

            if (where.IsSpecialDeal==1)
            {
                expr = expr.And(p => p.UserCode == where.UserCode
                || p.ZfCsUserCode == where.UserCode 
                || p.DbCsUserCode == where.UserCode 
                || p.DbUserCode == where.UserCode 
                 );
            }
            else
            {
                if (!string.IsNullOrEmpty(where.UserCode))
                    expr = expr.And(p => p.UserCode == where.UserCode);
            }
           


            if (!string.IsNullOrEmpty(where.UserName))
                expr = expr.And(p => p.UserName == where.UserName);

            if (where.IsDeal > 0)
                expr = expr.And(p => p.IsDeal == where.IsDeal);

            if (where.IsMark ==2)
                expr = expr.And(p => p.IsMark == where.IsMark);

            // if (!string.IsNullOrEmpty(where.Coords))
            //  expr = expr.And(p => p.Coords == where.Coords);

            if (where.State > 0)
            {
                expr = expr.And(p => p.State == where.State);
            }
            else if (!string.IsNullOrWhiteSpace(where.S_State))
            {
                expr = expr.And(p => where.S_State.Contains(p.State.ToString()));
            }


            if (where.IsUrgent < 2)
                expr = expr.And(p => p.IsUrgent == where.IsUrgent);
            if (where.IsSendMessage < 2)
                expr = expr.And(p => p.IsSendMessage == where.IsSendMessage);

            if (where.DbState > 0)
                expr = expr.And(p => p.DbState == where.DbState);

            if (where.IsExposure < 2)
                expr = expr.And(p => p.IsExposure == where.IsExposure);

            expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.DepartmentRemark))
            //  expr = expr.And(p => p.DepartmentRemark == where.DepartmentRemark);
            // if (!string.IsNullOrEmpty(where.DepartmentOpTime))
            //  expr = expr.And(p => p.DepartmentOpTime == where.DepartmentOpTime);
            // if (!string.IsNullOrEmpty(where.TopDepartmentRemark))
            //  expr = expr.And(p => p.TopDepartmentRemark == where.TopDepartmentRemark);
            // if (!string.IsNullOrEmpty(where.TopDepartmentOpTime))
            //  expr = expr.And(p => p.TopDepartmentOpTime == where.TopDepartmentOpTime);
            // if (!string.IsNullOrEmpty(where.FinishOpTime))
            //  expr = expr.And(p => p.FinishOpTime == where.FinishOpTime);
            // if (!string.IsNullOrEmpty(where.FinishRemark))
            //  expr = expr.And(p => p.FinishRemark == where.FinishRemark);
            // if (!string.IsNullOrEmpty(where.ReturnRemark))
            //  expr = expr.And(p => p.ReturnRemark == where.ReturnRemark);
            // if (!string.IsNullOrEmpty(where.ReturnOpTime))
            //  expr = expr.And(p => p.ReturnOpTime == where.ReturnOpTime);
            // if (!string.IsNullOrEmpty(where.IsExposure))
            //  expr = expr.And(p => p.IsExposure == where.IsExposure);
            // if (!string.IsNullOrEmpty(where.ExposureLever))
            //  expr = expr.And(p => p.ExposureLever == where.ExposureLever);
            // if (!string.IsNullOrEmpty(where.IsSendMessage))
            //  expr = expr.And(p => p.IsSendMessage == where.IsSendMessage);
            // if (!string.IsNullOrEmpty(where.IsActive))
            //  expr = expr.And(p => p.IsActive == where.IsActive);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            //if (!string.IsNullOrEmpty(where.CreationTime))
            //    expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserName))
            //  expr = expr.And(p => p.LastModifierUserName == where.LastModifierUserName);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            // if (!string.IsNullOrEmpty(where.DeleteRemark))
            //  expr = expr.And(p => p.DeleteRemark == where.DeleteRemark);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.DeleteUserName))
            //  expr = expr.And(p => p.DeleteUserName == where.DeleteUserName);
            // if (!string.IsNullOrEmpty(where.DeleteUserCode))
            //  expr = expr.And(p => p.DeleteUserCode == where.DeleteUserCode);
            // if (!string.IsNullOrEmpty(where.DeleteTime))
            //  expr = expr.And(p => p.DeleteTime == where.DeleteTime);
            #endregion
            var list = _riverProblemApplyRepository.Query().Fetch(p => p.RiverEntity).Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _riverProblemApplyRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RiverProblemApplyEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RiverProblemApplyEntity> GetList(RiverProblemApplyEntity where)
        {
            var expr = PredicateBuilder.True<RiverProblemApplyEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.Title))
            //  expr = expr.And(p => p.Title == where.Title);
            // if (!string.IsNullOrEmpty(where.Des))
            //  expr = expr.And(p => p.Des == where.Des);
            // if (!string.IsNullOrEmpty(where.ProblemType))
            //  expr = expr.And(p => p.ProblemType == where.ProblemType);
            // if (!string.IsNullOrEmpty(where.PicUrl))
            //  expr = expr.And(p => p.PicUrl == where.PicUrl);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.RiverId))
            //  expr = expr.And(p => p.RiverId == where.RiverId);
            // if (!string.IsNullOrEmpty(where.RiverName))
            //  expr = expr.And(p => p.RiverName == where.RiverName);
            // if (!string.IsNullOrEmpty(where.UserCode))
            //  expr = expr.And(p => p.UserCode == where.UserCode);
            // if (!string.IsNullOrEmpty(where.UserName))
            //  expr = expr.And(p => p.UserName == where.UserName);
            // if (!string.IsNullOrEmpty(where.Coords))
            //  expr = expr.And(p => p.Coords == where.Coords);
            // if (!string.IsNullOrEmpty(where.State))
            //  expr = expr.And(p => p.State == where.State);
            // if (!string.IsNullOrEmpty(where.DepartmentRemark))
            //  expr = expr.And(p => p.DepartmentRemark == where.DepartmentRemark);
            // if (!string.IsNullOrEmpty(where.DepartmentOpTime))
            //  expr = expr.And(p => p.DepartmentOpTime == where.DepartmentOpTime);
            // if (!string.IsNullOrEmpty(where.TopDepartmentRemark))
            //  expr = expr.And(p => p.TopDepartmentRemark == where.TopDepartmentRemark);
            // if (!string.IsNullOrEmpty(where.TopDepartmentOpTime))
            //  expr = expr.And(p => p.TopDepartmentOpTime == where.TopDepartmentOpTime);
            // if (!string.IsNullOrEmpty(where.FinishOpTime))
            //  expr = expr.And(p => p.FinishOpTime == where.FinishOpTime);
            // if (!string.IsNullOrEmpty(where.FinishRemark))
            //  expr = expr.And(p => p.FinishRemark == where.FinishRemark);
            // if (!string.IsNullOrEmpty(where.ReturnRemark))
            //  expr = expr.And(p => p.ReturnRemark == where.ReturnRemark);
            // if (!string.IsNullOrEmpty(where.ReturnOpTime))
            //  expr = expr.And(p => p.ReturnOpTime == where.ReturnOpTime);
            // if (!string.IsNullOrEmpty(where.IsExposure))
            //  expr = expr.And(p => p.IsExposure == where.IsExposure);
            // if (!string.IsNullOrEmpty(where.ExposureLever))
            //  expr = expr.And(p => p.ExposureLever == where.ExposureLever);
            // if (!string.IsNullOrEmpty(where.IsSendMessage))
            //  expr = expr.And(p => p.IsSendMessage == where.IsSendMessage);
            // if (!string.IsNullOrEmpty(where.IsActive))
            //  expr = expr.And(p => p.IsActive == where.IsActive);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserName))
            //  expr = expr.And(p => p.LastModifierUserName == where.LastModifierUserName);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            // if (!string.IsNullOrEmpty(where.DeleteRemark))
            //  expr = expr.And(p => p.DeleteRemark == where.DeleteRemark);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.DeleteUserName))
            //  expr = expr.And(p => p.DeleteUserName == where.DeleteUserName);
            // if (!string.IsNullOrEmpty(where.DeleteUserCode))
            //  expr = expr.And(p => p.DeleteUserCode == where.DeleteUserCode);
            // if (!string.IsNullOrEmpty(where.DeleteTime))
            //  expr = expr.And(p => p.DeleteTime == where.DeleteTime);
            #endregion
            var list = _riverProblemApplyRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="tel"></param>
        public void SendMsg(RiverProblemApplyEntity entity, string opname, int state)
        {
            var Message = "";
            if (state == 1)
            {
                Message = string.Format(new MessageTemplate().Template1, entity.ApplyManName, entity.PkId);
            }
            else
            {
                Message = string.Format(new MessageTemplate().Template2, entity.ApplyManName, entity.PkId, opname);
            }

            var tel = "";
            if (entity.State == 1)
            {
                var departmentInfo = DepartmentService.GetInstance().GetModelByDepartmentCode(entity.DepartmentCode);
                if (departmentInfo == null)
                {
                    return;
                }
                else
                {
                    tel = departmentInfo.Mobile;
                }
            }
            else
            {
                var userInfo = UserInfoService.GetInstance().GetUserInfo(entity.UserCode);
                if (userInfo == null)
                {
                    return;
                }
                else
                {
                    tel = userInfo.Mobile;
                }
            }

            if (!string.IsNullOrWhiteSpace(tel))
            {
                string Url = "http://jx008.openmas.net:9080/OpenMasService";
                Sms Client = new Sms(Url);
                string externcode = "2"; //自定义扩展代码（模块）
                string ApplicationID = "8088002";
                string Password = "GWuFdgdAJROI";
                try
                {

                    //发送短信
                    var result = Client.SendMessage(new[] { tel }, Message, externcode, ApplicationID, Password);
                }
                catch (Exception)
                {


                }
            }
        }


        #endregion
    }

    public class MessageTemplate
    {
        public string Template1
        {
            get { return "您好，当前有【{0}】举报的河道问题【{1}】待处理，请登录系统及时处理。"; }
        }

        public string Template2
        {
            get { return "您好，当前有【{0}】举报的河道问题【{1}】【{2}】已处理完毕，请登录系统查阅。"; }
        }
    }
}




