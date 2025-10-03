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
    public class Gen_ServiceRatesController : Controller
    {
        EMB_ProductionEntities2 DB = new EMB_ProductionEntities2();
        // GET: Design 
        public ActionResult ServiceRates()
        {
            var model = new Gen_ServiceRates();
            //ViewBag.ServiceRatesList = DB.Gen_ServiceRates
            //   .Where(b => (bool)b.Deleted)
            //   .ToList();
            ViewBag.CustomerList = DB.Hr_Customers
       .Where(c => !c.Deleted) // optional filter
       .Select(c => new SelectListItem
       {
           Value = c.Id.ToString(),
           Text = c.Customer_Name
       })
       .ToList();

            ViewBag.ServiceList = DB.Gen_TheServices
      .Where(c => (bool)!c.Deleted) // optional filter
      .Select(c => new SelectListItem
      {
          Value = c.Id.ToString(),
          Text = c.Service_Name
      })
      .ToList();
        //    ViewBag.ServiceRatesList = DB.Gen_ServiceRates
        //.Include(r => r.Hr_Customers)
        //.Include(r => r.Gen_TheServices) // Eager load related service
        //.ToList();
            ViewBag.ServiceRatesList = DB.Gen_ServiceRates.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveServiceRates(Gen_ServiceRates ServiceRates, string IsDeleteMode)
        {
            if (IsDeleteMode == "true")
            {
                var toDelete = DB.Gen_ServiceRates.Find(ServiceRates.Id);
                if (toDelete != null)
                {
                    DB.Gen_ServiceRates.Remove(toDelete);
                }
            }
            else
            {
                var entity = DB.Gen_ServiceRates.Find(ServiceRates.Id);

                if (entity != null)
                {
                    // Update existing
                    entity.Customer_Id = ServiceRates.Customer_Id;
                    entity.Service_Id = ServiceRates.Service_Id;
                    entity.Charge_Fector = ServiceRates.Charge_Fector;
                    entity.Remarks = ServiceRates.Remarks;
                    entity.BranchId = ServiceRates.BranchId;
                    entity.Active = ServiceRates.Active;
                    entity.Deleted = ServiceRates.Deleted;
                    entity.BranchId = ServiceRates.BranchId;
                    entity.CreatedBy = ServiceRates.CreatedBy;
                    entity.CreatedDate = ServiceRates.CreatedDate;
                    entity.UpdatedBy = ServiceRates.UpdatedBy;
                    entity.UpdatedDate = ServiceRates.UpdatedDate;
                    entity.RecordTimeStamp = ServiceRates.RecordTimeStamp;  

                    DB.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    // Create new
                    entity = new Gen_ServiceRates
                    {
                        Customer_Id = ServiceRates.Customer_Id,
                        Service_Id = ServiceRates.Service_Id,
                        Charge_Fector = ServiceRates.Charge_Fector,
                        Remarks = ServiceRates.Remarks,
                        BranchId = ServiceRates.BranchId,
                        Active = ServiceRates.Active,
                        Deleted = ServiceRates.Deleted,
                        CreatedBy = ServiceRates.CreatedBy,
                        CreatedDate = ServiceRates.CreatedDate,
                        UpdatedBy = ServiceRates.UpdatedBy,
                        UpdatedDate = ServiceRates.UpdatedDate,
                        RecordTimeStamp = ServiceRates.RecordTimeStamp,
                    };
                    DB.Gen_ServiceRates.Add(entity);
                }
                DB.SaveChanges();
            }

            return RedirectToAction("ServiceRates");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Gen_ServiceRates brand = DB.Gen_ServiceRates.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerList = DB.Hr_Customers
      .Where(c => !c.Deleted) // optional filter
      .Select(c => new SelectListItem
      {
          Value = c.Id.ToString(),
          Text = c.Customer_Name
      })
      .ToList();

            ViewBag.ServiceList = DB.Gen_TheServices
      .Where(c => (bool)!c.Deleted) // optional filter
      .Select(c => new SelectListItem
      {
          Value = c.Id.ToString(),
          Text = c.Service_Name
      })
      .ToList();
            ViewBag.ServiceRatesList = DB.Gen_ServiceRates.ToList();
            return View("ServiceRates", brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
        "Id,Customer_Id,Service_Id,Charge_Fector,Remarks,BranchId,RecordTimeStamp,Active,Deleted")] Gen_ServiceRates brand)
        {
            if (ModelState.IsValid)
            {
                DB.Entry(brand).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("ServiceRates");
            }
            return View(brand);
        }
        // POST: Delete brand by ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteServiceRates(int id)
        {
            var toDelete = DB.Gen_ServiceRates.Find(id);
            if (toDelete != null)
            {
                DB.Gen_ServiceRates.Remove(toDelete);
                DB.SaveChanges();
            }

            return RedirectToAction("ServiceRates");
        }
    }
}