using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace LEISIO.Controllers
{
    public class VendasController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult DownloadViewPDF()
        {
            return new Rotativa.MVC.ViewAsPdf("Index")
            {
                FileName = "Vendas.pdf"
            };
        }

    }
}
