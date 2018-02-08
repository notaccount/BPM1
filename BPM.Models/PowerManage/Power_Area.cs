//-------------------------------------------------------------------
//模块名称：Hasng.CadreFile.Models
//功能说明：
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Models
{
	/// <summary>
	/// 权限——区域管理-Power_Area(Power_Area)
	/// </summary>
    [Serializable] 
	public partial class Power_Area : BaseEntity
	{  

		#region 基本属性

		/// <summary>
		/// 编码
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 级别
		/// </summary>
		public int? Level { get; set; }

		/// <summary>
		/// 上级ID
		/// </summary>
		public Guid? ParentID { get; set; }

		/// <summary>
		/// 是否开启三元
		/// </summary>
		public bool? IsOpenTrilateral { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		public bool? IsEnable { get; set; }

		#endregion

	}
}
