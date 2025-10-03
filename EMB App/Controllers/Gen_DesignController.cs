using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DataBaseLibrary;
using EMB_App.Models;

namespace EMB_App.Controllers
{
    public class Gen_DesignController : Controller
    {
        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2();

        // GET: Gen_Design
        public ActionResult Design()
        {
            // Get all valid codes from ThreadUsage (as int? to match Component_Code)
            var validCodes = db.Gen_ThreadUsage
                .Where(x => x.Code != null)
                .Select(x => x.Code)
                .Distinct()
                .ToList();
            ViewBag.ValidCodesHelp = "Valid codes: " + string.Join(", ", validCodes);
            // Get designs and determine their status
            var designList = db.Gen_Designs.ToList();
            foreach (var design in designList)
            {
                if (!design.Component_Code.HasValue)
                {
                    design.Status = false;
                }
                else
                {
                    design.Status = validCodes.Contains(design.Component_Code.Value);
                }
            }

            // Thread usage data for collections dropdown
            var threadUsageData = db.Gen_ThreadUsage
                .Select(x => new
                {
                    x.ThreadId,
                    x.Type,
                    x.Usage
                })
                .ToList();

            ViewBag.Collections = threadUsageData
                .GroupBy(x => x.ThreadId)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = $"{g.Key} - {(g.First().Usage != null ? g.First().Usage.ToString() : "")}"
                })
                .ToList();

            ViewBag.DesignList = designList;
            ViewBag.ValidCodes = validCodes;

            return View(new DataBaseLibrary.Gen_Designs());
        }

        [HttpGet] 
        public ActionResult GetThreadUsage(int? code)
        {
            if (!code.HasValue)
            {
                return Json(new { success = false, message = "Invalid code" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var threadUsages = db.Gen_ThreadUsage
                    .Where(t => t.Code == code.Value)
                    .Select(t => new
                    {
                        t.ThreadId,
                        t.Usage,
                        t.Type
                    })
                    .ToList();

                var threadBoxes = threadUsages
                    .Where(t => t.Type.Equals("thread", StringComparison.OrdinalIgnoreCase))
                    .Select(t => new
                    {
                        ThreadId = t.ThreadId,
                        Usage = t.Usage.ToString()
                    })
                    .ToList();

                var seqBoxes = threadUsages
                    .Where(t => t.Type.Equals("seq", StringComparison.OrdinalIgnoreCase))
                    .Select(t => new
                    {
                        ThreadId = t.ThreadId,
                        Usage = t.Usage.ToString()
                    })
                    .ToList();

                return Json(new
                {
                    success = true,
                    threadBoxes,
                    seqBoxes
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDesign(DataBaseLibrary.Gen_Designs model, List<string> SelectedCollections)
        {
            if (ModelState.IsValid)
            {
                model.CollectionName = SelectedCollections != null ? string.Join(",", SelectedCollections) : null;
                try
                {
                    // Get all thread usages
                    var threadUsages = db.Gen_ThreadUsage
                        .Where(t => t.Code == model.Component_Code.Value)
                        .ToList();

                    // Group by type and store in ViewBag
                    var seqThreads = threadUsages
                        .Where(t => t.Type.Equals("seq", StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    var regularThreads = threadUsages
                        .Where(t => t.Type.Equals("thread", StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    // Save to database fields (comma-separated)
                    model.Seq_Id = string.Join(", ", seqThreads.Select(t => t.ThreadId));
                    model.Seq_Usage = string.Join(", ", seqThreads.Select(t => t.Usage.ToString()));
                    model.T_Id = string.Join(", ", regularThreads.Select(t => t.ThreadId));
                    model.T_Usage = string.Join(", ", regularThreads.Select(t => t.Usage.ToString()));

                    if (model.Id > 0)
                    {
                        db.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Gen_Designs.Add(model);
                    }

                    db.SaveChanges();
                    return RedirectToAction("Design");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Reload necessary data if model is invalid
            var threadUsageData = db.Gen_ThreadUsage
                .Select(x => new
                {
                    x.ThreadId,
                    x.Type,
                    x.Usage
                })
                .ToList();

            ViewBag.Collections = threadUsageData
                .GroupBy(x => x.ThreadId)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = $"{g.Key} - {(g.First().Usage != null ? g.First().Usage.ToString() : "")}"
                })
                .ToList();

            ViewBag.DesignList = db.Gen_Designs.ToList();
            return View("Design", model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

    var model = db.Gen_Designs.Find(id);
    if (model == null) return HttpNotFound();

    // Load thread boxes based on component code
    if (model.Component_Code.HasValue) 
    {
        var threadUsages = db.Gen_ThreadUsage
            .Where(t => t.Code == model.Component_Code.Value)
            .ToList();

        ViewBag.ThreadBoxes = threadUsages
            .Where(t => t.Type.Equals("thread", StringComparison.OrdinalIgnoreCase))
            .Select(t => new DataBaseLibrary.Gen_ThreadUsage
            {
                ThreadId = t.ThreadId,
                Usage = t.Usage
            })
            .ToList();

        ViewBag.SeqBoxes = threadUsages
            .Where(t => t.Type.Equals("seq", StringComparison.OrdinalIgnoreCase))
            .Select(t => new DataBaseLibrary.Gen_ThreadUsage
            {
                ThreadId = t.ThreadId,
                Usage = t.Usage
            })
            .ToList();
    }


            // Load all available collections
            var allCollections = db.Gen_ThreadUsage
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ThreadId + " - " + c.Usage.ToString()
                })
                .ToList();

            ViewBag.Collections = allCollections;

            // Set the selected collections if they exist
            if (!string.IsNullOrEmpty(model.CollectionName))
            {
                var selectedCollectionIds = model.CollectionName.Split(',');
                ViewBag.SelectedCollections = selectedCollectionIds;
            }

            ViewBag.DesignList = db.Gen_Designs.ToList();
            return View("Design", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDesign(int id)
        {
            try
            {
                var toDelete = db.Gen_Designs.Find(id);
                if (toDelete != null)
                {
                    db.Gen_Designs.Remove(toDelete);
                    db.SaveChanges();
                }
                return RedirectToAction("Design");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Delete Error: " + ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}












//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Diagnostics;
//using System.Linq;
//using System.Net;
//using System.Web.Mvc;
//using System.Web.UI.WebControls;
//using DataBaseLibrary;
//using EMB_App.Models;


//namespace EMB_App.Controllers
//{
//    public class Gen_DesignController : Controller
//    {
//        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2();

//        // GET: Gen_Design
//        public ActionResult Design()
//        {
//            // Get all valid codes from ThreadUsage (as int? to match Component_Code)
//            var validCodes = db.Gen_ThreadUsage
//                .Where(x => x.Code != null)
//                .Select(x => x.Code) // Code is int? which matches Component_Code
//                .Distinct()
//                .ToList();

//            // Get designs and determine their status
//            var designList = db.Gen_Designs.ToList();
//            foreach (var design in designList)
//            {
//                if (!design.Component_Code.HasValue)
//                {
//                    // If Component_Code is null, set Status to false (Dis Active)
//                    design.Status = false;
//                }
//                else
//                {
//                    // Check if Component_Code exists in ThreadUsage codes
//                    design.Status = validCodes.Contains(design.Component_Code.Value);
//                }
//            }

//            // Thread usage data for collections dropdown
//            var threadUsageData = db.Gen_ThreadUsage
//                .Select(x => new
//                {
//                    x.ThreadId,
//                    x.Type,
//                    x.Usage
//                })
//                .ToList();

//            ViewBag.Collections = threadUsageData
//                .GroupBy(x => x.ThreadId)
//                .Select(g => new SelectListItem
//                {
//                    Value = g.Key.ToString(),
//                    Text = $"{g.Key} - {(g.First().Usage != null ? g.First().Usage.ToString() : "")}"
//                })
//                .ToList();

//            ViewBag.DesignList = designList;
//            ViewBag.ValidCodes = validCodes;

//            return View(new DataBaseLibrary.Gen_Designs());
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult SaveDesign(DataBaseLibrary.Gen_Designs model, List<string> SelectedCollections)
//        {
//            if (ModelState.IsValid)
//            {
//                model.CollectionName = SelectedCollections != null ? string.Join(",", SelectedCollections) : null;
//                try
//                {
//                    // Get all thread usages
//                    var threadUsages = db.Gen_ThreadUsage
//                        .Where(t => t.Code == model.Component_Code.Value)
//                        .ToList();

//                    // Group by type and store in ViewBag
//                    var seqThreads = threadUsages
//                        .Where(t => t.Type.Equals("seq", StringComparison.OrdinalIgnoreCase))
//                        .ToList();

//                    var regularThreads = threadUsages
//                        .Where(t => t.Type.Equals("thread", StringComparison.OrdinalIgnoreCase))
//                        .ToList();

//                    // Save to database fields (comma-separated)
//                    model.Seq_Id = string.Join(", ", seqThreads.Select(t => t.ThreadId));
//                    model.Seq_Usage = string.Join(", ", seqThreads.Select(t => t.Usage.ToString())); // Store individual usages
//                    model.T_Id = string.Join(", ", regularThreads.Select(t => t.ThreadId));
//                    model.T_Usage = string.Join(", ", regularThreads.Select(t => t.Usage.ToString())); // Store individual usages

//                    db.Gen_Designs.Add(model);
//                    db.SaveChanges();
//                    return RedirectToAction("Design");
//                }
//                catch (Exception ex)
//                {
//                    ModelState.AddModelError("", ex.Message);
//                }
//            }

//            ViewBag.Collections = db.Gen_ThreadUsage
//                .Select(c => new SelectListItem
//                {
//                    Value = c.Id.ToString(),
//                    Text = $"{c.ThreadId} - {c.Usage}"
//                })
//                .ToList();

//        // Reload thread data for the view
//            ViewBag.DesignList = db.Gen_Designs.ToList();
//            return View("Design", model);
//        }

//        public ActionResult Edit(int? id)
//        {
//            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

//            var model = db.Gen_Designs.Find(id);
//            if (model == null) return HttpNotFound();

//            // Load all available collections
//            var allCollections = db.Gen_ThreadUsage
//                .Select(c => new SelectListItem
//                {
//                    Value = c.Id.ToString(),
//                    Text = c.ThreadId + " - " + c.Usage.ToString()
//                })
//                .ToList();

//            ViewBag.Collections = allCollections;

//            // Set the selected collections if they exist
//            if (!string.IsNullOrEmpty(model.CollectionName))
//            {
//                var selectedCollectionIds = model.CollectionName.Split(',');
//                ViewBag.SelectedCollections = selectedCollectionIds;
//            }

//            ViewBag.DesignList = db.Gen_Designs.ToList();
//            return View("Design", model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteDesign(int id)
//        {
//            try
//            {
//                var toDelete = db.Gen_Designs.Find(id);
//                if (toDelete != null)
//                {
//                    db.Gen_Designs.Remove(toDelete);
//                    db.SaveChanges();
//                }
//                return RedirectToAction("Design");
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("Delete Error: " + ex);
//                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
//            }
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing) db.Dispose();
//            base.Dispose(disposing);
//        }
//    }
//}











