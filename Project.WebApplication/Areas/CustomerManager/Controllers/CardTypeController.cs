

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
using Project.Model.CustomerManager;
using Project.Service.CustomerManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.CustomerManager.Controllers
{
    public class CardTypeController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = CardTypeService.GetInstance().GetModelByPk(pkId);
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
            var where = new CardTypeEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.CardtypeName = RequestHelper.GetString("CardtypeName");
            //where.Discount = RequestHelper.GetFormString("Discount");
            var searchList = CardTypeService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<CardTypeEntity> postData)
        {
            var addResult = CardTypeService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<CardTypeEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<CardTypeEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = CardTypeService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = CardTypeService.GetInstance().Update(mergInfo);

            var result = new AjaxResponse<CardTypeEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = CardTypeService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<CardTypeEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


        public AbpJsonResult GetList_Combobox()
        {
            var where = new CardTypeEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.KeyCode = RequestHelper.GetFormString("KeyCode");
            // where.ParentKeyCode = RequestHelper.GetQueryString("ParentKeyCode");
            //where.KeyName = RequestHelper.GetFormString("KeyName");
            //where.KeyValue = RequestHelper.GetFormString("KeyValue");
            var searchList = CardTypeService.GetInstance().GetList(where);
            //if (!string.IsNullOrEmpty(RequestHelper.GetQueryString("AllFlag")))
            //{
            //    searchList.Insert(0, new DictionaryEntity() { KeyName = "全部", KeyValue = "" });
            //}
            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }

    }
}




