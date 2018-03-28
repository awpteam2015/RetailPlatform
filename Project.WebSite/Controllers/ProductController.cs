using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.ProductManager;
using Project.Model.ProductManager.Request;
using Project.Service.ProductManager;
using Project.WebSite.Models;

namespace Project.WebSite.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var model = new ProductListVm();

            //通过分类查询
            var systemCategoryAttributeEntityList = SystemCategoryAttributeService.GetInstance().GetList(new SystemCategoryAttributeEntity() { SystemCategoryId = 6 });
            var attributeEntityList = new List<ExtAttributeEntity>();
            systemCategoryAttributeEntityList.ForEach(p =>
            {
                attributeEntityList.Add(p.AttributeEntity);
            });
            model.AttributList = Mapper.Map<List<AttributeVm>>(attributeEntityList);

            //商品列表
            var where = new ProductSearchCondition();
            where.AttributeValue1 = RequestHelper.GetInt("Attr1");
            where.AttributeValue2 = RequestHelper.GetInt("Attr2");
            where.AttributeValue3 = RequestHelper.GetInt("Attr3");
            where.ProductCode = RequestHelper.GetString("ProductCode");
            var pIndex = 0;
            var pSize = 100;
            where.skipResults = (pIndex - 1)*pSize;
            where.maxResults = pSize;
            var result = ProductService.GetInstance().SearchFront(where);
            model.ProductList = Mapper.Map<List<ProductVm>>(result.Item1);



            return View(model);
        }


    }
}