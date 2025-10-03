using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataBaseLibrary;
using EMB_App.Models;

namespace EMB_App.Controllers
{
    public class Gen_MachinesController : Controller
    {
        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2();

        // GET: Gen_Machines
        public ActionResult Machines()
        {
            var model = new Gen_Machines();

            // Populate the dropdown lists
            ViewBag.BrandId = new SelectList(db.Gen_Brands.ToList(), "Id", "Code");
            //ViewBag.ServiceId = new SelectList(db.Gen_TheServices.ToList(), "Id", "Service_Name");

            ViewBag.MachinesList = db.Gen_Machines.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMachines(Gen_Machines model, string IsDeleteMode)
        {
            if (IsDeleteMode == "true")
            {
                var toDelete = db.Gen_Machines.Find(model.Id);
                if (toDelete != null)
                {
                    db.Gen_Machines.Remove(toDelete);
                }
            }
            else
            {
                var entity = db.Gen_Machines.Find(model.Id) ?? new Gen_Machines();

                // Copy simple properties
                entity.Code = model.Code;
                entity.BrandId = model.BrandId;
                entity.ServiceId = model.ServiceId;
                entity.HeadtoHead = model.HeadtoHead;
                entity.FrameHeight = model.FrameHeight;
                entity.HeadsNum = model.HeadsNum;
                entity.NeedlesNum = model.NeedlesNum;
                entity.Serial_Number = model.Serial_Number;
                entity.Calling_Number = model.Calling_Number;
                entity.Warranty = model.Warranty;
                entity.Deleted = model.Deleted;
                entity.Active = model.Active;
                entity.BranchId = model.BranchId;
                entity.CreatedBy = model.CreatedBy; // Or set to current user ID
                entity.UpdatedBy = model.UpdatedBy; // Or set to current user ID

                // Handle dates safely
                entity.PurchaseDate = model.PurchaseDate >= new DateTime(1753, 1, 1)
                    ? model.PurchaseDate
                    : null; // Set to null if invalid

                entity.ServiceEnd = model.ServiceEnd >= new DateTime(1753, 1, 1)
                    ? model.ServiceEnd
                    : DateTime.Now; // Fallback to now if invalid

                entity.ServiceNext = model.ServiceNext >= new DateTime(1753, 1, 1)
                    ? model.ServiceNext
                    : DateTime.Now.AddMonths(1); // Fallback to future date

                // Ensure non-nullable dates are set
                entity.RecordTimeStamp = model.RecordTimeStamp; 
                entity.CreatedDate = model.CreatedDate; // Set only if new
                // entity.CreatedDate = entity.Id == 0 ? DateTime.Now : entity.CreatedDate;
                entity.UpdatedDate = model.UpdatedDate; // Always update on save

                if (entity.Id == 0)
                {
                    db.Gen_Machines.Add(entity);
                }
                else
                {
                    db.Entry(entity).State = EntityState.Modified;
                }
            }

            db.SaveChanges();
            return RedirectToAction("Machines");
        }
        // GET: Gen_Machines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Machines gen_Machines = db.Gen_Machines.Find(id);
            if (gen_Machines == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Gen_Brands, "Id", "Code", gen_Machines.BrandId);
            //ViewBag.ServiceId = new SelectList(db.Gen_TheServices, "Id", "Service_Name", gen_Machines.ServiceId);
            ViewBag.MachinesList = db.Gen_Machines.ToList();
            return View("Machines", gen_Machines);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMachines(int id)
        {
            var toDelete = db.Gen_Machines.Find(id);
            if (toDelete != null)
            {
                db.Gen_Machines.Remove(toDelete);
                db.SaveChanges();
            }
            return RedirectToAction("Machines");
        }
    }
}