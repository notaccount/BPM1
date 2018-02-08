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
//    public class StationController : Controller
//    {

//        public StationController()
//        {
//        }


//        /// <summary>
//        /// 主页面
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult Index()
//        {
//            logcache.AddServiceLogForView(CurrentMenuID, "工作岗位管理浏览", "");
//            return View();
//        }

//        /// <summary>
//        /// 数据页面
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult AddOrEditStationView(String id)
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
//            if (string.IsNullOrEmpty(jqgridparam.sortField))
//                jqgridparam.sortField = "U_SortNo";
//            string strJson = _stationbll.FindListJsonPage(ParameterJson, ref jqgridparam);
//            return JsonManager.ToPageJson(jqgridparam, strJson);
//        }

//        /// <summary>
//        /// 获取ViewModel
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public string GetEntity(string id)
//        {
//            Power_StationsView entity = _stationbll.GetViewModel(id);
//            return entity.ToJson();
//        }

//        /// <summary>
//        /// 获取岗位的json
//        /// </summary>
//        /// <returns></returns>
//        public string GetStationJson()
//        {

//            #region 条件组合
//            string sCondition = JqConditionParam.ToJson();
//            #endregion

//            #region 排序
//            string strSort = new JqSortParam().ToJson();

//            #endregion

//            string strJson = _stationbll.GetListJson(sCondition, strSort);
//            return strJson;
//        }

//        /// <summary>
//        /// 编辑或新增
//        /// </summary>
//        /// <param name="model"></param>
//        public void AddOrUpdate(Power_StationsView model)
//        {
//            Power_StationsView data = new Power_StationsView();
//            //判断前端返回回来的model的id是否为空
//            if (model.ID == Guid.Empty)
//            {
//                data = model;
//                data.U_CreateDate = DateTime.Now;
//                try
//                {
//                    _stationbll.Insert(data);
//                    logcache.AddServiceLogForAdd<Power_StationsView>(data, CurrentMenuID, "新增岗位成功", "Power_Stations");
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForAdd<Power_StationsView>(data, CurrentMenuID, "新增岗位异常", "Power_Stations", Request.Path, ex.Message, ex.StackTrace);
//                }
//            }
//            else
//            {
//                data = _stationbll.GetViewModel(model.ID.ToString());
//                Power_StationsView oldData = ObjectClone.ToClone<Power_StationsView>(data);

//                try
//                {
//                    data.Code = model.Code;
//                    data.Name = model.Name;
//                    data.Remarks = model.Remarks;
//                    data.U_LastModifiedDate = DateTime.Now;

//                    _stationbll.Update(data);
//                    logcache.AddServiceLogForUpdate(data, oldData, CurrentMenuID, "更新岗位成功", "Power_Stations");
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForUpdate(data, oldData, CurrentMenuID, "更新岗位异常", "Power_Stations", Request.Path, ex.Message, ex.StackTrace);
//                }
//            }
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
//                Power_StationsView model = _stationbll.GetViewModel(id);
//                try
//                {
//                    _stationbll.Delete(Guid.Parse(id));
//                    logcache.AddServiceLogForDelete<Power_StationsView>(model, CurrentMenuID, "删除岗位成功", "Power_Stations");
//                    return true;
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForDelete<Power_StationsView>(model, CurrentMenuID, "删除岗位异常", "Power_Stations", Request.Path, ex.Message, ex.StackTrace);
//                    return false;
//                }

//            }
//            return false;
//        }

//        /// <summary>
//        /// 新增时验证输入的值是否唯一
//        /// </summary>
//        /// <param name="tableName">表名</param>
//        /// <param name="filed">字段名</param>
//        /// <param name="val">字段值</param>
//        /// <returns></returns>
//        public bool FindOnly(string stationCode)
//        {
//            #region 条件组合
//            List<ParameterJson> list = new List<ParameterJson>();

//            list.Add(new ParameterJson("Code", ConditionOperate.Equal.ToString(), stationCode));
//            list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
//            list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
//            string sCondition = JsonConvert.SerializeObject(list);
//            #endregion

//            return _stationbll.IsOnlyRecord(sCondition);
//        }

//        /// <summary>
//        /// 编辑时验证输入的值是否唯一
//        /// </summary>
//        /// <param name="tableName">表名</param>
//        /// <param name="filed">字段名</param>
//        /// <param name="val">字段值</param>
//        /// <returns></returns>
//        public bool FindOnlyEdit(string stationCode, string ID)
//        {
//            #region 条件组合
//            List<ParameterJson> list = new List<ParameterJson>();

//            list.Add(new ParameterJson("Code", ConditionOperate.Equal.ToString(), stationCode));
//            list.Add(new ParameterJson("ID", ConditionOperate.NotEqual.ToString(), ID));
//            list.Add(new ParameterJson("U_AreaCode", ConditionOperate.Equal.ToString(), ManageProvider.Current.U_AreaCode));
//            list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
//            string sCondition = JsonConvert.SerializeObject(list);
//            #endregion

//            return _stationbll.IsOnlyRecord(sCondition);
//        }
//    }
//}