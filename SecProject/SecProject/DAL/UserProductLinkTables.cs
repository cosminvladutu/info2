using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecProject.DAL
{
    public class UserProductLinkTables
    {
        [Key]
        public int Id{ get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
    }
}