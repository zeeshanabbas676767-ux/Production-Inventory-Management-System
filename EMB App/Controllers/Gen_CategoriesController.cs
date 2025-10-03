using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataBaseLibrary;
 
namespace EMB_App.Controllers
{
    public class Gen_CategoriesController : Controller
    {
        private EMB_ProductionEntities2 DB = new EMB_ProductionEntities2();
         
        public ActionResult Categories() 
        {
            var model = new Gen_Categories();
            ViewBag.CatagoriesList = DB.Gen_Categories
               .Where(b => (bool)b.Deleted)
               .ToList();
            ViewBag.CatagoriesList = DB.Gen_Categories.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCategories(Gen_Categories model, string IsDeleteMode)
        {
            if (model == null)
            {
                ModelState.AddModelError("", "Invalid data.");
                return RedirectToAction("Categories"); 
            }

            if (IsDeleteMode == "true")
            {
                var toDelete = DB.Gen_Categories.Find(model.Id);
                if (toDelete != null)
                {
                    DB.Gen_Categories.Remove(toDelete);
                    DB.SaveChanges();
                }
            }
            else if (model.Id > 0)
            {
                var toUpdate = DB.Gen_Categories.Find(model.Id);
                if (toUpdate != null)
                {
                    // Check if category is used in products
                    //bool isUsed = DB.Gen_Products.Any(p =>
                    //    p.Price == toUpdate.Price &&
                    //    p.Tax == toUpdate.Tax &&
                    //    p.Prod_Code == toUpdate.Code &&
                    //    p.Brand_Name == toUpdate.Name &&
                    //    p.Category_Name == toUpdate.Category_Name &&
                    //    p.Prod_Description == toUpdate.Description);

                    //if (isUsed)
                    //{
                    //    if (string.IsNullOrWhiteSpace(model.Name))
                    //        ModelState.AddModelError("Name", "Name can't be empty; it's used in a product.");
                    //    if (string.IsNullOrWhiteSpace(model.Category_Name))
                    //        ModelState.AddModelError("Category_Name", "Category Name can't be empty; it's used in a product.");
                    //    if (string.IsNullOrWhiteSpace(model.Description))
                    //        ModelState.AddModelError("Description", "Description can't be empty; it's used in a product.");

                    //    if (string.IsNullOrWhiteSpace(model.Price?.ToString()))
                    //        ModelState.AddModelError("Price", "Price can't be empty; it's used in a product.");
                    //    if (string.IsNullOrWhiteSpace(model.Tax?.ToString()))
                    //        ModelState.AddModelError("Tax", "Tax can't be empty; it's used in a product.");
                    //    if (string.IsNullOrWhiteSpace(model.Code))
                    //        ModelState.AddModelError("Code", "Code can't be empty; it's used in a product.");
                    //}

                    if (!ModelState.IsValid)
                    {
                        var categories = DB.Gen_Categories.ToList();
                        var products = DB.Gen_Products.ToList();

                        var categoryVM = categories.Select(cat => new Gen_Categories
                        {
                            Id = cat.Id,
                            BrandId = cat.BrandId,
                            BranchId = cat.BranchId,
                            RecordTimeStamp = cat.RecordTimeStamp,
                            Code = cat.Code,
                            Name = cat.Name,
                            Category_Name = cat.Category_Name,
                            Description = cat.Description,
                            Price = cat.Price,
                            Tax = cat.Tax,
                            MeasureType = cat.MeasureType,
                            MeasureValue = cat.MeasureValue,
                            Active = cat.Active,    
                            Deleted = cat.Deleted,
                            //IsUsedInProducts = products.Any(p =>
                            // p.Price == cat.Price && 
                            // p.Tax == cat.Tax && 
                            // p.Prod_Code == cat.Code &&
                            //    p.Brand_Name == cat.Name &&
                            //    p.Category_Name == cat.Category_Name &&
                            //    p.Prod_Description == cat.Description
                            //) ? "Yes" : "No"
                        }).ToList();

                        return View("Categories", categoryVM);
                    }

                    // Safe to update
                    toUpdate.BrandId = model.BrandId;
                    toUpdate.BranchId = model.BranchId;
                    toUpdate.Code = model.Code;
                    toUpdate.RecordTimeStamp = model.RecordTimeStamp;
                    toUpdate.Name = model.Name;
                    toUpdate.Category_Name = model.Category_Name;
                    toUpdate.Description = model.Description;
                    toUpdate.Price = model.Price;
                    toUpdate.Tax = model.Tax;
                    toUpdate.MeasureValue = model.MeasureValue;
                    toUpdate.MeasureType = model.MeasureType;
                    toUpdate.Active = model.Active;
                    toUpdate.Deleted = model.Deleted;

                    DB.SaveChanges();
                }
            }
            else
            {
                // New category
                DB.Gen_Categories.Add(new Gen_Categories
                {
                    Code = model.Code,
                    Name = model.Name,
                    BranchId = model.BranchId,
                    BrandId = model.BrandId,    
                    RecordTimeStamp = model.RecordTimeStamp,
                    Category_Name = model.Category_Name,
                    Description = model.Description,
                    Price = model.Price,
                    Tax = model.Tax,
                    MeasureValue = model.MeasureValue,
                    MeasureType = model.MeasureType,    
                    Active = model.Active,
                    Deleted = model.Deleted,
                });

                DB.SaveChanges(); 
            }

            return RedirectToAction("Categories");
        }
        // GET: Gen_Machines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Categories cate = DB.Gen_Categories.Find(id);
            if (cate == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatagoriesList = DB.Gen_Categories.ToList();
            return View("Categories", cate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
        "Id,BranchId,Name,Code,Description,MeasureValue,MeasureType,Price,RecordTimeStamp,Tax,Category_Name,Active,Deleted,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Gen_Categories cate)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(cate).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View(cate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            var category = DB.Gen_Categories.Find(id);
            if (category != null)
            {
                DB.Gen_Categories.Remove(category);
                DB.SaveChanges(); 
            }
            return RedirectToAction("Categories");
        }
    }
}
