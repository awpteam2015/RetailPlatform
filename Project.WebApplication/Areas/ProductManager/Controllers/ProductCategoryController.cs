﻿

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
    public class ProductCategoryController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = ProductCategoryService.GetInstance().GetModelByPk(pkId);
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
            var where = new ProductCategoryEntity();
            var searchList = ProductCategoryService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridTreeResponse<ProductCategoryEntity>(searchList.Count, searchList);
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetList_Combotree()
        {
            //var where = new ProductCategoryEntity();
            //var searchList = ProductCategoryService.GetInstance().GetList(where);

            var dataGridEntity = ProductCategoryService.GetInstance().GetModelByPk(1);
            
            return new AbpJsonResult(new List<ProductCategoryEntity>() { dataGridEntity }, new NHibernateContractResolver(new string[] { "children" }));
        }
        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<ProductCategoryEntity> postData)
        {
            var addResult = ProductCategoryService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<ProductCategoryEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<ProductCategoryEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = ProductCategoryService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = ProductCategoryService.GetInstance().Update(mergInfo);

            var result = new AjaxResponse<ProductCategoryEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = ProductCategoryService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<ProductCategoryEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




