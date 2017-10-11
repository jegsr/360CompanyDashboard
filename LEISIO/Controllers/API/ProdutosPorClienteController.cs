using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class ProdutosPorClienteController : ApiController
    {
     

        //GET: /api/ProdutosPorCliente?dataIn={mes}&dataFm={ano}
        public IEnumerable<Lib_Primavera.Model.ProdutosPorCliente> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.ProdutosPorCliente(dataIn, dataFm);
        }
    }
}
