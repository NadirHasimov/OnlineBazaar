using OnlineBazaar.DALC.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBazaar.Models
{
    public class CategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }

        public IEnumerable<SelectListItem> ParentCategories { get; set; }
    }
}