

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.ContentManager;
using Project.Service.ContentManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.ContentManager.Controllers
{
    public class OfflineActivityController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = OfflineActivityService.GetInstance().GetModelByPk(pkId);
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
            var where = new OfflineActivityEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.Tttle = RequestHelper.GetFormString("Tttle");
			//where.OfflineActivityAddress = RequestHelper.GetFormString("OfflineActivityAddress");
			//where.StartDate = RequestHelper.GetFormString("StartDate");
			//where.EndDate = RequestHelper.GetFormString("EndDate");
			//where.ImageUrl = RequestHelper.GetFormString("ImageUrl");
			//where.BriefDescription = RequestHelper.GetFormString("BriefDescription");
			//where.State = RequestHelper.GetFormString("State");
			//where.DeletionTime = RequestHelper.GetFormString("DeletionTime");
			//where.DeleterUserCode = RequestHelper.GetFormString("DeleterUserCode");
			//where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
			//where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
			//where.CreationTime = RequestHelper.GetFormString("CreationTime");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            var searchList = OfflineActivityService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<OfflineActivityEntity> postData)
        {
            var addResult = OfflineActivityService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<OfflineActivityEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<OfflineActivityEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = OfflineActivityService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = OfflineActivityService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<OfflineActivityEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = OfflineActivityService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<OfflineActivityEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




