

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
    public class ProductAttributeValueController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = ProductAttributeValueService.GetInstance().GetModelByPk(pkId);
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
            var where = new ProductAttributeValueEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.AttributeValueId = RequestHelper.GetFormString("AttributeValueId");
			//where.AttributeValueName = RequestHelper.GetFormString("AttributeValueName");
			//where.AttributeId = RequestHelper.GetFormString("AttributeId");
			//where.ProductId = RequestHelper.GetFormString("ProductId");
			//where.ValueContent = RequestHelper.GetFormString("ValueContent");
            var searchList = ProductAttributeValueService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<ProductAttributeValueEntity> postData)
        {
            var addResult = ProductAttributeValueService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<ProductAttributeValueEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<ProductAttributeValueEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = ProductAttributeValueService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = ProductAttributeValueService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<ProductAttributeValueEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = ProductAttributeValueService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<ProductAttributeValueEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




