using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenLongNhat.Models
{
    public class ProductController : Controller
    {
        // GET: Product
        NWDataContext da = new NWDataContext();
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult ListProducts()
        {
            List<Product> dssp = da.Products.Select(s => s).ToList();
            return View(dssp);
        }

        public ActionResult Details(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }

        public ActionResult Create()
        {
            ViewData["LSP"] = new SelectList(da.Categories, "CategoryID", "CategoryName");
            ViewData["NCC"] = new SelectList(da.Suppliers, "SupplierID", "CompanyName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, Product p)
        {

            var tenSP = collection["ProductName"];
            if (String.IsNullOrEmpty(tenSP))
            {
                return this.Create();
            }
            p.CategoryID = int.Parse(collection["LSP"]);
            p.SupplierID = int.Parse(collection["NCC"]);
            da.Products.InsertOnSubmit(p);
            da.SubmitChanges();
            return RedirectToAction("ListProducts");

        }

        public ActionResult Delete(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }

        [HttpPost]
        public ActionResult Delete(FormCollection collection, int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            da.Products.DeleteOnSubmit(p);
            da.SubmitChanges();
            return RedirectToAction("ListProducts");
        }

        public ActionResult Edit(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            ViewData["LSP"] = new SelectList(da.Categories, "CategoryID", "CategoryName");
            ViewData["NCC"] = new SelectList(da.Suppliers, "SupplierID", "CompanyName");
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection, int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            p.ProductName = collection["ProductName"];
            p.CategoryID = int.Parse(collection["LSP"]);
            p.SupplierID = int.Parse(collection["NCC"]);
            p.UnitPrice = decimal.Parse(collection["UnitPrice"]);

            UpdateModel(p);
            da.SubmitChanges();
            return RedirectToAction("ListProducts");
        }
    }
}