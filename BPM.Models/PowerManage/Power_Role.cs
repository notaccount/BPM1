
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
	/// <summary>
	/// 权限——角色管理-Power_Role(Power_Role)
	/// </summary>
    [Serializable] 
	public partial class Power_Role : BaseEntity
	{ 
		#region 基本属性

		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 编码(内置，不显示)
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// 是否三元
		/// </summary>
		public bool? IsTrilateral { get; set; }

		/// <summary>
		/// 工作台ID
		/// </summary>
		public Guid? WorkbenchID { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public string Remarks { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		public Guid? Status { get; set; }

		/// <summary>
		/// 是否超级
		/// </summary>
		public bool? IsSuperUser { get; set; }

		#endregion
	}
}
