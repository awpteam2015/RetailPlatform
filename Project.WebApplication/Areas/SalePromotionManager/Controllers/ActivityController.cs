

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
using Project.Model.SalePromotionManager;
using Project.Service.SalePromotionManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.SalePromotionManager.Controllers
{
    public class ActivityController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = ActivityService.GetInstance().GetModelByPk(pkId);
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
            var where = new ActivityEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.Title = RequestHelper.GetString("Title");
            //where.StartDate = RequestHelper.GetFormString("StartDate");
            //where.EndDate = RequestHelper.GetFormString("EndDate");
            where.State = RequestHelper.GetInt("State");
            //where.BriefDescription = RequestHelper.GetFormString("BriefDescription");
            var searchList = ActivityService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<ActivityEntity> postData)
        {
            postData.RequestEntity.BriefDescription = Base64Helper.DecodeBase64(postData.RequestEntity.BriefDescription);
            var addResult = ActivityService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<ActivityEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<ActivityEntity> postData)
        {
            postData.RequestEntity.BriefDescription = Base64Helper.DecodeBase64(postData.RequestEntity.BriefDescription);
            var newInfo = postData.RequestEntity;
            var orgInfo = ActivityService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = ActivityService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<ActivityEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = ActivityService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<ActivityEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




