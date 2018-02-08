using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Authorization;
using BPM.Repository;
using BPM.Models;
using Microsoft.EntityFrameworkCore;
using BPM.Repository.PowerManage;

namespace Hasng.CadreFile.WebApp.Areas.PowerManage.Controllers
{
    [Area("PowerManage")]
    public class UsersController : Controller
    {
        DataContext _db;
        PowerUserRepository _userRepository;
        public UsersController(DataContext db, PowerUserRepository userRepository)
        {
            _db = db;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 主界面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        ///// <summary>
        ///// 用户图片上传
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult ImageUpload()
        //{
        //    Power_UserView _UserView = _userbll.GetViewModel(ManageProvider.Current.ID.ToString());
        //    if (_UserView.A0191A != null)
        //    {
        //        Sys_AttachmentView _AttachmentView = _AttachmentBLL.GetViewModel(_UserView.A0191A.ToString());
        //        ViewData["A019A"] = _AttachmentView.ID;
        //        ViewData["ImageFile"] = _AttachmentView.ImageFile;
        //    }
        //    else
        //    {
        //        ViewData["A019A"] = null;
        //        ViewData["ImageFile"] = "/page/img/user.png";
        //    }
        //    logcache.AddServiceLogForView(CurrentMenuID, "用户图片上传", "");
        //    return View();
        //}
        ///// <summary>
        ///// 用户选择角色
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult SelectRoleList()
        //{
        //    logcache.AddServiceLogForView(CurrentMenuID, "用户角色浏览", "");
        //    return View();
        //}
        ///// <summary>
        ///// 选择角色按钮
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult SelectRoleEdit()
        //{
        //    return View();
        //}
        /// <summary>
        /// 新增或编辑用户View
        /// </summary>
        /// <returns></returns>
        public IActionResult AddOrEditUsers()
        {
            return View();
        }
        ///// <summary>
        ///// 选择人员
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult OrgUserSelectPage()
        //{
        //    logcache.AddServiceLogForView(CurrentMenuID, "选择人员浏览", "");
        //    return View();
        //}
        ///// <summary>
        ///// 密码初始化表单
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult InitializePassWord()
        //{
        //    logcache.AddServiceLogForView(CurrentMenuID, "密码初始化浏览", "");
        //    return View();
        //}
        ///// <summary>
        ///// 修改密码页面
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public IActionResult ModifyPassWord(String id)
        //{
        //    logcache.AddServiceLogForView(CurrentMenuID, "修改密码浏览", "");
        //    return View();
        //}
        //#endregion

        //#region 用户的增、删、改、查
        /// <summary>
        /// 添加或删除
        /// </summary>
        /// <returns></returns>
        public void AddOrUpdate(Power_User data)
        {
            Power_User userModel = new Power_User();

            //判断前端传过来的对象中的id是否为空
            if (data.ID == Guid.Empty)
            {
                try
                {
                    userModel = data;
                    //userModel.U_AreaCode = ManageProvider.Current.U_AreaCode;
                    userModel.PassWord = "123456"; //MD5Helper.MD5Encrypt64("123456"); //对密码进行MD5加密
                    userModel.Isinitial = true;
                    //userModel.U_IsSystem = false;
                    //userModel.U_IsValid = true;
                    userModel.IsSuperUser = false;
                    userModel.IsTrilateral = false;
                    //userModel.U_LastModifiedDate = DateTime.Now;
                    userModel.CreateTime = DateTime.Now;

   
                    _userRepository.Add(userModel);
                    //人员表插入数据
                    //userModel = _userbll.Insert(userModel);
                    //记录日志
                    //logcache.AddServiceLogForAdd<Power_UserView>(userModel, CurrentMenuID, "新增人员", "Power_User");
                }
                catch (Exception ex)
                {
                    //logcache.AddServiceLogForAdd<Power_UserView>(userModel, CurrentMenuID, "新增人员异常", "Power_User", Request.Path, ex.Message, ex.StackTrace);
                }
            }
            else
            {
                userModel = _userRepository.GetById(data.ID);
                //userModel = _db.Power_User.Find(data.ID);
                //userModel = _userbll.GetViewModel(data.ID.ToString());
                //Power_UserView oldUserModel = ObjectClone.ToClone<Power_UserView>(userModel);

                try
                {
                    userModel.Cn = data.Cn;
                    userModel.UID = data.UID;
                    userModel.StationsID = data.StationsID;
                    //userModel.OrgID = data.OrgID;
                    userModel.Status = data.Status;

                    //修改用户表记录
                    _db.Entry(userModel).State = EntityState.Modified;
                    _db.SaveChanges();
                    //userModel = _userbll.Update(userModel);
                    //记录日志
                    //logcache.AddServiceLogForUpdate(userModel, oldUserModel, CurrentMenuID, "更新用户", "Power_User");
                }
                catch (Exception ex)
                {
                    //logcache.AddServiceLogForUpdate(userModel, oldUserModel, CurrentMenuID, "更新用户异常", "Power_User", Request.Path, ex.Message, ex.StackTrace);
                }
            }
        }


        ///// <summary>  
        ///// 删除
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public bool Delete(string id)
        //{
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        Power_UserView userModel = _userbll.GetViewModel(id);
        //        try
        //        {
        //            _userbll.Delete(Guid.Parse(id));
        //            //记录日志
        //            logcache.AddServiceLogForDelete<Power_UserView>(userModel, CurrentMenuID, "删除用户成功", "Power_User");
        //        }
        //        catch (Exception ex)
        //        {
        //            logcache.AddServiceLogForDelete<Power_UserView>(userModel, CurrentMenuID, "删除用户异常", "Power_User", Request.Path, ex.Message, ex.StackTrace);
        //        }

        //        return true;
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 获取对象
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public String GetEntity(string id)
        //{
        //    Power_UserView _modelView = _userbll.GetViewModel(id);
        //    return JsonConvert.SerializeObject(_modelView);
        //}


        //#endregion

        //#region 根据组织机构ID、角色ID 获取用户集合（用户ID集合） 通用

        //public string GetPagerJsonRole(string ParameterJson, JqGridParam jqgridparam)
        //{
        //    if (string.IsNullOrEmpty(jqgridparam.sortField))
        //        jqgridparam.sortField = "U_SortNo";

        //    string strJson = _userbll.GetListJsonByRole(ParameterJson, ref jqgridparam);
        //    logcache.AddServiceLogForQuery(CurrentMenuID, "用户角色查询", "", ParameterJson);
        //    return JsonManager.ToPageJson(jqgridparam, strJson);
        //}

        ///// <summary>
        ///// 查询人员列表 并且 排除指定机构的人员
        ///// </summary>
        ///// <param name="ParameterJson"></param>
        ///// <param name="orgID"></param>
        ///// <param name="jqgridparam"></param>
        ///// <returns></returns>
        //public string GetPagerJsonOutOrg(string ParameterJson, string orgID, JqGridParam jqgridparam)
        //{
        //    if (string.IsNullOrEmpty(jqgridparam.sortField))
        //        jqgridparam.sortField = "U_SortNo";

        //    string sCondition = JqConditionParam.CombinationCon(ParameterJson, JqConditionParam.ToJson());


        //    string strJson = _userbll.GetPagerJsonOutOrg(sCondition, orgID, ref jqgridparam);
        //    return JsonManager.ToPageJson(jqgridparam, strJson);
        //}


        ///// <summary>
        ///// 根据组织机构查询人员
        ///// </summary>
        ///// <param name="ParameterJson"></param>
        ///// <param name="orgID"></param>
        ///// <param name="jqgridparam"></param>
        ///// <returns></returns>
        //public string GetPagerJsonInOrg(string ParameterJson, string orgID, JqGridParam jqgridparam)
        //{
        //    if (string.IsNullOrEmpty(jqgridparam.sortField))
        //        jqgridparam.sortField = "U_SortNo";

        //    string sCondition = JqConditionParam.CombinationCon(ParameterJson, JqConditionParam.ToJson());
        //    string strJson = _userbll.GetPagerJsonInOrg(sCondition, orgID, ref jqgridparam);
        //    logcache.AddServiceLogForQuery(CurrentMenuID, "根据组织机构查询人员", "", ParameterJson);
        //    return JsonManager.ToPageJson(jqgridparam, strJson);
        //}

        ///// <summary>
        ///// 根据组织机构查询人员
        ///// </summary>
        ///// <param name="ParameterJson"></param>
        ///// <param name="orgID"></param>
        ///// <param name="jqgridparam"></param>
        ///// <returns></returns>
        //public string GetPagerJsonInOrgAndRole(string ParameterJson, string orgID, Guid roleID, JqGridParam jqgridparam)
        //{
        //    if (string.IsNullOrEmpty(jqgridparam.sortField))
        //        jqgridparam.sortField = "U_SortNo";
        //    string sCondition = JqConditionParam.CombinationCon(ParameterJson, JqConditionParam.ToJson());
        //    string strJson = _userbll.GetPagerJsonInOrgAndRole(sCondition, orgID, roleID, ref jqgridparam);
        //    logcache.AddServiceLogForQuery(CurrentMenuID, "根据组织机构查询人员", "", sCondition);
        //    return JsonManager.ToPageJson(jqgridparam, strJson);
        //}

        ///// <summary>
        ///// 查询所有人员列表
        ///// </summary>
        ///// <param name="ParameterJson"></param>
        ///// <param name="jqgridparam"></param>
        ///// <returns></returns>
        //public string GetPagerJson(string ParameterJson, JqGridParam jqgridparam)
        //{
        //    if (string.IsNullOrEmpty(jqgridparam.sortField))
        //        jqgridparam.sortField = "U_SortNo";

        //    string sCondition = JqConditionParam.CombinationCon(ParameterJson, JqConditionParam.ToJson());
        //    string strJson = _userbll.FindListJsonPage(sCondition, ref jqgridparam);
        //    logcache.AddServiceLogForQuery(CurrentMenuID, "根据组织机构查询人员", "", sCondition);
        //    return JsonManager.ToPageJson(jqgridparam, strJson);
        //}







        ///// <summary>
        ///// 根据组织机构Id获取用户列表
        ///// </summary>
        ///// <param name="orgId"></param>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //public string GetUserListJsonByOrgId(string orgId, JqGridParam param)
        //{
        //    if (!string.IsNullOrEmpty(orgId))
        //        return JsonManager.ToPageJson(param, _userbll.GetUserListJsonByOrgId(Guid.Parse(orgId), ref param));
        //    else
        //        return "";
        //}

        ///// <summary>
        ///// 根据角色ID获取用户列表
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //public string GetUserListJsonByRoleId(string roleId, JqGridParam param)
        //{
        //    if (!string.IsNullOrEmpty(roleId))
        //        return JsonManager.ToPageJson(param, _userbll.GetUserListJsonByRoleId(Guid.Parse(roleId), ref param));
        //    else
        //        return "";
        //}

        ///// <summary>
        ///// 根据组织机构ID 获取人员Id集合
        ///// </summary>
        ///// <param name="orgId"></param>
        ///// <returns></returns>
        //public string GetCheckUserIdByOrgId(string orgId)
        //{
        //    return _userbll.GetCheckUserIdByOrgId(orgId);
        //}

        ///// <summary>
        ///// 根据角色Id 获取人员Id集合
        ///// </summary>
        ///// <param name="roleid"></param>
        ///// <returns></returns>
        //public string GetCheckUserIdByRoleId(string roleid)
        //{
        //    return _userbll.GetCheckUserIdByRoleId(roleid);
        //}

        //#endregion


        //#region 人员列表
        ///// <summary>
        ///// 查询所有人员列表
        ///// </summary>
        ///// <param name="ParameterJson"></param>
        ///// <param name="jqgridparam"></param>
        ///// <returns></returns>
        //public string GetUserPagerJson(string ParameterJson, JqGridParam jqgridparam)
        //{
        //    if (string.IsNullOrEmpty(jqgridparam.sortField))
        //        jqgridparam.sortField = "U_SortNo";

        //    string sCondition = JqConditionParam.CombinationCon(ParameterJson, JqConditionParam.ToJson());

        //    string strJson = _userbll.GetUserInfoListJson(sCondition, ref jqgridparam);
        //    return JsonManager.ToPageJson(jqgridparam, strJson);
        //}
        //#endregion

        //#region 验证输入的值是否唯一
        /// <summary>
        /// 新增验证输入的值是否唯一
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="filed">字段名</param>
        /// <param name="val">字段值</param>
        /// <returns></returns>
        public bool FindOnly(string UID)
        {
            #region 条件组合
            //List<ParameterJson> list = new List<ParameterJson>();

            //list.Add(new ParameterJson("UID", ConditionOperate.Equal.ToString(), UID));
            //list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
            //list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //string sCondition = JsonConvert.SerializeObject(list);
            #endregion

            int num = _db.Power_User.Where(x => x.UID == UID).Count();
            return num == 0;
        }


        /// <summary>
        /// 编辑时验证输入是否唯一
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public bool FindOnlyEdit(string UID, string ID)
        {
            #region 条件组合
            //List<ParameterJson> list = new List<ParameterJson>();

            //list.Add(new ParameterJson("UID", ConditionOperate.Equal.ToString(), UID));
            //list.Add(new ParameterJson("ID", ConditionOperate.NotEqual.ToString(), ID));
            //list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
            //list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //string sCondition = JsonConvert.SerializeObject(list);
            #endregion
            int num = _db.Power_User.Where(x => x.UID == UID && x.ID != Guid.Parse(ID)).Count();
            return num == 0;
        }
        //#endregion

        //#region 修改密码、密码初始化

        ///// <summary>
        ///// 修改密码、密码初始化
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public string UpdatePassWord(string oldpassword, string password)
        //{
        //    try
        //    {
        //        MessageUser mu = ManageProvider.Current;
        //        Power_UserView userview = _userbll.GetViewModel(mu.ID.ToString());
        //        Power_UserView newModel = new Power_UserView();
        //        if (MD5Helper.MD5Encrypt64(oldpassword) == userview.PassWord)
        //        {
        //            userview.PassWord = MD5Helper.MD5Encrypt64(password); //对修改后的密码进行MD5加密
        //            userview.Isinitial = false;
        //            try
        //            {
        //                newModel = _userbll.InsertOrUpdate(userview);
        //                logcache.AddServiceLogForUpdate(newModel, userview, CurrentMenuID, "更新密码", "Power_User");
        //            }
        //            catch (Exception ex)
        //            {
        //                logcache.AddServiceLogForUpdate(newModel, userview, CurrentMenuID, "更新密码异常", "Power_User", Request.Path, ex.Message, ex.StackTrace);
        //            }
        //            return JsonConvert.SerializeObject(new { success = true, message = "" });
        //        }
        //        else
        //        {
        //            return JsonConvert.SerializeObject(new { success = false, message = "密码的原密码不正确" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
        //    }
        //}


        ///// <summary>
        ///// 密码初始化
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public void InitializePs(string id, string pwd)
        //{
        //    Power_UserView user = _userbll.GetViewModel(id);
        //    Power_UserView userNew = new Power_UserView();
        //    try
        //    {

        //        user.PassWord = MD5Helper.MD5Encrypt64(pwd); //对修改后的密码进行MD5加密
        //        user.Isinitial = true;
        //        userNew = _userbll.InsertOrUpdate(user);
        //        logcache.AddServiceLogForUpdate(userNew, user, CurrentMenuID, "密码初始化成功", " Power_User");
        //    }
        //    catch (Exception ex)
        //    {
        //        logcache.AddServiceLogForUpdate(userNew, user, CurrentMenuID, "密码初始化成功", " Power_User", HttpContext.Request.Path, ex.Message, ex.StackTrace);
        //    }


        //}

        //#endregion

        //#region 更新用户图片
        ///// <summary>
        ///// 更新用户图片
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public string updateImage(string A0191A)
        //{
        //    Power_UserView _modelView = _userbll.GetViewModel(ManageProvider.Current.ID.ToString());
        //    _modelView.A0191A = Guid.Parse(A0191A);
        //    _userbll.Update(_modelView);
        //    Sys_AttachmentView _AttachmentView = _AttachmentBLL.GetViewModel(_modelView.A0191A.ToString());
        //    return _AttachmentView.ImageFile;
        //}
        //#endregion

    }
}