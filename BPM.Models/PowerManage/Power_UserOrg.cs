
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
	/// <summary>
	/// 权限——用户组织机构关系-Power_UserOrg(Power_UserOrg)
	/// </summary>
    [Serializable] 
	public partial class Power_UserOrg : BaseEntity
    { 
		#region 基本属性


		/// <summary>
		/// 用户
		/// </summary>
		public Guid? UserID { get; set; }

		/// <summary>
		/// 组织机构
		/// </summary>
		public Guid? OrgID { get; set; }

		/// <summary>
		/// 是否超级
		/// </summary>
		public bool? IsSuperUser { get; set; }

		#endregion

	}
}
