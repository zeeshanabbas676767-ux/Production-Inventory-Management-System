using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataBaseLibrary;

namespace EMB_App.Controllers
{
    public class Hr_CustomersController : Controller
    {
        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2();

        // GET: Hr_Customers 
        public ActionResult Hr_Customers() 
        {
            var model = new Hr_Customers();
            ViewBag.Hr_CustomersList = db.Hr_Customers
               .Where(b => b.Deleted)
               .ToList();
            ViewBag.Hr_CustomersList = db.Hr_Customers.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveHr_Customers(Hr_Customers model, string IsDeleteMode)
        {
            if (IsDeleteMode == "true")
            {
                var toDelete = db.Hr_Customers.Find(model.Id);
                if (toDelete != null)
                {
                    db.Hr_Customers.Remove(toDelete);
                }
            }
            else
            {
                var entity = db.Hr_Customers.Find(model.Id) ?? new Hr_Customers();
                // Copy simple properties
                entity.Customer_Id = model.Customer_Id;
                entity.Customer_Name = model.Customer_Name;
                entity.NTN_Number = model.NTN_Number;
                entity.STRN_Number = model.STRN_Number;
                entity.Customer_Address = model.Customer_Address;
                entity.Customer_City = model.Customer_City;
                entity.Customer_Country = model.Customer_Country;
                entity.Customer_Email = model.Customer_Email;
                entity.Customer_Email2 = model.Customer_Email2; 
                entity.Customer_Phone = model.Customer_Phone;
                entity.Customer_Phone2 = model.Customer_Phone2;
                entity.Customer_Mobile = model.Customer_Mobile;
                entity.Customer_Fax = model.Customer_Fax;
                entity.Customer_Credit_Limit_CAP = model.Customer_Credit_Limit_CAP;
                entity.Customer_Invoice_Period = model.Customer_Invoice_Period;
                entity.Deleted = model.Deleted;
                entity.Active = model.Active;
                entity.BranchId = model.BranchId;
                entity.CreatedBy = model.CreatedBy; 
                entity.UpdatedBy = model.UpdatedBy; 
                entity.RecordTimeStamp = model.RecordTimeStamp; 
                entity.CreatedBy = model.CreatedBy;
                entity.UpdatedBy = model.UpdatedBy;
                entity.CreatedDate = model.CreatedDate;
                entity.UpdatedDate = model.UpdatedDate; 

                if (entity.Id == 0)
                {
                    db.Hr_Customers.Add(entity);
                }
                else
                {
                    db.Entry(entity).State = EntityState.Modified;
                }
            }

            db.SaveChanges();
            return RedirectToAction("Hr_Customers");
        }
        // GET: Gen_Machines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hr_Customers hr_Customers = db.Hr_Customers.Find(id);
            if (hr_Customers == null)
            {
                return HttpNotFound();
            }
            ViewBag.Hr_CustomersList = db.Hr_Customers.ToList();
            return View( "Hr_Customers",hr_Customers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit ([Bind(Include = 
        "Id,Customer_Id,Customer_Name,NTN_Number,STRN_Number," +
        "Customer_Address,Customer_City,Customer_Country,Customer_Email," +
        "Customer_Email2,Customer_Phone,Customer_Phone2,Customer_Mobile," +
        "Customer_Fax,Customer_Credit_Limit_CAP,Customer_Invoice_Period,Active," +
        "Deleted,RecordTimeStamp,BranchId,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Hr_Customers Hr_Customers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Hr_Customers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Hr_Customers");
            }
            return View(Hr_Customers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteHr_Customers(int id)
        {
            var toDelete = db.Hr_Customers.Find(id);
            if (toDelete != null)
            {
                db.Hr_Customers.Remove(toDelete);
                db.SaveChanges();
            }
            return RedirectToAction("Hr_Customers");
        }
    }
}

