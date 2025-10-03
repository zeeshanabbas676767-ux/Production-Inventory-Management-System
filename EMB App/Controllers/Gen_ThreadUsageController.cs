using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataBaseLibrary;

namespace EMB_App.Controllers
{
    public class Gen_ThreadUsageController : Controller
    {
        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2();

        // GET: Gen_ThreadUsage
        public ActionResult ThreadUsage()
        {
            var model = new Gen_ThreadUsage();
            ViewBag.ThreadList = db.Gen_ThreadUsage.ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveThreadUsage(Gen_ThreadUsage model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    var existing = db.Gen_ThreadUsage.Find(model.Id);
                    if (existing != null)
                    {
                        TryUpdateModel(existing, "", new[] {
                            "Code", "CompId", "ThreadId", "Usage", "Type",
                            "Active", "Deleted", "BranchId", "UpdatedBy", "UpdatedDate"
                        });

                        existing.UpdatedBy = model.UpdatedBy;
                        existing.UpdatedDate = DateTime.Now;
                        db.Entry(existing).State = EntityState.Modified;
                    }
                }
                else
                {
                    model.CreatedBy = model.CreatedBy;
                    model.CreatedDate = model.CreatedDate;
                    model.Active = model.Active ?? true;
                    model.Deleted = model.Deleted ?? false;
                    db.Gen_ThreadUsage.Add(model);
                }

                db.SaveChanges();
                return RedirectToAction("ThreadUsage");
            }

            ViewBag.ThreadList = db.Gen_ThreadUsage.ToList();
            return View("ThreadUsage", model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = db.Gen_ThreadUsage.Find(id);
            if (model == null) return HttpNotFound();

            ViewBag.ThreadList = db.Gen_ThreadUsage.ToList();
            return View("ThreadUsage", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,CompId,ThreadId,Usage,Type,Active,Deleted,BranchId,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Gen_ThreadUsage gen_ThreadUsage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gen_ThreadUsage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThreadUsage");
            }
            return View(gen_ThreadUsage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteThreadUsage(int id)
        {
            var toDelete = db.Gen_ThreadUsage.Find(id);
            if (toDelete != null)
            {
                db.Gen_ThreadUsage.Remove(toDelete);
                db.SaveChanges();
            }
            return RedirectToAction("ThreadUsage");
        }

        [HttpGet]
        public JsonResult GetThreadUsageByCode(string code)
        {
            try
            {
                Debug.WriteLine($"\n--- Thread Usage Request for Code: {code} ---");

                if (!int.TryParse(code, out int codeInt))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Please enter numbers only (e.g. 1200)"
                    }, JsonRequestBehavior.AllowGet);
                }

                var results = db.Gen_ThreadUsage
                    .Where(t => t.Code == codeInt)
                    .Select(t => new {
                        t.ThreadId,
                        t.Usage,
                        t.Type
                    })
                    .ToList();

                Debug.WriteLine($"Found {results.Count} records");

                return Json(new
                {
                    success = true,
                    data = results,
                    count = results.Count
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex}");
                return Json(new
                {
                    success = false,
                    message = "System error: " + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetAvailableCodes()
        {
            try
            {
                var codes = db.Gen_ThreadUsage
                    .Where(t => (t.Active ?? false) && !(t.Deleted ?? true))
                    .Select(t => t.Code)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();

                Debug.WriteLine($"Returning {codes.Count} available codes");
                return Json(codes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting codes: {ex}");
                return Json(new List<int>(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}