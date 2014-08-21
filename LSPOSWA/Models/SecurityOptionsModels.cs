using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSPOSWA.Models
{
    public class ItemsModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public string parent_id { get; set; }
        public string className { get; set; }
        public bool leaf { get; set; }
    }

    public class RootModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public string parent_id { get; set; }
        public string className { get; set; }
        public bool leaf { get; set; }
        public ItemsModel[] items { get; set; }
    }
}