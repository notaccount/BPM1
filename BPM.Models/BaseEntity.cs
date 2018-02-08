using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace BPM.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid ID { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public int U_SortNo { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string U_Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? U_CreateDate { get; set; }

        /// <summary>
        /// 创建人所属机构
        /// </summary>
        public Guid? U_CreatorOrgID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string U_LastModifieder { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? U_LastModifiedDate { get; set; }

        /// <summary>
        /// 最后修改人所属机构
        /// </summary>
        public Guid? U_LastModifiederOrgID { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [DefaultValue(true)]
        public bool? U_IsValid { get; set; }

        /// <summary>
        /// 是否系统内置
        /// </summary>
        [DefaultValue(1)]
        public bool? U_IsSystem { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public string U_AreaCode { get; set; }

    }
}
