using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelContentController : Controller
    {
        ContentManager contentManager = new ContentManager(new EfContentDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());

        public ActionResult MyContent(string p)
        {
            p = (string)Session["WriterMail"];
            var writerIDInfo = writerManager.GetIDByMail(p);
            var contentValues = contentManager.GetListByWriter(writerIDInfo);
            return View(contentValues);
        }
    }
}