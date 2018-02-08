//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;

//using Newtonsoft.Json;

//using Microsoft.AspNetCore.Authorization;

//namespace Hasng.CadreFile.WebApp.Areas.PowerManage.Controllers
//{

//    [Authorize]
//    [Area("PowerManage")]
//    public class UsersOrganizationController : Controller
//    {

//        public UsersOrganizationController()
//        {
//        }


//        /// <summary>
//        /// 主页面
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult Index()
//        {
//            return View();
//        }

//        /// <summary>
//        /// 新增、编辑
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult AddUsersOrgView()
//        {
//            return View();
//        }

//        /// <summary>
//        /// 组织机构关联
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult Mapping()
//        {
//            logcache.AddServiceLogForView(CurrentMenuID, "浏览组织机构关联", "Power_UserOrganization");
//            return View();
//        }

//        /// <summary>
//        /// 新增或编辑
//        /// </summary>
//        /// <param name="model"></param>
//        public void AddOrUpdate(Power_UserOrganizationView model)
//        {
//            Power_UserOrganizationView data = new Power_UserOrganizationView();
//            if (model.ID == Guid.Empty)
//            {
//                try
//                {
//                    data = model;
//                    data.IsEnable = model.IsEnable;
//                    data.U_IsSystem = false;
//                    data.U_IsValid = true;
//                    data.Remarks = model.Remarks;
//                    data = _uorbll.Insert(data);
//                    //记录日志
//                    logcache.AddServiceLogForAdd<Power_UserOrganizationView>(data, CurrentMenuID, "新增组织机构异常", "Power_UserOrganization");
//                }
//                catch (Exception ex)
//                {
//                    //记录日志
//                    logcache.AddServiceLogForAdd<Power_UserOrganizationView>(data, CurrentMenuID, "新增组织机构异常", "Power_UserOrganization", Request.Path,ex.Message,ex.StackTrace);
//                }
//            }
//            else
//            {
//                data = _uorbll.GetViewModel(model.ID.ToString());
//                Power_UserOrganizationView oldData = ObjectClone.ToClone<Power_UserOrganizationView>(data); ;

//                try
//                {
//                    data.Name = model.Name;
//                    data.ParentID = model.ParentID;
//                    data.IsEnable = model.IsEnable;
//                    data.Remarks = model.Remarks;

//                    data = _uorbll.Update(data);
//                    //记录日志
//                    logcache.AddServiceLogForUpdate(data, oldData, CurrentMenuID, "更新组织机构", "Power_UserOrganization");
//                }catch (Exception ex)
//                {
//                    logcache.AddServiceLogForUpdate(data, oldData, CurrentMenuID, "更新组织机构异常", "Power_UserOrganization", Request.Path,ex.Message,ex.StackTrace);
//                }
//            }
//        }

//        /// <summary>
//        /// 获取对象，AreaCode
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public string GetEntity(string id)
//        {
//            Power_UserOrganizationView model = _uorbll.GetViewModel(id);
//            return JsonConvert.SerializeObject(model);
//        }

//        /// <summary>
//        /// 获取表记录模型  AreaId
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public string GetModel(string id)
//        {
//            Power_UserOrganizationView entity = _uorbll.GetViewModel(id);

//            Power_AreaView viewmodel = _areabll.GetAreaViewByCode(entity.U_AreaCode);
//            if (viewmodel != null && viewmodel.ID != Guid.Empty)
//                entity.U_AreaCode = viewmodel.ID.ToString();
//            return entity.ToJson();
//        }

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
//            string strJson = _uorbll.FindListJsonPage(ParameterJson, ref jqgridparam);
//            return JsonManager.ToPageJson(jqgridparam, strJson);
//        }

//        /// <summary>
//        /// 获取json  所有数据
//        /// </summary>
//        /// <returns></returns>
//        public string GetJson()
//        {
//            string sCondition = JqConditionParam.ToJson();
//            string strSort = new JqSortParam().ToJson();
//            string strJson = _uorbll.GetListJson(sCondition, strSort);
//            return strJson;
//        }

//        /// <summary>
//        /// 通用获取组织机构 树形结构
//        /// </summary>
//        /// <returns></returns>
//        public string GetOrganizationTreeJsons()
//        {
//            string userOrgJson = _uorbll.GetOrganizationTreeJson();
//            return userOrgJson;
//        }

//        /// <summary>
//        /// 删除
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public bool Delete(string id)
//        {
//            if (!string.IsNullOrEmpty(id))
//            {
//                Power_UserOrganizationView model = _uorbll.GetViewModel(id);
//                try
//                {
//                    _uorbll.Delete(Guid.Parse(id));
//                    //写入日志
//                    logcache.AddServiceLogForDelete<Power_UserOrganizationView>(model, CurrentMenuID, "删除组织机构", "Power_UserOrganization");
//                    return true;
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForDelete<Power_UserOrganizationView>(model, CurrentMenuID, "删除组织机构异常", "Power_UserOrganization", Request.Path,ex.Message,ex.StackTrace);
//                }
//            }
//            return false;
//        }

//    }
//}