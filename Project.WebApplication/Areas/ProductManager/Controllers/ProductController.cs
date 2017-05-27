

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
    public class ProductController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = ProductService.GetInstance().GetModelByPk(pkId);
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
            var where = new ProductEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.ProductName = RequestHelper.GetFormString("ProductName");
			//where.ProductCode = RequestHelper.GetFormString("ProductCode");
			//where.Price = RequestHelper.GetFormString("Price");
			//where.ProductCategoryId = RequestHelper.GetFormString("ProductCategoryId");
			//where.SpecId1 = RequestHelper.GetFormString("SpecId1");
			//where.SpecId2 = RequestHelper.GetFormString("SpecId2");
			//where.SpecId3 = RequestHelper.GetFormString("SpecId3");
			//where.SpecName1 = RequestHelper.GetFormString("SpecName1");
			//where.SpecName2 = RequestHelper.GetFormString("SpecName2");
			//where.SpecName3 = RequestHelper.GetFormString("SpecName3");
			//where.Attribute1 = RequestHelper.GetFormString("Attribute1");
			//where.Attribute2 = RequestHelper.GetFormString("Attribute2");
			//where.Attribute3 = RequestHelper.GetFormString("Attribute3");
			//where.PicUrl1 = RequestHelper.GetFormString("PicUrl1");
			//where.PicUrl2 = RequestHelper.GetFormString("PicUrl2");
			//where.PicUrl3 = RequestHelper.GetFormString("PicUrl3");
            var searchList = ProductService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<ProductEntity> postData)
        {
            var addResult = ProductService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<ProductEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<ProductEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = ProductService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = ProductService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<ProductEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = ProductService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<ProductEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




