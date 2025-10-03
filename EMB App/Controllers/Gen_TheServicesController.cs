using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using DataBaseLibrary;
using EMB_App.Models;
using Newtonsoft.Json;

namespace EMB_App.Controllers
{
    public class Gen_TheServicesController : Controller
    {
        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2();

        // GET: Gen_TheServices/Service
        public ActionResult Service()
        {
            var model = new Gen_TheServices();
            ViewBag.ServicesList = db.Gen_TheServices
               .Where(b => (bool)b.Deleted)
               .ToList();
            ViewBag.ServicesList = db.Gen_TheServices.ToList();
            //var service = db.Gen_ServiceType.Select(p => new SelectListItem
            //{
            //    Value = p.Service_Type,
            //    Text = p.Service_Type
            //}).ToList();
            //ViewBag.Service_Type = service;
            //ViewBag.Service_Type = new SelectList(db.Gen_ServiceType.ToList(), "Id", "Service_Type");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveService(Gen_TheServices model, string IsDeleteMode)
        {
            if (IsDeleteMode == "true")
            {
                var toDelete = db.Gen_TheServices.Find(model.Id);
                if (toDelete != null)
                {
                    db.Gen_TheServices.Remove(toDelete);
                }
            }
            else
            {
                var entity = db.Gen_TheServices.Find(model.Id);

                if (entity != null)
                {
                    entity.Service_Type = model.Service_Type;
                    entity.Service_Name = model.Service_Name;
                    entity.Service_RateCharge = model.Service_RateCharge;
                    entity.Machine_Device = model.Machine_Device;
                    entity.Max_Range = model.Max_Range;
                    entity.Max_Charges = model.Max_Charges;
                    entity.Min_Charges = model.Min_Charges;
                    entity.Field1 = model.Field1;
                    entity.Field2 = model.Field2;
                    entity.BranchId = model.BranchId;
                    entity.Active = model.Active;
                    entity.Deleted = model.Deleted;
                    entity.CreatedBy = model.CreatedBy;
                    entity.CreatedDate = model.CreatedDate;
                    entity.UpdatedBy = model.UpdatedBy;
                    entity.UpdatedDate = model.UpdatedDate;
                    entity.RecordTimeStamp = model.RecordTimeStamp;

                    db.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    // Create new
                    entity = new Gen_TheServices
                    {
                        Service_Type = model.Service_Type,
                        Service_Name = model.Service_Name,
                        Service_RateCharge = model.Service_RateCharge,
                        Machine_Device = model.Machine_Device,
                        Max_Range = model.Max_Range,
                        Max_Charges = model.Max_Charges,
                        Min_Charges = model.Min_Charges,
                        Field1 = model.Field1,
                        Field2 = model.Field2,
                        BranchId = model.BranchId,
                        Active = model.Active,
                        Deleted = model.Deleted,
                        CreatedBy = model.CreatedBy,
                        CreatedDate = model.CreatedDate,
                        UpdatedBy = model.UpdatedBy,
                        UpdatedDate = model.UpdatedDate,
                        RecordTimeStamp = model.RecordTimeStamp,
                    };
                    db.Gen_TheServices.Add(entity);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Service");
        }
        // GET: Gen_TheServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_TheServices gen_TheServices = db.Gen_TheServices.Find(id);
            if (gen_TheServices == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Service_Type = db.Gen_ServiceType.Select(p => new SelectListItem
            //{
            //    Value = p.Service_Type,
            //    Text = p.Service_Type,
            //    Selected = p.Service_Type == gen_TheServices.Service_Type.ToString()
            //}).ToList();
            ViewBag.ServicesList = db.Gen_TheServices.ToList();
            //ViewBag.Service_Type = new SelectList(db.Gen_ServiceType.ToList(), "Id", "Service_Type");
            //ViewBag.ServicesList = db.Gen_TheServices.Include(g => g.Gen_ServiceType).ToList();
            return View("Service", gen_TheServices);
        }

        // POST: Gen_TheServices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gen_TheServices gen_TheServices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_TheServices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Service");
            }
            //ViewBag.Service_Type = db.Gen_ServiceType.Select(p => new SelectListItem
            //{
            //    Value = p.Service_Type,
            //    Text = p.Service_Type,
            //    Selected = p.Service_Type == gen_TheServices.Service_Type.ToString()
            //}).ToList();
            ViewBag.ServicesList = db.Gen_TheServices.ToList();
            //ViewBag.Service_Type = new SelectList(db.Gen_ServiceType.ToList(), "Id", "Service_Type");
            //ViewBag.ServicesList = db.Gen_TheServices.Include(g => g.Gen_ServiceType).ToList();
            return View(gen_TheServices);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteServices(int id)
        {
            var toDelete = db.Gen_TheServices.Find(id);
            if (toDelete != null)
            {
                db.Gen_TheServices.Remove(toDelete);
                db.SaveChanges();
            }
            return RedirectToAction("Service");
        }
    }
}




