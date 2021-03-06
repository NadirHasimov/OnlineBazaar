﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBazaar.DomainModels;
using OnlineBazaar.DALC.Category;
using OnlineBazaar.Models;
using OnlineBazaar.Resources;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using OnlineBazaar.DALC;
using ClosedXML.Excel;
using System.IO;

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
            List<SelectListItem> parents = new List<SelectListItem>();
            foreach (var parent in CategoryDALC.GetAll().Select(row => new { row.Id, row.Path }))
            {
                parents.Add(new SelectListItem
                {
                    Text = parent.Path,
                    Value = parent.Id.ToString()
                });
            }
            CategoryViewModel model = new CategoryViewModel();
            if (id == 0)
            {
                model.ParentCategories = parents;
                return View(model);
            }
            else
            {
                model = MapToCategoryViewModel(CategoryDALC.GetById(id));
                model.ParentCategories = parents;
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
            if (model.Id == 0)
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
            return RedirectToAction("Index");
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

        public ActionResult CheckCategoryNameExists(string name, int id)
        {
            if (CategoryDALC.CheckNameExists(name, id))
            {
                return Json("Category name already exists!", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
       
        public void ExportToExcell()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Categories";
            using (OracleConnection con = new OracleConnection(AppConfig.ConnectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.Category.GetAll;
                    OracleDataAdapter oda = new OracleDataAdapter(cmd);
                    oda.Fill(dt);
                }
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Categories Report.xlsx");

                using (MemoryStream memoryStream=new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.End();
                }
            }
        }
        private CategoryViewModel MapToCategoryViewModel(CategoryModel category)
        {
            return new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                Path = category.Path,
                DisplayOrder = category.DisplayOrder,
                Description = category.Description,
            };
        }
        private CategoryModel MapToCategoryModel(CategoryViewModel category)
        {
            return new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                DisplayOrder = category.DisplayOrder,
                Description = category.Description,
            };
        }
    }
}