

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
    public class TicketController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = TicketService.GetInstance().GetModelByPk(pkId);
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
            var where = new TicketEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.TicketCode = RequestHelper.GetFormString("TicketCode");
			//where.TickettypeId = RequestHelper.GetFormString("TickettypeId");
			//where.Status = RequestHelper.GetFormString("Status");
			//where.AvaildateStart = RequestHelper.GetFormString("AvaildateStart");
			//where.AvaildateEnd = RequestHelper.GetFormString("AvaildateEnd");
			//where.OrderNo = RequestHelper.GetFormString("OrderNo");
			//where.UseDate = RequestHelper.GetFormString("UseDate");
			//where.CustomerId = RequestHelper.GetFormString("CustomerId");
			//where.RuleId = RequestHelper.GetFormString("RuleId");
			//where.ActivityId = RequestHelper.GetFormString("ActivityId");
            var searchList = TicketService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<TicketEntity> postData)
        {
            var addResult = TicketService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<TicketEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<TicketEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = TicketService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = TicketService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<TicketEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = TicketService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<TicketEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




