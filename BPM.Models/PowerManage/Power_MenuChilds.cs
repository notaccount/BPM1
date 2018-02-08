

using BPM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
	/// <summary>
	/// 权限——子菜单管理-Power_MenuChilds(Power_MenuChilds)
	/// </summary>
    [Serializable] 
	public partial class Power_MenuChilds : BaseEntity
	{  
		#region 基本属性


		/// <summary>
		/// 主菜单
		/// </summary>
		public Guid? MenuID { get; set; }

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

		#endregion
	}
}
