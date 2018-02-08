
using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
    /// <summary>
    /// 权限——岗位管理-Power_Stations(Power_Stations)
    /// </summary>
    [Serializable]
    public partial class Power_Stations : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 是否超级
        /// </summary>
        public bool? IsSuperUser { get; set; }

        #endregion
    }
}
