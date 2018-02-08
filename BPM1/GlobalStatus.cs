using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPM1
{
    public class GlobalStatus
    {

        private static Dictionary<string, string> dicStatus
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("HX00", "登陆成功");
                dic.Add("HX01", "数据库连接失败");
                dic.Add("HX02", "用户名、密码或所在区域输入错误。");
                dic.Add("HX03", "当前账号已登陆");
                dic.Add("HX04", "此用户未选择组织机构");
                dic.Add("HX05", "此用户未分配权限");
                dic.Add("HX06", "用户名、密码、管档单位不能为空");
                return dic;
            }
        }
        public static string GetStatus(string key)
        {
            var item = dicStatus.Where(x => x.Key == key).FirstOrDefault();
            return JsonConvert.SerializeObject(item);
        }
    }
}
