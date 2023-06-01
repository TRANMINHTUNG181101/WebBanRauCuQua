using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanRauCuQua.Models;

namespace WebBanRauCuQua.Controllers
{
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contact
        public ActionResult Index(string id)
        {
            //var item = db.Contacts
            return View();
        }
    }
}