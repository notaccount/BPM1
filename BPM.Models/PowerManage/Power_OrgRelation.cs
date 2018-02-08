
using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
	/// <summary>
	/// 权限——组织机构对应关系-Power_OrgRelation(Power_OrgRelation)
	/// </summary>
    [Serializable] 
	public partial class Power_OrgRelation : BaseEntity
	{  
		#region 基本属性


		/// <summary>
		/// 用户组织机构
		/// </summary>
		public Guid? UserOrgID { get; set; }

		/// <summary>
		/// 档案组织机构
		/// </summary>
		public Guid? B0000 { get; set; }

		#endregion
	}
}
