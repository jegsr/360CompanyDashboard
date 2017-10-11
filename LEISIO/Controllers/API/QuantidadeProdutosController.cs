using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class QuantidadeProdutosController : ApiController
    {
        

        //GET: /api/QuantidadeProdutos?dataIn={mes}&dataFm={ano}
        public IEnumerable<Lib_Primavera.Model.QuantidadeProdutos> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.QuantidadeProdutos(dataIn, dataFm);
        }
    }
}
