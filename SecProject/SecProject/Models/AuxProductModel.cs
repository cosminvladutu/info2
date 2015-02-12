using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

namespace SecProject.Models
{
    public class AuxProductModel
    {
        public ProductInstance ProductsInstance { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }

    }
}