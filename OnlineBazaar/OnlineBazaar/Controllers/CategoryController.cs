using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBazaar.DomainModels;
using OnlineBazaar.DALC.Category;
using OnlineBazaar.Models;
using OnlineBazaar.Resources;

namespace OnlineBazaar.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            var model = CategoryDALC.GetAll().Select(category => MapToCategoryViewModel(category)).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool result = false;
            String message;
            if (!CategoryDALC.CheckChildExists(id))
            {
                if (CategoryDALC.Delete(id))
                {
                    result = true;
                    message = OnlineBazaarResources.DeleteCategorySuccessMessage;
                }
                else
                {
                    message = OnlineBazaarResources.DeleteCategoryFailureMessage;
                }
            }
            else
            {
                message = OnlineBazaarResources.DeleteCategoryChildExistsMessage;
            }

            return Json(new { result, message });
        }

        public ActionResult CategoriesList()
        {
            var model = CategoryDALC.GetAll().Select(category => MapToCategoryViewModel(category)).ToList();
            return PartialView("_CategoriesList", model);
        }

        private CategoryViewModel MapToCategoryViewModel(CategoryModel category)
        {
            return new CategoryViewModel()
            {
                ID = category.ID,
                Name = category.Name,
                ParentID = category.ParentID,
                Path = category.Path,
                DisplayOrder = category.DisplayOrder,
                Description = category.Description
            };
        }
    }
}