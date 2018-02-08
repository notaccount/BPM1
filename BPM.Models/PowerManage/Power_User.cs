
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
	/// <summary>
	/// 权限——用户管理-Power_User(Power_User)
	/// </summary>
    [Serializable] 
	public partial class Power_User : BaseEntity
	{  
		#region 基本属性


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
		/// 照片
		/// </summary>
		public Guid? A0191A { get; set; }

		/// <summary>
		/// 是否三元
		/// </summary>
		public bool? IsTrilateral { get; set; }

		/// <summary>
		/// 是否超级
		/// </summary>
		public bool? IsSuperUser { get; set; }

		#endregion
	}
}
