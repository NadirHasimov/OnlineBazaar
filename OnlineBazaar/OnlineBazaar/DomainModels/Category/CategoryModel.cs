using OnlineBazaar.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OnlineBazaar.DomainModels
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }

        public CategoryModel() { }

        public CategoryModel(DataRow row)
        {
            ID = int.Parse(row["CATEGORY_ID"].ToString());
            Name = row["NAME"].ToString();
            DisplayOrder = int.Parse(row["DISPLAY_ORDER"].ToString());
            Description = row["DESCRIPTION"].ToString();
            ParentID = row["PARENT_ID"].ToString().ToNullable<int>();
            Path = row["CATEGORY_PATH"].ToString();
        }
    }
}