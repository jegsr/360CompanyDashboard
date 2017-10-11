﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEISIO.Controllers
{
    public class ComprasController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadViewPDF()
        {
            return new Rotativa.MVC.ViewAsPdf("Index")
            {
                FileName = "Compras.pdf"
            };
        }
    }
}