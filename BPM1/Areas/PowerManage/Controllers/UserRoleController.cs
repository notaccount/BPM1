//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

//using Newtonsoft.Json;

//using Microsoft.AspNetCore.Authorization;


//namespace Hasng.CadreFile.WebApp.Areas.PowerManage.Controllers
//{
//    [Area("PowerManage")]
//    public class UserRoleController : Controller
//    {


//        public UserRoleController()
//        {
//        }
//        /// <summary>
//        /// 主界面
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult Index()
//        {
//            logcache.AddServiceLogForView(CurrentMenuID, "角色人员列表", "");
//            return View();
//        }
//        /// <summary>  
//        /// 删除
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public bool Delete(string parameterJson)
//        {
//            if (!string.IsNullOrEmpty(parameterJson))
//            {
//                Power_UserRoleView userRoleInfo = _userRoleBll.GetViewInfo(parameterJson);
//                try
//                {
//                    _userRoleBll.Delete(parameterJson);
//                    //记录日志
//                    logcache.AddServiceLogForDelete<Power_UserRoleView>(userRoleInfo, CurrentMenuID, "删除角色对应人员", "Power_UserRole");
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForDelete<Power_UserRoleView>(userRoleInfo, CurrentMenuID, "删除角色对应人员", "Power_UserRole", Request.Path, ex.Message, ex.StackTrace);
//                }

//                return true;
//            }
//            return false;
//        }


//        public void Insert(List<Power_UserView> users,Guid RoleId)
//        {
//            #region 角色对应人员列表
//            List<ParameterJson> list = new List<ParameterJson>();
//            list.Add(new ParameterJson("RoleId", ConditionOperate.Equal.ToString(), RoleId.ToString()));
//            string sCondition = JsonConvert.SerializeObject(list);

//            string sExists = _userRoleBll.GetListJson(sCondition);

//            List<Power_UserRoleView> existList = JsonConvert.DeserializeObject<List<Power_UserRoleView>>(sExists);
//            #endregion

//            #region 批量添加

//            List<Power_UserRoleView> insertList = new List<Power_UserRoleView>();
//            foreach (Power_UserView info in users)
//            {
//                if (!existList.Exists(d => d.UserID == info.ID))
//                {
//                    Power_UserRoleView userrole = new Power_UserRoleView();
//                    userrole.RoleID = RoleId;
//                    userrole.UserID = info.ID;

//                    insertList.Add(userrole);
//                }
//            }

//            _userRoleBll.PlentyInsert(insertList); 
//            #endregion
            
//            #region 更新人员对应菜单
//            List<Guid> userIdList = new List<Guid>();
//            foreach (Power_UserView info in users)
//            {
//                if (!existList.Exists(d => d.UserID == info.ID))
//                {
//                    userIdList.Add(info.ID);
//                }
//            }

//            _myUserMenuBll.UpdateUsersMenu(userIdList);

//            #endregion
//        }


//        public void InsertBatch(string RoleIds,string UserID)
//        {
//            #region 角色对应人员列表
//            List<ParameterJson> list = new List<ParameterJson>();
//            list.Add(new ParameterJson("UserID", ConditionOperate.Equal.ToString(), UserID));
//            string sCondition = JsonConvert.SerializeObject(list);

//            string sExists = _userRoleBll.GetListJson(sCondition);

//            List<Power_UserRoleView> existList = JsonConvert.DeserializeObject<List<Power_UserRoleView>>(sExists);
//            #endregion

//            #region 批量删除
//            if (existList.Count > 0)
//            {
//                list = new List<ParameterJson>();
//                foreach (Power_UserRoleView info in existList)
//                {
//                    list.Add(new ParameterJson("ID", ConditionOperate.Equal.ToString(), info.ID.ToString(),"OR"));
//                }
//                _userRoleBll.Delete(JsonConvert.SerializeObject(list));
//            }
//            #endregion

//            #region 批量添加
//            List<Power_UserRoleView> insertList = new List<Power_UserRoleView>();
//            foreach (string roleid in RoleIds.Split(','))
//            {
//                Power_UserRoleView userrole = new Power_UserRoleView();
//                userrole.RoleID = Guid.Parse(roleid);
//                userrole.UserID = Guid.Parse(UserID);

//                insertList.Add(userrole);
//            } 
//            _userRoleBll.PlentyInsert(insertList);
//            #endregion


//            #region 更新人员对应菜单
//            _myUserMenuBll.UpdateUsersMenu(new List<Guid>() { new Guid(UserID) });

//            #endregion

//        }

//    }
//}
