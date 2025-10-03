using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataBaseLibrary;
using EMB_App.Models;
using Newtonsoft.Json;

namespace EMB_App.Controllers
{
    public class Gen_StocksController : Controller
    {
        private readonly EMB_ProductionEntities2 _context;

        public Gen_StocksController()
        {
            _context = new EMB_ProductionEntities2();
        }
         
        // GET: Gen_Stocks
        public ActionResult Stock()
        {
            var stockList = new Gen_Stock();
            ViewBag.StockList = _context.Gen_Stock
               .Where(b => (bool)b.Deleted)
               .ToList();
            ViewBag.StockList = _context.Gen_Stock.ToList();
            var products2 = _context.Gen_Products.Select(p => new SelectListItem
            {
                Value = p.Prod_Name,
                Text = p.Prod_Name
            }).ToList();
            ViewBag.ProductName = products2;
            //ViewBag.ProdId = new SelectList(_db.Gen_Products, "Id", "Prod_Name"); 
            return View(stockList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveStock(Gen_Stock model, string StagedEntriesJson)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View("Stock", _context.Gen_Stock.ToList());
            }

            try
            {
                // Handle single entry or multiple staged entries
                if (!string.IsNullOrEmpty(StagedEntriesJson))
                {
                    // Process multiple staged entries
                    var stagedEntries = JsonConvert.DeserializeObject<List<Gen_Stock>>(StagedEntriesJson);

                    foreach (var entry in stagedEntries)
                    {
                        var stock = new Gen_Stock
                        {
                            BrandName = entry.BrandName,
                            VendorName = entry.VendorName,
                            ProductName = entry.ProductName,
                            StockStatus = entry.StockStatus,
                            MeasureValue = entry.MeasureValue,
                            Quantity = entry.Quantity,
                            Price = entry.Price,
                            BillNumber = entry.BillNumber,
                            GatePass = entry.GatePass,
                            StockDescription = entry.StockDescription,
                            RecordTimeStamp = entry.RecordTimeStamp,
                            Condition = entry.Condition,
                            Identity = entry.Identity,
                            CreatedDate = entry.CreatedDate
                        };

                        _context.Gen_Stock.Add(stock);
                    }
                }
                else
                {
                    // Process single entry (edit or new)
                    if (model.Id == 0)
                    {
                        // New entry
                        var stock = new Gen_Stock
                        {
                            BrandName = model.BrandName,
                            VendorName = model.VendorName,
                            ProductName = model.ProductName,
                            StockStatus = model.StockStatus,
                            MeasureValue = model.MeasureValue,
                            Quantity = model.Quantity,
                            Price = model.Price,
                            BillNumber = model.BillNumber,
                            GatePass = model.GatePass,
                            StockDescription = model.StockDescription,
                            RecordTimeStamp = model.RecordTimeStamp,
                            Condition = model.Condition,
                            Identity = model.Identity,
                            CreatedDate = model.CreatedDate
                        };

                        _context.Gen_Stock.Add(stock);
                    }
                    else
                    {
                        // Edit existing entry
                        var stockInDb = _context.Gen_Stock.Single(s => s.Id == model.Id);
                        stockInDb.BrandName = model.BrandName;
                        stockInDb.VendorName = model.VendorName;
                        stockInDb.ProductName = model.ProductName;
                        stockInDb.StockStatus = model.StockStatus;
                        stockInDb.MeasureValue = model.MeasureValue;
                        stockInDb.Quantity = model.Quantity;
                        stockInDb.Price = model.Price;
                        stockInDb.BillNumber = model.BillNumber;
                        stockInDb.GatePass = model.GatePass;
                        stockInDb.StockDescription = model.StockDescription;
                        stockInDb.RecordTimeStamp = model.RecordTimeStamp;
                        stockInDb.Condition = model.Condition;
                        stockInDb.Identity = model.Identity;
                    }
                }

                _context.SaveChanges();
                TempData["SuccessMessage"] = "Stock saved successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error saving stock: " + ex.Message;
            }

            return RedirectToAction("Stock");
        }
        // GET: Gen_Machines/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gen_Stock brand = _context.Gen_Stock.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductName = _context.Gen_Products.Select(p => new SelectListItem
            {
                Value = p.Prod_Name,
                Text = p.Prod_Name,
                Selected = p.Prod_Name == brand.ProductName
            }).ToList();
            ViewBag.StockList = _context.Gen_Stock.ToList();
            return View("Stock", brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
        "Id,BrandName,ProductName,VendorName,BillNumber,GatePass,StockStatus,Condition" +
            ",Identity,Price,Quantity,StockDescription,RecordTimeStamp")] Gen_Stock brand)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(brand).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Stock");
            }
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStock(int id)
        {
            try
            {
                var stockInDb = _context.Gen_Stock.Single(s => s.Id == id);
                _context.Gen_Stock.Remove(stockInDb);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Stock deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting stock: " + ex.Message;
            }

            return RedirectToAction("Stock");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}





//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Validation;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Web;
//using System.Web.Mvc;
//using DataBaseLibrary;

//namespace EMB_App.Controllers
//{
//    public class Gen_StocksController : Controller
//    {
//        private EMB_ProductionEntities2 db = new EMB_ProductionEntities2();

//        // GET: Gen_Stocks
//        public ActionResult Stock()
//        {
//            var model = new Gen_Stocks();
//            ViewBag.StockList = db.Gen_Stocks
//               .Where(b => b.Deleted)
//               .ToList();
//            ViewBag.StockList = db.Gen_Stocks.ToList();
//            return View(model);
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult SaveStock(Gen_Stocks model, string IsDeleteMode)
//        {
//            if (IsDeleteMode == "true")
//            {
//                var toDelete = db.Hr_Customers.Find(model.Id);
//                if (toDelete != null)
//                {
//                    db.Hr_Customers.Remove(toDelete);
//                }
//            }
//            else
//                {
//                    var entity = db.Gen_Stocks.Find(model.Id) ?? new Gen_Stocks();
//                entity.ProdId = model.ProdId;
//                entity.InStock = model.InStock;
//                entity.New = model.New;
//                entity.In = model.In;
//                entity.Out = model.Out;
//                entity.Rack = model.Rack;
//                entity.Shelf = model.Shelf;
//                entity.MeasureValue = model.MeasureValue;
//                entity.TotalStock = model.TotalStock;
//                entity.TotalPrice = model.TotalPrice;
//                entity.BillNumber = model.BillNumber;
//                entity.GatePass = model.GatePass;
//                entity.BranchId = model.BranchId;
//                entity.RecordTimeStamp =DateTime.Now;
//                entity.Active = model.Active;
//                entity.Deleted = model.Deleted;
//                entity.CreatedBy = model.CreatedBy;
//                entity.CreatedDate = entity.Id == 0 ? DateTime.Now : entity.CreatedDate;
//                entity.UpdatedBy = entity.UpdatedBy;
//                entity.UpdatedDate = DateTime.Now;
//                entity.StockDescription = model.StockDescription;
//                entity.Gen_Products = model.Gen_Products;

//                    if (entity.Id == 0)
//                    {
//                        db.Gen_Stocks.Add(entity);
//                    }
//                    else
//                    {
//                        db.Entry(entity).State = EntityState.Modified;
//                    }
//                }

//            db.SaveChanges();
//                return RedirectToAction("Stock");
//            }
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            Gen_Stocks model = db.Gen_Stocks.Find(id);
//            if (model == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.StockList = db.Gen_Stocks.ToList();
//            return View("Stock", model);
//        }
//      [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include =
//        "Id,ProdId,InStock,New, In,Out,StockDescription,Rack,Shelf,MeasureValue," +
//            "TotalStock,TotalPrice,BillNumber,GatePass,BranchId, Active,Deleted," +
//            "RecordTimeStamp,StockStatus,ProductName,Condition,BrandName,VendorName," +
//            "Identity,Price,Quantity")] Gen_Stocks brand)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(brand).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Stock");
//            }
//            return View(brand);
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteStock(int id)
//        {
//            var toDelete = db.Gen_Stocks.Find(id);
//            if (toDelete != null)
//            {
//                db.Gen_Stocks.Remove(toDelete);
//                db.SaveChanges();
//            }
//            return RedirectToAction("Stock");
//        }
//    }
//}
