

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
    public class CustomerAddressController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = CustomerAddressService.GetInstance().GetModelByPk(pkId);
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
            var where = new CustomerAddressEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.CustomerId = RequestHelper.GetInt("CustomerId");
            //where.Province = RequestHelper.GetFormString("Province");
            //where.CityId = RequestHelper.GetFormString("CityId");
            //where.CountryId = RequestHelper.GetFormString("CountryId");
            //where.Address = RequestHelper.GetFormString("Address");
            //where.IsDefault = RequestHelper.GetFormString("IsDefault");
            //where.Remarks = RequestHelper.GetFormString("Remarks");
            //where.ReceiverName = RequestHelper.GetFormString("ReceiverName");
            //where.FamilyTelephone = RequestHelper.GetFormString("FamilyTelephone");
            //where.PostCode = RequestHelper.GetFormString("PostCode");
            //where.Mobilephone = RequestHelper.GetFormString("Mobilephone");
            var searchList = CustomerAddressService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<CustomerAddressEntity> postData)
        {
            var addResult = CustomerAddressService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<CustomerAddressEntity>()
               {
                   success = addResult>0,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<CustomerAddressEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = CustomerAddressService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = CustomerAddressService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<CustomerAddressEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = CustomerAddressService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<CustomerAddressEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




