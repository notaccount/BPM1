using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPM1
{
    public class JqSortParam
    {

        public JqSortParam()
        {

        }
        public JqSortParam(string _sortField, string _sortOrder)
        {
            this.sortField = _sortField;
            this.sortOrder = _sortOrder;
        }

        public string sortField
        {
            get;
            set;
        }

        public string sortOrder
        {
            get;
            set;
        }

        public string ToJson()
        {
            if (string.IsNullOrEmpty(this.sortField))
            {
                this.sortField = "U_SortNo";
            }
            if (string.IsNullOrEmpty(this.sortOrder))
            {
                this.sortOrder = "asc";
            }

            return JsonConvert.SerializeObject(this);
        }

    }
    public class JqGridParam
    {

        public int pageSize
        {
            get;
            set;
        }

        public int pageIndex
        {
            get;
            set;
        }

        public string sortField
        {
            get;
            set;
        }

        public string sortOrder
        {
            get;
            set;
        }

        public int records
        {
            get;
            set;
        }

        public int total
        {
            get
            {
                int result;
                if (this.records > 0)
                {
                    result = ((this.records % this.pageSize == 0) ? (this.records / this.pageSize) : (this.records / this.pageSize + 1));
                }
                else
                {
                    result = 1;
                }
                return result;
            }
        }
    }


    public class JsonManager
    {
        public static string ToPageJson(JqGridParam jqgridparam, string ll)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("'pageIndex':{0},", jqgridparam.pageIndex);
            sb.AppendFormat("'pageSize':{0},", jqgridparam.pageSize);
            sb.AppendFormat("'total':{0},", jqgridparam.records);
            sb.AppendFormat("'data':{0}", ll);
            sb.Append("}");
            return sb.ToString();
        }

        public static string ToPagerTreeJson(JqGridParam jqgridparam, string ll, string ids)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            //sb.AppendFormat("'pageIndex':{0},", jqgridparam.pageIndex);
            //sb.AppendFormat("'pageSize':{0},", jqgridparam.pageSize);
            sb.AppendFormat("'total':{0},", jqgridparam.records);
            sb.AppendFormat("'data':{0},", ll);
            sb.AppendFormat("'allIds':{0}", ids);
            sb.Append("}");
            return sb.ToString();
        }
    }


    public class MiniTreeNode2
    {
        public string id { get; set; }

        public string pid { get; set; }

        public string text { get; set; }

        public string style { get; set; }

        public string url { get; set; }
        public bool isshow { get; set; }

        public bool @checked { get; set; }

        public string img { get; set; }

        public bool expanded { get; set; }

        public int sort { get; set; }
    }

}