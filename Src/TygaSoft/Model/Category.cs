using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotterySln.Model
{
    [Serializable]
    public class Category
    {
        public object NumberID { get; set; }
        public string CategoryName { get; set; }
        public object ParentID { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public string Title { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
