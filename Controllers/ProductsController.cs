using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Market.Models;

using System.IO;
namespace E_Market.Controllers
{





    public class ProductsController : Controller
    {


        private storeEntities db = new storeEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        public JsonResult GetGategoryName(string term)
        {
            List<string> allCategory;
            allCategory = db.Categories.Where(x => x.name.ToLower().StartsWith(term.ToLower())).Select(y => y.name).ToList();
            return Json(allCategory, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            List<Product> products;
            if (string.IsNullOrEmpty(searchTerm))
            {
                products = db.Products.Include(u => u.Category).ToList();
            }
            else
            {
                products = db.Products.Include(u => u.Category).Where(a => a.Category.name.ToLower().StartsWith(searchTerm.ToLower())).ToList();
            }
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "Id", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,price,image,description,category_id")] Product product, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                string path = "";
                if (imgFile.FileName.Length > 0)
                {
                    path = "~/images/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));

                }
                product.image = path;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.category_id = new SelectList(db.Categories, "Id", "name", product.category_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "Id", "name", product.category_id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,price,description,category_id")] Product product, HttpPostedFileBase imgFile)
        {
            string path = "";
            if (imgFile.FileName.Length > 0)
            {
                path = "~/images/" + Path.GetFileName(imgFile.FileName);
                imgFile.SaveAs(Server.MapPath(path));

            }

            product.image = path;
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Categories, "Id", "name", product.category_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int id)
        {
            var ProductInCart = new Cart();
            var product = db.Carts.SingleOrDefault(u => u.product_id == id);
            if (product != null)
            {
                return RedirectToAction("Index");
            }
            else
            {

                ProductInCart.product_id = id;
                ProductInCart.added_at = System.DateTime.Now;

                db.Carts.Add(ProductInCart);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }


}