using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class AuthorizationController : Controller
    {
        AdminManager adminManager = new AdminManager(new EfAdminDal());

        public ActionResult Index()
        {
            var adminValues = adminManager.GetList();
            return View(adminValues);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            List<SelectListItem> valueRole = new List<SelectListItem>()
            {
                new SelectListItem {
                    Text="A",
                    Value="A"
                },
                new SelectListItem
                {
                     Text="B",
                    Value="B"
                },
                new SelectListItem
                {
                     Text="C",
                    Value="C"
                },
                new SelectListItem
                {
                     Text="D",
                    Value="D"
                }
            };
            ViewBag.vr = valueRole;
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(AdminDto admin)
        {
            adminManager.AddAdmin(admin);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            List<SelectListItem> valueRole = new List<SelectListItem>()
            {
                new SelectListItem {
                    Text="A",
                    Value="A"
                },
                new SelectListItem
                {
                     Text="B",
                    Value="B"
                },
                new SelectListItem
                {
                     Text="C",
                    Value="C"
                },
                new SelectListItem
                {
                     Text="D",
                    Value="D"
                }
            };
            ViewBag.vr = valueRole;
            var adminValue = adminManager.GetByID(id);
            AdminDto dto = new AdminDto()
            {
                AdminRole = adminValue.AdminRole,
                UserName = adminValue.AdminUserName
            };
            return View(dto);
        }

        [HttpPost]
        public ActionResult EditAdmin(AdminDto admin)
        {
            adminManager.AdminUpdate(admin);
            return RedirectToAction("Index");
        }
    }
}