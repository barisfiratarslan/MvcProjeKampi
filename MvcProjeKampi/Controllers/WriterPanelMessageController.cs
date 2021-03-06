using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();

        public ActionResult Inbox()
        {
            string mail = (string)Session["WriterMail"];
            var messageList = messageManager.GetListInbox(mail);
            return View(messageList);
        }

        public ActionResult Sendbox()
        {
            string mail = (string)Session["WriterMail"];
            var messageList = messageManager.GetListSendbox(mail);
            return View(messageList);
        }

        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = messageManager.GetByID(id);
            values.MessageIsReaden = true;
            messageManager.MessageUpdate(values);
            return View(values);
        }

        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = messageManager.GetByID(id);
            values.MessageIsReaden = true;
            messageManager.MessageUpdate(values);
            return View(values);
        }

        public PartialViewResult MessageListMenu()
        {
            string mail = (string)Session["WriterMail"];
            ViewBag.inbox = messageManager.GetUnReadenInboxNumber(mail);
            ViewBag.sendbox = messageManager.GetUnReadenSendboxNumber(mail);
            return PartialView();
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            string sender = (string)Session["WriterMail"];
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                message.SenderMail = sender;
                messageManager.MessageAdd(message);
                return RedirectToAction("Sendbox");
            }
            foreach (var item in results.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();
        }

        public ActionResult Readen(int id)
        {
            var values = messageManager.GetByID(id);
            values.MessageIsReaden = true;
            messageManager.MessageUpdate(values);
            return RedirectToAction("Inbox");
        }

        public ActionResult UnReaden(int id)
        {
            var values = messageManager.GetByID(id);
            values.MessageIsReaden = false;
            messageManager.MessageUpdate(values);
            return RedirectToAction("Inbox");
        }

        public ActionResult SenReaden(int id)
        {
            var values = messageManager.GetByID(id);
            values.MessageIsReaden = true;
            messageManager.MessageUpdate(values);
            return RedirectToAction("Sendbox");
        }

        public ActionResult SenUnReaden(int id)
        {
            var values = messageManager.GetByID(id);
            values.MessageIsReaden = false;
            messageManager.MessageUpdate(values);
            return RedirectToAction("Sendbox");
        }
    }
}