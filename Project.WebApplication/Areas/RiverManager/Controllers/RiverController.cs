

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.PermissionManager;
using Project.Model.RiverManager;
using Project.Service.PermissionManager;
using Project.Service.ReportManager;
using Project.Service.RiverManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.RiverManager.Controllers
{
    public class RiverController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RiverService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);

                ViewBag.UserList = JsonHelper.JsonSerializer(new DataGridResponse()
                {
                    total = entity.RiverOwerList.Count,
                    rows = entity.RiverOwerList
                }, new NHibernateContractResolver());

            }
            return View();
        }

        public ActionResult ViewHd(int pkId = 0)
        {
            return Hd(pkId);
        }


        public ActionResult SetRiverChief(int pkId = 0)
        {
            return Hd(pkId);
        }

        public ActionResult TdMap(int pkId = 0, string command = "")
        {
            if (pkId > 0)
            {

                switch (command)
                {
                    case "setArea":
                        ViewBag.Coords = RiverService.GetInstance().GetModelByPk(pkId).Coords;
                        break;
                    case "setPoint":
                        ViewBag.Coords = "[" + RiverCheckService.GetInstance().GetModelByPk(pkId).Coords + "]";
                        break;
                    case "setPoint2":
                        ViewBag.Coords = "[" + RiverProblemApplyService.GetInstance().GetModelByPk(pkId).Coords + "]";
                        break;
                    default:
                        break;
                }
            }
            return View();
        }

        public ActionResult TdMap3()
        {
            var where = new RiverEntity();
            where.Attr_DepartmentCodes = LoginUserInfo.UserDepartmentIntList;

            //var coordsList = RiverService.GetInstance().GetList(new RiverEntity()).Select(p=>p.Coords).Aggregate((a, b) =>
            //{
            //    return a +"-"+ b;
            //});

            var riverList = RiverService.GetInstance().Search(where, 0, 99999).Item1.Select(p => new
            {
                RiverName = p.RiverName,
                DepartmentName = p.DepartmentName,
                RiverRank = p.RiverRank,
                RiverStart = p.RiverStart,
                RiverEnd = p.RiverEnd,
                RiverLength = p.RiverLength,
                RiverCrossArea = p.RiverCrossArea,
                WaterQualityChange = RiverAttachService.GetInstance().GetLatestRecord(p.PkId).WaterQualityChange,
                WaterQualityRank = RiverAttachService.GetInstance().GetLatestRecord(p.PkId).WaterQualityRank,
                Coords = p.Coords
            });

            // ViewBag.CoordsList = coordsList;
            ViewBag.RiverList = JsonConvert.SerializeObject(riverList);
            return View();
        }


        public ActionResult TdMap2(int pkId = 0)
        {
            //var entity = RiverService.GetInstance().GetModelByPk(pkId);
            //ViewBag.Coords = entity.Coords;
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult CheckStateList()
        {
            return View();
        }

        public ActionResult PageList()
        {
            return View();
        }
        public ActionResult SumList()
        {
            return View();
        }


        public AbpJsonResult GetList()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new RiverEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.RiverName = RequestHelper.GetFormString("RiverName");
            where.RiverRank = RequestHelper.GetFormString("RiverRank");
            //where.RiverArea = RequestHelper.GetFormString("RiverArea");
            //where.RiverLength = RequestHelper.GetFormString("RiverLength");
            where.RiverCrossArea = RequestHelper.GetFormString("RiverCrossArea");


            if (UserInfoService.GetInstance().IsDepartmentRole(LoginUserInfo.UserCode))
            {
                where.Attr_DepartmentCodes = LoginUserInfo.UserDepartmentIntList;
            }
            else
            {
                where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            }


            where.UserCode = RequestHelper.GetFormString("UserCode");
            where.UserName = RequestHelper.GetFormString("UserName");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = RiverService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetCheckStateList()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new UserInfoEntity();
  
            where.UserName = RequestHelper.GetString("UserName");
            where.BeginDate = RequestHelper.GetDateTime("BeginDate");
            where.EndDate = RequestHelper.GetDateTime("EndDate");
            where.RiverName = RequestHelper.GetString("RiverName");

            var searchList = UserInfoService.GetInstance().Search2(where, (pIndex - 1) * pSize, pSize);


            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public void ExportReport1()
        {
            string excelFileName = DateTime.Now.ToString() + ".xls";
            //防止中文文件名IE下乱码的问题
            // if (Request.Browser.Browser == "IE" || Request.Browser.Browser == "InternetExplorer")
            if (Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") != -1)
            {

            }
            else
                excelFileName = HttpUtility.UrlPathEncode(excelFileName);

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + excelFileName);
            Response.AddHeader("Cache-Control", "max-age=0");
            Response.Charset = "GB2312";
            Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            Response.ContentType = "application/vnd.xls";

            var where = new UserInfoEntity();

            where.UserName = RequestHelper.GetString("UserName");
            where.BeginDate = RequestHelper.GetDateTime("BeginDate");
            where.EndDate = RequestHelper.GetDateTime("EndDate");
            where.RiverName = RequestHelper.GetString("RiverName");
            var searchList = UserInfoService.GetInstance().Search2(where,0, 0,true);

            var jsonBuilder = new StringBuilder();
            jsonBuilder.AppendFormat(@"<table class='GridViewStyle' style='border-collapse:collapse;width:1000px;' cellspacing='0' rules='all' border='1'>
                <tr>
                    <th>河长姓名</th>
                    <th>河长级别</th>
                    <th>巡河情况</th>
                    <th>状态</th>
                   </tr>");
            searchList.Item1.ForEach(p =>
            {
                jsonBuilder.Append("<tr>");
                jsonBuilder.AppendFormat("<td>{0}</td>", p.UserName);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.Jb);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.Times);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.State);
               
                jsonBuilder.Append("</tr>");
            });

            jsonBuilder.Append("</table>");
            Response.Write(jsonBuilder.ToString());
            Response.End();
        }



        public AbpJsonResult GetListNoPage()
        {

            var where = new RiverEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.RiverName = RequestHelper.GetFormString("RiverName");
            where.RiverRank = RequestHelper.GetFormString("RiverRank");
            //where.RiverArea = RequestHelper.GetFormString("RiverArea");
            //where.RiverLength = RequestHelper.GetFormString("RiverLength");
            //where.RiverCrossArea = RequestHelper.GetFormString("RiverCrossArea");
            //where.Coords = RequestHelper.GetFormString("Coords");
            //where.IsActive = RequestHelper.GetFormString("IsActive");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = RiverService.GetInstance().GetList(where);


            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }

        public AbpJsonResult GetRiverDetail(int PkId)
        {
            var riverInfo = RiverService.GetInstance().GetModelByPk(PkId);

            var userList = UserRoleService.GetInstance().GetList(new UserRoleEntity() {RoleId = int.Parse(ConfigurationManager.AppSettings["ZfRole"] .ToString())});
            userList.ForEach(p =>
            {
                riverInfo.RiverOwerList.Add(new RiverOwerEntity()
                {
                    UserCode = p.UserCode,
                    UserName = UserInfoService.GetInstance().GetUserInfo(p.UserCode).UserName
                });
            });


            if (riverInfo != null)
            {
                return new AbpJsonResult(riverInfo, new NHibernateContractResolver(new string[] { "RiverOwerList" }));
            }
            else
            {
                return new AbpJsonResult("");
            }


        }



        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<RiverEntity> postData)
        {
            postData.RequestEntity.Coords = Base64Helper.DecodeBase64(postData.RequestEntity.Coords);
            var addResult = RiverService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<RiverEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<RiverEntity> postData)
        {
            postData.RequestEntity.Coords = Base64Helper.DecodeBase64(postData.RequestEntity.Coords);
            var newInfo = postData.RequestEntity;
            var orgInfo = RiverService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = RiverService.GetInstance().Update(mergInfo);

            var result = new AjaxResponse<RiverEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }



        [HttpPost]
        public AbpJsonResult SetRiverChief()
        {
            var RiverId = RequestHelper.GetInt("RiverId");
            var UserCode = RequestHelper.GetString("UserCode");
            var UserName = RequestHelper.GetString("UserName");
            var isCheck = RequestHelper.GetInt("IsCheck");

            var addResult = RiverService.GetInstance().SetRiverChief(RiverId, UserName, UserCode, isCheck);
            var result = new AjaxResponse<string>()
            {
                success = addResult,
                result = null
            };
            return new AbpJsonResult(result, null);
        }




        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = RiverService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<RiverEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




