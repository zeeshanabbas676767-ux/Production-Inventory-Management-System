using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataBaseLibrary;

namespace EMB_App.Controllers
{
    public class Gen_ComponentsController : Controller
    {
        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2();

        // GET: Gen_Components
        public ActionResult Component() 
        {
            var model = new Gen_Components(); // Create new instance for the form
            ViewBag.ComponentList = db.Gen_Components.ToList(); // Populate the table data
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveComponent(Gen_Components model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    // Update existing record
                    var existing = db.Gen_Components.Find(model.Id);
                    if (existing != null)
                    {
                        // Automatically update all matching properties (except Id, CreatedBy, CreatedDate)
                        TryUpdateModel(existing, "", new[] {
                    "Code", "D_Code", "Name", "HeadToHead", "Stitch",
                    "Tilla", "Seq_A", "Seq_B", "BranchId", "Active","Deleted"
                });

                        // Manually set audit fields
                        existing.UpdatedBy = model.UpdatedBy; // or your preferred user tracking
                        existing.UpdatedDate = model.UpdatedDate;

                        db.Entry(existing).State = EntityState.Modified;
                    }
                }
                else
                {
                    // Create new record
                    model.CreatedBy = model.CreatedBy;
                    model.CreatedDate = model.CreatedDate;
                    model.Active = model.Active; // (or set default: model.Active = true;)
                    model.Deleted = model.Deleted; // (or set default: model.Deleted = false;)

                    db.Gen_Components.Add(model);
                }

                db.SaveChanges();
                return RedirectToAction("Component");
            }

            // If validation fails, reload the form with data
            ViewBag.ComponentList = db.Gen_Components.ToList();
            return View("Component", model);
        }
        // GET: Gen_Components/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = db.Gen_Components.Find(id);
            if (model == null) return HttpNotFound();

            ViewBag.ComponentList = db.Gen_Components.ToList(); // Ensure table data is loaded
            return View("Component", model); // Return the main view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,D_Code,Name,HeadToHead,Stitch,Tilla,Seq_A,Seq_B,Active,Deleted,BranchId,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Gen_Components gen_Components)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_Components).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Component");
            }
            return View(gen_Components);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComponents(int id)
        {
            var toDelete = db.Gen_Components.Find(id);
            if (toDelete != null)
            {
                db.Gen_Components.Remove(toDelete);
                db.SaveChanges();
            }

            return RedirectToAction("Component");
        }
    }
}
