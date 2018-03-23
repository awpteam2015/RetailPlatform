

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
using Project.Model.SystemSetManager;
using Project.Service.SystemSetManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.SystemSetManager.Controllers
{
    public class AreaController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = AreaService.GetInstance().GetModelByPk(pkId);
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
            var where = new AreaEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.AreaId = RequestHelper.GetFormString("AreaId");
            where.Area = RequestHelper.GetFormString("Area");
            where.CityId = RequestHelper.GetFormString("CityId");
            //where.FirstWeightPrice = RequestHelper.GetFormString("FirstWeightPrice");
            //where.SecondWeightPrice = RequestHelper.GetFormString("SecondWeightPrice");
            var searchList = AreaService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        /// <summary>
        /// 省
        /// </summary>
        /// <returns></returns>
        public AbpJsonResult GetList_Combobox_Province()
        {
            var where = new ProvinceEntity();
            var searchList = ProvinceService.GetInstance().GetList(where);
            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }

        /// <summary>
        /// 市
        /// </summary>
        /// <returns></returns>
        public AbpJsonResult GetList_Combobox_City()
        {
            var where = new CityEntity();
            where.ProvinceId = RequestHelper.GetString("ProvinceId");
            var searchList = CityService.GetInstance().GetList(where);
            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }

        /// <summary>
        /// 区
        /// </summary>
        /// <returns></returns>
        public AbpJsonResult GetList_Combobox_Area()
        {
            var where = new AreaEntity();
            where.CityId = RequestHelper.GetString("CityId");
            var searchList = AreaService.GetInstance().GetList(where);
            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<AreaEntity> postData)
        {
            var addResult = AreaService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<AreaEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<AreaEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = AreaService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = AreaService.GetInstance().Update(mergInfo);

            var result = new AjaxResponse<AreaEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = AreaService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<AreaEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




