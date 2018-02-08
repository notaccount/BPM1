
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
	/// <summary>
	/// 权限——组织机构(用户)-Power_UserOrganization(Power_UserOrganization)
	/// </summary>
    [Serializable] 
	public partial class Power_UserOrganization : BaseEntity
	{  

		#region 基本属性



		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 上级ID
		/// </summary>
		public Guid? ParentID { get; set; }

		/// <summary>
		/// 是否启用
		/// </summary>
		public bool? IsEnable { get; set; }

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
