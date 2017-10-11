﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class TopProdutoController : ApiController
    {
       

        //GET: /api/TopProdutos?dataIn={mes}&dataFm={ano}
        public IEnumerable<Lib_Primavera.Model.TopProdutos> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.ListaTopProduto(dataIn, dataFm);
        }
    }
}
