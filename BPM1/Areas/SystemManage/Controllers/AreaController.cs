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
        /// ������
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <returns></returns>
        public String GetAllAreas(string ParameterJson, JqSortParam jqsortparam)
        {
            //string f = JsonConvert.SerializeObject(jqsortparam);
            var list = _db.Power_Area.ToList();
            return  JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// ����id��ȡViewModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public string GetModel(string id)
        //{
        //    Power_AreaView entity = _areabll.GetViewModel(id);
        //    return entity.ToJson();
        //}

        ///// <summary>
        ///// ����ҳ�����б�
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
        ///// ��ȡ���ݱ���Ч��json����
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
        ///// ��ʼ������Ĭ��ֵ
        ///// <param name="AreaCode">����code</param>
        ///// <param name="AreaCodeName">��������</param>
        ///// </summary>
        ///// <returns></returns>
        //public int AreaInit(string AreaCode, string AreaCodeName)
        //{
        //    return _areabll.AreaInit(AreaCode, AreaCodeName);
        //}

        ///// <summary>
        ///// ��������Ա�����ʼ��Ĭ��ֵ
        ///// <param name="AreaCode">����code</param>
        ///// <param name="AreaCodeName">��������</param>
        ///// </summary>
        ///// <returns></returns>
        //public int AreaSystemInit(string AreaCode, string AreaCodeName)
        //{
        //    return _areabll.AreaSystemInit(AreaCode, AreaCodeName);
        //}


        //#region ��ȡXML�ļ�����
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
        ///// ���á����� ����
        ///// </summary>
        ///// <param name="ID"></param>
        ///// <param name="modifyType"></param>
        ///// <param name="areaCode"></param>
        ///// isOpen  �Ƿ�����Ԫ
        ///// <returns></returns>
        //public Boolean ModifyIsEnable(String ID, int modifyType, string areaCode, int isOpen)
        //{
        //    Boolean enableFlag = false;

        //    //������Ԫ�����Ա �˺�
        //    ThreeMemberSettings model = XmlHelperRoleUser.GetTableInfo();
        //    //��������
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
        //    //��������
        //    else
        //    {
        //        if (_areabll.ModifyIsEnable(ID, false) > 0) { enableFlag = true; }
        //    }
        //    return enableFlag;
        //}

        ///// <summary>
        ///// �ر���Ա����
        ///// </summary>
        ///// <param name="areaCode"></param>
        //private void CloseSanYuan(string areaCode)
        //{
        //    _areabll.CloseSanYuan(areaCode);
        //}

        ///// <summary>
        ///// ɾ��
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
        //        logCache.AddServiceLogForDelete(power_AreaView, CurrentMenuID, "�ܵ���λά��ɾ��", "Power_Area");
        //    }
        //    catch (Exception ex)
        //    {
        //        logCache.AddServiceLogForDelete(power_AreaView, CurrentMenuID, "�ܵ���λά��ɾ��", "Power_Area", HttpContext.Request.Path, ex.Message, ex.StackTrace);
        //    }

        //    return false;
        //}
    }
}