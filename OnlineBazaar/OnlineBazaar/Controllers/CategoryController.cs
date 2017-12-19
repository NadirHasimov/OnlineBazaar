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

        public ActionResult Edit(int id = 0)
        {
            CategoryViewModel model = new CategoryViewModel();
            model.ParentCategories = CategoryDALC.GetParents();
            if (id == 0)
            {
                return View(model);
            }
            else
            {
                model = MapToCategoryViewModel(CategoryDALC.GetByID(id));
                model.ParentCategories = CategoryDALC.GetParents();
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel viewModel)
        {
            bool success = false;
            string message;
            if (!ModelState.IsValid)
            {
                return View();
            }
            CategoryModel model = MapToCategoryModel(viewModel);
            if (model.ID == 0)
            {
                if (CategoryDALC.Create(model))
                {
                    success = true;
                    message = OnlineBazaarResources.CreateCategorySuccessMessage;
                }
                else
                {
                    message = OnlineBazaarResources.CreateCategoryFailureMessage;
                }
            }
            else
            {
                if (CategoryDALC.Update(model))
                {
                    success = true;
                    message = OnlineBazaarResources.UpdateCategorySuccessMessage;
                }
                else
                {
                    message = OnlineBazaarResources.UpdateCategoryFailureMessage;
                }
            }
            TempData["success"] = success;
            TempData["message"] = message;
            return RedirectToAction("/Index");
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

        public ActionResult CheckCategoryNameExists(string name,int ID)
        {
            if (CategoryDALC.CheckNameExists(name,ID))
            {
                return Json("Category name already exists!", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
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
        private CategoryModel MapToCategoryModel(CategoryViewModel category)
        {
            return new CategoryModel()
            {
                ID = category.ID,
                Name = category.Name,
                ParentID = category.ParentID,
                DisplayOrder = category.DisplayOrder,
                Description = category.Description
            };
        }
    }
}