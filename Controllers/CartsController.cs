using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Market.Models;
using System.Collections.Generic;
using System.Web.Routing;

namespace E_Market.Controllers
{
    public class CartsController : Controller
    {
        private storeEntities db = new storeEntities();

        public ActionResult Index()
        {
            return PartialView(db.Carts.ToList());
        }
        public ActionResult RemoveItem(int id)
        {
            Cart cart = db.Carts.SingleOrDefault(u => u.product_id == id);

            db.Carts.Remove(cart);
            db.SaveChanges();

            return RedirectToAction("Index", "Product"); ;
        }
    }
}

//// GET: Carts
//public ActionResult Index()
//{
//    var carts = db.Carts.Include(c => c.Product);
//    return View(carts.ToList());
//}

//// GET: Carts/Details/5
//public ActionResult Details(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    Cart cart = db.Carts.Find(id);
//    if (cart == null)
//    {
//        return HttpNotFound();
//    }
//    return View(cart);
//}

//// GET: Carts/Create
//public ActionResult Create()
//{
//    ViewBag.product_id = new SelectList(db.Products, "Id", "name");
//    return View();
//}

//// POST: Carts/Create
//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Create([Bind(Include = "product_id,added_at")] Cart cart)
//{
//    if (ModelState.IsValid)
//    {
//        db.Carts.Add(cart);
//        db.SaveChanges();
//        return RedirectToAction("Index");
//    }

//    ViewBag.product_id = new SelectList(db.Products, "Id", "name", cart.product_id);
//    return View(cart);
//}

//// GET: Carts/Edit/5
//public ActionResult Edit(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    Cart cart = db.Carts.Find(id);
//    if (cart == null)
//    {
//        return HttpNotFound();
//    }
//    ViewBag.product_id = new SelectList(db.Products, "Id", "name", cart.product_id);
//    return View(cart);
//}

//// POST: Carts/Edit/5
//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Edit([Bind(Include = "product_id,added_at")] Cart cart)
//{
//    if (ModelState.IsValid)
//    {
//        db.Entry(cart).State = EntityState.Modified;
//        db.SaveChanges();
//        return RedirectToAction("Index");
//    }
//    ViewBag.product_id = new SelectList(db.Products, "Id", "name", cart.product_id);
//    return View(cart);
//}

//// GET: Carts/Delete/5
//public ActionResult Delete(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    Cart cart = db.Carts.Find(id);
//    if (cart == null)
//    {
//        return HttpNotFound();
//    }
//    return View(cart);
//}

//// POST: Carts/Delete/5
//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public ActionResult DeleteConfirmed(int id)
//{
//    Cart cart = db.Carts.Find(id);
//    db.Carts.Remove(cart);
//    db.SaveChanges();
//    return RedirectToAction("Index");
//}

//protected override void Dispose(bool disposing)
//{
//    if (disposing)
//    {
//        db.Dispose();
//    }
//    base.Dispose(disposing);
//}