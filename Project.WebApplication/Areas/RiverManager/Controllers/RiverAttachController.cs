

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using AutoMapper;
using Project.Config;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.Extensions;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.HRManager;
using Project.Model.Other;
using Project.Model.RiverManager;
using Project.Service.HRManager;
using Project.Service.RiverManager;
using Project.WebApplication.Controllers;
using Project.WebApplication.Models;
using ErrorInfo = Project.Infrastructure.FrameworkCore.WebMvc.Models.ErrorInfo;

namespace Project.WebApplication.Areas.RiverManager.Controllers
{
    public class RiverAttachController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RiverAttachService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            return View();
        }


        public ActionResult ViewHd(int pkId = 0)
        {
           
                var entity = RiverAttachService.GetInstance().GetModelByPk(pkId);

                var list = RiverAttachService.GetInstance().GetSameList(entity.RiverId.GetValueOrDefault(), entity.Year, entity.Month);


            
            return View(new ViewHdVM()
            {
                list = list
            });
        }


        public ActionResult List()
        {
            return View();
        }

        public ActionResult ViewList()
        {
            return View();
        }
        public ActionResult Upload()
        {
            return View();
        }
        public AbpJsonResult GetList()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new RiverAttachEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");  
            where.RiverId = RequestHelper.GetInt("RiverId");
            where.RiverName = RequestHelper.GetFormString("RiverName");
            where.RecordTime = RequestHelper.GetDateTime("RecordTime");
            //where.Remark = RequestHelper.GetFormString("Remark");
            where.IsMainData = 1;
            var searchList = RiverAttachService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetList2()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new RiverAttachEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");  
            where.RiverId = RequestHelper.GetInt("RiverId");
            where.RiverName = RequestHelper.GetFormString("RiverName");
            where.RecordTime = RequestHelper.GetDateTime("RecordTime");
            //where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = RiverAttachService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<RiverAttachEntity> postData)
        {
            var addResult = RiverAttachService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<RiverAttachEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<RiverAttachEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = RiverAttachService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = RiverAttachService.GetInstance().Update(mergInfo);

            var result = new AjaxResponse<RiverAttachEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = RiverAttachService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<RiverAttachEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


        [HttpPost]
        public AbpJsonResult Upload(AjaxRequest<UploadEntity> postData)
        {
            var path = Server.MapPath(postData.RequestEntity.FileUrl + "/" + postData.RequestEntity.FileName);
            Workbook workbook = new Workbook(path);
            Worksheet sheet = workbook.Worksheets[0];
            Cells cells = sheet.Cells;

            var resultMessage = "";
            for (int i = 1; i < cells.MaxDataRow + 1; i++)
            {
                var row = new RiverAttachEntity();
                //var month = cells[i, 0].StringValue.Trim();
                //var motherNum = int.Parse(month.Replace("月", ""));
                // row.RecordTime = new DateTime(DateTime.Now.Year, motherNum, 1);
                row.RiverId = int.Parse(cells[i, 0].StringValue.Trim());
                row.RiverName = cells[i, 1].StringValue.Trim();
                row.RiverChief = cells[i, 2].StringValue.Trim();
                row.RiverArea = cells[i, 3].StringValue.Trim();
                row.PointName = cells[i, 4].StringValue.Trim();
                row.RecordTime = new DateTime(DateTime.Now.Year, int.Parse(cells[i, 5].StringValue.Trim()), int.Parse(cells[i, 6].StringValue.Trim()));
                row.RiverFlow = cells[i, 7].StringValue.Trim();
                row.Zb1 = cells[i, 8].StringValue.Trim();
                row.Zb2 = cells[i, 9].StringValue.Trim();
                row.Zb3 = cells[i, 10].StringValue.Trim();
                row.WaterQualityRank = cells[i, 11].StringValue.Trim();
                row.Pointer = cells[i, 12].StringValue.Trim();
                row.Day = int.Parse(cells[i, 6].StringValue.Trim());
                row.Year = DateTime.Now.Year;
                row.Month = int.Parse(cells[i, 5].StringValue.Trim());


                //查找上个月数据
                if (!RiverAttachService.GetInstance().IfHasMonthRecord(row.RiverId.GetValueOrDefault(), row.Year,row.Month))
                {
                    var lowestRank = RiverAttachService.GetInstance()
                          .GetLowestRank(row.RiverId.GetValueOrDefault(), row.RecordTime.GetValueOrDefault().AddMonths(-1));

                    var oldsort = new SiteConfigEntiy().WaterRankList.IndexOf(lowestRank);
                    var newsort = new SiteConfigEntiy().WaterRankList.IndexOf(row.WaterQualityRank);

                    if (newsort > oldsort)
                    {
                        row.WaterQualityChange = 2;
                    }
                    else
                    {
                        row.WaterQualityChange = 1;
                    }
                    row.IsMainData = 1;
                }

                //row.Remark = cells[i, 3].StringValue.Trim();
                var rowResult = RiverAttachService.GetInstance().Add(row);
                if (rowResult < 0)
                {
                    resultMessage += row.RiverName + ",";
                }
            }

            var result = new AjaxResponse<string>()
            {
                success = string.IsNullOrWhiteSpace(resultMessage),

                error = string.IsNullOrWhiteSpace(resultMessage) ? null : new ErrorInfo() { message = resultMessage + "上传失败！" }
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());

        }

        public void ExportReport()
        {

            var excelStream = new FileStream(Server.MapPath("~/TemplateFile/RiverAttach.xlsx"), FileMode.Open);
            var workbook = new Workbook(excelStream);
            var designer = new WorkbookDesigner(workbook);

            var riverList = RiverService.GetInstance().GetList(new RiverEntity());
            riverList.ForEach(p =>
            {
                p.Attr_Temple_Date = DateTime.Now.Month + "月";
            });


            var datatable = riverList.ToDataTable();
            datatable.TableName = "Table1";
            designer.SetDataSource(datatable);
            designer.Process();

            var ms = designer.Workbook.SaveToStream();
            excelStream.Close();
            byte[] bt = ms.ToArray();
            string fileName = "水质水纹" + DateTime.Now.Month + "月" + ".xls";//客户端保存的文件名
                                                                         //以字符流的形式下载文件
            Response.ContentType = "application/vnd.ms-excel";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bt);
            Response.Flush();
            Response.End();
        }


    }
}




