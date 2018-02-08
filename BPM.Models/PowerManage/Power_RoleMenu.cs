
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
	/// <summary>
	/// 权限——角色菜单关系-Power_RoleMenu(Power_RoleMenu)
	/// </summary>
    [Serializable] 
	public partial class Power_RoleMenu : BaseEntity
	{  
		#region 基本属性


		/// <summary>
		/// 菜单ID
		/// </summary>
		public Guid? MenuID { get; set; }

		/// <summary>
		/// 角色ID
		/// </summary>
		public Guid? RoleID { get; set; }

		/// <summary>
		/// 是否超级
		/// </summary>
		public bool? IsSuperUser { get; set; }

		#endregion
        
	}
}
