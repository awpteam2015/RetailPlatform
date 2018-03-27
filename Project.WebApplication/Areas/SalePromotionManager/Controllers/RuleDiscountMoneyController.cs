

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
using Project.Model.SalePromotionManager;
using Project.Service.SalePromotionManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.SalePromotionManager.Controllers
{
    public class RuleDiscountMoneyController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RuleDiscountMoneyService.GetInstance().GetModelByPk(pkId);
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
            var where = new RuleDiscountMoneyEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.RuleId = RequestHelper.GetFormString("RuleId");
			//where.ActivityId = RequestHelper.GetFormString("ActivityId");
			//where.StartMoney = RequestHelper.GetFormString("StartMoney");
			//where.EndMoney = RequestHelper.GetFormString("EndMoney");
			//where.DiscountMoney = RequestHelper.GetFormString("DiscountMoney");
			//where.CardTypeIds = RequestHelper.GetFormString("CardTypeIds");
            var searchList = RuleDiscountMoneyService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<RuleDiscountMoneyEntity> postData)
        {
            var addResult = RuleDiscountMoneyService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<RuleDiscountMoneyEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<RuleDiscountMoneyEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = RuleDiscountMoneyService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = RuleDiscountMoneyService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<RuleDiscountMoneyEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = RuleDiscountMoneyService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<RuleDiscountMoneyEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




