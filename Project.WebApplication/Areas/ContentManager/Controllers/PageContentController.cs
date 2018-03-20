

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
using Project.Model.ContentManager;
using Project.Service.ContentManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.ContentManager.Controllers
{
    public class PageContentController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = PageContentService.GetInstance().GetModelByPk(pkId);
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
            var where = new PageContentEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.Title1 = RequestHelper.GetFormString("Title1");
            //where.Title2 = RequestHelper.GetFormString("Title2");
            //where.Title3 = RequestHelper.GetFormString("Title3");
            //where.Description1 = RequestHelper.GetFormString("Description1");
            //where.Description2 = RequestHelper.GetFormString("Description2");
            //where.Description3 = RequestHelper.GetFormString("Description3");
            //where.ImageUrl1 = RequestHelper.GetFormString("ImageUrl1");
            //where.ImageUrl2 = RequestHelper.GetFormString("ImageUrl2");
            //where.ImageUrl3 = RequestHelper.GetFormString("ImageUrl3");
            //where.DeletionTime = RequestHelper.GetFormString("DeletionTime");
            //where.DeleterUserCode = RequestHelper.GetFormString("DeleterUserCode");
            //where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            var searchList = PageContentService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<PageContentEntity> postData)
        {
            postData.RequestEntity.Description1= Base64Helper.DecodeBase64(postData.RequestEntity.Description1);
            postData.RequestEntity.Description2 = Base64Helper.DecodeBase64(postData.RequestEntity.Description2);
            postData.RequestEntity.Description3 = Base64Helper.DecodeBase64(postData.RequestEntity.Description3);

            var addResult = PageContentService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<PageContentEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<PageContentEntity> postData)
        {
            postData.RequestEntity.Description1 = Base64Helper.DecodeBase64(postData.RequestEntity.Description1);
            postData.RequestEntity.Description2 = Base64Helper.DecodeBase64(postData.RequestEntity.Description2);
            postData.RequestEntity.Description3 = Base64Helper.DecodeBase64(postData.RequestEntity.Description3);
            var newInfo = postData.RequestEntity;
            var orgInfo = PageContentService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = PageContentService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<PageContentEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = PageContentService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<PageContentEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




