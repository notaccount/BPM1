using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BPM.Repository;
using Newtonsoft.Json;
using System.Text;

namespace BPM1.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _db;
        public HomeController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            string areaTitle = string.Empty;
            //Power_AreaView o = _AreaBLL.GetAreaViewByCode(ManageProvider.Current.U_AreaCode);
            //if (o != null)
            //{
            //    areaTitle = _AreaBLL.GetAreaViewByCode(ManageProvider.Current.U_AreaCode).Title.ToString();
            //}
            //else
            //{
            //    areaTitle = "信息中心管理";
            //}
            //ViewData["areaName"] = areaTitle;
            //MessageUser mu = ManageProvider.Current;


            #region 新方法，直接取用户有权限的全部菜单，可以通过配置修改菜单名称和顺序
            //string sList = _userMenuBll.GetUserLeftMenu(mu.ID.ToString());
            var list1 = _db.Power_Menu.ToList();
            string sList = JsonConvert.SerializeObject(list1);

            List<MiniTreeNode2> list = list1.Select(x => new MiniTreeNode2
            {
                id = x.ID.ToString(),
                pid = x.ParentID.ToString(),
                text = x.Name,
                url = x.Path

            }).ToList<MiniTreeNode2>();
            #endregion

            var a = list.Where(x => x.pid == "").ToList();
            StringBuilder sb1 = new StringBuilder();
            foreach (var item in a)
            {
                StringBuilder sb01 = new StringBuilder();
                //第一层
                sb01.Append("<li class=\"menu-item-heade\" >");
                sb01.Append("    <div class=\"head-title zhezhaotitle\">");
                sb01.Append("        <span class=\"line-bar\"></span>");
                sb01.AppendFormat("        <span class=\"iconfont icon icon-left {0}\"></span>", item.style);
                sb01.AppendFormat("  <span class=\"big-title\" >{0}</span>", item.text);
                sb01.Append("<span class=\"iconfont icon icon-slide icon-jiantou-copy head-title-span\"></span>");
                //sb01.Append("        <span class=\"iconfont icon icon-slide\" ></span>");
                sb01.Append("    </div>");


                StringBuilder sb2 = new StringBuilder();
                var b = list.Where(x => x.pid == item.id).ToList();
                sb2.Append("<ul class=\"list-slide\" >");
                foreach (var mm in b)
                {
                    //第二层

                    sb2.Append("    <li class=\"li-item\">");
                    sb2.Append("	<span class=\"line-bar head-title-span head-title-span-current\"></span>");
                    //sb2.AppendFormat("   <span class=\"item-title item-btn-one\" href=\"{1}\">{0}<span class=\"iconfont icon icon-slide ul-second-icon\"></span></span>", mm.text, mm.url);
                    if ((list.Where(x => x.pid == mm.id).ToList().Count) > 0)
                    {
                        //sb2.AppendFormat("   <span class=\"item-title iconfont icon {2}\" href=\"{1}\">&nbsp;&nbsp;{0}<span class=\"iconfont icon icon-slide ul-second-icon\"></span></span>", mm.text, mm.url, mm.style);

                        sb2.AppendFormat("<span class=\"item-title\" href=\"{0}\">", mm.url);
                        sb2.AppendFormat("	<span class=\"icon-small icon {0}\"></span>", mm.style);
                        sb2.AppendFormat("	<span class=\"sec-title\" >{0}</span>", mm.text);
                        sb2.Append("	<span class=\"iconfont icon icon-slide ul-second-icon icon-icon07\"></span>");
                        sb2.Append("</span>");
                    }
                    else
                    {
                        //sb2.AppendFormat("   <span class=\"item-title item-one iconfont icon {2}\" href=\"{1}\">&nbsp;&nbsp;{0}<span class=\"iconfont icon icon-slide ul-second-icon\"></span></span>", mm.text, mm.url, mm.style);

                        sb2.AppendFormat("<span class=\"item-title item-one\" href=\"{0}\">", mm.url);
                        sb2.AppendFormat("	<span class=\"icon-small icon {0}\"></span>", mm.style);
                        sb2.AppendFormat("	<span class=\"sec-title\">{0}</span>", mm.text);
                        sb2.Append("	<span class=\"iconfont icon icon-slide ul-second-icon icon-icon07\"></span>");
                        sb2.Append("</span>");
                    }


                    #region 第三层
                    StringBuilder sb3 = new StringBuilder();
                    var c = list.Where(x => x.pid == mm.id).ToList();
                    if (c.Count > 0)
                    {
                        sb3.Append("<ul class=\"second-ul\">");
                        foreach (var nn in c)
                        {
                            //第三层
                            //sb3.AppendFormat("<li class=\"indextitle \" href=\"{0}\" >{1}</li>", nn.url, nn.text);
                            sb3.AppendFormat("<li class=\"indextitle \" href=\"/Home/TopMenus/?parentid={0}&url={1}\" >{2}</li>", mm.id, nn.url, nn.text);
                        }
                        sb3.Append("</ul>");
                    }
                    sb2.Append(sb3);
                    #endregion

                    sb2.Append("    </li>");

                }
                sb2.Append("</ul>");
                sb01.Append(sb2);
                sb01.Append("</li>");

                sb1.Append(sb01);
            }

            //ViewBag.UserId = mu.ID.ToString();
            //ViewBag.AreaCode = mu.U_AreaCode;
            //ViewBag.UserJson = JsonConvert.SerializeObject(mu);
            ViewBag.menulist = sb1.ToString();
            //Power_UserView _UserView = _UserBLL.GetViewModel(ManageProvider.Current.ID.ToString());
            //if (_UserView.A0191A != null)
            //{
            //    Sys_AttachmentView _AttachmentView = _AttachmentBLL.GetViewModel(_UserView.A0191A.ToString());
            //    ViewData["A019A"] = _AttachmentView.ID;
            //    ViewData["ImageFile"] = _AttachmentView.ImageFile;
            //}
            //else
            //{
            //    ViewData["ImageFile"] = "/page/img/user.png";
            //}
            return View();
        }
    }
}