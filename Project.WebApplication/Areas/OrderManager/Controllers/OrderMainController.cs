

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
    public class OrderMainController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = OrderMainService.GetInstance().GetModelByPk(pkId.ToString());
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
            var where = new OrderMainEntity();
			//where.OrderNo = RequestHelper.GetFormString("OrderNo");
			//where.State = RequestHelper.GetFormString("State");
			//where.Totalamount = RequestHelper.GetFormString("Totalamount");
			//where.PresentPoints = RequestHelper.GetFormString("PresentPoints");
			//where.CustomerId = RequestHelper.GetFormString("CustomerId");
			//where.CustomerName = RequestHelper.GetFormString("CustomerName");
			//where.Linkman = RequestHelper.GetFormString("Linkman");
			//where.LinkmanTel = RequestHelper.GetFormString("LinkmanTel");
			//where.LinkmanMobilephone = RequestHelper.GetFormString("LinkmanMobilephone");
			//where.LinkmanProvinceId = RequestHelper.GetFormString("LinkmanProvinceId");
			//where.LinkmanCityId = RequestHelper.GetFormString("LinkmanCityId");
			//where.LinkmanAreaId = RequestHelper.GetFormString("LinkmanAreaId");
			//where.LinkmanAddress = RequestHelper.GetFormString("LinkmanAddress");
			//where.LinkmanAddressfull = RequestHelper.GetFormString("LinkmanAddressfull");
			//where.LinkmanPostcode = RequestHelper.GetFormString("LinkmanPostcode");
			//where.LinkmanRemark = RequestHelper.GetFormString("LinkmanRemark");
			//where.CustomerAddressId = RequestHelper.GetFormString("CustomerAddressId");
			//where.PayTime = RequestHelper.GetFormString("PayTime");
			//where.PayRemark = RequestHelper.GetFormString("PayRemark");
			//where.SendTime = RequestHelper.GetFormString("SendTime");
			//where.SendNo = RequestHelper.GetFormString("SendNo");
			//where.SendRemark = RequestHelper.GetFormString("SendRemark");
			//where.ReturnTime = RequestHelper.GetFormString("ReturnTime");
			//where.ReturnRemark = RequestHelper.GetFormString("ReturnRemark");
			//where.ConfirmTime = RequestHelper.GetFormString("ConfirmTime");
			//where.ConfirmRemark = RequestHelper.GetFormString("ConfirmRemark");
			//where.UserIp = RequestHelper.GetFormString("UserIp");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreationTime = RequestHelper.GetFormString("CreationTime");
			//where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
			//where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
			//where.DeleterUserCode = RequestHelper.GetFormString("DeleterUserCode");
			//where.DeletionTime = RequestHelper.GetFormString("DeletionTime");
            var searchList = OrderMainService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<OrderMainEntity> postData)
        {
            var addResult = OrderMainService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<OrderMainEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<OrderMainEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = OrderMainService.GetInstance().GetModelByPk(postData.RequestEntity.PkId.ToString());
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = OrderMainService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<OrderMainEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = OrderMainService.GetInstance().DeleteByPkId(pkid.ToString());
            var result = new AjaxResponse<OrderMainEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




