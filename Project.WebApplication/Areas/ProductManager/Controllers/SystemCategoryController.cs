

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
using Project.Model.ProductManager;
using Project.Service.ProductManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.ProductManager.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemCategoryController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = SystemCategoryService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);

                //var specList = SpecService.GetInstance().GetList(new SpecEntity());
                //var extAttributeList = ExtAttributeService.GetInstance().GetList(new ExtAttributeEntity());

                //ViewBag.SpecList = JsonHelper.JsonSerializer(new DataGridResponse()
                //{
                //    total = entity.SystemCategorySpecList.Count,
                //    rows = entity.SystemCategorySpecList
                //}, new NHibernateContractResolver());
                //ViewBag.ExtAttributeList = JsonHelper.JsonSerializer(new DataGridResponse()
                //{
                //    total = entity.SystemCategoryAttributeList.Count,
                //    rows = entity.SystemCategoryAttributeList
                //}, new NHibernateContractResolver());
            }
            

            var specHtml = "";
            var specList = SpecService.GetInstance().GetList(new SpecEntity());
            specList.ForEach(p =>
            {
                specHtml += "<option value=\""+p.PkId+"\">"+p.SpecName+"</option>";
            });

            var attributeHtml = "";
            var attributeList = ExtAttributeService.GetInstance().GetList(new ExtAttributeEntity());
            attributeList.ForEach(p =>
            {
                attributeHtml += "<option value=\"" + p.PkId + "\">" + p.AttributeName + "</option>";
            });

            ViewBag.SpecHtml = specHtml;
            ViewBag.AttributeHtml = attributeHtml;

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
            var where = new SystemCategoryEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.SystemCategoryName = RequestHelper.GetFormString("SystemCategoryName");
            //where.Sort = RequestHelper.GetFormString("Sort");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
            //where.DeleterUserCode = RequestHelper.GetFormString("DeleterUserCode");
            //where.DeletionTime = RequestHelper.GetFormString("DeletionTime");
            var searchList = SystemCategoryService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetListALL()
        {
            var searchList = SystemCategoryService.GetInstance().GetList(new SystemCategoryEntity());
            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }


        public AbpJsonResult GetSystemCategoryAttributeList()
        {
            var where = new SystemCategoryAttributeEntity();
            where.SystemCategoryId= RequestHelper.GetInt("SystemCategoryId");

            var list = SystemCategoryAttributeService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = list.Count(),
                rows = list
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetSystemCategorySpecList()
        {
            var where = new SystemCategorySpecEntity();
            where.SystemCategoryId= RequestHelper.GetInt("SystemCategoryId");

            var list = SystemCategorySpecService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = list.Count(),
                rows = list
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<SystemCategoryEntity> postData)
        {
            var addResult = SystemCategoryService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<SystemCategoryEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<SystemCategoryEntity> postData)
        {
            var updateResult = SystemCategoryService.GetInstance().Update(postData.RequestEntity);

            var result = new AjaxResponse<SystemCategoryEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = SystemCategoryService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<SystemCategoryEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




