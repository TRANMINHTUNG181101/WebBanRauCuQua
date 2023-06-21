using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanRauCuQua.Models;
using WebBanRauCuQua.Models.EF;

namespace WebBanRauCuQua.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(int? page)
        {
            var pageSize = 8;
            if (page == null)
            {
                page = 1;
            }

            IEnumerable<Product> items = db.Products.ToList();
            var pageIndex = page.HasValue
                ? Convert.ToInt32(page)
                : 1;

            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Detail(string alias, int id)
        {
            var items = db.Products.Find(id);

            // xu ly dem so luot truy cap
            if (items != null)
            {
                db.Products.Attach(items);
                items.ViewCount = items.ViewCount + 1;
                db.Entry(items).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            return View(items);
        }

        public ActionResult ProductCategory(string alias, int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSellers()
        {
            var items = db.Products.Where(x => x.IsHot && x.IsActive).Take(6).ToList();
            return PartialView(items);
        }
    }
}