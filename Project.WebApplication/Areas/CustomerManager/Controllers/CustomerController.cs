

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
    public class CustomerController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = CustomerService.GetInstance().GetModelByPk(pkId);
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
            var where = new CustomerEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.CardNo = RequestHelper.GetFormString("CardNo");
            //where.Password = RequestHelper.GetFormString("Password");
            where.CustomerName = RequestHelper.GetFormString("CustomerName");
            //where.Gender = RequestHelper.GetFormString("Gender");
            //where.Birthday = RequestHelper.GetFormString("Birthday");
            //where.Email = RequestHelper.GetFormString("Email");
            //where.Familytelephone = RequestHelper.GetFormString("Familytelephone");
            //where.Postcode = RequestHelper.GetFormString("Postcode");
            where.Mobilephone = RequestHelper.GetFormString("Mobilephone");
            //where.ProvinceId = RequestHelper.GetFormString("ProvinceId");
            //where.CityId = RequestHelper.GetFormString("CityId");
            //where.CountryId = RequestHelper.GetFormString("CountryId");
            //where.Address = RequestHelper.GetFormString("Address");
            //where.Memo = RequestHelper.GetFormString("Memo");
            //where.Discount = RequestHelper.GetFormString("Discount");
            //where.Totalamount = RequestHelper.GetFormString("Totalamount");
            //where.Totalpoints = RequestHelper.GetFormString("Totalpoints");
            //where.Availablepoints = RequestHelper.GetFormString("Availablepoints");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
            //where.DeleterUserCode = RequestHelper.GetFormString("DeleterUserCode");
            //where.DeletionTime = RequestHelper.GetFormString("DeletionTime");
            var searchList = CustomerService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<CustomerEntity> postData)
        {
            postData.RequestEntity.Password = Encrypt.MD5Encrypt(postData.RequestEntity.Password);

            var addResult = CustomerService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<CustomerEntity>()
               {
                   success = addResult.Item1,
                   result = postData.RequestEntity,
                   error = new ErrorInfo() { message = addResult.Item2 }
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<CustomerEntity> postData)
        {
           

            var newInfo = postData.RequestEntity;
            var orgInfo = CustomerService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = CustomerService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<CustomerEntity>()
            {
                success = updateResult.Item1,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        //[HttpPost]
        //public AbpJsonResult Delete(int pkid)
        //{
        //    var deleteResult = CustomerService.GetInstance().DeleteByPkId(pkid);
        //    var result = new AjaxResponse<CustomerEntity>()
        //    {
        //        success = deleteResult
        //    };
        //    return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        //}
    }
}




