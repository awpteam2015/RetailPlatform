

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
using Project.Service.ProductManager;
using Project.WebApplication.Areas.ProductManager.Models;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.ProductManager.Controllers
{
    public class ProductController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {

            var systemCategoryId = RequestHelper.GetInt("SystemCategoryId");

            var systemCategorySpecEntityList = SystemCategorySpecService.GetInstance().GetList(new SystemCategorySpecEntity() { SystemCategoryId = systemCategoryId });

            var systemCategoryAttributeEntityList = SystemCategoryAttributeService.GetInstance().GetList(new SystemCategoryAttributeEntity() { SystemCategoryId = systemCategoryId });

            var specEntityList = new List<SpecEntity>();
            systemCategorySpecEntityList.ForEach(p =>
            {
                specEntityList.Add(p.SpecEntity);
            });
            var specVmList = Mapper.Map<List<SpecVm>>(specEntityList);


            var attributeEntityList = new List<ExtAttributeEntity>();
            systemCategoryAttributeEntityList.ForEach(p =>
            {
                attributeEntityList.Add(p.AttributeEntity);
            });
            var attributeVmList = Mapper.Map<List<AttributeVm>>(attributeEntityList);


            if (pkId > 0)
            {
                var entity = ProductService.GetInstance().GetModelByPk(pkId);
                entity.GoodsEntityList.ForEach(p => { p.IsUse = 1; });

                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity,new NullToEmptyStringResolver());
                specVmList.ForEach(p =>
                {
                    p.SpecValueList.ForEach(x =>
                    {
                        if (entity.GoodsSpecValueEntityList.Any(z=>z.SpecValueId==x.SpecValueId))
                        {
                            x.IsCheck = 1;
                        }
                    });
                });
            }


            ViewBag.SpecVmList = JsonHelper.JsonSerializer(specVmList);
            ViewBag.AttributeVmList = JsonHelper.JsonSerializer(attributeVmList);
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
            var where = new ProductEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.ProductName = RequestHelper.GetFormString("ProductName");
            //where.SystemCategoryId = RequestHelper.GetFormString("SystemCategoryId");
            //where.ProductCategoryId = RequestHelper.GetFormString("ProductCategoryId");
            //where.ProductCategoryRoute = RequestHelper.GetFormString("ProductCategoryRoute");
            //where.BrandId = RequestHelper.GetFormString("BrandId");
            //where.ProductCode = RequestHelper.GetFormString("ProductCode");
            //where.Unit = RequestHelper.GetFormString("Unit");
            //where.BriefDescription = RequestHelper.GetFormString("BriefDescription");
            //where.Description = RequestHelper.GetFormString("Description");
            //where.Weight = RequestHelper.GetFormString("Weight");
            //where.WeightUnit = RequestHelper.GetFormString("WeightUnit");
            //where.MarketPrice = RequestHelper.GetFormString("MarketPrice");
            //where.SellPrice = RequestHelper.GetFormString("SellPrice");
            //where.Cost = RequestHelper.GetFormString("Cost");
            //where.PriceUnit = RequestHelper.GetFormString("PriceUnit");
            //where.StockNum = RequestHelper.GetFormString("StockNum");
            //where.BuyMaxNum = RequestHelper.GetFormString("BuyMaxNum");
            //where.BuyMinNum = RequestHelper.GetFormString("BuyMinNum");
            //where.ViewNum = RequestHelper.GetFormString("ViewNum");
            //where.CommentNum = RequestHelper.GetFormString("CommentNum");
            //where.SelledNum = RequestHelper.GetFormString("SelledNum");
            //where.Title = RequestHelper.GetFormString("Title");
            //where.Meta_Keywords = RequestHelper.GetFormString("Meta_Keywords");
            //where.Meta_Description = RequestHelper.GetFormString("Meta_Description");
            //where.IsShow = RequestHelper.GetFormString("IsShow");
            //where.IsCommand = RequestHelper.GetFormString("IsCommand");
            //where.Pdt_desc = RequestHelper.GetFormString("Pdt_desc");
            //where.Spec_desc = RequestHelper.GetFormString("Spec_desc");
            //where.Params_desc = RequestHelper.GetFormString("Params_desc");
            //where.Tags_desc = RequestHelper.GetFormString("Tags_desc");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
            //where.DeleterUserCode = RequestHelper.GetFormString("DeleterUserCode");
            //where.DeletionTime = RequestHelper.GetFormString("DeletionTime");
            //where.p_1 = RequestHelper.GetFormString("p_1");
            //where.p_2 = RequestHelper.GetFormString("p_2");
            //where.p_3 = RequestHelper.GetFormString("p_3");
            //where.p_4 = RequestHelper.GetFormString("p_4");
            //where.p_5 = RequestHelper.GetFormString("p_5");
            //where.p_6 = RequestHelper.GetFormString("p_6");
            //where.p_7 = RequestHelper.GetFormString("p_7");
            //where.p_8 = RequestHelper.GetFormString("p_8");
            //where.p_9 = RequestHelper.GetFormString("p_9");
            var searchList = ProductService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<ProductEntity> postData)
        {
            postData.RequestEntity.BriefDescription = Base64Helper.DecodeBase64(postData.RequestEntity.BriefDescription);
            postData.RequestEntity.Description = Base64Helper.DecodeBase64(postData.RequestEntity.Description);
            postData.RequestEntity.GoodsEntityList =new HashSet<GoodsEntity>(postData.RequestEntity.GoodsEntityList.Where(p => p.IsUse == 1));

           var addResult = ProductService.GetInstance().Add(postData.RequestEntity);

            var result = new AjaxResponse<ProductEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<ProductEntity> postData)
        {
            postData.RequestEntity.BriefDescription = Base64Helper.DecodeBase64(postData.RequestEntity.BriefDescription);
            postData.RequestEntity.Description = Base64Helper.DecodeBase64(postData.RequestEntity.Description);

            var updateResult =ProductService.GetInstance().Update(postData.RequestEntity);

            var result = new AjaxResponse<ProductEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = ProductService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<ProductEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




