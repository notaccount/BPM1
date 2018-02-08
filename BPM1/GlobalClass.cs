using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BPM1
{
    public class JqSortParam
    {

        public JqSortParam()
        {

        }
        public JqSortParam(string _sortField, string _sortOrder)
        {
            this.sortField = _sortField;
            this.sortOrder = _sortOrder;
        }

        public string sortField
        {
            get;
            set;
        }

        public string sortOrder
        {
            get;
            set;
        }

        public string ToJson()
        {
            if (string.IsNullOrEmpty(this.sortField))
            {
                this.sortField = "U_SortNo";
            }
            if (string.IsNullOrEmpty(this.sortOrder))
            {
                this.sortOrder = "asc";
            }

            return JsonConvert.SerializeObject(this);
        }

    }
    public class JqGridParam
    {

        public int pageSize
        {
            get;
            set;
        }

        public int pageIndex
        {
            get;
            set;
        }

        public string sortField
        {
            get;
            set;
        }

        public string sortOrder
        {
            get;
            set;
        }

        public int records
        {
            get;
            set;
        }

        public int total
        {
            get
            {
                int result;
                if (this.records > 0)
                {
                    result = ((this.records % this.pageSize == 0) ? (this.records / this.pageSize) : (this.records / this.pageSize + 1));
                }
                else
                {
                    result = 1;
                }
                return result;
            }
        }
    }


    public class JsonManager
    {
        public static string ToPageJson(JqGridParam jqgridparam, string ll)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("'pageIndex':{0},", jqgridparam.pageIndex);
            sb.AppendFormat("'pageSize':{0},", jqgridparam.pageSize);
            sb.AppendFormat("'total':{0},", jqgridparam.records);
            sb.AppendFormat("'data':{0}", ll);
            sb.Append("}");
            return sb.ToString();
        }

        public static string ToPagerTreeJson(JqGridParam jqgridparam, string ll, string ids)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            //sb.AppendFormat("'pageIndex':{0},", jqgridparam.pageIndex);
            //sb.AppendFormat("'pageSize':{0},", jqgridparam.pageSize);
            sb.AppendFormat("'total':{0},", jqgridparam.records);
            sb.AppendFormat("'data':{0},", ll);
            sb.AppendFormat("'allIds':{0}", ids);
            sb.Append("}");
            return sb.ToString();
        }
    }


    public class MiniTreeNode2
    {
        public string id { get; set; }

        public string pid { get; set; }

        public string text { get; set; }

        public string style { get; set; }

        public string url { get; set; }
        public bool isshow { get; set; }

        public bool @checked { get; set; }

        public string img { get; set; }

        public bool expanded { get; set; }

        public int sort { get; set; }
    }


    public static class MD5Helper
    {
        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string password)
        {
            string cl = password;
            //实例化一个md5对像
            MD5 md5 = MD5.Create();
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] strMd5 = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(strMd5);
        }


        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            //实例化一个md5对像
            MD5 md5 = MD5.Create();
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
    }




    public class MessageUser
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Cn { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public Guid? StationsID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Guid? Status { get; set; }

        /// <summary>
        /// 是否初始用户
        /// </summary>
        public bool? Isinitial { get; set; }

        /// <summary>
        /// 是否系统内置
        /// </summary>
        public bool? IsSystem { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime LogTime { get; set; }

        public Guid? OrgId { get; set; }


        public Guid? U_CreatorOrgID { get; set; }

        public string U_AreaCode { get; set; }

        public string RoleIds { get; set; }

    }

    public class MenuPower
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }

        public List<string> AllUrls { get; set; }
    }

    public class ManageProvider
    {
        public static string UserId
        {
            get
            {
                byte[] aa;
                HttpContext.Current.Session.TryGetValue("CurrentUserId", out aa);
                string userId =  System.Text.Encoding.Default.GetString(aa);
                //string userId = HttpContext.Current.Session.GetString("CurrentUserId");
                if (!string.IsNullOrEmpty(userId))
                    return userId.Replace("\"", "");
                return null;
            }
            set {
                byte[] aa = System.Text.Encoding.Default.GetBytes(value);
                HttpContext.Current.Session.Set("CurrentUserId", aa);
            }
        }

        public static string AreaCode
        {
            get
            {
                byte[] aa;
                HttpContext.Current.Session.TryGetValue("CurrentAreaCode", out aa);
                string AreaCode = System.Text.Encoding.Default.GetString(aa);
                //string AreaCode = HttpContext.Current.Session.GetString("CurrentAreaCode");
                if (!string.IsNullOrEmpty(AreaCode))
                    return AreaCode.Replace("\"", "");
                return null;
            }
        }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public static MessageUser Current
        {
            get
            {
                string Id = ManageProvider.UserId;
               
                if (string.IsNullOrEmpty(Id))
                {
                    return null;
                }
                byte[] aa;
                HttpContext.Current.Session.TryGetValue(Id, out aa);
                string jsonmodel = System.Text.Encoding.Default.GetString(aa);
                //string json = CacheHelper.HashGet<string>((int)RedisDB.Power, areaCode + ":RegisterList", Id);
                if (jsonmodel == null)
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<MessageUser>(jsonmodel);
            }
            set
            {
                string userid1 = ManageProvider.UserId;
                if (!string.IsNullOrEmpty(userid1))
                {
                    //string areaCode = ManageProvider.AreaCode;
                    //CacheHelper.HashSet<string>((int)RedisDB.Power, areaCode + ":RegisterList", ManageProvider.UserId, JsonConvert.SerializeObject(value));
                    string key = userid1;
                    string va = JsonConvert.SerializeObject(value);
                    byte[] aa = System.Text.Encoding.Default.GetBytes(va);
                    HttpContext.Current.Session.Set(key, aa);
                }
            }

        }


        //public static LoginUserMenuPower CurrentMenuList
        //{
        //    get
        //    {
        //        string id = ManageProvider.UserId;
        //        string areaCode = ManageProvider.AreaCode;
        //        string json = CacheHelper.HashGet<string>((int)RedisDB.Power, areaCode + ":RegisterMenuPowerList", id);
        //        return new LoginUserMenuPower() { UserId = id, menuList = JsonConvert.DeserializeObject<List<MenuPower>>(json) };
        //    }
        //    set
        //    {
        //        LoginUserMenuPower model = value;
        //        if (model != null)
        //        {
        //            if (!string.IsNullOrEmpty(model.UserId))
        //            {
        //                string areaCode = ManageProvider.AreaCode;
        //                CacheHelper.HashSet<string>((int)RedisDB.Power, areaCode + ":RegisterMenuPowerList", model.UserId, JsonConvert.SerializeObject(model.menuList));
        //            }
        //        }
        //    }
        //}


        //public static MenuPower GetMenuInfo(string url)
        //{
        //    var list = ManageProvider.CurrentMenuList;
        //    if (list != null)
        //    {
        //        MenuPower entity = list.menuList.Where(x => x.AllUrls.Contains(url)).FirstOrDefault();
        //        if (entity != null)
        //        {
        //            return entity;
        //        }
        //    }

        //    return null;
        //}
    }

    public class ValidateUser
    {
        public static bool IsValidate = true;

        public static string Message = "";

    }



    public class LoginUserMenuPower
    {
        public string UserId { get; set; }

        public List<MenuPower> menuList { get; set; }
    }



    public static class HttpContext
    {
        //static HttpContext()
        //{ }

        public static IServiceProvider ServiceProvider;

        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {

                object factory = ServiceProvider.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor));
                Microsoft.AspNetCore.Http.HttpContext context = ((Microsoft.AspNetCore.Http.HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }

    }


    public class CommonHelper
    {
        public static string GetGuid
        {
            get
            {
                return System.Guid.NewGuid().ToString().ToLower();
            }
        }

        public static string ToObjectString(object obj)
        {
            return (obj == null) ? string.Empty : obj.ToString();
        }

        public static int GetInt(object obj)
        {
            int result;
            if (obj != null)
            {
                int num;
                int.TryParse(obj.ToString(), out num);
                result = num;
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public static float GetFloat(object obj)
        {
            float result;
            float.TryParse(obj.ToString(), out result);
            return result;
        }

        public static int GetInt(object obj, int exceptionvalue)
        {
            int result;
            if (obj == null)
            {
                result = exceptionvalue;
            }
            else if (string.IsNullOrEmpty(obj.ToString()))
            {
                result = exceptionvalue;
            }
            else
            {
                int num = exceptionvalue;
                try
                {
                    num = System.Convert.ToInt32(obj);
                }
                catch
                {
                    num = exceptionvalue;
                }
                result = num;
            }
            return result;
        }

        public static byte Getbyte(object obj)
        {
            byte result;
            if (obj != null && obj.ToString() != "")
            {
                result = byte.Parse(obj.ToString());
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public static long GetLong(object obj)
        {
            long result;
            if (obj != null && obj.ToString() != "")
            {
                result = long.Parse(obj.ToString());
            }
            else
            {
                result = 0L;
            }
            return result;
        }

        public static long GetLong(object obj, long exceptionvalue)
        {
            long result;
            if (obj == null)
            {
                result = exceptionvalue;
            }
            else if (string.IsNullOrEmpty(obj.ToString()))
            {
                result = exceptionvalue;
            }
            else
            {
                long num = exceptionvalue;
                try
                {
                    num = System.Convert.ToInt64(obj);
                }
                catch
                {
                    num = exceptionvalue;
                }
                result = num;
            }
            return result;
        }

        public static decimal GetDecimal(object obj)
        {
            decimal result;
            if (obj != null && obj.ToString() != "")
            {
                result = decimal.Parse(obj.ToString());
            }
            else
            {
                result = 0m;
            }
            return result;
        }

        public static System.DateTime GetDateTime(object obj)
        {
            System.DateTime result;
            if (obj != null && obj.ToString() != "")
            {
                result = System.DateTime.Parse(obj.ToString());
            }
            else
            {
                result = System.DateTime.Now;
            }
            return result;
        }

        public static string CostTime(long t)
        {
            long num = t / 1440000L;
            long num2 = (t - num * 1440000L) / 60000L;
            long num3 = (t - num * 1440000L - num2 * 60000L) / 1000L;
            long num4 = t - num * 1440000L - num2 * 60000L - num3 * 1000L;
            string obj = string.Concat(new string[]
            {
                num.ToString(),
                ":",
                num2.ToString(),
                ":",
                num3.ToString(),
                ".",
                num4.ToString()
            });
            string text = CommonHelper.GetDateTime(obj).ToString("HH:mm:ss");
            if (text == "00:00:00")
            {
                text = "00:00:01";
            }
            return text;
        }

        public static System.DateTime? ToDateTime(object obj)
        {
            System.DateTime? result;
            if (obj != null && obj.ToString() != "")
            {
                result = new System.DateTime?(System.DateTime.Parse(obj.ToString()));
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static string GetFormatDateTime(object obj, string Format)
        {
            string result;
            if (obj != null && obj.ToString() != null && obj.ToString() != "")
            {
                result = System.DateTime.Parse(obj.ToString()).ToString(Format);
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static System.DateTime JsonToDateTime(string jsonDate)
        {
            string text = jsonDate.Substring(5, jsonDate.Length - 6) + "+0800";
            System.DateTimeKind dateTimeKind = System.DateTimeKind.Utc;
            int num = text.IndexOf('+', 1);
            if (num == -1)
            {
                num = text.IndexOf('-', 1);
            }
            if (num != -1)
            {
                dateTimeKind = System.DateTimeKind.Local;
                text = text.Substring(0, num);
            }
            long num2 = long.Parse(text, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
            long ticks = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc).Ticks;
            System.DateTime dateTime = new System.DateTime(num2 * 10000L + ticks, System.DateTimeKind.Utc);
            System.DateTime result;
            switch (dateTimeKind)
            {
                case System.DateTimeKind.Unspecified:
                    result = System.DateTime.SpecifyKind(dateTime.ToLocalTime(), System.DateTimeKind.Unspecified);
                    return result;
                case System.DateTimeKind.Local:
                    result = dateTime.ToLocalTime();
                    return result;
            }
            result = dateTime;
            return result;
        }

        public static bool GetBool(object obj)
        {
            bool result;
            if (obj != null)
            {
                bool flag;
                bool.TryParse(obj.ToString(), out flag);
                result = flag;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static byte[] GetByte(object obj)
        {
            byte[] result;
            if (obj.ToString() != null && obj.ToString() != "")
            {
                result = (byte[])obj;
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static string GetString(object obj)
        {
            string result;
            if (obj != null && obj != System.DBNull.Value)
            {
                result = obj.ToString();
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static bool IsDateTime(string strValue)
        {
            bool result;
            if (strValue == null || strValue == "")
            {
                result = false;
            }
            else
            {
                string pattern = "[1-2]{1}[0-9]{3}((-|[.]){1}(([0]?[1-9]{1})|(1[0-2]{1}))((-|[.]){1}((([0]?[1-9]{1})|([1-2]{1}[0-9]{1})|(3[0-1]{1})))( (([0-1]{1}[0-9]{1})|2[0-3]{1}):([0-5]{1}[0-9]{1}):([0-5]{1}[0-9]{1})(\\.[0-9]{3})?)?)?)?$";
                if (Regex.IsMatch(strValue, pattern))
                {
                    int num;
                    int num2;
                    int num3;
                    if (-1 != (num = strValue.IndexOf("-")))
                    {
                        num2 = strValue.IndexOf("-", num + 1);
                        num3 = strValue.IndexOf(":");
                    }
                    else
                    {
                        num = strValue.IndexOf(".");
                        num2 = strValue.IndexOf(".", num + 1);
                        num3 = strValue.IndexOf(":");
                    }
                    if (-1 == num2)
                    {
                        result = true;
                        return result;
                    }
                    if (-1 == num3)
                    {
                        num3 = strValue.Length + 3;
                    }
                    int num4 = System.Convert.ToInt32(strValue.Substring(0, num));
                    int num5 = System.Convert.ToInt32(strValue.Substring(num + 1, num2 - num - 1));
                    int num6 = System.Convert.ToInt32(strValue.Substring(num2 + 1, num3 - num2 - 4));
                    if ((num5 < 8 && 1 == num5 % 2) || (num5 > 8 && 0 == num5 % 2))
                    {
                        if (num6 < 32)
                        {
                            result = true;
                            return result;
                        }
                    }
                    else if (num5 != 2)
                    {
                        if (num6 < 31)
                        {
                            result = true;
                            return result;
                        }
                    }
                    else if (num4 % 400 == 0 || (num4 % 4 == 0 && 0 < num4 % 100))
                    {
                        if (num6 < 30)
                        {
                            result = true;
                            return result;
                        }
                    }
                    else if (num6 < 29)
                    {
                        result = true;
                        return result;
                    }
                }
                result = false;
            }
            return result;
        }

        public static bool IsEmpty(string obj)
        {
            return CommonHelper.ToObjectString(obj).Trim() == string.Empty;
        }

        public static bool IsDateTime(object obj)
        {
            bool result;
            try
            {
                System.DateTime dateTime = System.DateTime.Parse(CommonHelper.ToObjectString(obj));
                if (dateTime > System.DateTime.MinValue && System.DateTime.MaxValue > dateTime)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool IsInt(object obj)
        {
            bool result;
            try
            {
                int.Parse(CommonHelper.ToObjectString(obj));
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool IsLong(object obj)
        {
            bool result;
            try
            {
                long.Parse(CommonHelper.ToObjectString(obj));
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool IsFloat(object obj)
        {
            bool result;
            try
            {
                float.Parse(CommonHelper.ToObjectString(obj));
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool IsDouble(object obj)
        {
            bool result;
            try
            {
                double.Parse(CommonHelper.ToObjectString(obj));
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool IsDecimal(object obj)
        {
            bool result;
            try
            {
                decimal.Parse(CommonHelper.ToObjectString(obj));
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static string CreateNo()
        {
            System.Random random = new System.Random();
            string str = random.Next(1000, 10000).ToString();
            return System.DateTime.Now.ToString("yyyyMMddHHmmss") + str;
        }

        public static string RndNum(int codeNum)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(codeNum);
            System.Random random = new System.Random();
            for (int i = 1; i < codeNum + 1; i++)
            {
                int num = random.Next(9);
                stringBuilder.AppendFormat("{0}", num);
            }
            return stringBuilder.ToString();
        }

        //public static string WebPathTran(string path)
        //{
        //    string result;
        //    try
        //    {
        //        result = HttpContext.Current.Server.MapPath(path);
        //    }
        //    catch
        //    {
        //        result = path;
        //    }
        //    return result;
        //}

        public static Stopwatch TimerStart()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            return stopwatch;
        }

        public static string TimerEnd(Stopwatch watch)
        {
            watch.Stop();
            return ((double)watch.ElapsedMilliseconds).ToString();
        }

        public static Boolean CheckGuid(String isGuid)
        {
            Boolean isCheck = false;
            try
            {
                Guid g = Guid.Parse(isGuid);
                isCheck = true;
            }
            catch (Exception)
            {
                isCheck = false;
            }
            return isCheck;
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            Microsoft.AspNetCore.Http.HttpContext httpContext = HttpContext.Current;
            var ip = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = httpContext.Connection.RemoteIpAddress.ToString();
                if (ip == "::1")
                    ip = httpContext.Connection.LocalIpAddress.ToString();
            }
            return ip;
        }



    }

    public class CookieAuthenInfo
    {
        public static string WebCookieInstance = "MyInstance";
    }
}