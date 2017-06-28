

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
    public class GoodsController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = GoodsService.GetInstance().GetModelByPk(pkId);
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
            var where = new GoodsEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.GoodsCode = RequestHelper.GetFormString("GoodsCode");
			//where.GoodsName = RequestHelper.GetFormString("GoodsName");
			//where.ProductCode = RequestHelper.GetFormString("ProductCode");
			//where.SpecValue1 = RequestHelper.GetFormString("SpecValue1");
			//where.SpecValue2 = RequestHelper.GetFormString("SpecValue2");
			//where.SpecValue3 = RequestHelper.GetFormString("SpecValue3");
            var searchList = GoodsService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetAllList()
        {
   
            var where = new GoodsEntity();
            where.ProductId = RequestHelper.GetFormInt("ProductId",0);
          
            var searchList = GoodsService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count,
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<GoodsEntity> postData)
        {
            var addResult = GoodsService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<GoodsEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<GoodsEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = GoodsService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = GoodsService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<GoodsEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = GoodsService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<GoodsEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




