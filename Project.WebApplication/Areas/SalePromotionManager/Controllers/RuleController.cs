

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
using Project.Model.SalePromotionManager;
using Project.Service.SalePromotionManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.SalePromotionManager.Controllers
{
    public class RuleController : BaseController
    {

        #region 视图

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RuleService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            return View();
        }

        public ActionResult RuleRaHd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RuleService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            return View();
        }

        public ActionResult RuleRbHd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RuleService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            return View();
        }

        public ActionResult RuleRcHd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RuleService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            return View();
        }

        public ActionResult RuleTypeList()
        {
            return View();
        }

        #endregion

        #region 列表搜索
        public ActionResult List()
        {
            return View();
        }

        public AbpJsonResult GetList()
        {
            //var pIndex = this.Request["page"].ConvertTo<int>();
            //var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new RuleEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.ActivityId = RequestHelper.GetInt("ActivityId");
            //where.RuleType = RequestHelper.GetFormString("RuleType");
            //where.Title = RequestHelper.GetFormString("Title");
            var searchList = RuleService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count(),
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetPromotionGoodsList()
        {
            //var pIndex = this.Request["page"].ConvertTo<int>();
            //var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new RulePromotionGoodsEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.ActivityId = RequestHelper.GetFormString("ActivityId");
            //where.RuleId = RequestHelper.GetFormString("RuleId");
            //where.ProductId = RequestHelper.GetFormString("ProductId");
            //where.ProductCode = RequestHelper.GetFormString("ProductCode");
            //where.GoodsCode = RequestHelper.GetFormString("GoodsCode");
            //where.GoodsId = RequestHelper.GetFormString("GoodsId");
            //where.Price = RequestHelper.GetFormString("Price");
            //where.PromotionPrice = RequestHelper.GetFormString("PromotionPrice");
            var searchList = RulePromotionGoodsService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count(),
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        #endregion

        #region 相关操作
        [HttpPost]
        public AbpJsonResult RuleRaAdd(AjaxRequest<RuleEntity> postData)
        {
            var addResult = RuleService.GetInstance().RuleRaAdd(postData.RequestEntity);
            var result = new AjaxResponse<RuleEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult RuleRaEdit(AjaxRequest<RuleEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = RuleService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = RuleService.GetInstance().RuleRaEdit(mergInfo);

            var result = new AjaxResponse<RuleEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


        [HttpPost]
        public AbpJsonResult RuleRbAdd(AjaxRequest<RuleEntity> postData)
        {
            var addResult = RuleService.GetInstance().RuleRbAdd(postData.RequestEntity);
            var result = new AjaxResponse<RuleEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult RuleRbEdit(AjaxRequest<RuleEntity> postData)
        {
            var updateResult = RuleService.GetInstance().RuleRbEdit(postData.RequestEntity);
            var result = new AjaxResponse<RuleEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }



        [HttpPost]
        public AbpJsonResult RuleRcAdd(AjaxRequest<RuleEntity> postData)
        {
            var addResult = RuleService.GetInstance().RuleRcAdd(postData.RequestEntity);
            var result = new AjaxResponse<RuleEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult RuleRcEdit(AjaxRequest<RuleEntity> postData)
        {
            var updateResult = RuleService.GetInstance().RuleRcEdit(postData.RequestEntity);

            var result = new AjaxResponse<RuleEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = RuleService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<RuleEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        #endregion
    }
}




