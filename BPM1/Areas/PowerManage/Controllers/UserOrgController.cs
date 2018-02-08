
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using StackExchange.Redis;
//using System;
//using Newtonsoft.Json;
//using System.Collections.Generic;

//using System.Linq;
//using Microsoft.AspNetCore.Authorization;



//namespace Hasng.CadreFile.WebApp.Areas.PowerManage.Controllers
//{
//    [Area("PowerManage")]
//    public class UserOrgController : Controller
//    {

//        public UserOrgController()
//        {
//        }


//        public IActionResult Index()
//        {
//            logCache.AddServiceLogForView(CurrentMenuID, "用户机构（组）管理浏览", "");
//            return View();
//        }

//        public IActionResult AddUsers()
//        {
//            return View();
//        }

//        /// <summary>
//        /// 列表分页
//        /// </summary>
//        /// <param name="ParameterJson"></param>
//        /// <param name="jqgridparam"></param>
//        /// <returns></returns>
//        public string GetPagerJson(string ParameterJson, JqGridParam jqgridparam)
//        {
//            jqgridparam.sortField = "Pxh";
//            string sCondition = JqConditionParam.CombinationCon(ParameterJson, JqConditionParam.ToJson());

//            string strJson = _userbll.GetListJsonByOrg(sCondition, ref jqgridparam);
//            logCache.AddServiceLogForQuery(CurrentMenuID, "用户机构（组）管理查询", "",sCondition);
//            return JsonManager.ToPageJson(jqgridparam, strJson);
//        }

//        /// <summary>
//        /// 新增机构用户关系
//        /// </summary>
//        /// <param name="Userlist"></param>
//        /// <param name="OrgID"></param>
//        [HttpPost]
//        public void InsertUserOrg(List<Power_UserView> Userlist,Guid OrgID)
//        {
//            //todo
//            List<Power_UserOrgView> list = new List<Power_UserOrgView>();
//            Power_UserOrgView model = new Power_UserOrgView();
//            foreach (var item in Userlist)
//            {
//                Power_UserOrgView info = new Power_UserOrgView();
//                info.OrgID = OrgID;
//                info.UserID = item.ID;

//                list.Add(info);
//            }
//            try {
//                _userOrgBll.InsertUserOrg(list, CurrentMenuID, HttpContext.Request.Path);
//                foreach (Power_UserOrgView item in list)
//                {
//                    model = item;
//                    logCache.AddServiceLogForAdd(model, CurrentMenuID, "用户机构组新增人员", "Power_UserOrg");
//                }
                  
//            }
//            catch (Exception ex) {            
//                    logCache.AddServiceLogForAdd(model, CurrentMenuID, "用户机构组新增人员异常", "Power_UserOrg", Request.Path, ex.Message, ex.StackTrace);
                        
//            }

//        }

//        /// <summary>
//        /// 删除
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public bool Del(string id)
//        {
//            Power_UserOrgView userOrgView = new Power_UserOrgView();
//            try
//            {
//                if (!string.IsNullOrEmpty(id))
//                {
//                    userOrgView = _userOrgBll.GetViewModel(id);
//                    _userOrgBll.Delete(Guid.Parse(id));
//                    logCache.AddServiceLogForDelete(userOrgView, CurrentMenuID, "用户机构组删除", "Power_UserOrg");
//                    return true;
//                }
                
//            }
//            catch (Exception ex)
//            {
//                logCache.AddServiceLogForDelete(userOrgView, CurrentMenuID, "用户机构组删除异常", "Power_UserOrg", HttpContext.Request.Path, ex.Message, ex.StackTrace);
//            }

//            return false;
//        }
//    }
//}
