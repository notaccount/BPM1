//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

//using Microsoft.AspNetCore.Authorization;

//using Newtonsoft.Json;

//namespace Hasng.CadreFile.WebApp.Areas.PowerManage.Controllers
//{
//    [Area("PowerManage")]
//    public class WorkbenchController : Controller
//    {

//        public WorkbenchController()
//        {
//        }
//        #region 页面加载
//        //
//        // GET: /Sys_ArchClassifyManage/

//        public IActionResult Index()
//        {
//            logCache.AddServiceLogForView(CurrentMenuID, "工作台配置", "");
//           ViewData["U_AreaCode"] =ManageProvider.Current.U_AreaCode;
//            return View();
//        }
//        public IActionResult AddOrEdit()
//        {
//            return View();
//        }

//        public IActionResult AddOrEditMenu()
//        {
//            return View();
//        }
//        #endregion

//        #region 工作台类型模块

//        /// <summary>
//        /// 添加或编辑
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        public string InsertOrUpdate(Power_WorkbenchView data)
//        {

//            Power_WorkbenchView model = new Power_WorkbenchView();
//            Power_WorkbenchView modelNew = new Power_WorkbenchView();
//            if (data.ID == Guid.Empty)
//            {
//                try {
//                    model = _Workbenc.Insert(data);
//                    logCache.AddServiceLogForAdd(model,CurrentMenuID,"工作台增加", "Power_Workbench");
//                }
//                catch (Exception ex) {
//                    logCache.AddServiceLogForAdd(model, CurrentMenuID, "工作台增加异常", "Power_Workbench",HttpContext.Request.Path,ex.Message,ex.StackTrace);
//                }

               
//            }
//            else
//            {
//                try {
//                    model = _Workbenc.GetViewModel(data.ID.ToString());
//                    #region 全部字段赋值

//                    //编码;
//                    model.Name = data.Name;

//                    //编号;
//                    model.U_IsSystem = data.U_IsSystem;

//                    #endregion
//                    modelNew = _Workbenc.Update(model);
//                    logCache.AddServiceLogForUpdate(modelNew, model, CurrentMenuID, "工作台更新", "Power_Workbench");
//                }
//                catch (Exception ex) {
//                    logCache.AddServiceLogForUpdate( modelNew, model, CurrentMenuID, "工作台更新异常", "Power_Workbench", HttpContext.Request.Path, ex.Message, ex.StackTrace);
//                }

          
//            }

//            return model.ID.ToString();
//        }
//        /// <summary>
//        /// 获取对象
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public string GetEntity(string id)
//        {
//            Power_WorkbenchView model = _Workbenc.GetViewModel(id);
//            return JsonConvert.SerializeObject(model);
//        }
//        /// <summary>
//        /// 列表分页
//        /// </summary>
//        /// <param name="ParameterJson"></param>
//        /// <param name="jqgridparam"></param>
//        /// <returns></returns>
//        public string GetWorkbenchJson(string ParameterJson, JqGridParam jqgridparam)
//        {
//            string areaCode = ManageProvider.Current.U_AreaCode;
//            if (string.IsNullOrEmpty(jqgridparam.sortField))
//            {
//                jqgridparam.sortField = "U_SortNo";
//            }
//            List<ParameterJson> list = new List<ParameterJson>();
//            list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), areaCode));
//            ParameterJson = JsonConvert.SerializeObject(list);
//            string strJson = _Workbenc.FindListJsonPage(ParameterJson, ref jqgridparam);
//            return JsonManager.ToPageJson(jqgridparam, strJson);
//        }

//        /// <summary>
//        /// 获取十大类json
//        /// </summary>
//        /// <returns></returns>
//        public string GetJson()
//        {
//            #region 条件组合
//            List<ParameterJson> list = new List<ParameterJson>();
//            list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
//            string sCondition = JsonConvert.SerializeObject(list);
//            #endregion

//            #region 排序
//            //方法一，默认排序字段U_SortNo，排序方式asc
//            string strSort = new JqSortParam().ToJson();

//            ////方法二，默认排序字段U_SortNo，排序方式asc
//            //string strSort = new JqSortParam("U_SortNo", "asc").ToJson();
//            #endregion

//            string strJson = _archClassifbll.GetListJson(sCondition, strSort);
//            return strJson;
//        }

//        /// <summary>
//        /// 获取工作台列表
//        /// </summary>
//        /// <returns></returns>
//        public string GetWorkBenchAllJson()
//        {
//            List<ParameterJson> list = new List<ParameterJson>();
//            list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
//            list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
//            string sCondition = JsonConvert.SerializeObject(list);
//            string sJson = _Workbenc.GetListJson(sCondition);
//            return sJson;
//        }

//        /// <summary>
//        /// 删除
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public bool Del(string id)
//        {
//            Power_WorkbenchView model = new Power_WorkbenchView();
//            try {
//                if (!string.IsNullOrEmpty(id))
//                {
//                    model = _Workbenc.GetViewModel(id);
//                    _Workbenc.Delete(Guid.Parse(id));
//                    logCache.AddServiceLogForDelete(model, CurrentMenuID, "工作台删除", "Power_Workbench");
//                    return true;
//                }
              
//            }
//            catch (Exception ex) {
//                logCache.AddServiceLogForDelete(model, CurrentMenuID, "工作台删除异常", "Power_Workbench", HttpContext.Request.Path, ex.Message, ex.StackTrace);
//            }
           
//            return false;
//        }
//        #endregion

//        #region 工作台内容模块
//        public string GetWorkbenchMenusJson(string ParameterJson, JqGridParam jqgridparam)
//        {
//            if (string.IsNullOrEmpty(jqgridparam.sortField))
//                jqgridparam.sortField = "U_SortNo";
//            string strJson = _WorkbenchMenus.GetWorkbenchMenusJson(ParameterJson, ref jqgridparam);
//            return JsonManager.ToPageJson(jqgridparam, strJson);
//        }

//        #region 工作台12大模块
//        public string GetMenusJsons()
//        {
//            string strJson = _WorkbenchMenus.GetMenusJsonsBll();
//            return strJson;
//        }
//        #endregion

//        /// <summary>
//        /// 删除
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public bool DelMenu(string id)
//        {
//            Power_WorkbenchMenusView model = new Power_WorkbenchMenusView();      
//            try
//            {
//                if (!string.IsNullOrEmpty(id))
//                {
//                    model = _WorkbenchMenus.GetViewModel(id);
//                    _WorkbenchMenus.Delete(Guid.Parse(id));
//                    logCache.AddServiceLogForDelete(model, CurrentMenuID, "工作台内容删除", "Power_WorkbenchMenus");
//                    return true;
//                }
              
//            }
//            catch (Exception ex)
//            {
//                logCache.AddServiceLogForDelete(model, CurrentMenuID, "工作台内容删除异常", "Power_WorkbenchMenus", HttpContext.Request.Path, ex.Message, ex.StackTrace);
//            }

 
//            return false;
//        }

//        /// <summary>
//        /// 添加或编辑
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        public string InsertOrUpdateMenu(string data,string WorkbenchID)
//        {
//            Power_WorkbenchMenusView model = null;
//            try
//            {
//                if (!string.IsNullOrEmpty(data))
//                {
//                    List<ParameterJson> list2 = new List<ParameterJson>();
//                    list2.Add(new ParameterJson("WorkbenchID", ConditionOperate.Equal.ToString(), WorkbenchID));
//                    _WorkbenchMenus.Delete(JsonConvert.SerializeObject(list2));
//                    string[] sList = data.Split(',');
//                    foreach (string oMenuid in sList)
//                    {
//                         model = new Power_WorkbenchMenusView();
//                        model.WorkbenchID = new Guid(WorkbenchID);
//                        model.MenuID = new Guid(oMenuid);
//                        model = _WorkbenchMenus.Insert(model);
//                        logCache.AddServiceLogForAdd(model,CurrentMenuID,"工作台内容增加", "Power_WorkbenchMenus");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                logCache.AddServiceLogForAdd(model, CurrentMenuID, "工作台内容增加异常", "Power_WorkbenchMenus", HttpContext.Request.Path, ex.Message, ex.StackTrace);

//            }  
//            return "true";
//        }
//        /// <summary>
//        /// 获取对象
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public string GetEntityMenu(string id)
//        {
//            Power_WorkbenchMenusView model = _WorkbenchMenus.GetViewModel(id);
//            return JsonConvert.SerializeObject(model);
//        }

//        public string GetWorkbenchMenuJson(string ParameterJson, JqGridParam jqgridparam,string workbenchid)
//        {
//            MessageUser mu = ManageProvider.Current;
//            string roleIds = mu.RoleIds;         
//            List<ParameterJson> list = new List<ParameterJson>();
//            list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));       
//            list.Add(new ParameterJson("Type", ConditionOperate.Equal.ToString(), "490CB91A-EA3F-4796-9650-8F55FD9B2EBD"));
//            string menuJson = _menubll.GetListJson(JsonConvert.SerializeObject(list));
//            List<Power_MenuView> MenuList = JsonConvert.DeserializeObject<List<Power_MenuView>>(menuJson);
//            MenuList = MenuList.OrderBy(x => x.U_SortNo).ToList();
//            List<ParameterJson> list2 = new List<ParameterJson>();
//            list2.Add(new ParameterJson("WorkbenchID", ConditionOperate.Equal.ToString(), workbenchid));
//            string workBenchJson = _WorkbenchMenus.GetListJson(JsonConvert.SerializeObject(list2));
//            List<Power_WorkbenchMenusView> myMenu = JsonConvert.DeserializeObject<List<Power_WorkbenchMenusView>>(workBenchJson);
//            var treelist = MenuList.Select(x => new
//            {
//                id = x.ID,
//                text = x.Name,
//                pid = x.ParentID,
//                @checked = myMenu.Where(m => m.MenuID == x.ID).Count() > 0
//            }).ToList();

//            return treelist.ToJson();


           
//        }

//        #endregion
//    }
//}
