using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Castle.Core.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Model.Other;
using Project.Model.PermissionManager;
using Project.Model.ReportManager;
using Project.Model.RiverManager;
using Project.Service.HRManager;
using Project.Service.PermissionManager;
using Project.Service.ReportManager;
using Project.Service.RiverManager;
using Project.WebApplication.Models;
using Project.WebApplication.Models.Request;
using Project.WebApplication.Models.Response;

namespace Project.WebApplication.Controllers
{
    public class Zb
    {
        public string lngNTU { get; set; }

        public string latNTU { get; set; }


        public string MercatorLng { get; set; }

        public string MercatorLat { get; set; }


        public decimal Lat
        {
            get { return decimal.Parse(latNTU); }
        }

        public decimal Lng
        {
            get { return decimal.Parse(lngNTU); }
        }
    }


    public class OpenController : ApiController
    {

        protected override JsonResult<T> Json<T>(T content, JsonSerializerSettings serializerSettings, Encoding encoding)
        {
            serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new JsonConverter[]
                {
                    new IsoDateTimeConverter {DateTimeFormat = "yyyy-MM-dd HH:mm:ss"},
                    new StringEnumConverter() {}
                },
                // NullValueHandling = NullValueHandling.Ignore,
            };
            return new JsonResult<T>(content, serializerSettings, encoding, this);
        }

        //[HttpGet]
        //public JsonResult<WebAPIResponse<IList<string>>> GetRecords(string id)
        //{
        //    var list = new List<string>();
        //    return Json(new WebAPIResponse<IList<string>>(list));
        //}

        //[HttpPost]
        //public JsonResult<WebAPIResponse<IList<string>>> SetRecords(A a)
        //{
        //    var list = new List<string>();
        //    return Json(new WebAPIResponse<IList<string>>(list));
        //}

        /// <summary>
        /// 获取河流信息
        /// </summary>
        /// <param name="riverId"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<RiverEntity>> GetRiverEntity(int riverId)
        {
            var river = RiverService.GetInstance().GetModelByPk(riverId);
            //if (river.Coords != "")
            //{
            //    var jd = JsonConvert.DeserializeObject<Zb>(river.Coords);
            //    river.lngNTU =(decimal.Parse(jd.lngNTU)/100000).ToString() ;
            //    river.latNTU = (decimal.Parse(jd.latNTU) / 100000).ToString();
            //}


            if (river.RiverOwerList != null && river.RiverOwerList.Any())
            {
                river.RiverOwerList.ToList().ForEach(x =>
                {
                    //river.Attr_Lever += "," + DictionaryService.GetInstance().GetModelByKeyCode("Lever", UserInfoService.GetInstance().GetUserInfo(x.UserCode).Lever).KeyName;
                    river.Attr_Lever += "," + UserInfoService.GetInstance().GetUserInfo(x.UserCode).Lever;
                });
                river.Attr_Lever = river.Attr_Lever.Substring(1, river.Attr_Lever.Length - 1);
            }


            return Json(new WebAPIResponse<RiverEntity>(river));
        }

        /// <summary>
        /// 获取河流列表 老接口保留 最好统一用下面post的接口
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="skipResults">开始位置</param>
        /// <param name="maxResults">结束位置</param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<IList<RiverEntity>>> GetRiverList(string userCode, int skipResults, int maxResults)
        {
            var result = RiverService.GetInstance().Search(new RiverEntity() { UserCode = userCode }, skipResults, maxResults);
            return Json(new WebAPIResponse<IList<RiverEntity>>(result.Item1));
        }

        /// <summary>
        /// 获取河流列表 新接口post
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<WebAPIResponse<IList<RiverEntity>>> GetRiverList(GetRiverListRequest request)
        {
            var result = RiverService.GetInstance().Search(new RiverEntity()
            {
                UserCode = request.UserCode,
                UserName = request.UserName,
                DepartmentName = request.DepartmentName,
                RiverRank = request.RiverRank,
                RiverStart = request.RiverStart,
                RiverEnd = request.RiverEnd,
                RiverCrossArea = request.RiverCrossArea,
            }, request.skipResults, request.maxResults);
            return Json(new WebAPIResponse<IList<RiverEntity>>(result.Item1));
        }



        /// <summary>
        ///上传图片
        /// 参考客户端 .net 例子
        /// var client = new HttpClient();
        //var imageStream = Request.Files["UploadedImage"];
        //var fileContent = new StreamContent(imageStream.InputStream);
        //fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //{
        //    Name = "\"UploadedImage\"",
        //        FileName = "\"1.jpg\""
        //    }; // the extra quotes are key here

        //// fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        //var content = new MultipartFormDataContent { fileContent };
        //var t = client.PostAsync("http://localhost:8655//api/WeiXin/PostFile", content).Result.Content.ReadAsStringAsync().Result;
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult<WebAPIResponse<FileInfoDTO>>> PostFile()
        {
            //var result = new HttpResponseMessage(HttpStatusCode.OK);
            var fileInfo = new FileInfoDTO();
            if (Request.Content.IsMimeMultipartContent())
            {
                await Request.Content.ReadAsMultipartAsync(
                    new MultipartMemoryStreamProvider())
                    .ContinueWith(task =>
                    {
                        MultipartMemoryStreamProvider provider = task.Result;
                        foreach (HttpContent content in provider.Contents)
                        {
                            var orgFileName = content.Headers.ContentDisposition.FileName;
                            orgFileName = orgFileName.Substring(1, orgFileName.Length - 2);
                            Stream stream = content.ReadAsStreamAsync().Result;
                            Image image = Image.FromStream(stream);

                            string directoryName = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationManager.AppSettings["UploadFile"]);
                            if (!Directory.Exists(directoryName))
                            {
                                Directory.CreateDirectory(directoryName);
                            }
                            var fileName = string.Format("{0}-{1}-{2}", DateTime.Now.ToString("yyyyMMddHHmmss"),
                                Guid.NewGuid(), orgFileName);
                            var fullPath = Path.Combine(directoryName, fileName);
                            image.Save(fullPath);
                            fileInfo.fileUrl = ConfigurationManager.AppSettings["UploadFile"];
                            fileInfo.fileName = fileName;
                        }
                    });

                var opresult = fileInfo;
                var result = new WebAPIResponse<FileInfoDTO>(opresult);
                return Json(result);
            }
            else
            {
                var result = new WebAPIResponse<FileInfoDTO>(new ErrorInfo() { Message = "上传失败" });
                return Json(result);
            }
        }



        /// <summary>
        /// 用户的登录权限由管理进行分配，系统不提供用户注册，仅提供已登录用户的个人信息修改、密码修改等功能。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<UserInfoResponse>> Login(LoginRequest request)
        {
            var userInfo = UserInfoService.GetInstance().GetUserInfo(request.UserCode);
            if (userInfo == null || userInfo.Password != request.UserPassword)
            {
                return Json(new WebAPIResponse<UserInfoResponse>(new ErrorInfo()
                {
                    Message = "用户名或密码错误"
                }));
            }
            else
            {
                var mergInfo = Mapper.Map(userInfo, new UserInfoResponse());
                if (userInfo.UserDepartmentList != null && userInfo.UserDepartmentList.Any())
                {
                    mergInfo.Departments = userInfo.UserDepartmentList.ToList().Select(p => DepartmentService.GetInstance().GetModelByDepartmentCode(p.DepartmentCode).DepartmentName
                     ).Aggregate((a, b) => { return a + "," + b; });
                }

                if (userInfo.UserRoleList != null && userInfo.UserRoleList.Any())
                {
                    if (userInfo.UserRoleList.Any(p => p.RoleId == 1))
                        mergInfo.Role = "1";
                    else if (userInfo.UserRoleList.Any(p => p.RoleId == 4))
                    {
                        mergInfo.Role = "2";
                    }
                    else if (userInfo.UserRoleList.Any(p => p.RoleId == 5))
                    {
                        mergInfo.Role = "3";
                    }
                    else if (userInfo.UserRoleList.Any(p => p.RoleId == 1007))
                    {
                        mergInfo.Role = "4";
                    }
                    else if (userInfo.UserRoleList.Any(p => p.RoleId == 1008))
                    {
                        mergInfo.Role = "5";
                    }
                }
                return Json(new WebAPIResponse<UserInfoResponse>(mergInfo));
            }
        }



        /// <summary>
        /// 修改用户基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ChangeUserInfo(ChangeUserInfoRequest request)
        {
            var userInfo = UserInfoService.GetInstance().GetUserInfo(request.UserCode);
            userInfo.UserName = request.UserName;
            userInfo.Mobile = request.Mobile;
            var result = UserInfoService.GetInstance().UpdateNormalInfo(userInfo);
            return Json(new WebAPIResponse<string>(result));
        }

        /// <summary>
        ///修改密码 密码MD5
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ChangePassword(ChangePasswordRequest request)
        {
            var userInfo = UserInfoService.GetInstance().GetUserInfo(request.UserCode);
            if (userInfo == null || userInfo.Password != request.OldPassword)
            {
                return Json(new WebAPIResponse<string>(new Tuple<bool, string>(false, "旧密码错误")));
            }
            userInfo.Password = request.NewPassword;
            var result = UserInfoService.GetInstance().ChangePassword(userInfo);
            return Json(new WebAPIResponse<string>(result));
        }


        /// <summary>
        ///	巡河签到  河长需至所属河道附近进行巡河签到，系统将自动记录河长所在的位置信息、时间，并可上传图片，图片支持多张。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> Sign(SignRequest request)
        {

            var riverCheckEntity = new RiverCheckEntity();
            riverCheckEntity.UserCode = request.UserCode;
            riverCheckEntity.UserName = UserInfoService.GetInstance().GetUserInfo(request.UserCode).UserName;
            riverCheckEntity.Remark = request.Remark;
            riverCheckEntity.PicUrl1 = request.PicUrl1;
            riverCheckEntity.PicUrl2 = request.PicUrl2;
            riverCheckEntity.PicUrl3 = request.PicUrl3;
            riverCheckEntity.Coords = "{\"MercatorLng\":\"\",\"MercatorLat\":\"\",\"lngNTU\":" + request.lngNTU * 100000 + ",\"latNTU\":" + request.latNTU * 100000 + "}";
            riverCheckEntity.RiverId = request.RiverId;
            var riverInfo = RiverService.GetInstance().GetModelByPk(request.RiverId);

            riverCheckEntity.RiverName = riverInfo.RiverName;

            var riverArea = JsonConvert.DeserializeObject<List<Zb>>(riverInfo.Coords);
            var isIn = IsInside(new Zb()
            {
                latNTU = (request.latNTU * 100000).ToString(),
                lngNTU = (request.lngNTU * 100000).ToString()
            }, riverArea, request.EffectArea);
            if (isIn)
            {
                var result = RiverCheckService.GetInstance().Add(riverCheckEntity);

                return Json(new WebAPIResponse<string>(new Tuple<bool, string>(result > 0, "")));
            }
            else
            {
                return Json(new WebAPIResponse<string>(new Tuple<bool, string>(false, "未在河道区域")));
            }
        }


        public bool IsInside(Zb p, List<Zb> Points, int effectArea)
        {
            int count = Points.Count;

            if (count < 3)
            {
                return false;
            }

            bool result = false;

            for (int i = 0, j = count - 1; i < count; i++)
            {
                var p1 = Points[i];
                var p2 = Points[j];

                if (p1.Lat < p.Lat && p2.Lat >= p.Lat || p2.Lat < p.Lat && p1.Lat >= p.Lat)
                {
                    if (p1.Lng + (p.Lat - p1.Lat) / (p2.Lat - p1.Lat) * (p2.Lng - p1.Lng) < p.Lng)
                    {
                        result = !result;
                    }
                }
                j = i;
            }

            if (result == false)
            {
                var t = DictionaryService.GetInstance().GetModelByKeyCode("EffectArea");
                if (int.Parse(t.KeyValue) > effectArea)
                {
                    result = true;
                }
            }

            return result;
        }




        /// <summary>
        /// 问题提交
        /// </summary>
        /// <param name="request">上传实体 上传精度/纬度 lngNTU/latNTU
        /// </param>
        /// <returns>新生成的主键</returns>
        [HttpPost]
        public JsonResult<WebAPIResponse<int>> ProblemReport(ProblemReportRequest request)
        {
            var entity = new RiverProblemApplyEntity();
            entity.RiverId = request.RiverId;
            entity.ApplyManName = request.ApplyManName;
            entity.ApplyManTel = request.ApplyManTel;
            entity.Title = request.Title;
            entity.Des = request.Des;
            entity.ProblemType = int.Parse(request.ProblemType);
            entity.PicUrl1 = request.PicUrl1;
            entity.PicUrl2 = request.PicUrl2;
            entity.PicUrl3 = request.PicUrl3;
            entity.Coords = "{\"MercatorLng\":\"\",\"MercatorLat\":\"\",\"lngNTU\":" + request.lngNTU * 100000 + ",\"latNTU\":" + request.latNTU * 100000 + "}";

            var riverInfo = RiverService.GetInstance().GetModelByPk(request.RiverId);
            entity.RiverName = riverInfo.RiverName;
            entity.DepartmentCode = riverInfo.DepartmentCode;
            entity.DepartmentName = riverInfo.DepartmentName;
            entity.State = 1;

            var result = RiverProblemApplyService.GetInstance().Add(entity);
            if (result > 0)
            {
                RiverProblemApplyService.GetInstance().SendMsg(entity, entity.UserName, 1);
            }
            return Json(new WebAPIResponse<int>(new Tuple<bool, int>(result > 0, result)));
        }


        /// <summary>
        /// 问题转发
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ProblemZf(ProblemZfRequest request)
        {
            var entity = RiverProblemApplyService.GetInstance().GetModelByPk(request.RiverProblemApplyId);
            entity.UserCode = request.ToUserCode;
            entity.UserName = UserInfoService.GetInstance().GetUserInfo(request.ToUserCode).UserName;
            if (!string.IsNullOrWhiteSpace(request.ZfCsUserCode))
            {
                entity.ZfCsUserCode = request.ZfCsUserCode;
                entity.ZfCsUserName = UserInfoService.GetInstance().GetUserInfo(request.ZfCsUserCode).UserName;
            }
            entity.DepartmentRemark = request.Remark;
            entity.State = 2;
            var result = RiverProblemApplyService.GetInstance().Update(entity);
            if (result)
            {
                RiverProblemApplyService.GetInstance().SendMsg(entity, entity.UserName, 1);
            }
            return Json(new WebAPIResponse<string>(new Tuple<bool, string>(result, "")));
        }



        /// <summary>
        /// 问题督办 并发送短信
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ProblemDb(ProblemDbRequest request)
        {
            var entity = RiverProblemApplyService.GetInstance().GetModelByPk(request.RiverProblemApplyId);
            entity.DbUserCode = request.DbUserCode;
            entity.DbUserName = UserInfoService.GetInstance().GetUserInfo(request.DbUserCode).UserName;
            if (!string.IsNullOrWhiteSpace(request.DbCsUserCode))
            {
                entity.DbCsUserCode = request.DbCsUserCode;
                entity.DbCsUserName = UserInfoService.GetInstance().GetUserInfo(request.DbCsUserCode).UserName;
            }

            entity.UrgentRemark = request.Remark;
            entity.IsUrgent = 1;
            var result = RiverProblemApplyService.GetInstance().Update(entity);
            if (result)
            {
                RiverProblemDbService.GetInstance().Add(new RiverProblemDbEntity()
                {
                    DbRemark = request.Remark,
                    CreateTime = DateTime.Now,
                    UserName = request.UserName,
                    UserCode = request.UserCode,
                    RiverProblemApplyId = request.RiverProblemApplyId
                });

                RiverProblemApplyService.GetInstance().SendMsg(entity, request.UserName, 1);
            }
            return Json(new WebAPIResponse<string>(new Tuple<bool, string>(result, "")));
        }

        /// <summary>
        /// 督办完结
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ProblemDbWj(ProblemDbWjRequest request)
        {
            var entity = RiverProblemApplyService.GetInstance().GetModelByPk(request.RiverProblemApplyId);
            entity.DbState = 2;
            var result = RiverProblemApplyService.GetInstance().Update(entity);

            return Json(new WebAPIResponse<string>(new Tuple<bool, string>(result, "")));
        }


        /// <summary>
        /// 督办回退
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ProblemDbHt(ProblemDbRequest request)
        {
            var entity = RiverProblemApplyService.GetInstance().GetModelByPk(request.RiverProblemApplyId);
            entity.DbState = 1;
            var result = RiverProblemApplyService.GetInstance().Update(entity);

            return Json(new WebAPIResponse<string>(new Tuple<bool, string>(result, "")));
        }

        /// <summary>
        /// 获取督办 转发 对应的角色人
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<WebAPIResponse<IList<UserRoleEntity>>> GetUserInfoList(GetUserInfoListRequest request)
        {

            var resultList = UserInfoService.GetInstance().GetUserListByRole(request.DepartmentCode, request.UserType);

            return Json(new WebAPIResponse<IList<UserRoleEntity>>(resultList));
        }

        /// <summary>
        /// 问题完结
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ProblemWj(ProblemWjRequest request)
        {
            var entity = RiverProblemApplyService.GetInstance().GetModelByPk(request.RiverProblemApplyId);
            entity.FinishRemark = request.Remark;
            entity.FinishOpTime = DateTime.Now;
            entity.State = 3;
            entity.FinishPicUrl = request.FinishPicUrl;
            entity.LastModifierUserCode = request.UserCode;
            entity.LastModifierUserName = request.UserName;
            var result = RiverProblemApplyService.GetInstance().Update(entity);
            if (result)
            {
                RiverProblemApplyService.GetInstance().SendMsg(entity, request.UserName, 2);
            }
            return Json(new WebAPIResponse<string>(new Tuple<bool, string>(result, "")));
        }

        /// <summary>
        /// 问题回退
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ProblemHt(ProblemHtRequest request)
        {
            var entity = RiverProblemApplyService.GetInstance().GetModelByPk(request.RiverProblemApplyId);
            entity.ReturnRemark = request.Remark;
            entity.ReturnOpTime = DateTime.Now;
            entity.State = 5;
            entity.LastModifierUserCode = request.UserCode;
            entity.LastModifierUserName = request.UserName;
            var result = RiverProblemApplyService.GetInstance().Update(entity);
            if (result)
            {
                RiverProblemApplyService.GetInstance().SendMsg(entity, request.UserName, 2);
            }
            return Json(new WebAPIResponse<string>(new Tuple<bool, string>(result, "")));
        }


        /// <summary>
        /// 批示重新生成
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ProblemPs(ProblemPsRequest request)
        {

            var orgInfo = RiverProblemApplyService.GetInstance().GetModelByPk(request.RiverProblemApplyId);
            orgInfo.State = 4;
            var updateResult = RiverProblemApplyService.GetInstance().Update(orgInfo);

            var newInfo = new RiverProblemApplyEntity();
            var mergInfo = Mapper.Map(updateResult, newInfo);
            mergInfo.TopDepartmentRemark = request.Remark;
            mergInfo.State = 1;
            mergInfo.FinishRemark = "";
            mergInfo.DeleteRemark = "";
            mergInfo.IsDeleted = 0;
            mergInfo.IsExposure = 0;
            mergInfo.IsSendMessage = 0;
            var result = RiverProblemApplyService.GetInstance().Add(mergInfo);
            return Json(new WebAPIResponse<string>(new Tuple<bool, string>(result > 0, "")));
        }

        /// <summary>
        /// 曝光
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ProblemBg(ProblemBgRequest request)
        {
            var entity = RiverProblemApplyService.GetInstance().GetModelByPk(request.RiverProblemApplyId);
            entity.IsExposure = request.IsExposure;
            entity.ExposureLever = request.ExposureLever;
            var result = RiverProblemApplyService.GetInstance().Update(entity);
            return Json(new WebAPIResponse<string>(new Tuple<bool, string>(result, "")));
        }


        /// <summary>
        /// 问题标识
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<string>> ProblemMark(ProblemMarkRequest request)
        {
            var entity = RiverProblemApplyService.GetInstance().GetModelByPk(request.RiverProblemApplyId);
            entity.IsMark = 2;
            var result = RiverProblemApplyService.GetInstance().Update(entity);
            return Json(new WebAPIResponse<string>(new Tuple<bool, string>(result, "")));
        }



        /// <summary>
        /// 问题列表
        /// </summary>
        /// <param name="request">
        /// 详细查看112.11.105.171 桌面 请求实体（会及时调整更新）
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<WebAPIResponse<IList<ProblemResponse>>> GetProblemList(GetProblemListRequest request)
        {
            string role = "";
            List<string> departments = new List<string>();
            if (!string.IsNullOrWhiteSpace(request.UserCode))
            {
                var userInfo = UserInfoService.GetInstance().GetUserInfo(request.UserCode);
                if (userInfo.UserRoleList != null && userInfo.UserRoleList.Any())
                {
                    if (userInfo.UserRoleList.Any(p => p.RoleId == 1) || userInfo.UserRoleList.Any(p => p.RoleId == 4))
                    {
                        departments = userInfo.UserDepartmentList.Select(p => p.DepartmentCode).ToList();
                        request.UserCode = "";
                    }
                }
            }


            var where = new RiverProblemApplyEntity();
            where.Attr_DepartmentCode = departments;
            where.Title = request.Title;
            where.UserCode = request.UserCode;
            where.IsDeal = request.IsDeal;
            where.UserName = request.UserName;
            where.RiverEntity.RiverRank = request.RiverRank;
            where.Attr_CreationTimeStart = request.StartDateTime;
            where.Attr_CreationTimeEnd = request.EndDateTime;
            where.Des = request.Des;
            where.IsDeleted = 0;
            where.IsUrgent = 2;
            where.IsExposure = request.IsExposure;
            where.IsSpecialDeal = departments.Any<string>() ? 0 : 1;
            where.ApplyManName = request.ApplyManName;

            var searchList = RiverProblemApplyService.GetInstance().Search(where, request.skipResults, request.maxResults);

            var mergInfo = Mapper.Map<IList<ProblemResponse>>(searchList.Item1);


            if (request.IsExposure == 1)
            {
                mergInfo.ForEach(p =>
                {
                    p.IsRead = MessageTagService.GetInstance().IsExist(1, int.Parse(p.PkId), request.UserCode);
                });
            }

            return Json(new WebAPIResponse<IList<ProblemResponse>>(mergInfo));
        }


        ///// <summary>
        ///// 新问题列表 列表  抄送可以从这里获取
        ///// </summary>
        ///// <param name="request">
        ///// </param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult<WebAPIResponse<IList<ProblemResponse>>> GetProblemList2(GetProblemListRequest2 request)
        //{
        //    var where = new RiverProblemApplyEntity();

        //    where.Title = request.Title;
        //    // where.IsDeal = request.IsDeal;
        //    where.UserName = request.UserName;
        //    where.RiverEntity.RiverRank = request.RiverRank;
        //    where.Attr_CreationTimeStart = request.StartDateTime;
        //    where.Attr_CreationTimeEnd = request.EndDateTime;
        //    where.Des = request.Des;
        //    where.IsDeleted = 0;
        //    where.IsUrgent = 2;
        //    // where.IsExposure = request.IsExposure;

        //    var command = request.Command;
        //    if (command == "zf")
        //    {
        //        List<string> departments = new List<string>();
        //        if (!string.IsNullOrWhiteSpace(request.UserCode))
        //        {
        //            var userInfo = UserInfoService.GetInstance().GetUserInfo(request.UserCode);
        //            if (userInfo.UserRoleList != null && userInfo.UserRoleList.Any())
        //            {
        //                if (userInfo.UserRoleList.Any(p => p.RoleId == 1) || userInfo.UserRoleList.Any(p => p.RoleId == 4))
        //                {
        //                    departments = userInfo.UserDepartmentList.Select(p => p.DepartmentCode).ToList();
        //                    request.UserCode = "";
        //                }
        //            }
        //        }
        //        where.Attr_DepartmentCode = departments;
        //    }

        //    if (command == "finish")
        //    {
        //        where.UserCode = request.UserCode;
        //    }

        //    if (command == "dbfinish")
        //    {
        //        where.DbUserCode = request.UserCode;
        //    }
        //    if (command == "zfcs")
        //    {
        //        where.ZfCsUserCode = request.UserCode;
        //    }

        //    if (command == "dbcs")
        //    {
        //        where.DbCsUserCode = request.UserCode;
        //    }

        //    if (command == "mark")
        //    {
        //        where.IsMark = 2;
        //    }

        //    var searchList = RiverProblemApplyService.GetInstance().Search(where, request.skipResults, request.maxResults);

        //    var mergInfo = Mapper.Map<IList<ProblemResponse>>(searchList.Item1);


        //    //if (request.IsExposure == 1)
        //    //{
        //    //    mergInfo.ForEach(p =>
        //    //    {
        //    //        p.IsRead = MessageTagService.GetInstance().IsExist(1, int.Parse(p.PkId), request.UserCode);
        //    //    });
        //    //}

        //    return Json(new WebAPIResponse<IList<ProblemResponse>>(mergInfo));
        //}


        /// <summary>
        /// 获取问题详情
        /// </summary>
        /// <param name="riverProblemApplyId"></param>
        /// <returns></returns>
        public JsonResult<WebAPIResponse<ProblemResponse>> GetProblemDetail(int riverProblemApplyId)
        {
            var entity = RiverProblemApplyService.GetInstance().GetModelByPk(riverProblemApplyId);
            var mergInfo = Mapper.Map<ProblemResponse>(entity);
            return Json(new WebAPIResponse<ProblemResponse>(mergInfo));
        }


        /// <summary>
        /// 获取通告
        /// </summary>
        /// <param name="request">序列化请求实体GetMsgNoticeRequest</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<WebAPIResponse<IList<MsgNoticeEntity>>> GetMsgNotice(GetMsgNoticeRequest request)
        {
            var userInfo = UserInfoService.GetInstance().GetUserInfo(request.UserCode);
            var belongCompanys = userInfo.UserDepartmentList.Any() ? userInfo.UserDepartmentList.Select(p => p.DepartmentCode).Aggregate((a, b) =>
                {
                    return a + "," + b;
                }) : "";

            var list = MsgNoticeService.GetInstance()
                .Search(new MsgNoticeEntity() { BelongCompanys = belongCompanys }, request.skipResults, request.maxResults);
            list.Item1.ForEach(p =>
            {
                p.IsRead = MessageTagService.GetInstance().IsExist(1, p.PkId, request.UserCode);
            });
            return Json(new WebAPIResponse<IList<MsgNoticeEntity>>(list.Item1));
        }

        /// <summary>
        /// 标记已读
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<WebAPIResponse<bool>> TagMessageState(TagMessageStateRequest request)
        {
            var result = MessageTagService.GetInstance().Add(new MessageTagEntity()
            {
                Kind = request.Kind,
                MessageId = request.MessageId,
                UserCode = request.UserCode,
                UserName = request.UserName
            });
            return Json(new WebAPIResponse<bool>(result > 0));
        }



        /// <summary>
        /// 获取通讯录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<WebAPIResponse<IList<UserInfoResponse>>> GetAddressList(GetAddressListRequest request)
        {
            var list = UserInfoReportService.GetInstance()
                 .GerAddressList(new UserInfoViewEntity()
                 {
                     Mobile = request.Mobile,
                     UserName = request.UserName,
                     RiverName = request.RiverName,
                     DepartmentName = request.DepartmentName
                 }, request.skipResults, request.maxResults);
            var mergInfo = Mapper.Map<IList<UserInfoResponse>>(list.Item1);

            return Json(new WebAPIResponse<IList<UserInfoResponse>>(mergInfo));
        }


        /// <summary>
        /// 获取督办历史记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<WebAPIResponse<IList<RiverProblemDbEntity>>> GetDbHistoryList(GetDbHistoryListRequest request)
        {
            var list = RiverProblemDbService.GetInstance()
                 .Search(new RiverProblemDbEntity()
                 {
                     RiverProblemApplyId = request.RiverProblemApplyId,

                 }, request.skipResults, request.maxResults);

            return Json(new WebAPIResponse<IList<RiverProblemDbEntity>>(list.Item1));
        }


        /// <summary>
        /// 获取水质列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<WebAPIResponse<IList<GetRiverAttachListResponse>>> GetRiverAttachList(GetRiverAttachListRequest request)
        {
            var list = RiverAttachService.GetInstance()
                 .Search(new RiverAttachEntity() { IsMainData = 1, RiverId = request.RiverId },
                 request.skipResults, request.maxResults);

            var mergInfo = Mapper.Map<IList<GetRiverAttachListResponse>>(list.Item1);

            return Json(new WebAPIResponse<IList<GetRiverAttachListResponse>>(mergInfo));
        }

    }

    public class A
    {
        public string Name { get; set; }
    }
}