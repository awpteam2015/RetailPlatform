

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.RiverManager;
using Project.Service.PermissionManager;
using Project.Service.RiverManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.RiverManager.Controllers
{
    public class MsgNoticeController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = MsgNoticeService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            return View();
        }

 
        public ActionResult List()
        {
            return View();
        }

        public AbpJsonResult GetList()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new MsgNoticeEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			where.Title = RequestHelper.GetFormString("Title");


            var departmentList =
               UserInfoService.GetInstance().GetUserInfo(LoginUserInfo.UserCode).UserDepartmentList;
            if (departmentList.Any())
            {
                where.BelongCompanys = departmentList.Select(p => p.DepartmentCode).Aggregate((a, b) =>
                {
                    return a + "" + b;
                });
            }

            //where.Des = RequestHelper.GetFormString("Des");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.IsDelete = RequestHelper.GetFormString("IsDelete");
            //where.IsSend = RequestHelper.GetFormString("IsSend");
            //where.SendTime = RequestHelper.GetFormString("SendTime");
            var searchList = MsgNoticeService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<MsgNoticeEntity> postData)
        {
            postData.RequestEntity.Des = Base64Helper.DecodeBase64(postData.RequestEntity.Des);

            var departmentList =
                UserInfoService.GetInstance().GetUserInfo(postData.RequestEntity.CreatorUserCode).UserDepartmentList;
            if (departmentList.Any())
            {
                postData.RequestEntity.BelongCompanys = departmentList.Select(p=>p.DepartmentCode).Aggregate((a, b) =>
                {
                    return a + "," + b;
                });
            }
          
            var addResult = MsgNoticeService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<MsgNoticeEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<MsgNoticeEntity> postData)
        {
            postData.RequestEntity.Des = Base64Helper.DecodeBase64(postData.RequestEntity.Des);
            var newInfo = postData.RequestEntity;
            var orgInfo = MsgNoticeService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = MsgNoticeService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<MsgNoticeEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = MsgNoticeService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<MsgNoticeEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




