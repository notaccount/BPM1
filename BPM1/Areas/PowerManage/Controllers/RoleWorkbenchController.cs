//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

//using Microsoft.AspNetCore.Authorization;
//using Newtonsoft.Json;
//using System.Xml;

//namespace Hasng.CadreFile.WebApp.Areas.PowerManage.Controllers
//{
//    [Area("PowerManage")]
//    public class RoleWorkbenchController : Controller
//    {

//        public RoleWorkbenchController()
//        {
//        }

//        #region 页面加载
//        public IActionResult Index()
//        {
//            logcache.AddServiceLogForView(CurrentMenuID, "角色工作台设置", "");
//            ViewData["U_AreaCode"] = ManageProvider.Current.U_AreaCode;
//            ViewData["WorkbenchType"] = _workbenchBll.GetListJson();
//            return View();
//        }
//        public IActionResult RoleWorkbenchEdit()
//        {

//            return View();
//        }

//        #endregion

//        #region  列表分页
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
//            string strJson = _rolebll.FindListJsonPage(ParameterJson, ref jqgridparam);
//            logcache.AddServiceLogForQuery(CurrentMenuID, "角色工作台设置查询", "", ParameterJson);
//            return JsonManager.ToPageJson(jqgridparam, strJson);





//        }
//        #endregion

//        #region 获取工作台列表
//        /// <summary>
//        /// 获取工作台列表
//        /// </summary>
//        /// <returns></returns>
//        public string GetWorkBenchJson()
//        {
//            string sJson = _workbenchBll.GetListJson();
//            return sJson;
//        }
//        #endregion

//        #region 根据id获取实体
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
//        #endregion

//        #region 保存方法
//        public void AddOrUpdate(Power_RoleView data)
//        {
//            Power_RoleView model = new Power_RoleView();
//            Power_RoleView modelNew = new Power_RoleView();
//            if (data.ID == Guid.Empty)
//            {
//                try
//                {
//                    model = _rolebll.Insert(data);
//                    logcache.AddServiceLogForAdd(model, CurrentMenuID, "角色工作台设置保存", " Power_Role");
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForAdd(model, CurrentMenuID, "角色工作台设置保存", " Power_Role", HttpContext.Request.Path, ex.Message, ex.StackTrace);
//                }

//            }
//            else
//            {
//                try
//                {
//                    model = _rolebll.GetViewModel(data.ID.ToString());
//                    #region 全部字段赋值
//                    model.WorkbenchID = data.WorkbenchID;
//                    #endregion
//                    modelNew = _rolebll.Update(model);
//                    logcache.AddServiceLogForUpdate(modelNew, model, CurrentMenuID, "角色工作台设置更新", " Power_Role");
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForUpdate(modelNew, model, CurrentMenuID, "角色工作台设置更新", " Power_Role", HttpContext.Request.Path, ex.Message, ex.StackTrace);
//                }

//            }


//        }
//        #endregion
//    }
//}