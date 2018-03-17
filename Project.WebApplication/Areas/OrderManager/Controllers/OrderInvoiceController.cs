

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
    public class OrderInvoiceController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = OrderInvoiceService.GetInstance().GetModelByPk(pkId);
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
            var where = new OrderInvoiceEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.OrderNo = RequestHelper.GetFormString("OrderNo");
			//where.InvoiceTitle = RequestHelper.GetFormString("InvoiceTitle");
			//where.InvoiceContent = RequestHelper.GetFormString("InvoiceContent");
			//where.InvoiceCompany = RequestHelper.GetFormString("InvoiceCompany");
			//where.InvoiceNo = RequestHelper.GetFormString("InvoiceNo");
			//where.Money = RequestHelper.GetFormString("Money");
			//where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = OrderInvoiceService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<OrderInvoiceEntity> postData)
        {
            var addResult = OrderInvoiceService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<OrderInvoiceEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<OrderInvoiceEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = OrderInvoiceService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = OrderInvoiceService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<OrderInvoiceEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = OrderInvoiceService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<OrderInvoiceEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




