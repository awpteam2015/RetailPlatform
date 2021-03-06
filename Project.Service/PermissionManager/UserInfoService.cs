﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using AutoMapper;
using NHibernate.Criterion;
using NHibernate.Mapping;
using NHibernate.Util;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Model.PermissionManager;
using Project.Model.RiverManager;
using Project.Repository.PermissionManager;
using Project.Repository.RiverManager;
using Project.Service.HRManager;
using Project.Service.PermissionManager.DTO;
using Project.Service.PermissionManager.Validate;
using Project.Service.RiverManager;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;

namespace Project.Service.PermissionManager
{
    public class UserInfoService
    {
        #region
        private static readonly UserInfoService Instance = new UserInfoService();
        private readonly UserInfoRepository _userInfoRepository;
        private readonly UserDepartmentRepository _userDepartmentRepository;
        private readonly UserRoleRepository _userRoleRepository;
        private readonly RiverOwerRepository _riverOwerRepository;
        private readonly FunctionDetailRepository _functionDetailRepository;
        private readonly UserFunctionDetailRepository _userFunctionDetailRepository;

        private UserInfoService()
        {
            _userInfoRepository = new UserInfoRepository();
            _userDepartmentRepository = new UserDepartmentRepository();
            _userRoleRepository = new UserRoleRepository();
            this._functionDetailRepository = new FunctionDetailRepository();
            _userFunctionDetailRepository = new UserFunctionDetailRepository();
            _riverOwerRepository = new RiverOwerRepository();
        }

        public static UserInfoService GetInstance()
        {
            return Instance;
        }

        public void Test()
        {
            var t = _userInfoRepository.GetById(1);
        }
        #endregion


        public Tuple<bool, string, LoginUserInfoDTO> Login(string userCode, string password)
        {
            var userInfoEntity = this.GetList(new UserInfoEntity() { UserCode = userCode, Password = Encrypt.MD5Encrypt(password) }).FirstOrDefault();
            if (userInfoEntity != null)
            {
                userInfoEntity.UserFunctionDetailList_Checked = this.GetFunctionDetailList_Checked(userCode);


                var loginUserInfo = Mapper.Map<UserInfoEntity, LoginUserInfoDTO>(userInfoEntity);
                loginUserInfo.IsAdmin = loginUserInfo.UserCode.ToUpper() == "ADMIN";
                loginUserInfo.UserDepartmentIntList = new List<string>();
                loginUserInfo.UserDepartmentList.ToList().ForEach(p => loginUserInfo.UserDepartmentIntList.Add(p.DepartmentCode));
                loginUserInfo.UserDepartmentList.Clear();
                return new Tuple<bool, string, LoginUserInfoDTO>(true, "", loginUserInfo);
            }
            else
            {
                return new Tuple<bool, string, LoginUserInfoDTO>(false, "用户名或者密码错误！", null);
            }
        }


        /// <summary>
        /// 获取用户对应的菜单信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public IList<MenuDTO> GetMenuDTOList(string userCode, IList<int> permissionCodeList)
        {
            var allPermissionFunction = PermissionService.GetInstance().GetAllPermissionFunction();

            var userPermissionFunction = PermissionService.GetInstance().GetAllPermissionFunction();
            if (!PermissionService.GetInstance().IsAdmin(userCode))
            {
                userPermissionFunction = userPermissionFunction.Where(p => permissionCodeList.Contains(p.PkId)).ToList();
            }
            var module = userPermissionFunction.Select(p => p.ModuleId).Distinct().ToList();
            var function = userPermissionFunction.Select(p => p.FunctionId).Distinct().ToList();
            var menuDtoList = new List<MenuDTO>();
            var moduleList = ModuleService.GetInstance().GetList(new ModuleEntity());
            moduleList.Where(p => module.Contains(p.PkId)).ToList().ForEach(p =>
            {
                var temp = new MenuDTO()
                {
                    Url = "",
                    Name = p.ModuleName,
                    RankId = p.RankId
                };

                var list = p.FunctionEntityList.Where(x => function.Contains(x.PkId)).ToList();

                list.ForEach(
                    x =>
                    {
                        if (x.IsDisplayOnMenu == 1)
                        {
                            temp.MenuDTOList.Add(new MenuDTO()
                            {
                                Url = x.FunctionUrl,
                                Name = x.FunctionnName,
                                RankId = x.RankId
                            });
                        }
                    }
                    );
                if (temp.MenuDTOList.Any())
                {
                    temp.MenuDTOList = temp.MenuDTOList.OrderBy(item => item.RankId).ToList();
                    menuDtoList.Add(temp);
                }
            });
            return menuDtoList.OrderBy(item => item.RankId).ToList();
        }



        public UserInfoEntity GetModel(int pkId)
        {
            return _userInfoRepository.GetById(pkId);
        }

        public UserInfoEntity GetUserInfo(string userCode)
        {
            return _userInfoRepository.Query().FirstOrDefault(p => p.UserCode == userCode);
        }


        public Tuple<bool, string> Add(UserInfoEntity entity)
        {
            var validateResult = UserInfoValidate.GetInstance().IsHasSameUserCode(entity.UserCode);
            if (!validateResult.Item1)
            {
                return validateResult;
            }
            var addResult = _userInfoRepository.Save(entity);
            if (addResult > 0)
            {
                return new Tuple<bool, string>(true, "");
            }
            else
            {
                return new Tuple<bool, string>(false, "");
            }
        }


        public Tuple<bool, string> ChangePassword(string userCode, string password)
        {
            var model = this.GetUserInfo(userCode);
            model.Password = Encrypt.MD5Encrypt(password);//加密
            try
            {
                _userInfoRepository.Update(model);
                return new Tuple<bool, string>(true, "");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Tuple<bool, string> ChangePassword(UserInfoEntity entity)
        {
            try
            {
                _userInfoRepository.Update(entity);
                return new Tuple<bool, string>(true, "");
            }
            catch (Exception e)
            {

                return new Tuple<bool, string>(false, e.Message);
            }

        }

        public Tuple<bool, string> UpdateNormalInfo(UserInfoEntity entity)
        {
            try
            {
                _userInfoRepository.Update(entity);
                return new Tuple<bool, string>(true, "");
            }
            catch (Exception e)
            {

                return new Tuple<bool, string>(false, e.Message);
            }

        }


        public Tuple<bool, string> Update(UserInfoEntity entity)
        {
            var validateResult = UserInfoValidate.GetInstance().IsHasSameUserCode(entity.UserCode, entity.PkId);
            if (!validateResult.Item1)
            {
                return validateResult;
            }

            var oldEntity = _userInfoRepository.GetById(entity.PkId);
            entity.CreationTime = oldEntity.CreationTime;
            entity.CreatorUserCode = oldEntity.CreatorUserCode;
            if (entity.Password != oldEntity.Password)
            {
                entity.Password = Encrypt.MD5Encrypt(entity.Password);//加密
            }

            var deleteList = oldEntity.UserDepartmentList.Where(
             p => entity.UserDepartmentList.All(x => x.DepartmentCode != p.DepartmentCode)).ToList();

            var deleteList2 = oldEntity.UserRoleList.Where(
                  p => entity.UserRoleList.All(x => x.RoleId != p.RoleId)).ToList();

            //var deleteList3 = oldEntity.RiverOwerList.Where(
            //     p => entity.RiverOwerList.All(x => x.RiverId != p.RiverId)).ToList();
            //  SessionFactoryManager.Clear();
            oldEntity.RiverOwerList.ToList().ForEach(p => { _riverOwerRepository.Delete(p); });
            SessionFactoryManager.Clear();

            entity.RiverOwerList = new HashSet<RiverOwerEntity>();
            if (!string.IsNullOrWhiteSpace(entity.Attr_SelectRiverIds))
            {
                var arr = entity.Attr_SelectRiverIds.Split(',');
                if (arr != null)
                {
                    arr.Distinct().ToList().ForEach(p =>
                    {
                        var newEntity = new RiverOwerEntity();
                        newEntity.RiverId = int.Parse(p);
                        newEntity.UserCode = entity.UserCode;
                        newEntity.RiverName = RiverService.GetInstance().GetModelByPk(int.Parse(p)).RiverName;
                        newEntity.UserName = entity.UserName;
                        // entity.RiverOwerList.Add(newEntity);
                        _riverOwerRepository.Save(newEntity);
                    });
                }
            }
            SessionFactoryManager.Clear();


            //using (var tx = NhTransactionHelper.BeginTransaction())
            //{
            try
            {

                deleteList2.ForEach(p => { _userRoleRepository.Delete(p); });

                deleteList.ForEach(p => { _userDepartmentRepository.Delete(p); });

                _userInfoRepository.Merge(entity);

                //if (entity.RiverOwerList.Count > 0)
                //{
                //    entity.RiverOwerList.ForEach(p =>
                //    {
                //        _riverOwerRepository.Save(p);

                //    });
                //}

                //tx.Commit();
                return new Tuple<bool, string>(true, "");
            }
            catch (Exception e)
            {
                // tx.Rollback();
                throw e;
            }
            // }
        }

        public bool Delete(int pkId)
        {
            try
            {
                var entity = _userInfoRepository.GetById(pkId);
                _userInfoRepository.Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public System.Tuple<IList<UserInfoEntity>, int> Search(UserInfoEntity entity, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<UserInfoEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            if (!string.IsNullOrEmpty(entity.UserCode))
                expr = expr.And(p => p.UserCode == entity.UserCode);
            if (!string.IsNullOrEmpty(entity.Search_UserCodes))
            {
                var arr = entity.Search_UserCodes.Split(',');
                expr = expr.And(p => arr.Contains(p.UserCode));
            }

            // if (!string.IsNullOrEmpty(entity.Password))
            //  expr = expr.And(p => p.Password == entity.Password);
            if (!string.IsNullOrEmpty(entity.UserName))
                expr = expr.And(p => p.UserName.Contains(entity.UserName));
            // if (!string.IsNullOrEmpty(entity.Email))
            //  expr = expr.And(p => p.Email == entity.Email);
            if (!string.IsNullOrEmpty(entity.Mobile))
                expr = expr.And(p => p.Mobile == entity.Mobile);
            // if (!string.IsNullOrEmpty(entity.Tel))
            //  expr = expr.And(p => p.Tel == entity.Tel);
            if (entity.IsActive != 0 && entity.IsActive != null)
                expr = expr.And(p => p.IsActive == entity.IsActive);
            // if (!string.IsNullOrEmpty(entity.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == entity.CreatorUserCode);
            // if (!string.IsNullOrEmpty(entity.CreationTime))
            //  expr = expr.And(p => p.CreationTime == entity.CreationTime);
            // if (!string.IsNullOrEmpty(entity.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == entity.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(entity.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == entity.LastModificationTime);
            // if (!string.IsNullOrEmpty(entity.Remark))
            //  expr = expr.And(p => p.Remark == entity.Remark);
            // if (!string.IsNullOrEmpty(entity.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == entity.IsDeleted);
            #endregion

            var expr2 = PredicateBuilder.True<UserDepartmentEntity>();
            if (entity.Attr_DepartmentCodes != null && entity.Attr_DepartmentCodes.Any())
                expr2 = expr2.And(p => entity.Attr_DepartmentCodes.Contains(p.DepartmentCode));

            var query = _userInfoRepository.Query().Where(expr);
            if (entity.Attr_DepartmentCodes != null && entity.Attr_DepartmentCodes.Any())
            {
                query = (from a in _userInfoRepository.Query().Where(expr)
                         from b in _userDepartmentRepository.Query().Where(expr2)
                         where a.UserCode == b.UserCode
                         select a);
            }

            var list = query.OrderBy(p => p.UserCode).Skip(skipResults).Take(skipResults + maxResults).ToList();

            list.ForEach(p =>
            {
                p.Jb = DictionaryService.GetInstance().GetByKeyValue(p.Lever, "Lever");
            });

            var count = query.Count();
            return new System.Tuple<IList<UserInfoEntity>, int>(list, count);
        }


        public System.Tuple<IList<UserInfoEntity>, int> Search2(UserInfoEntity entity, int skipResults, int maxResults,bool isExport=false)
        {
            var expr = PredicateBuilder.True<UserInfoEntity>();

            if (!string.IsNullOrEmpty(entity.UserName))
                expr = expr.And(p => p.UserName.Contains(entity.UserName));

            var query = _userInfoRepository.Query().Where(expr);

            var list =new List<UserInfoEntity>();

            if (isExport)
            {
                list= query.OrderBy(p => p.UserCode).ToList();
            }
            else
            {
                list= query.OrderBy(p => p.UserCode).Skip(skipResults).Take(skipResults + maxResults).ToList();
            }

            list.ForEach(p =>
            {
                var checkList = RiverCheckService.GetInstance().GetList(new RiverCheckEntity()
                {
                    UserCode = p.UserCode,
                    RiverName = entity.RiverName,
                    Attr_BeginDate = entity.BeginDate.GetValueOrDefault(),
                    Attr_EndDate = entity.EndDate.GetValueOrDefault()
                });

                var available = 0;
                switch (p.Lever)
                {
                    case "1"://市
                        available =int.Parse(DictionaryService.GetInstance().GetModelByKeyCode("SJHZ").KeyValue) ;
                        break;
                    case "2"://镇
                        available = int.Parse(DictionaryService.GetInstance().GetModelByKeyCode("ZJHZ").KeyValue);
                        break;
                    case "3"://村
                        available = int.Parse(DictionaryService.GetInstance().GetModelByKeyCode("CJHZ").KeyValue);
                        break;
                }

                var isNormal = true;
                if (checkList.Count()>0)
                {
                    for (int i = 0; i < checkList.Count(); i++)
                    {
                        if (i > 1)
                        {
                            if (checkList[i].CreationTime.GetValueOrDefault().Subtract(checkList[i - 1].CreationTime.GetValueOrDefault()).Days > available)
                            {
                                isNormal = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    isNormal = false;
                }
              
                p.Jb = DictionaryService.GetInstance().GetByKeyValue(p.Lever, "Lever");
                p.Times = checkList.Count();
                p.State = isNormal ? "正常" : "异常";
            });

            var count = query.Count();
            return new System.Tuple<IList<UserInfoEntity>, int>(list, count);
        }


        public IList<UserInfoEntity> GetUserList(string departmentCode, int roleId)
        {
            var query = (from a in _userInfoRepository.Query()
                         from b in _userDepartmentRepository.Query().Where(p => p.DepartmentCode == departmentCode)
                         from c in _userRoleRepository.Query().Where(p => p.RoleId == roleId)
                         where a.UserCode == b.UserCode
                         where a.UserCode == c.UserCode
                         select a);
            return query.ToList();
        }


        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<UserInfoEntity> GetList(UserInfoEntity entity)
        {
            var expr = PredicateBuilder.True<UserInfoEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            if (!string.IsNullOrEmpty(entity.UserCode))
                expr = expr.And(p => p.UserCode == entity.UserCode);
            if (!string.IsNullOrEmpty(entity.Password))
                expr = expr.And(p => p.Password == entity.Password);
            // if (!string.IsNullOrEmpty(entity.UserName))
            //  expr = expr.And(p => p.UserName == entity.UserName);
            // if (!string.IsNullOrEmpty(entity.Email))
            //  expr = expr.And(p => p.Email == entity.Email);
            // if (!string.IsNullOrEmpty(entity.Mobile))
            //  expr = expr.And(p => p.Mobile == entity.Mobile);
            // if (!string.IsNullOrEmpty(entity.Tel))
            //  expr = expr.And(p => p.Tel == entity.Tel);
            // if (!string.IsNullOrEmpty(entity.IsActive))
            //  expr = expr.And(p => p.IsActive == entity.IsActive);
            // if (!string.IsNullOrEmpty(entity.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == entity.CreatorUserCode);
            // if (!string.IsNullOrEmpty(entity.CreationTime))
            //  expr = expr.And(p => p.CreationTime == entity.CreationTime);
            // if (!string.IsNullOrEmpty(entity.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == entity.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(entity.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == entity.LastModificationTime);
            // if (!string.IsNullOrEmpty(entity.Remark))
            //  expr = expr.And(p => p.Remark == entity.Remark);
            // if (!string.IsNullOrEmpty(entity.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == entity.IsDeleted);
            #endregion
            var list = _userInfoRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }


        public IList<UserFunctionDetailEntity> GetUserFunctionDetailList(UserFunctionDetailEntity entity)
        {
            var expr = PredicateBuilder.True<UserFunctionDetailEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            if (!string.IsNullOrEmpty(entity.UserCode))
                expr = expr.And(p => p.UserCode == entity.UserCode);
            if (entity.FunctionId > 0)
                expr = expr.And(p => p.FunctionId == entity.FunctionId);
            if (entity.FunctionDetailId > 0)
                expr = expr.And(p => p.FunctionDetailId == entity.FunctionDetailId);
            #endregion
            var list = _userFunctionDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }


        /// <summary>
        /// 获取当前用户所属详细功能
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<int> GetFunctionDetailList_Checked(string userCode)
        {
            var list = new List<int>();
            var userInfo = this.GetUserInfo(userCode);
            userInfo.UserRoleList.ToList().ForEach(p =>
            {
                var roleFunctionDetailList = RoleService.GetInstance().GetFunctionDetailList_Checked(p.RoleId);
                list.AddRange(roleFunctionDetailList);
            });

            list = list.Distinct().ToList();
            userInfo.UserFunctionDetailList.ToList().ForEach(p =>
            {
                if (p.State == 1)
                {
                    list.Add(p.FunctionDetailId);
                }
                else
                {
                    list.Remove(p.FunctionDetailId);
                }
            });

            return list.Distinct().ToList(); ;
        }


        /// <summary>
        /// 设置用户权限
        /// </summary>
        public bool SetRowFunction(string userCode, int functionPkId, int functionDetailPkId, bool isCheck)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var checkList = GetFunctionDetailList_Checked(userCode);

                    var functionDetailList = new List<FunctionDetailEntity>();
                    if (functionPkId > 0)
                    {
                        functionDetailList = FunctionService.GetInstance().GetFunctionDetailList(new FunctionDetailEntity() { FunctionId = functionPkId }).ToList();
                    }
                    else
                    {
                        functionDetailList.Add(_functionDetailRepository.GetById(functionDetailPkId));
                    }




                    if (isCheck == true)
                    {
                        var addList = functionDetailList.Where(p => checkList.All(x => x != p.PkId)).ToList();
                        addList.ForEach(p =>
                        {
                            //DeleteUserFunctionDetail(userCode, p.PkId, p.FunctionId);

                            var entiy =
                                GetUserFunctionDetailList(new UserFunctionDetailEntity()
                                {
                                    UserCode = userCode,
                                    FunctionDetailId = p.PkId,
                                    FunctionId = p.FunctionId
                                }).FirstOrDefault();
                            if (entiy == null)
                            {
                                var temp = new UserFunctionDetailEntity();
                                temp.FunctionDetailId = p.PkId;
                                temp.FunctionId = p.FunctionId;
                                temp.State = 1;
                                temp.UserCode = userCode;
                                _userFunctionDetailRepository.Save(temp);
                            }
                            else
                            {
                                entiy.State = 1;
                                _userFunctionDetailRepository.Update(entiy);
                            }


                        });
                    }
                    else
                    {
                        var userFunctionDetailList =
                            _userFunctionDetailRepository.Query().Where(p => p.UserCode == userCode).ToList();

                        var delList = functionDetailList.Where(p => userFunctionDetailList.Any(x => x.FunctionDetailId == p.PkId)).ToList();
                        delList.ForEach(p =>
                        {
                            var updateEntity = userFunctionDetailList.FirstOrDefault(x => x.FunctionDetailId == p.PkId);
                            updateEntity.State = -1;
                            _userFunctionDetailRepository.Update(updateEntity);
                        });

                        var addList = functionDetailList.Where(p => userFunctionDetailList.All(x => x.FunctionDetailId != p.PkId)).ToList();
                        addList.ForEach(p =>
                        {
                            var entiy =
                               GetUserFunctionDetailList(new UserFunctionDetailEntity()
                               {
                                   UserCode = userCode,
                                   FunctionDetailId = p.PkId,
                                   FunctionId = p.FunctionId
                               }).FirstOrDefault();
                            if (entiy == null)
                            {
                                var temp = new UserFunctionDetailEntity();
                                temp.FunctionDetailId = p.PkId;
                                temp.FunctionId = p.FunctionId;
                                temp.State = 1;
                                temp.UserCode = userCode;
                                _userFunctionDetailRepository.Save(temp);
                            }
                            else
                            {
                                entiy.State = 1;
                                _userFunctionDetailRepository.Update(entiy);
                            }
                        });

                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }



        public bool IsDepartmentRole(string userCode)
        {
            var userInfo = UserInfoService.GetInstance().GetUserInfo(userCode);
            if (userInfo.UserRoleList != null && userInfo.UserRoleList.Any())
            {
                if (userInfo.UserRoleList.Any(p => p.RoleId == 4))
                {
                    return true;
                }
            }
            return false;
        }



        public List<UserRoleEntity> GetUserListByRole(string departmentCode, int type)
        {
            var tempList = new List<UserInfoEntity>();

            var list1 = UserInfoService.GetInstance()
                .GetUserList(departmentCode, int.Parse(ConfigurationManager.AppSettings["ZfRole"])).ToList();
            var list2 = UserInfoService.GetInstance()
             .GetUserList(departmentCode, int.Parse(ConfigurationManager.AppSettings["DbRole"])).ToList();

            switch (type)
            {
                case 1:
                    tempList = list1;
                    break;
                case 2:
                    tempList = list2;
                    break;
                case 3:
                    tempList.AddRange(list1);
                    tempList.AddRange(list2);
                    break;
            }

            var resultList = new List<UserRoleEntity>();
            tempList.ForEach(p =>
            {
                resultList.Add(new UserRoleEntity()
                {
                    UserCode = p.UserCode,
                    UserName = p.UserName
                });
            });
            resultList = resultList.Distinct(p => new
            {
                p.UserCode,
                p.UserName
            }).ToList();

            return resultList;
        }


    }


}
