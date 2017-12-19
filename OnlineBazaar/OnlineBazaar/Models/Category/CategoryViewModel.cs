using OnlineBazaar.DALC.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBazaar.Resources;
using System.ComponentModel;

namespace OnlineBazaar.Models
{
    public class CategoryViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(OnlineBazaarResources),
           ErrorMessageResourceName = "NameRequiredMessage")]
        [Display(Name = "CategoryNameField", ResourceType = typeof(OnlineBazaarResources))]
        [Remote("CheckCategoryNameExists","Category",AdditionalFields ="ID")]
        public string Name { get; set; }

        [Display(Name = "CategoryDisplayOrderField", ResourceType = typeof(OnlineBazaarResources))]
        [Required(ErrorMessageResourceType = typeof(OnlineBazaarResources),
            ErrorMessageResourceName = "DisplayOrderRequiredMessage")]
        [Range(1, int.MaxValue, ErrorMessageResourceType =typeof(OnlineBazaarResources),
            ErrorMessageResourceName = "DisplayOrderRangeErrorMessage")]
        public int? DisplayOrder { get; set; }

        public string Path { get; set; }

        [Display(Name = "CategoryDescriptionField",ResourceType =typeof(OnlineBazaarResources))]
        public string Description { get; set; }

        [Display(Name = "CategoryParentField", ResourceType = typeof(OnlineBazaarResources))]
        public int? ParentID { get; set; }

        public IEnumerable<SelectListItem> ParentCategories { get; set; }
    }
}