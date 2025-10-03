using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataBaseLibrary;

namespace EMB_App.Controllers
{
    public class Gen_BrandsController : Controller
    { 
        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2(); 

        // GET: Hr_Customers 
        public ActionResult Brand()  
        {
            var model = new Gen_Brands();
            ViewBag.BrandList = db.Gen_Brands
               .Where(b => b.Deleted)
               .ToList();
            ViewBag.BrandList = db.Gen_Brands.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBrand(Gen_Brands model, string IsDeleteMode)
        {
            if (IsDeleteMode == "true")
            {
                var toDelete = db.Gen_Brands.Find(model.Id);
                if (toDelete != null)
                {
                    db.Gen_Brands.Remove(toDelete);
                }
            }
            else
            {
                var entity = db.Gen_Brands.Find(model.Id) ?? new Gen_Brands();
                // Copy simple properties
                entity.Code = model.Code;
                entity.BrandName = model.BrandName;
                entity.Description = model.Description;
                entity.Address = model.Address;
                entity.Deleted = model.Deleted;
                entity.Active = model.Active;
                entity.BranchId = model.BranchId;
                entity.UpdatedBy = model.UpdatedBy;
                entity.RecordTimeStamp = DateTime.Now;
                entity.CreatedBy = model.CreatedBy;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = DateTime.Now;

                if (entity.Id == 0)
                {
                    db.Gen_Brands.Add(entity);
                }
                else
                {
                    db.Entry(entity).State = EntityState.Modified;
                }
            }

            db.SaveChanges();
            return RedirectToAction("Brand");
        }
        // GET: Gen_Machines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Brands brand = db.Gen_Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandList = db.Gen_Brands.ToList();
            return View("Brand", brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
        "Id,Code,BrandName,Description,Address,Active," +
        "Deleted,RecordTimeStamp,BranchId,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Gen_Brands brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Brand");
            }
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBrand(int id)
        {
            var toDelete = db.Gen_Brands.Find(id);
            if (toDelete != null)
            {
                db.Gen_Brands.Remove(toDelete);
                db.SaveChanges();
            }
            return RedirectToAction("Brand");
        }
    }
}

