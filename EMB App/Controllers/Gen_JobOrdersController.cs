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
    public class Gen_JobOrdersController : Controller
    {
        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2(); 

        // GET: Gen_JobOrders
        public ActionResult JobOrder()
        {
            var model = new Gen_JobOrders();
            ViewBag.JobOrderList = db.Gen_JobOrders
               .Where(b => (bool)b.Deleted)
               .ToList();
            ViewBag.JobOrderList = db.Gen_JobOrders.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveJobOrder(Gen_JobOrders model, string IsDeleteMode)
        {
            if (IsDeleteMode == "true")
            {
                var toDelete = db.Gen_JobOrders.Find(model.Id);
                if (toDelete != null)
                {
                    db.Gen_JobOrders.Remove(toDelete);
                }
            }
            else
            {
                var entity = db.Gen_JobOrders.Find(model.Id) ?? new Gen_JobOrders();
                entity.Code = model.Code;
                entity.CompId = model.CompId;
                entity.ReqRepeats = model.ReqRepeats;
                entity.ReqHead = model.ReqHead;
                entity.Active = model.Active;
                entity.Deleted = model.Deleted;
                entity.BranchId = model.BranchId;
                entity.Panni = model.Panni;
                entity.Solving = model.Solving;
                entity.CreatedBy = model.CreatedBy;
                entity.CreatedDate = model.CreatedDate;
              //  entity.CreatedDate = entity.Id == 0 ? DateTime.Now : entity.CreatedDate;
                entity.UpdatedBy = model.UpdatedBy;
                entity.UpdatedDate = model.UpdatedDate;

                if (entity.Id == 0)
                {
                    db.Gen_JobOrders.Add(entity);
                }
                else
                {
                    db.Entry(entity).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            return RedirectToAction("JobOrder");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Gen_JobOrders brand = db.Gen_JobOrders.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobOrderList = db.Gen_JobOrders.ToList();
            return View("JobOrder",  brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
        "Id,Code,CompId,ReqRepeats,ReqHead,Active," +
        "Deleted,BranchId,Panni,Solving,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Gen_JobOrders brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("JobOrder");
            }
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteJobOrders(int id)
        {
            var toDelete = db.Gen_JobOrders.Find(id);
            if (toDelete != null)
            {
                db.Gen_JobOrders.Remove(toDelete);
                db.SaveChanges();
            }
            return RedirectToAction("JobOrder");
        }
    }
}