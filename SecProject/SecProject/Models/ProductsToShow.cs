﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SecProject.DAL;

namespace SecProject.Models
{
    public class ProductsToShow
    {
        public string Name { get; set; }
        public string BuyUrl { get; set; }
        public string ImgUrl { get; set; }
        public string SubCateg { get; set; }
        public string Categ { get; set; }
        public List<string> UserProductLinkTable { get; set; }
    }
}