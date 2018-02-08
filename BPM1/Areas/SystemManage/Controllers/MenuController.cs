using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using BPM.Repository;
using BPM1.Areas.SystemManage.Models;
using BPM.Models;
using Microsoft.EntityFrameworkCore;
using BPM.Repository.PowerManage;

namespace BPM1.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class MenuController : Controller
    {
        PowerMenuRepository _menuRepository;
        DataContext _db;
        public MenuController(DataContext db, PowerMenuRepository menuRepository)
        {
            _db = db;
            _menuRepository = menuRepository;
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
        /// ��������
        /// </summary>
        /// <param name="ParameterJson"></param>
        /// <param name="jqgridparam"></param>
        /// <returns></returns>
        public string GetMenusJson(string ParameterJson, JqGridParam jqgridparam)
        {
            var menuList = _menuRepository.List();
            //var menuList = _db.Power_Menu.ToList();
            //string a = _menubll.FindListJsonPage(ParameterJson, ref jqgridparam);
            //List<Power_MenuView> list = JsonConvert.DeserializeObject<List<Power_MenuView>>(a);
            //var menuList = list.Where(p => p.IsEnable != false).ToList();
            string menuJson = JsonConvert.SerializeObject(menuList);

            //jqgridparam.pageIndex = 1;
            //jqgridparam.pageSize = 20;
            //jqgridparam.records = _db.Power_Menu.Count();
            return menuJson;
        }



        /// <summary>
        /// �����˵�
        /// </summary>
        /// <returns></returns>
        public IActionResult AddView()
        {
            return View();
        }

        ///// <summary>
        ///// �˵�ѡ��ͼ��ҳ��
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult CheckIcon()
        //{
        //    return View();
        //}

        ////TODO���˳� ��ȡ�˵�Json�ķ�������Ҫ����  GetPowerMenuJson��GetMenuJons��GetMenusJson��GetLeftMenuJsons��GetMiniTreeMenuJson

        ///// <summary>
        ///// ����RoleId��ȡȨ��
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <returns></returns>
        //public string GetPowerMenuJson(string roleId)
        //{
        //    List<MiniTreeNode2> tree = new List<MiniTreeNode2>();

        //    List<ParameterJson> list1 = new List<ParameterJson>();
        //    list1.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
        //    list1.Add(new ParameterJson("RoleId", ConditionOperate.Equal.ToString(), roleId));
        //    string json = _rolemenubll.GetListJson(JsonConvert.SerializeObject(list1));
        //    if (!string.IsNullOrEmpty(json))
        //    {
        //        List<Power_RoleMenuView> roleMenuList = JsonConvert.DeserializeObject<List<Power_RoleMenuView>>(json);

        //        #region �������
        //        List<ParameterJson> list = new List<ParameterJson>();
        //        list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
        //        list.Add(new ParameterJson("IsEnable", ConditionOperate.Equal.ToString(), "1"));
        //        list.Add(new ParameterJson("IsSuperUser", ConditionOperate.Equal.ToString(), "0"));
        //        string sCondition = JsonConvert.SerializeObject(list);
        //        #endregion

        //        string strSort = new JqSortParam().ToJson();

        //        string sJson = _menubll.GetListJson(sCondition, strSort);
        //        if (!string.IsNullOrEmpty(sJson))
        //        {
        //            List<Power_MenuView> roleMenuList1 = JsonConvert.DeserializeObject<List<Power_MenuView>>(sJson);
        //            foreach (var item in roleMenuList1)
        //            {
        //                MiniTreeNode2 node = new MiniTreeNode2();
        //                node.id = item.ID.ToString();
        //                node.pid = item.ParentID.ToString();
        //                node.text = item.Name;
        //                node.@checked = roleMenuList.Where(x => x.MenuID == item.ID).Count() > 0 ? true : false;
        //                tree.Add(node);
        //            }
        //        }
        //    }
        //    return JsonConvert.SerializeObject(tree);
        //}

        ///// <summary>
        ///// ��������
        ///// </summary>
        ///// <param name="ParameterJson"></param>
        ///// <param name="jqgridparam"></param>
        ///// <returns></returns>
        //public string GetMenuJons(string ParameterJson, JqGridParam jqgridparam)
        //{
        //    string a = _menubll.FindListJsonPage(ParameterJson, ref jqgridparam);
        //    List<Power_MenuView> list = JsonConvert.DeserializeObject<List<Power_MenuView>>(a);
        //    var menuList = list.Where(p => p.IsEnable != false).ToList();
        //    string menuJson = menuList.ToJson();
        //    return JsonManager.ToPageJson(jqgridparam, menuJson);
        //}

        ///// <summary>
        ///// ��ȡ���еĲ˵�����
        ///// </summary>
        ///// <returns></returns>
        //public string GetMenusJson()
        //{
        //    #region �������
        //    List<ParameterJson> list = new List<ParameterJson>();
        //    list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
        //    string sCondition = JsonConvert.SerializeObject(list);
        //    #endregion

        //    #region ����
        //    //����һ��Ĭ�������ֶ�U_SortNo������ʽasc
        //    string strSort = new JqSortParam().ToJson();
        //    #endregion

        //    string sJson = _menubll.GetListJson(sCondition, strSort);
        //    return sJson;
        //}

        /// <summary>
        /// ��ȡ�˵�json�������ֶ�
        /// </summary>
        /// <returns></returns>
        public string GetLeftMenuJsons()
        {
            IEnumerable<Power_Menu> list = _menuRepository.List(p => p.IsEnable == true);
            //List<Power_Menu> list = _db.Power_Menu.Where(p => p.IsEnable == true).ToList();
            //List<Power_MenuView> list = menuCache.GetMenuList();
            //var menuList = list.Where(p => p.IsEnable == true).OrderBy(d => d.U_SortNo).ToList();
            return JsonConvert.SerializeObject(list);
        }

        ///// <summary>
        ///// ��ȡ�˵�Json MiniTree
        ///// </summary>
        ///// <returns></returns>
        //public string GetMiniTreeMenuJson()
        //{
        //    List<Power_MenuView> list = menuCache.GetMenuList();

        //    list = list.Where(d => d.IsEnable == true).OrderBy(d => d.U_SortNo).ToList();

        //    var a = list.Select(x => new MiniTreeNode2()
        //    {
        //        id = x.ID.ToString(),
        //        pid = x.ParentID.ToString(),
        //        text = x.Name,
        //        url = x.Path,
        //        sort = x.U_SortNo == null ? 0 : (int)x.U_SortNo
        //    }).ToList();
        //    return JsonConvert.SerializeObject(a.OrderBy(x => x.sort));
        //}

        ///// <summary>
        ///// ����userId ��ȡ�û�Ȩ��
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public string GetMenuJsonsByUserId(string id)
        //{
        //    MessageUser mu = ManageProvider.Current;
        //    string roleIds = mu.RoleIds;
        //    List<MiniTreeNode2> list = menuCache.GetMenuForLeftMenu(roleIds, mu.U_AreaCode);
        //    list = list.OrderBy(x => x.sort).ToList();
        //    return JsonConvert.SerializeObject(list);
        //}

        /// <summary>
        /// ������༭
        /// </summary>
        /// <param name="model"></param>
        public void InsertOrUpdate(Power_Menu model)
        {
            Guid Id = model.ID;
            if (Id == Guid.Empty)
            {
                model.ID = Guid.NewGuid();
                model.IsEnable = true;
                model.IsShow = true;
                model.Style = "add";

                try
                {
                    //_db.Power_Menu.Add(model);
                    //_db.Set<Power_Menu>().Add(model);
                    //_db.SaveChanges();
                    _menuRepository.Add(model);
                }
                catch (Exception ex)
                {
                    //logcache.AddServiceLogForAdd<Power_MenuView>(model, CurrentMenuID, "�����˵��쳣", "Power_Menu", Request.Path, ex.Message, ex.StackTrace);
                }

            }
            else
            {
                Power_Menu model1 = _menuRepository.GetById(model.ID);
                //Power_Menu model1 = _db.Power_Menu.Find(model.ID);
                //Power_MenuView model1 = _menubll.GetViewModel(model.ID.ToString());
                //Power_MenuView oldmodel = ObjectClone.ToClone<Power_MenuView>(model1);
                try
                {
                    model1.Name = model.Name;
                    model1.Path = model.Path;
                    model1.Style = model.Style;
                    model1.ParentID = model.ParentID;
                    model1.Type = model.Type;
                    model1.Code = model.Code;
                    model1.IsEnable = model.IsEnable;
                    model1.IsSuperUser = model.IsSuperUser;
                    model1.IsShow = model.IsShow;


                    //_db.Entry(model1).State = EntityState.Modified;
                    //_db.SaveChanges();

                    _menuRepository.Edit(model1);
                    //���»���
                    //menuCache.AddOrUpdate(model1);
                    //ѭ�������ӽڵ�
                    //if (model.IsEnable != oldmodel.IsEnable)
                    //{
                    //    Fun(model1);
                    //}
                    //��¼��־
                    //logcache.AddServiceLogForUpdate(model1, oldmodel, CurrentMenuID, "���²˵�", "Power_Menu");
                }
                catch (Exception ex)
                {
                    //logcache.AddServiceLogForUpdate(model1, oldmodel, CurrentMenuID, "���²˵��쳣", "Power_Menu", Request.Path, ex.Message, ex.StackTrace);
                }
            }
            return;
        }

        ///// <summary>
        /////  ������༭ �ݹ鷽����
        ///// </summary>
        ///// <param name="model"></param>
        //public void Fun(Power_MenuView model)
        //{
        //    List<ParameterJson> list = new List<ParameterJson>();
        //    list.Add(new ParameterJson("ParentID", ConditionOperate.Equal.ToString(), model.ID.ToString()));
        //    string sCondition = JsonConvert.SerializeObject(list);
        //    string strSort = new JqSortParam().ToJson();
        //    string childList = _menubll.GetListJson(sCondition, strSort);
        //    List<Power_MenuView> oList = JsonConvert.DeserializeObject<List<Power_MenuView>>(childList);
        //    foreach (var item in oList)
        //    {
        //        item.IsEnable = model.IsEnable;
        //        //�������ݿ�IsEnable=false
        //        Power_MenuView o = _menubll.Update(item);
        //        //�������   
        //        menuCache.AddOrUpdate(item);
        //        Fun(o);

        //    }
        //}

        /// <summary>
        /// ����ID��ȡViewModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetModel(string id)
        {
            Power_Menu entity = _menuRepository.GetById(Guid.Parse(id));
            //Power_Menu entity = _db.Power_Menu.Find(Guid.Parse(id));
            //Power_MenuView entity = _menubll.GetViewModel(id);
            return JsonConvert.SerializeObject(entity);
        }

        /// <summary>
        /// ����idɾ��һ������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Del(Guid id)
        {
            if (id != Guid.Empty)
            {
                //ִ��ɾ��ǰ�Ȼ�ȡ��Ҫɾ�����ݵ�model
                //Power_MenuView model = _menubll.GetViewModel(id.ToString());
                Power_Menu model = _db.Power_Menu.Find(id);
                try
                {
                    _db.Set<Power_Menu>().Remove(model);
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// ��֤�����ֵ�Ƿ�Ψһ
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="filed">�ֶ���</param>
        /// <param name="val">�ֶ�ֵ</param>
        /// <returns></returns>
        public bool FindOnly(string code)
        {
            int num = _db.Power_Menu.Where(x => x.Code == code).Count();
            if (num > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// �༭ʱ��֤
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool FindOnlyEdit(string Code, string ID)
        {
            #region �������
            //List<ParameterJson> list = new List<ParameterJson>();

            //list.Add(new ParameterJson("Code", ConditionOperate.Equal.ToString(), Code));
            //list.Add(new ParameterJson("ID", ConditionOperate.NotEqual.ToString(), ID));
            //list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //string sCondition = JsonConvert.SerializeObject(list);
            #endregion
            int num = _db.Power_Menu.Where(x => x.Code == Code && x.ID != Guid.Parse(ID)).Count();
            return num == 0;
        }

        ///// <summary>
        ///// ����󣬸��»���
        ///// </summary>
        ///// <param name="sorts"></param>
        ///// <returns></returns>
        //public bool UpdateCache()
        //{
        //    return true;
        //}



        //#region ��ҳ��������༭\�б��ҳ

        ///// <summary>
        ///// ��ҳ��
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult MenuChildsList()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// ��ӻ�༭
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public void MenusChildsAddOrUpdate(string data)
        //{

        //    List<Power_MenuChildsView> list = JsonConvert.DeserializeObject<List<Power_MenuChildsView>>(data);
        //    Power_MenuChildsView childsView = new Power_MenuChildsView();
        //    if (list.Count > 0)
        //    {
        //        foreach (var model in list)
        //        {
        //            if (model.ID == Guid.Empty)
        //            {
        //                try
        //                {
        //                    model.ID = Guid.NewGuid();
        //                    childsView = _menuChildsbll.Insert(model);
        //                    logcache.AddServiceLogForAdd(childsView, CurrentMenuID, "�����˵�", "Power_MenuChilds");
        //                    menuCache.AddOrUpdateChild(childsView);

        //                }
        //                catch (Exception ex)
        //                {
        //                    logcache.AddServiceLogForAdd(childsView, CurrentMenuID, "�����˵��쳣", "Power_MenuChilds", Request.Path, ex.Message, ex.StackTrace);
        //                }


        //            }
        //            else
        //            {
        //                Power_MenuChildsView modelOld = _menuChildsbll.GetViewModel(model.ID.ToString());
        //                Power_MenuChildsView modelNew = null;
        //                try
        //                {

        //                    modelOld.MenuID = model.MenuID;
        //                    modelOld.Name = model.Name;
        //                    modelOld.Code = model.Code;
        //                    modelOld.Path = model.Path;
        //                    modelNew = _menuChildsbll.Update(modelOld);
        //                    logcache.AddServiceLogForUpdate(modelNew, modelOld, CurrentMenuID, "���²˵�", "Power_MenuChilds");
        //                    menuCache.AddOrUpdateChild(modelNew);
        //                }
        //                catch (Exception ex)
        //                {
        //                    logcache.AddServiceLogForUpdate(modelNew, modelOld, CurrentMenuID, "���²˵��쳣", "Power_MenuChilds", Request.Path, ex.Message, ex.StackTrace);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        logcache.AddServiceLogForAdd(childsView, CurrentMenuID, "û�и������ݻ���û��������Ϣ", " ");
        //    }
        //    return;
        //}

        ///// <summary>
        ///// ��ҳ��ɾ��
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public bool MenuChildsDelete(string id)
        //{
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        Power_MenuChildsView view = _menuChildsbll.GetViewModel(id);
        //        _menuChildsbll.Delete(Guid.Parse(id));
        //        //���»���
        //        menuCache.DeleteChild(view.ID.ToString(), view.MenuID.ToString());
        //        return true;
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// ��ҳ���б��ҳ
        ///// </summary>
        ///// <param name="ParameterJson"></param>
        ///// <param name="jqgridparam"></param>
        ///// <returns></returns>
        //public string GetMenusChildsPagerJson(string ParameterJson, JqGridParam jqgridparam)
        //{
        //    if (string.IsNullOrEmpty(jqgridparam.sortField))
        //        jqgridparam.sortField = "U_SortNo";
        //    string strJson = _menuChildsbll.FindListJsonPage(ParameterJson, ref jqgridparam);
        //    return JsonManager.ToPageJson(jqgridparam, strJson);
        //}
        //#endregion





    }
}