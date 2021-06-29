using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());

        [HttpGet]
        public ActionResult WriterProfile()
        {
            string p = (string)Session["WriterMail"];
            ViewBag.d = p;
            int id = writerManager.GetIDByMail(p);
            var writerValue = writerManager.GetByID(id);
            return View(writerValue);
        }

        [HttpPost]
        public ActionResult WriterProfile(Writer writer)
        {
            WriterValidator writerValidator = new WriterValidator();
            ValidationResult results = writerValidator.Validate(writer);
            if (results.IsValid)
            {
                writerManager.WriterUpdate(writer);
                return RedirectToAction("Index");
            }
            foreach (var item in results.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return RedirectToAction("AllHeading", "WriterPanel");
        }

        public ActionResult MyHeading(string p)
        {
            p = (string)Session["WriterMail"];
            var writerIDInfo = writerManager.GetIDByMail(p);
            var values = headingManager.GetListByWriter(writerIDInfo);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewHeading()
        {
            string deger = (string)Session["WriterMail"];
            ViewBag.m = deger;
            List<SelectListItem> valueCategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valueCategory;
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            string writerMailInfo = (string)Session["WriterMail"];
            var writerIDInfo = writerManager.GetIDByMail(writerMailInfo);
            ViewBag.d = writerIDInfo;
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.WriterID = 3;
            heading.HeadingStatus = true;
            headingManager.HeadingAdd(heading);
            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            var headingValue = headingManager.GetByID(id);
            List<SelectListItem> valueCategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valueCategory;
            return View(headingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            var head = headingManager.GetByID(heading.HeadingID);
            head.WriterID = 4;
            head.HeadingStatus = true;
            headingManager.HeadingUpdate(head);
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingValue = headingManager.GetByID(id);
            headingValue.HeadingStatus = false;
            headingManager.HeadingDelete(headingValue);
            return RedirectToAction("MyHeading");
        }

        public ActionResult AllHeading(int sayfa = 1)
        {
            var headings = headingManager.GetList().ToPagedList(sayfa, 4);
            return View(headings);
        }
    }
}