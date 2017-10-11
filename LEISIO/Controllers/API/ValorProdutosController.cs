using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class ValorProdutosController : ApiController
    {
      

        //GET: /api/ValorProduto?dataIn={mes}&dataFm={ano}
        public IEnumerable<Lib_Primavera.Model.ValorProduto> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.ValorProdutos(dataIn, dataFm);
        }
    }
}
