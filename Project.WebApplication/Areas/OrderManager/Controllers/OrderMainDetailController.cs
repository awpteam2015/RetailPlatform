

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
using Project.Model.OrderManager;
using Project.Service.OrderManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.OrderManager.Controllers
{
    public class OrderMainDetailController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = OrderMainDetailService.GetInstance().GetModelByPk(pkId);
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
            var where = new OrderMainDetailEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.OrderNo = RequestHelper.GetFormString("OrderNo");
			//where.ProductCategoryId = RequestHelper.GetFormString("ProductCategoryId");
			//where.ProductId = RequestHelper.GetFormString("ProductId");
			//where.GoodsCode = RequestHelper.GetFormString("GoodsCode");
			//where.GoodsId = RequestHelper.GetFormString("GoodsId");
			//where.Price = RequestHelper.GetFormString("Price");
			//where.PriceSubDiscount = RequestHelper.GetFormString("PriceSubDiscount");
			//where.TotalAmount = RequestHelper.GetFormString("TotalAmount");
			//where.ProductWeight = RequestHelper.GetFormString("ProductWeight");
			//where.SpecName = RequestHelper.GetFormString("SpecName");
            var searchList = OrderMainDetailService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<OrderMainDetailEntity> postData)
        {
            var addResult = OrderMainDetailService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<OrderMainDetailEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<OrderMainDetailEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = OrderMainDetailService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = OrderMainDetailService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<OrderMainDetailEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = OrderMainDetailService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<OrderMainDetailEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




