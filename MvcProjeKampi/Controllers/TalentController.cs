using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class TalentController : Controller
    {
        TalentManager talentManager = new TalentManager(new EfTalentDal());
        AdminManager adminManager = new AdminManager(new EfAdminDal());

        public ActionResult Index()
        {
            string mail = (string)Session["AdminUserName"];
            var admin = adminManager.GetByName(mail);
            var talentValues = talentManager.GetTalentsByAdmin(admin.AdminID);
            return View(talentValues);
        }

        [HttpGet]
        public ActionResult AddTalent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTalent(Talent talent)
        {
            string mail = (string)Session["AdminUserName"];
            var admin = adminManager.GetByName(mail);
            talent.AdminID = admin.AdminID;
            talentManager.AddTalent(talent);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateTalent(int id)
        {
            var talent = talentManager.GetByID(id);
            return View(talent);
        }

        [HttpPost]
        public ActionResult UpdateTalent(Talent talent)
        {
            talentManager.UpdateTalent(talent);
            return RedirectToAction("Index");
        }
    }
}