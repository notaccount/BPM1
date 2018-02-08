
using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
	/// <summary>
	/// 权限——用户角色关系-Power_UserRole(Power_UserRole)
	/// </summary>
    [Serializable] 
	public partial class Power_UserRole : BaseEntity
	{  
		#region 基本属性

		/// <summary>
		/// 角色ID
		/// </summary>
		public Guid? RoleID { get; set; }

		/// <summary>
		/// 用户ID
		/// </summary>
		public Guid? UserID { get; set; }

		/// <summary>
		/// 是否超级
		/// </summary>
		public bool? IsSuperUser { get; set; }

		#endregion
        
	}
}
