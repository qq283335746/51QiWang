﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotterySln.Model
{
    public class MenuNav
    {
        public string Href { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Title { get; set; }
        public bool Selected { get; set; }
    }
}
