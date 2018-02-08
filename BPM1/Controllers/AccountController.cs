using BPM.Models;
using BPM.Repository.PowerManage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BPM1.Controllers
{
    public class AccountController:Controller
    {
        PowerUserRepository _userRepository;
        public AccountController(PowerUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public String LoginValidate(string uid, string password, string areaCode)
        {
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(areaCode))
            {
                return GlobalStatus.GetStatus("HX06");
            }

           
            //List<ParameterJson> list = new List<ParameterJson>();
            //if (uid == "InforCenterAdmin")
            //{
            //    list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //    list.Add(new ParameterJson("UID", ConditionOperate.Equal.ToString(), uid));
            //    list.Add(new ParameterJson("PassWord", ConditionOperate.Equal.ToString(), MD5Helper.MD5Encrypt64(password)));
            //    areaCode = "0CFD858E";
            //    list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), areaCode));
            //}
            //else
            //{
            //    list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //    list.Add(new ParameterJson("UID", ConditionOperate.Equal.ToString(), uid));
            //    list.Add(new ParameterJson("PassWord", ConditionOperate.Equal.ToString(), MD5Helper.MD5Encrypt64(password)));
            //    list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), areaCode));
            //}
            //string sCondition = JsonConvert.SerializeObject(list);
            //#endregion

            string pwd = MD5Helper.MD5Encrypt32(password);
            Power_User model = _userRepository.List(x => x.U_IsValid == true && x.UID == uid && x.PassWord == pwd && x.U_AreaCode == areaCode).FirstOrDefault();

           
            //用户、密码、区域验证不通过，不跳转页面
            if (model == null) return GlobalStatus.GetStatus("HX02");

            //验证 账号同时只能一个在线
            //bool IsOpenOnlyUser = _op.IsOpenOnlyUser;
            //if (IsOpenOnlyUser)
            //{
            //    bool flag = CacheHelper.HashExists((int)RedisDB.Power, "RegisterUCRelation", model.ID.ToString());
            //    if (flag)
            //    {
            //        return GlobalStatus.GetStatus("HX03");
            //    }
            //}

          
            //List<ParameterJson> listparam = new List<ParameterJson>();
            //listparam.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //listparam.Add(new ParameterJson("UserId", ConditionOperate.Equal.ToString(), model.ID.ToString()));
            //string sCondition1 = JsonConvert.SerializeObject(listparam);
            //#endregion
            //string strRoleMenu = _userRolebll.GetListJson(sCondition1);
            //string roleIds = string.Empty;
            //if (!string.IsNullOrEmpty(strRoleMenu))
            //{
            //    List<Power_RoleMenuView> aaa = JsonConvert.DeserializeObject<List<Power_RoleMenuView>>(strRoleMenu);
            //    var ccc = aaa.Select(x => x.RoleID.ToString()).Distinct().ToList();
            //    roleIds = string.Join(",", ccc.ToArray());
            //}


            //Guid? orgId = Guid.Empty;
            //#region 条件组合
            //List<ParameterJson> listparam1 = new List<ParameterJson>();
            //listparam1.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //listparam1.Add(new ParameterJson("UserId", ConditionOperate.Equal.ToString(), model.ID.ToString()));
            //string sCondition2 = JsonConvert.SerializeObject(listparam1);
            //#endregion
            //string strjson = _userorgbll.GetListJson(sCondition2);
            //if (strjson != null && strjson != "[]")
            //{
            //    List<Power_UserOrgView> userorglist = JsonConvert.DeserializeObject<List<Power_UserOrgView>>(strjson);
            //    if (userorglist.Count > 0)
            //    {
            //        Power_UserOrgView entity = userorglist.FirstOrDefault();
            //        orgId = entity.OrgID;
            //    }
            //}
            //else
            //{
            //    return GlobalStatus.GetStatus("HX04");
            //}


            MessageUser mu = new MessageUser();
            mu.ID = model.ID;
            mu.Cn = model.Cn;
            mu.IPAddress = CommonHelper.GetIP();  //???
            mu.Isinitial = true;
            mu.IsSystem = true;
            mu.LogTime = DateTime.Now;
            mu.UID = model.UID;
            mu.U_AreaCode = model.U_AreaCode;
            //mu.RoleIds = roleIds;
            //mu.OrgId = orgId;


            //HttpContext.Current.Session.Set("CurrentUserId", ByteConvertHelper.Object2Bytes(mu.ID));
            //HttpContext.Current.Session.Set("CurrentAreaCode", ByteConvertHelper.Object2Bytes(mu.U_AreaCode));

            ManageProvider.UserId = mu.ID.ToString();
            ManageProvider.Current = mu;


            //读取用户权限
            //List<MenuPower> list11 = menuCache.GetAllMenuByRoleIds(roleIds, areaCode);
            //if (list11 == null || list11.Count == 0)
            //{
            //    return GlobalStatus.GetStatus("HX05");
            //}
            //ManageProvider.CurrentMenuList = new LoginUserMenuPower() { UserId = mu.ID.ToString(), menuList = list11 };

            //使用Form验证方式
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, model.Cn, ClaimValueTypes.String, model.ID.ToString()));
            var userIdentity = new ClaimsIdentity("管理员"); //角色
            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);


            HttpContext.SignInAsync(CookieAuthenInfo.WebCookieInstance, userPrincipal,
                  new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                  {
                      ExpiresUtc = DateTime.UtcNow.AddHours(12),
                      IsPersistent = true,
                      AllowRefresh = false
                  });

            //SignalR中缓存登录状态
            //GlobalStatus.connection.Start().ContinueWith(task =>
            //{
            //    if (!task.IsFaulted)
            //    {
            //        GlobalStatus.myHub.Invoke("Register", mu.ID, mu.U_AreaCode);//必須與 MyHub 的 Register 方法名稱一樣
            //    }
            //    else
            //    {
            //        throw new Exception("连线失败!");
            //    }
            //}).Wait();


            //记录日志
            //string message = string.Format("{0}({1})登陆系统！", mu.Cn, mu.UID);
            //List<ParameterJson> areaList = new List<ParameterJson>();
            //areaList.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //areaList.Add(new ParameterJson("Code", ConditionOperate.Equal.ToString(), areaCode));
            //string areaCondition = JsonConvert.SerializeObject(areaList);
            //strJson = _areaBll.GetListJson(areaCondition);
            //Power_AreaView areaModel = !string.IsNullOrEmpty(strJson) ? JsonConvert.DeserializeObject<List<Power_AreaView>>(strJson).FirstOrDefault() : null;
            //if (areaModel != null)
            //{
            //    CookieOptions options = new CookieOptions();
            //    options.Expires = DateTime.Now.AddYears(6);
            //    HttpContext.Response.Cookies.Append("loginAreaCode", areaCode, options);
            //    HttpContext.Response.Cookies.Append("loginAreaName", areaModel.Title, options);
            //}
            //logcache.AddSystemLog(Guid.Empty, message, true, "", "");
            return GlobalStatus.GetStatus("HX00");
        }

        public IActionResult LogOut()
        {
            //记录日志-退出系统
            MessageUser mu = ManageProvider.Current;
            if (mu != null && mu.ID != Guid.Empty)
            {
                string message = string.Format("{0}({1})退出系统", mu.Cn, mu.UID);
                //logcache.AddSystemLog(Guid.Empty, message, true, "", "");
            }
            //GlobalStatus.connection.Dispose();

            //退出系统方法
            HttpContext.SignOutAsync(CookieAuthenInfo.WebCookieInstance);
            //Hasng.CadreFile.Utilities.HttpContext.Current.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
            //HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
            return Redirect("/Account/Login");
        }
    }
}
