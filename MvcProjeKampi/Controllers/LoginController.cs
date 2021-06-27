using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminDto admin)
        {
            var adminUserInfo = adminManager.IsExist(admin);
            if (adminUserInfo)
            {
                FormsAuthentication.SetAuthCookie(admin.UserName, false);
                Session["AdminUserName"] = admin.UserName;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                return View();
            }
            //adminManager.AddAdmin(admin);
            //return View();
        }

        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterLogin(AdminDto admin)
        {
            var writerUserInfo = writerManager.WriterExist(admin);
            if (writerUserInfo != null)
            {
                FormsAuthentication.SetAuthCookie(admin.UserName, false);
                Session["WriterMail"] = writerUserInfo.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }
    }
}