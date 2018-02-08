
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
//    public class RoleController : Controller
//    {

//        public RoleController()
//        {
//        }

//        #region 界面 列表、新增编辑、角色选择人员、角色选择菜单
//        /// <summary>
//        /// 数据列表
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult Index()
//        {
//            logcache.AddServiceLogForView(CurrentMenuID, "角色管理浏览", "");
//            return View();
//        }

//        /// <summary>
//        /// 新增或编辑
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult RoleAddOrEdit()
//        {
//            return View();
//        }

//        /// <summary>
//        /// 选择人员
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult ChoosePersons()
//        {
//            return View();
//        }

//        /// <summary>
//        /// 选择角色对应菜单
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult ChooseMenus()
//        {
//            return View();
//        }
//        public IActionResult showMenus()
//        {
//            return View();
//        }
//        #endregion

//        /// <summary>
//        /// 根据RoleId获取权限
//        /// </summary>
//        /// <param name="roleId"></param>
//        /// <returns></returns>
//        public string GetPowerMenuJson(string roleId)
//        {
//            List<MiniTreeNode2> tree = new List<MiniTreeNode2>();
//            if (!string.IsNullOrEmpty(roleId))
//            {
            
//                #region 条件组合
//                List<ParameterJson> list = new List<ParameterJson>();
//                list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
//                list.Add(new ParameterJson("IsEnable", ConditionOperate.Equal.ToString(), "1"));
//                list.Add(new ParameterJson("IsSuperUser", ConditionOperate.Equal.ToString(), "0"));
//                string sCondition = JsonConvert.SerializeObject(list);
//                #endregion

//                string strSort = new JqSortParam().ToJson();

//                //string sJson = _menubll.GetListJson(sCondition, strSort);
//                string sJson = _menubll.GetMenusByRoleId(roleId,ManageProvider.Current.U_AreaCode);
//                if (!string.IsNullOrEmpty(sJson))
//                {
//                    List<Power_MenuView> roleMenuList1 = JsonConvert.DeserializeObject<List<Power_MenuView>>(sJson);
//                    var  o= roleMenuList1.Where(x => x.IsShow == true).OrderBy(m=>m.U_SortNo).ToList();
//                    foreach (var item in o)
//                    {
//                        MiniTreeNode2 node = new MiniTreeNode2();
//                        node.id = item.ID.ToString();
//                        node.pid = item.ParentID.ToString();
//                        node.text = item.Name;
//                        //node.@checked = roleMenuList.Where(x => x.MenuID == item.ID).Count() > 0 ? true : false;
//                        tree.Add(node);
//                    }
//                }
//            }
//            return JsonConvert.SerializeObject(tree);
//        }


//        /// <summary>
//        /// 新增或编辑
//        /// </summary>
//        /// <param name="model"></param>
//        public void AddOrUpdate(Power_RoleView data)
//        {
//            Power_RoleView model = new Power_RoleView();
//            List<ParameterJson> list = new List<ParameterJson>();
//            list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
//            list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
//            //list.Add(new ParameterJson("U_IsSystem", ConditionOperate.Equal.ToString(), "1"));
//            string sCondition = JsonConvert.SerializeObject(list);
//            string json = _workbenchBll.GetListJson(sCondition);
//            List<Power_WorkbenchView> workbenchList = JsonConvert.DeserializeObject<List<Power_WorkbenchView>>(json);
//            var a = workbenchList.Where(x => x.Name == "默认角色工作台").FirstOrDefault();
//            //判断前台传过来的model的id是否为空
//            if (data.ID == Guid.Empty)
//            {
                
//                model = data;
//                model.U_CreateDate = DateTime.Now;
//                model.U_IsSystem = false;
//                model.WorkbenchID = a.ID;
//                try
//                {
//                    _rolebll.Insert(model);
//                    //记录日志
//                    logcache.AddServiceLogForAdd<Power_RoleView>(model, CurrentMenuID, "新增角色成功", "Power_Role");
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForAdd<Power_RoleView>(model, CurrentMenuID, "新增角色异常", "Power_Role", Request.Path,ex.Message,ex.StackTrace);
//                }
//            }
//            else
//            {
//                model = _rolebll.GetViewModel(data.ID.ToString());
//                Power_RoleView oldModel = ObjectClone.ToClone<Power_RoleView>(model);
//                try
//                {
//                    model.Name = data.Name;
//                    model.Status = data.Status;
//                    model.WorkbenchID = data.WorkbenchID;
//                    model.Remarks = data.Remarks;
//                    model.U_LastModifiedDate = DateTime.Now;
//                    model.U_IsSystem = false;

//                    _rolebll.Update(model);
//                    //记录日志
//                    logcache.AddServiceLogForUpdate(model, oldModel, CurrentMenuID, "更新角色成功", "Power_Role");
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForUpdate(model, oldModel, CurrentMenuID, "更新角色异常", "Power_Role", Request.Path,ex.Message,ex.StackTrace);
//                }
//            }
//        }

//        /// <summary>
//        /// 角色选择确定按钮
//        /// </summary>
//        /// <param name="data"></param>
//        public void UserRoleSave(Power_UserRoleView data)
//        {
//            Power_UserRoleView model = new Power_UserRoleView();
//            Power_UserRoleView modelNew = new Power_UserRoleView();
//            try
//            {
           
//                model = _userroleBll.GetViewModel(data.ID.ToString());
//                //角色ID;
//                model.RoleID = data.RoleID;
//                modelNew= _userroleBll.Update(model);
//                logcache.AddServiceLogForUpdate(modelNew, model, CurrentMenuID, "更新角色选择", "Power_UserRole");
//            }
//            catch (Exception ex) {
//                logcache.AddServiceLogForUpdate(modelNew, model, CurrentMenuID, "更新角色选择异常", "Power_UserRole");
//            }

        
//        }

//        /// <summary>
//        /// 获取对象
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public string GetEntity(string id)
//        {
//            Power_RoleView model = _rolebll.GetViewModel(id);
//            return JsonConvert.SerializeObject(model);
//        }

//        /// <summary>
//        /// 获取角色人员关系对象
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public string GetUserRoleEntity(string id)
//        {
//            Power_UserRoleView model = _userroleBll.GetViewModel(id);
//            return JsonConvert.SerializeObject(model);
//        }

//        /// <summary>
//        /// 获取json，带工作台名称
//        /// </summary>
//        /// <returns></returns>
//        public string GetJsonListForRoles(string roleName)
//        {
//            #region 条件组合
//            List<ParameterJson> list = new List<ParameterJson>();
//            list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
//            list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
//            //list.Add(new ParameterJson("U_IsSystem", ConditionOperate.Equal.ToString(), "1"));
//            if (!string.IsNullOrEmpty(roleName))
//            {
//                list.Add(new ParameterJson("Name", ConditionOperate.Like.ToString(), roleName));
//            }
//            string sCondition = JsonConvert.SerializeObject(list);
//            #endregion

//            #region 排序
//            string strSort = new JqSortParam().ToJson();
//            #endregion
//            string strJson = _rolebll.GetListJson(sCondition, strSort);
//            return strJson;
//        }

//        /// <summary>
//        /// 根据角色获取菜单ID列表
//        /// </summary>
//        /// <param name="roleId">角色ID</param>
//        /// <returns></returns>
//        public string GetRoleMenuJson(string roleId)
//        {
//            List<ParameterJson> list1 = new List<ParameterJson>();
//            list1.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
//            list1.Add(new ParameterJson("RoleId", ConditionOperate.Equal.ToString(), roleId));
//            string json = _rolemenubll.GetListJson(JsonConvert.SerializeObject(list1));

//            return json;
//        }

//        //TODO Edit 潘超  角色管理 加载工作台需要修改
//        /// <summary>
//        /// 列表分页
//        /// </summary>
//        /// <param name="ParameterJson"></param>
//        /// <param name="jqgridparam"></param>
//        /// <returns></returns>
//        public string GetPagerJson(string ParameterJson, JqGridParam jqgridparam)
//        {
//            if (string.IsNullOrEmpty(jqgridparam.sortField))
//                jqgridparam.sortField = "U_SortNo";

//            List<ParameterJson> list = new List<ParameterJson>();
//            list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
//            list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
//            string sCondition = JqConditionParam.CombinationCon(JsonConvert.SerializeObject(list), ParameterJson);
            
//            string strJson = _rolebll.FindListJsonPage(sCondition, ref jqgridparam);
//            return JsonManager.ToPageJson(jqgridparam, strJson);
//        }

//        /// <summary>
//        /// 获取角色列表 角色人员关系页面使用
//        /// </summary>
//        /// <returns></returns>
//        public string GetRoleJson()
//        {
//            string sCondition = JqConditionParam.ToJson();
//            string strSort = new JqSortParam().ToJson();
//            string strJson = _rolebll.GetListJson(sCondition, strSort);
//            return strJson;
//        }

//        /// <summary>
//        /// 删除角色
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public bool Delete(string id)
//        {
//            if (!string.IsNullOrEmpty(id))
//            {
//                Power_RoleView model = _rolebll.GetViewModel(id);
//                try
//                {
//                    _rolebll.Delete(Guid.Parse(id));
//                    //记录日志
//                    logcache.AddServiceLogForDelete<Power_RoleView>(model, CurrentMenuID, "角色删除", "");
//                    return true;
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForDelete<Power_RoleView>(model, CurrentMenuID, "角色删除异常", "",Request.Path,ex.Message,ex.StackTrace);
//                    return false;
//                }
//            }
//            return false;
//        }

//        /// <summary>
//        /// 删除角色人员对应关系
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public bool UserRoleDelete(string id)
//        {
//            if (!string.IsNullOrEmpty(id))
//            {
//                Power_UserRoleView model = _userroleBll.GetViewModel(id);
//                try
//                {
//                    _userroleBll.Delete(Guid.Parse(id));
//                    logcache.AddServiceLogForDelete<Power_UserRoleView>(model, CurrentMenuID, "删除角色人员对应关系", "Power_UserRole");
//                    return true;
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForDelete<Power_UserRoleView>(model, CurrentMenuID, "删除角色人员对应关系异常", "Power_UserRole", Request.Path,ex.Message,ex.StackTrace);
//                    return false;
//                }
//            }
//            return false;
//        }

//        /// <summary>
//        /// 角色选择菜单
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="menus"></param>
//        public void ChooseMenu(string roleid,string menus)
//        {
//            MessageUser mu = ManageProvider.Current;
//            try {
//                //先判断传递过来的菜单id集合是否为空，为空不绑定该角色的菜单
//                if (menus == null || string.IsNullOrEmpty(menus))
//                {
//                    return;
//                }

//                #region 删除数据

//                List<ParameterJson> list = new List<ParameterJson>();
//                list.Add(new ParameterJson("RoleID", ConditionOperate.Equal.ToString(), roleid));
//                string sCondition = JsonConvert.SerializeObject(list);

//                _rolemenubll.Delete(sCondition);

//                roleCache.DeleteRoleMenu(roleid,mu.U_AreaCode); //删除缓存 
//                #endregion


//                List<Power_RoleMenuView> roleMenulist = new List<Power_RoleMenuView>();
//                String[] menuArray = menus.Split(',');

//                for (int i = 0; i < menuArray.Length; i++)
//                {
//                    if (CommonHelper.CheckGuid(menuArray[i].ToString()))
//                    {
//                        //用于新增一条数据
//                        Power_RoleMenuView model = new Power_RoleMenuView();
//                        model.RoleID = Guid.Parse(roleid);
//                        model.MenuID = Guid.Parse(menuArray[i].ToString());
//                        roleMenulist.Add(model);

//                        logcache.AddServiceLogForAdd<Power_RoleMenuView>(model, CurrentMenuID, "角色选择菜单增加成功", "Power_RoleMenu");
//                    }
//                }

//                if (roleMenulist.Count > 0)
//                    _rolemenubll.PlentyInsert(roleMenulist);


//                #region 更新角色对应人员菜单                
//                List<ParameterJson> userRolelist = new List<ParameterJson>();
//                list.Add(new ParameterJson("roleid", ConditionOperate.Equal.ToString(), roleid));
//                string sExists = _userroleBll.GetListJson(JsonConvert.SerializeObject(list));
//                List<Power_UserRoleView> existList = JsonConvert.DeserializeObject<List<Power_UserRoleView>>(sExists);


//                List<Guid> userIdList = new List<Guid>();
//                foreach (Power_UserRoleView info in existList)
//                {
//                    if (!userIdList.Exists(d => d == info.UserID.Value))
//                    {
//                        userIdList.Add(info.UserID.Value);
//                    }
//                }
//                _myMenuBll.UpdateUsersMenu(userIdList);
//                #endregion
//            }
//            catch (Exception ex) {
//                logcache.AddServiceLogForAdd<Power_RoleMenuView>(new Power_RoleMenuView(), CurrentMenuID, "角色选择菜单增加异常", "Power_RoleMenu",HttpContext.Request.Path,ex.Message,ex.StackTrace);
//            }      
//            roleCache.AddOrUpdate(roleid, menus,mu.U_AreaCode);
//        }

//        /// <summary>
//        /// 角色选择人员
//        /// </summary>
//        /// <param name="roleid"></param>
//        /// <param name="persons"></param>
//        public void ChoosePerson(String roleid, String areaCode, String persons)
//        {
//            Power_UserRoleView modelNew = new Power_UserRoleView();
//            try {
//                //先判断传递过来的菜单id集合是否为空，为空不绑定该角色的菜单
//                if (persons == null || string.IsNullOrEmpty(persons))
//                {
//                    _userroleBll.DelUserRole(roleid, areaCode);
//                    return;
//                }
//                String[] personsArray = persons.Split(',');
//                //插入时先删除该角色之前绑定的数据，再插入新数据
//                _userroleBll.DelUserRole(roleid, areaCode);
//                for (int i = 0; i < personsArray.Length; i++)
//                {
//                    //判断取回来的用户id是否是Guid
//                    if (CommonHelper.CheckGuid(personsArray[i].ToString()))
//                    {
//                        Power_UserRoleView model = new Power_UserRoleView();
//                        model.RoleID = Guid.Parse(roleid);
//                        model.UserID = Guid.Parse(personsArray[i].ToString());
//                        modelNew= _userroleBll.Insert(model);
//                        logcache.AddServiceLogForAdd(model, CurrentMenuID, "角色选择人员增加成功", " Power_UserRole");
//                    }
//                }
//            }
//            catch (Exception ex) {
//                logcache.AddServiceLogForAdd(modelNew, CurrentMenuID, "角色选择人员增加异常", " Power_UserRole",HttpContext.Request.Path,ex.Message,ex.StackTrace);
//            }



           
//        }

//    }
//}