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
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DisplayOrder { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        
        public CategoryModel() { }

        public CategoryModel(DataRow row)
        {
            Id = int.Parse(row["CATEGORY_ID"].ToString());
            Name = row["NAME"].ToString();
            DisplayOrder = int.Parse(row["DISPLAY_ORDER"].ToString());
            Description = row["DESCRIPTION"].ToString();
            ParentId = row["PARENT_ID"].ToString().ToNullable<int>();
            if (row.Table.Columns.Contains("CATEGORY_PATH"))
            {
                Path = row["CATEGORY_PATH"].ToString();
            }
        }
    }
}