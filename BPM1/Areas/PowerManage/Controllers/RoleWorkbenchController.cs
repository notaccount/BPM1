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

//        #region ҳ�����
//        public IActionResult Index()
//        {
//            logcache.AddServiceLogForView(CurrentMenuID, "��ɫ����̨����", "");
//            ViewData["U_AreaCode"] = ManageProvider.Current.U_AreaCode;
//            ViewData["WorkbenchType"] = _workbenchBll.GetListJson();
//            return View();
//        }
//        public IActionResult RoleWorkbenchEdit()
//        {

//            return View();
//        }

//        #endregion

//        #region  �б��ҳ
//        /// <summary>
//        /// �б��ҳ
//        /// </summary>
//        /// <param name="ParameterJson"></param>
//        /// <param name="jqgridparam"></param>
//        /// <returns></returns>
//        public string GetPagerJson(string ParameterJson, JqGridParam jqgridparam)
//        {
//            if (string.IsNullOrEmpty(jqgridparam.sortField))
//                jqgridparam.sortField = "U_SortNo";
//            string strJson = _rolebll.FindListJsonPage(ParameterJson, ref jqgridparam);
//            logcache.AddServiceLogForQuery(CurrentMenuID, "��ɫ����̨���ò�ѯ", "", ParameterJson);
//            return JsonManager.ToPageJson(jqgridparam, strJson);





//        }
//        #endregion

//        #region ��ȡ����̨�б�
//        /// <summary>
//        /// ��ȡ����̨�б�
//        /// </summary>
//        /// <returns></returns>
//        public string GetWorkBenchJson()
//        {
//            string sJson = _workbenchBll.GetListJson();
//            return sJson;
//        }
//        #endregion

//        #region ����id��ȡʵ��
//        /// <summary>
//        /// ��ȡ����
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public string GetEntity(string id)
//        {
//            Power_RoleView model = _rolebll.GetViewModel(id);
//            return JsonConvert.SerializeObject(model);
//        }
//        #endregion

//        #region ���淽��
//        public void AddOrUpdate(Power_RoleView data)
//        {
//            Power_RoleView model = new Power_RoleView();
//            Power_RoleView modelNew = new Power_RoleView();
//            if (data.ID == Guid.Empty)
//            {
//                try
//                {
//                    model = _rolebll.Insert(data);
//                    logcache.AddServiceLogForAdd(model, CurrentMenuID, "��ɫ����̨���ñ���", " Power_Role");
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForAdd(model, CurrentMenuID, "��ɫ����̨���ñ���", " Power_Role", HttpContext.Request.Path, ex.Message, ex.StackTrace);
//                }

//            }
//            else
//            {
//                try
//                {
//                    model = _rolebll.GetViewModel(data.ID.ToString());
//                    #region ȫ���ֶθ�ֵ
//                    model.WorkbenchID = data.WorkbenchID;
//                    #endregion
//                    modelNew = _rolebll.Update(model);
//                    logcache.AddServiceLogForUpdate(modelNew, model, CurrentMenuID, "��ɫ����̨���ø���", " Power_Role");
//                }
//                catch (Exception ex)
//                {
//                    logcache.AddServiceLogForUpdate(modelNew, model, CurrentMenuID, "��ɫ����̨���ø���", " Power_Role", HttpContext.Request.Path, ex.Message, ex.StackTrace);
//                }

//            }


//        }
//        #endregion
//    }
//}