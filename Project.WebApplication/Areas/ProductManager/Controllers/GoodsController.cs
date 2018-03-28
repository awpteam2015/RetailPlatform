

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
using Project.Model.ProductManager.Request;
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
            var where = new GoodsSearchCondition();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.ProductId = RequestHelper.GetFormString("ProductId");
            where.GoodsCode = RequestHelper.GetString("GoodsCode");
            where.ProductCode = RequestHelper.GetString("ProductCode");
            where.ProductName = RequestHelper.GetString("ProductName");
            where.skipResults = (pIndex - 1) * pSize;
            where.maxResults = pSize;
            //where.GoodsPrice = RequestHelper.GetFormString("GoodsPrice");
            //where.GoodsCost = RequestHelper.GetFormString("GoodsCost");
            //where.GoodsWeight = RequestHelper.GetFormString("GoodsWeight");
            //where.GoodsWeightUnit = RequestHelper.GetFormString("GoodsWeightUnit");
            //where.Unit = RequestHelper.GetFormString("Unit");
            //where.Title = RequestHelper.GetFormString("Title");
            //where.IsDefault = RequestHelper.GetFormString("IsDefault");
            var searchList = GoodsService.GetInstance().Search(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetGoodsInfo()
        {
            var entity = GoodsService.GetInstance().GetModelByGoodsCode(RequestHelper.GetString("GoodsCode"));

            var result = new AjaxResponse<GoodsEntity>()
            {
                success = entity!=null,
                result = entity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" ,"ProductInfo"}));
        }

    }
}




