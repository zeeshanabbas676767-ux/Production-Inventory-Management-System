using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataBaseLibrary;
using EMB_App.Models;

namespace EMB_App.Controllers
{
    public class Gen_ServiceTypeController : Controller
    {
        EMB_ProductionEntities2 DB = new EMB_ProductionEntities2();
        public ActionResult ServiceType()
        {
            var model = new Gen_ServiceType();
            ViewBag.ServiceTypeList = DB.Gen_ServiceType
               .Where(b => (bool)b.Deleted)
               .ToList();
            ViewBag.ServiceTypeList = DB.Gen_ServiceType.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveServiceType(Gen_ServiceType ServiceType, string IsDeleteMode)
        {
            if (IsDeleteMode == "true")
            {
                var toDelete = DB.Gen_ServiceType.Find(ServiceType.Id);
                if (toDelete != null)
                {
                    DB.Gen_ServiceType.Remove(toDelete);
                }
            }
            else
            {
                var entity = DB.Gen_ServiceType.Find(ServiceType.Id);

                if (entity != null)
                {
                    // Update existing
                    entity.Service_Type = ServiceType.Service_Type;
                    entity.Service_TypeDescription = ServiceType.Service_TypeDescription;
                    entity.BranchId = ServiceType.BranchId;
                    entity.RecordTimeStamp = ServiceType.RecordTimeStamp;
                    entity.Active = ServiceType.Active;
                    entity.Deleted = ServiceType.Deleted;
                    entity.CreatedBy = ServiceType.CreatedBy;
                    entity.CreatedDate = ServiceType.CreatedDate;
                    entity.UpdatedBy = ServiceType.UpdatedBy;
                    entity.UpdatedDate = ServiceType.UpdatedDate;

                    DB.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    // Create new
                    entity = new Gen_ServiceType
                    {
                        Service_Type = ServiceType.Service_Type,
                        Service_TypeDescription = ServiceType.Service_TypeDescription,
                        BranchId = ServiceType.BranchId,
                        RecordTimeStamp = ServiceType.RecordTimeStamp,
                        Active = ServiceType.Active,
                        Deleted = ServiceType.Deleted,
                        CreatedBy = ServiceType.CreatedBy,
                        CreatedDate = ServiceType.CreatedDate,
                        UpdatedBy = ServiceType.UpdatedBy,
                        UpdatedDate = ServiceType.UpdatedDate,
                    };
                    DB.Gen_ServiceType.Add(entity);
                }

                DB.SaveChanges();
            }

            return RedirectToAction("ServiceType");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Gen_ServiceType brand = DB.Gen_ServiceType.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceTypeList = DB.Gen_ServiceType.ToList();
            return View("ServiceType", brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
        "Id,Service_Type,Service_TypeDescription,RecordTimeStamp,BranchId,Active,Deleted,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Gen_ServiceType brand)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(brand).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("ServiceType");
            }
            return View(brand);
        }
        // POST: Delete brand by ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteServiceType(int id)
        {
            var toDelete = DB.Gen_ServiceType.Find(id);
            if (toDelete != null)
            {
                DB.Gen_ServiceType.Remove(toDelete);
                DB.SaveChanges();
            }

            return RedirectToAction("ServiceType");
        }
    }
}