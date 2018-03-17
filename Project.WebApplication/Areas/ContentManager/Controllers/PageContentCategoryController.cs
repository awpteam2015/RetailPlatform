

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
    public class PageContentCategoryController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = PageContentCategoryService.GetInstance().GetModelByPk(pkId);
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
            var where = new PageContentCategoryEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.PageContentCategoryName = RequestHelper.GetFormString("PageContentCategoryName");
			//where.ParentId = RequestHelper.GetFormString("ParentId");
			//where.Rank = RequestHelper.GetFormString("Rank");
			//where.Sort = RequestHelper.GetFormString("Sort");
			//where.Route = RequestHelper.GetFormString("Route");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreationTime = RequestHelper.GetFormString("CreationTime");
			//where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
			//where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
			//where.DeleterUserCode = RequestHelper.GetFormString("DeleterUserCode");
			//where.DeletionTime = RequestHelper.GetFormString("DeletionTime");
            var searchList = PageContentCategoryService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<PageContentCategoryEntity> postData)
        {
            var addResult = PageContentCategoryService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<PageContentCategoryEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<PageContentCategoryEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = PageContentCategoryService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = PageContentCategoryService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<PageContentCategoryEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = PageContentCategoryService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<PageContentCategoryEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




