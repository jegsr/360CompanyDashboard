using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class ValorGanhoClienteController : ApiController
    {
       

        //GET: /api/ValorGanhoCliente?dataIn={mes}&dataFm={ano}
        public IEnumerable<Lib_Primavera.Model.ValorGanhoCliente> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.ValorGanhoCliente(dataIn, dataFm);
        }
    }
}
