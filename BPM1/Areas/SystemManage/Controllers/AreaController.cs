using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Xml;
using BPM.Models;
using BPM.Repository;

namespace BPM1.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class AreaController : Controller
    {
        DataContext _db;
        public AreaController(DataContext db)
        {
            _db = db;
        }


        /// <summary>
        /// 主界面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 区域管理主数据
        /// </summary>
        /// <returns></returns>
        public String GetAllAreas(string ParameterJson, JqSortParam jqsortparam)
        {
            //string f = JsonConvert.SerializeObject(jqsortparam);
            var list = _db.Power_Area.ToList();
            return  JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 根据id获取ViewModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public string GetModel(string id)
        //{
        //    Power_AreaView entity = _areabll.GetViewModel(id);
        //    return entity.ToJson();
        //}

        ///// <summary>
        ///// 带分页数据列表
        ///// </summary>
        ///// <param name="ParameterJson"></param>
        ///// <param name="jqgridparam"></param>
        ///// <returns></returns>
        //public String GetPagerJons(string ParameterJson, JqGridParam jqgridparam)
        //{
        //    string sAreaJson = _areabll.FindListJsonPage(ParameterJson, ref jqgridparam);
        //    return JsonManager.ToPageJson(jqgridparam, sAreaJson);
        //}

  

        ///// <summary>
        ///// 获取数据表有效的json数据
        ///// </summary>
        ///// <returns></returns>
        //[AllowAnonymous]
        //public String GetAllAreasJsons()
        //{
        //    string areaJson = _areabll.GetListJson();
        //    List<Power_AreaView> list = JsonConvert.DeserializeObject<List<Power_AreaView>>(areaJson);

        //    var areaList = list.Where(p => p.IsEnable != false).OrderBy(d => d.U_SortNo).ToList();
        //    return areaList.ToJson();
        //    //return areaJson;
        //}


        ///// <summary>
        ///// 初始化区域默认值
        ///// <param name="AreaCode">区域code</param>
        ///// <param name="AreaCodeName">区域名字</param>
        ///// </summary>
        ///// <returns></returns>
        //public int AreaInit(string AreaCode, string AreaCodeName)
        //{
        //    return _areabll.AreaInit(AreaCode, AreaCodeName);
        //}

        ///// <summary>
        ///// 超级管理员区域初始化默认值
        ///// <param name="AreaCode">区域code</param>
        ///// <param name="AreaCodeName">区域名字</param>
        ///// </summary>
        ///// <returns></returns>
        //public int AreaSystemInit(string AreaCode, string AreaCodeName)
        //{
        //    return _areabll.AreaSystemInit(AreaCode, AreaCodeName);
        //}


        //#region 读取XML文件内容
        //public static string GetNodeInnerText(XmlNode node, string sName, string sDefaultValue = "")
        //{
        //    try
        //    {
        //        XmlAttribute attr = node.Attributes[sName];

        //        if (null != attr)
        //        {
        //            return attr.Value;
        //        }
        //        else
        //            return sDefaultValue;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //#endregion


        ///// <summary>
        ///// 启用、禁用 区域
        ///// </summary>
        ///// <param name="ID"></param>
        ///// <param name="modifyType"></param>
        ///// <param name="areaCode"></param>
        ///// isOpen  是否开启三元
        ///// <returns></returns>
        //public Boolean ModifyIsEnable(String ID, int modifyType, string areaCode, int isOpen)
        //{
        //    Boolean enableFlag = false;

        //    //创建三元与管理员 账号
        //    ThreeMemberSettings model = XmlHelperRoleUser.GetTableInfo();
        //    //启用区域
        //    if (modifyType == 1)
        //    {
        //        if (_areabll.ModifyIsEnable(ID, true) > 0)
        //        {
        //            _appinitbll.OpenArea(areaCode);
        //            if (isOpen == 1)
        //                _areabll.OpenSanYuan(areaCode);
        //            else
        //                _areabll.CloseSanYuan(areaCode);
        //        }
        //    }
        //    //禁用区域
        //    else
        //    {
        //        if (_areabll.ModifyIsEnable(ID, false) > 0) { enableFlag = true; }
        //    }
        //    return enableFlag;
        //}

        ///// <summary>
        ///// 关闭三员管理
        ///// </summary>
        ///// <param name="areaCode"></param>
        //private void CloseSanYuan(string areaCode)
        //{
        //    _areabll.CloseSanYuan(areaCode);
        //}

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public bool Del(string id)
        //{
        //    Power_AreaView power_AreaView = new Power_AreaView();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(id))
        //        {
        //            power_AreaView = _areabll.GetViewModel(id);
        //            _areabll.Delete(Guid.Parse(id));
        //            return true;
        //        }
        //        logCache.AddServiceLogForDelete(power_AreaView, CurrentMenuID, "管档单位维护删除", "Power_Area");
        //    }
        //    catch (Exception ex)
        //    {
        //        logCache.AddServiceLogForDelete(power_AreaView, CurrentMenuID, "管档单位维护删除", "Power_Area", HttpContext.Request.Path, ex.Message, ex.StackTrace);
        //    }

        //    return false;
        //}
    }
}