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
    public class Gen_MachineParametersController : Controller
    {
        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2();

        // GET: Gen_JobOrders
        public ActionResult MachineParameters()
        {
            var model = new Gen_MachineParameters();
            ViewBag.MachineParametersList = db.Gen_MachineParameters
               .Where(b => !b.Deleted)
               .ToList();
            ViewBag.MachineParametersList = db.Gen_MachineParameters.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMachineParameters(Gen_MachineParameters model, string IsDeleteMode)
        {
            if (IsDeleteMode == "true")
            {
                var toDelete = db.Gen_MachineParameters.Find(model.Id);
                if (toDelete != null)
                {
                    db.Gen_MachineParameters.Remove(toDelete);
                }
            }
            else
            {
                var entity = db.Gen_MachineParameters.Find(model.Id) ?? new Gen_MachineParameters();
                entity.HeadToHead = model.HeadToHead;
                entity.FrameHeight = model.FrameHeight;
                entity.Active = model.Active;
                entity.Deleted = model.Deleted;
                entity.CreatedBy = model.CreatedBy;
                entity.CreatedDate = model.CreatedDate;
                entity.UpdatedBy = model.UpdatedBy;
                entity.UpdatedDate = model.UpdatedDate;

                if (entity.Id == 0)
                {
                    db.Gen_MachineParameters.Add(entity);
                }
                else
                {
                    db.Entry(entity).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            return RedirectToAction("MachineParameters");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = db.Gen_MachineParameters.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            // Pass both the single item and the list to the view
            ViewBag.MachineParametersList = db.Gen_MachineParameters.Where(b => (bool)!b.Deleted).ToList();
            return View("MachineParameters", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult DeleteMachineParameters(int id)
        {
            var toDelete = db.Gen_MachineParameters.Find(id);
            if (toDelete != null)
            {
                db.Gen_MachineParameters.Remove(toDelete);
                db.SaveChanges();
            }
            return RedirectToAction("MachineParameters");
        }
    }
}


