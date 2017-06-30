

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
using Project.Model.ProductManager;
using Project.Service.ProductManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.ProductManager.Controllers
{
    public class SystemCategorySpecController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = SystemCategorySpecService.GetInstance().GetModelByPk(pkId);
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
            var where = new SystemCategorySpecEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.SpecId = RequestHelper.GetFormString("SpecId");
			//where.SystemCategoryId = RequestHelper.GetFormString("SystemCategoryId");
            var searchList = SystemCategorySpecService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<SystemCategorySpecEntity> postData)
        {
            var addResult = SystemCategorySpecService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<SystemCategorySpecEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<SystemCategorySpecEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = SystemCategorySpecService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = SystemCategorySpecService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<SystemCategorySpecEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = SystemCategorySpecService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<SystemCategorySpecEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




