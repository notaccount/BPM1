using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPM1.Areas.SystemManage.Models
{
    public partial class Power_MenuView 
    {


        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public Guid? ParentID { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// 功能类型
        /// </summary>
        public Guid? Type { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool? IsShow { get; set; }

        /// <summary>
        /// 是否超级
        /// </summary>
        public bool? IsSuperUser { get; set; }



    }
}
