

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
    public class ProductImageController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = ProductImageService.GetInstance().GetModelByPk(pkId);
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
            var where = new ProductImageEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.ProductId = RequestHelper.GetFormString("ProductId");
			//where.ImageUrl = RequestHelper.GetFormString("ImageUrl");
			//where.IsDefault = RequestHelper.GetFormString("IsDefault");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreationTime = RequestHelper.GetFormString("CreationTime");
			//where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
			//where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
			//where.DeleterUserCode = RequestHelper.GetFormString("DeleterUserCode");
			//where.DeletionTime = RequestHelper.GetFormString("DeletionTime");
            var searchList = ProductImageService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<ProductImageEntity> postData)
        {
            var addResult = ProductImageService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<ProductImageEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<ProductImageEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = ProductImageService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = ProductImageService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<ProductImageEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = ProductImageService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<ProductImageEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




