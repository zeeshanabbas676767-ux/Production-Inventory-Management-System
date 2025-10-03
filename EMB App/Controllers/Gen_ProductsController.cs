using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBaseLibrary;
using OfficeOpenXml;
using EMB_App.Models;
using System.Net;

namespace EMB_App.Controllers
{ 
    public class Gen_ProductsController : Controller
    { 
        EMB_ProductionEntities2 DB = new EMB_ProductionEntities2();

        public ActionResult Products()
        {
            var model = new Gen_Products();
            ViewBag.ProductList = DB.Gen_Products
               .Where(b => b.Deleted)
               .ToList();
            ViewBag.ProductList = DB.Gen_Products.ToList();
            ViewBag.Categories = new SelectList(DB.Gen_Categories.ToList(), "CatId", "Category_Name");
            ViewBag.Brands = new SelectList(DB.Gen_Brands.ToList(), "Id", "Brand_Name");

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProducts(Gen_Products product, string IsDeleteMode)
        {
            if (IsDeleteMode == "true")
            {
                var toDelete = DB.Gen_Products.Find(product.Id);
                if (toDelete != null)
                {
                    DB.Gen_Products.Remove(toDelete);
                }
            }
            else
            {
                var entity = DB.Gen_Products.Find(product.Id) ?? new Gen_Products();
                entity.CatId = product.CatId;
                entity.Prod_Name = product.Prod_Name;
                entity.Category_Name = product.Category_Name;
                entity.Brand_Name = product.Brand_Name;
                entity.Prod_Code = product.Prod_Code;
                entity.Prod_Description = product.Prod_Description;
                entity.Prod_Qty = product.Prod_Qty;
                entity.Prod_Length = product.Prod_Length;
                entity.Prod_Weight = product.Prod_Weight;
                //entity.Prod_DateIn = product.Prod_DateIn >= sqlMinDate ? product.Prod_DateIn : null;
                //entity.Prod_DateOut = product.Prod_DateOut >= sqlMinDate ? product.Prod_DateOut : null;
                entity.Price = product.Price;
                entity.Price_Method = product.Price_Method;
                entity.Tax = product.Tax;
                entity.BranchId = product.BranchId;
                entity.Deleted = product.Deleted;
                entity.Active = product.Active;
                entity.RecordTimeStamp = product.RecordTimeStamp;
                entity.CreatedBy = product.CreatedBy;
                entity.CreatedDate = product.CreatedDate;
                entity.UpdatedBy = product.UpdatedBy;
                entity.UpdatedDate = product.UpdatedDate;

                if (entity.Id == 0)
                {
                    DB.Gen_Products.Add(entity);
                }
                else
                {
                    DB.Entry(entity).State = EntityState.Modified;
                }
            }
            DB.SaveChanges();
            return RedirectToAction("Products");
        }

        [HttpGet]
        public ActionResult ImportProducts()
        {
            // ViewBag.PreviewList = TempData["PreviewList"] as List<Gen_Products>;
            var productEntities = TempData["PreviewList"] as List<Gen_Products>;

            var previewList = productEntities?.Select(p => new Gen_ProductsMV
            {
                Prod_Name = p.Prod_Name,
                Prod_Code = p.Prod_Code,
                Category_Name = p.Category_Name,
                Brand_Name = p.Brand_Name,
                Price = p.Price
            }).ToList();

            ViewBag.PreviewList = previewList;

            ViewBag.Brand = TempData["Brand"] as string;
            ViewBag.Category = TempData["Category"] as string;

            TempData.Keep("PreviewList");
            TempData.Keep("Brand");
            TempData.Keep("Category");

            return View();
        }

        [HttpPost]
        public ActionResult PreviewExcel(HttpPostedFileBase excelFile, string brand, string category)
        {
            List<Gen_Products> previewList = new List<Gen_Products>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (excelFile != null && excelFile.ContentLength > 0)
            {
                using (var package = new ExcelPackage(excelFile.InputStream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var name = worksheet.Cells[row, 1].Text;
                        var code = worksheet.Cells[row, 2].Text;
                        var priceText = worksheet.Cells[row, 3].Text;

                        if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(code) && string.IsNullOrWhiteSpace(priceText))
                            continue;

                        previewList.Add(new Gen_Products
                        {
                            Prod_Name = name,
                            Prod_Code = code,
                            Price = decimal.TryParse(priceText, out var val) ? val : (decimal?)null,
                            Category_Name = category,
                            Brand_Name = brand
                        });
                    }
                }

                TempData["PreviewList"] = previewList;
                TempData["Brand"] = brand;
                TempData["Category"] = category;
            }

            return RedirectToAction("ImportProducts");
        }

        [HttpPost]
        public ActionResult UploadExcel()
        {
            var previewList = TempData["PreviewList"] as List<Gen_Products>;
            var brand = TempData["Brand"] as string;
            var category = TempData["Category"] as string;

          //  var now = DateTime.Now;
            var sqlMinDate = new DateTime(1753, 1, 1);

            if (previewList != null)
            {
                foreach (var item in previewList)
                {
                    var entity = new Gen_Products
                    {
                        Prod_Name = item.Prod_Name,
                        Prod_Code = item.Prod_Code,
                        Category_Name = category,
                        Brand_Name = brand,
                        Prod_Description = item.Prod_Description,
                        Price = item.Price,
                        Tax = item.Tax,
                        RecordTimeStamp = item.RecordTimeStamp,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        BranchId = item.BranchId,
                        CreatedBy = item.CreatedBy,
                        UpdatedBy = item.UpdatedBy,
                        Deleted = item.Deleted,
                        Active = item.Active
                    };

                    if (item.Prod_DateIn.HasValue && item.Prod_DateIn >= sqlMinDate)
                        entity.Prod_DateIn = item.Prod_DateIn;
                    if (item.Prod_DateOut.HasValue && item.Prod_DateOut >= sqlMinDate)
                        entity.Prod_DateOut = item.Prod_DateOut;

                    DB.Gen_Products.Add(entity);
                }

                DB.SaveChanges();
            }

            return RedirectToAction("Products");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Gen_Products brand = DB.Gen_Products.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductList = DB.Gen_Products.ToList();
            return View("Products", brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
        "Id,Category_Name,Brand_Name,Prod_Description,Prod_Name,Prod_Code,Price")] Gen_Products brand)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(brand).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Products");
            }
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id)
        {
            var product = DB.Gen_Products.Find(id);
            if (product != null)
            {
                DB.Gen_Products.Remove(product);
                DB.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        [HttpGet]
        public JsonResult GetCategoryDetailsByBrand(string brandName)
        {
            var category = DB.Gen_Categories
                .FirstOrDefault(c => c.Name == brandName); // ← FIXED: use correct field

            if (category != null)
            {
                return Json(new
                {
                    success = true,
                    category = category.Category_Name,
                    description = category.Description,
                    code = category.Code,
                    tax = category.Tax, 
                    price = category.Price
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

    }
}
