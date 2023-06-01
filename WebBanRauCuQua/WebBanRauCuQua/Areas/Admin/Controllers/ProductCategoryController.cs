﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanRauCuQua.Models;
using WebBanRauCuQua.Models.EF;

namespace WebBanRauCuQua.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var items = db.ProductCategories;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanRauCuQua.Models.Common.Filter.FilterChar(model.Title);
                db.ProductCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}