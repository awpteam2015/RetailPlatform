

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.PermissionManager;
using Project.Model.RiverManager;
using Project.Service.PermissionManager;
using Project.Service.RiverManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.RiverManager.Controllers
{
    public class RiverProblemApplyController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RiverProblemApplyService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity, new NHibernateContractResolver());
                ViewBag.DbList = JsonHelper.JsonSerializer(RiverProblemDbService.GetInstance().GetList(new RiverProblemDbEntity() { RiverProblemApplyId = pkId }), new NHibernateContractResolver());
            }
            return View();
        }
        public ActionResult DelHd(int pkId = 0)
        {
            return Hd(pkId);
        }

        public ActionResult ZfHd(int pkId = 0)
        {
            return Hd(pkId);
        }

        public ActionResult BgHd(int pkId = 0)
        {
            return Hd(pkId);
        }

        public ActionResult PsHd(int pkId = 0)
        {
            return Hd(pkId);
        }
        public ActionResult DbHd(int pkId = 0)
        {
            return Hd(pkId);
        }
        public ActionResult ViewHd(int pkId = 0)
        {
            return Hd(pkId);
        }

        public ActionResult ReturnHd(int pkId = 0)
        {
            return Hd(pkId);
        }

        public ActionResult DbReturnHd(int pkId = 0)
        {
            return Hd(pkId);
        }

        public ActionResult DbFinishHd(int pkId = 0)
        {
            return Hd(pkId);
        }

        public ActionResult FinishHd(int pkId = 0)
        {
            return Hd(pkId);
        }


        public ActionResult List()
        {
            return View();
        }

        public ActionResult ZfList()
        {
            return View();
        }

        /// <summary>
        /// 问题督办列表 对已督办的问题进行不同颜色或图标进行标识。
        /// </summary>
        /// <returns></returns>
        public ActionResult DbList()
        {
            return View();
        }
 public ActionResult DbFinishList()
        {
            return View();
        }

        /// <summary>
        /// 问题曝光
        /// </summary>
        /// <returns></returns>
        public ActionResult BgList()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult FinishList()
        {
            return View();
        }

        public ActionResult DbCsList()
        {
            return View();
        }


        public ActionResult ZfCsList()
        {
            return View();
        }



        /// <summary>
        /// 问题删除曝光
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteList()
        {
            return View();
        }


        public AbpJsonResult GetZfList()
        {
            return GetList("zf");
        }
        public AbpJsonResult GetDbFinishList()
        {
            return GetList("dbfinish");
        }

        public AbpJsonResult GetZfCsList()
        {
            return GetList("zfcs");
        }


        public AbpJsonResult GetDbCsList()
        {
            return GetList("dbcs");
        }


        public AbpJsonResult GetFinishList()
        {
            return GetList("finish", "2,3,5");
        }

        public AbpJsonResult GetBgList()
        {
            return GetList("bg");
        }

        public AbpJsonResult GetAllRoleUserList(string departmentCode)
        {

            return new AbpJsonResult(UserInfoService.GetInstance().GetUserListByRole(departmentCode,3), new NHibernateContractResolver());
        }

        public AbpJsonResult GetRoleUserList(string departmentCode, int roleName)
        {

            return new AbpJsonResult(UserInfoService.GetInstance().GetUserListByRole(departmentCode, roleName), new NHibernateContractResolver());
        }

        public AbpJsonResult GetList(string command, string state = "")
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new RiverProblemApplyEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.Title = RequestHelper.GetFormString("Title");
            //where.Des = RequestHelper.GetFormString("Des");
            //where.ProblemType = RequestHelper.GetFormString("ProblemType");
            //where.PicUrl = RequestHelper.GetFormString("PicUrl");
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            //where.RiverId = RequestHelper.GetFormString("RiverId");
            where.RiverName = RequestHelper.GetFormString("RiverName");
            where.UserCode = RequestHelper.GetFormString("UserCode");
            where.UserName = RequestHelper.GetFormString("UserName");
            where.ProblemType = RequestHelper.GetFormInt("ProblemType", 0);
            //where.Coords = RequestHelper.GetFormString("Coords");
            where.State = RequestHelper.GetFormInt("State", 0);
            where.DbState = RequestHelper.GetFormInt("DbState", 0);
            where.S_State = state;


            where.IsUrgent = RequestHelper.GetFormInt("IsUrgent", 2);
            where.IsSendMessage = RequestHelper.GetFormInt("IsSendMessage", 2);
            where.IsExposure = RequestHelper.GetFormInt("IsExposure", 2);

            if (command == "zf")
            {
                where.Attr_DepartmentCode = LoginUserInfo.UserDepartmentIntList;
            }

            if (command == "finish")
            {
                where.UserCode = LoginUserInfo.UserCode;
            }

            if (command == "dbfinish")
            {
                where.DbUserCode = LoginUserInfo.UserCode;
            }
            if (command == "zfcs")
            {
                where.ZfCsUserCode = LoginUserInfo.UserCode;
            }

            if (command == "dbcs")
            {
                where.DbCsUserCode = LoginUserInfo.UserCode;
            }

            //if (command == "bg")
            //{
            //    where.IsExposure = 1;
            //}

            //where.DepartmentRemark = RequestHelper.GetFormString("DepartmentRemark");
            //where.DepartmentOpTime = RequestHelper.GetFormString("DepartmentOpTime");
            //where.TopDepartmentRemark = RequestHelper.GetFormString("TopDepartmentRemark");
            //where.TopDepartmentOpTime = RequestHelper.GetFormString("TopDepartmentOpTime");
            //where.FinishOpTime = RequestHelper.GetFormString("FinishOpTime");
            //where.FinishRemark = RequestHelper.GetFormString("FinishRemark");
            //where.ReturnRemark = RequestHelper.GetFormString("ReturnRemark");
            //where.ReturnOpTime = RequestHelper.GetFormString("ReturnOpTime");
            //where.IsExposure = RequestHelper.GetFormString("IsExposure");
            //where.ExposureLever = RequestHelper.GetFormString("ExposureLever");
            //where.IsSendMessage = RequestHelper.GetFormString("IsSendMessage");
            //where.IsActive = RequestHelper.GetFormString("IsActive");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModifierUserName = RequestHelper.GetFormString("LastModifierUserName");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.DeleteRemark = RequestHelper.GetFormString("DeleteRemark");
            where.IsDeleted = RequestHelper.GetInt("IsDeleted");
            //where.DeleteUserName = RequestHelper.GetFormString("DeleteUserName");
            //where.DeleteUserCode = RequestHelper.GetFormString("DeleteUserCode");
            //where.DeleteTime = RequestHelper.GetFormString("DeleteTime");
            var searchList = RiverProblemApplyService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<RiverProblemApplyEntity> postData)
        {
            postData.RequestEntity.Coords = Base64Helper.DecodeBase64(postData.RequestEntity.Coords);
            postData.RequestEntity.Des = Base64Helper.DecodeBase64(postData.RequestEntity.Des);

            var addResult = RiverProblemApplyService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<RiverProblemApplyEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<RiverProblemApplyEntity> postData)
        {
            var orgInfo = RiverProblemApplyService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);

            postData.RequestEntity.Coords = Base64Helper.DecodeBase64(postData.RequestEntity.Coords);

            if (!string.IsNullOrWhiteSpace(postData.RequestEntity.DeleteRemark))
            {
                postData.RequestEntity.IsDeleted = 1;
            }

            if (!string.IsNullOrWhiteSpace(postData.RequestEntity.UrgentRemark))
            {
                postData.RequestEntity.IsUrgent = 1;

                RiverProblemDbService.GetInstance().Add(new RiverProblemDbEntity()
                {
                    DbRemark = Base64Helper.DecodeBase64(postData.RequestEntity.UrgentRemark),
                    CreateTime = DateTime.Now,
                    UserName = orgInfo.UserName,
                    UserCode = orgInfo.UserCode,
                    RiverProblemApplyId = orgInfo.PkId
                });
            }

            postData.RequestEntity.DbFinishRemark = Base64Helper.DecodeBase64(postData.RequestEntity.DbFinishRemark);
            postData.RequestEntity.DbReturnRemark = Base64Helper.DecodeBase64(postData.RequestEntity.DbReturnRemark);


            postData.RequestEntity.UrgentRemark = Base64Helper.DecodeBase64(postData.RequestEntity.UrgentRemark);
            postData.RequestEntity.ReturnRemark = Base64Helper.DecodeBase64(postData.RequestEntity.ReturnRemark);
            postData.RequestEntity.TopDepartmentRemark = Base64Helper.DecodeBase64(postData.RequestEntity.TopDepartmentRemark);
            postData.RequestEntity.FinishRemark = Base64Helper.DecodeBase64(postData.RequestEntity.FinishRemark);
            postData.RequestEntity.DepartmentRemark = Base64Helper.DecodeBase64(postData.RequestEntity.DepartmentRemark);
            postData.RequestEntity.Des = Base64Helper.DecodeBase64(postData.RequestEntity.Des);
            postData.RequestEntity.DeleteRemark = Base64Helper.DecodeBase64(postData.RequestEntity.DeleteRemark);
            var newInfo = postData.RequestEntity;

            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = RiverProblemApplyService.GetInstance().Update(mergInfo);

            var result = new AjaxResponse<RiverProblemApplyEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


        /// <summary>
        /// 总部批示
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        public AbpJsonResult EditPs(AjaxRequest<RiverProblemApplyEntity> postData)
        {
            postData.RequestEntity.Coords = Base64Helper.DecodeBase64(postData.RequestEntity.Coords);
            var orgInfo = RiverProblemApplyService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            orgInfo.State = 4;
            var updateResult = RiverProblemApplyService.GetInstance().Update(orgInfo);

            var newInfo = new RiverProblemApplyEntity();
            var mergInfo = Mapper.Map(RiverProblemApplyService.GetInstance().GetModelByPk(postData.RequestEntity.PkId), newInfo);
            mergInfo.TopDepartmentRemark = Base64Helper.DecodeBase64(postData.RequestEntity.TopDepartmentRemark);
            mergInfo.State = 1;
            mergInfo.FinishRemark = "";
            mergInfo.DeleteRemark = "";
            mergInfo.IsDeleted = 0;
            mergInfo.IsExposure = 0;
            mergInfo.IsSendMessage = 0;


            var addResult = RiverProblemApplyService.GetInstance().Add(mergInfo);
            var result = new AjaxResponse<RiverProblemApplyEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = RiverProblemApplyService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<RiverProblemApplyEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Deal(int pkid)
        {
            var model = RiverProblemApplyService.GetInstance().GetModelByPk(pkid);
            model.IsDeal = 2;
            var dealResult = RiverProblemApplyService.GetInstance().Update(model);
            var result = new AjaxResponse<RiverProblemApplyEntity>()
            {
                success = dealResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


        [HttpPost]
        public AbpJsonResult Mark(int pkid)
        {
            var model = RiverProblemApplyService.GetInstance().GetModelByPk(pkid);
            model.IsMark = 2;
            var dealResult = RiverProblemApplyService.GetInstance().Update(model);
            var result = new AjaxResponse<RiverProblemApplyEntity>()
            {
                success = dealResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




