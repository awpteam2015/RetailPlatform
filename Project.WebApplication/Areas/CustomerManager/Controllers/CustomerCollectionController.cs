

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
using Project.Model.CustomerManager;
using Project.Service.CustomerManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.CustomerManager.Controllers
{
    public class CustomerCollectionController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = CustomerCollectionService.GetInstance().GetModelByPk(pkId);
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
            var where = new CustomerCollectionEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.CustomerId = RequestHelper.GetFormString("CustomerId");
			//where.ProductId = RequestHelper.GetFormString("ProductId");
			//where.ProductName = RequestHelper.GetFormString("ProductName");
			//where.GoodsId = RequestHelper.GetFormString("GoodsId");
			//where.GoodsCode = RequestHelper.GetFormString("GoodsCode");
			//where.ProductCode = RequestHelper.GetFormString("ProductCode");
			//where.ImageUrl = RequestHelper.GetFormString("ImageUrl");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreationTime = RequestHelper.GetFormString("CreationTime");
			//where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
			//where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
			//where.DeleterUserCode = RequestHelper.GetFormString("DeleterUserCode");
			//where.DeletionTime = RequestHelper.GetFormString("DeletionTime");
            var searchList = CustomerCollectionService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<CustomerCollectionEntity> postData)
        {
            var addResult = CustomerCollectionService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<CustomerCollectionEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<CustomerCollectionEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = CustomerCollectionService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = CustomerCollectionService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<CustomerCollectionEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = CustomerCollectionService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<CustomerCollectionEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




