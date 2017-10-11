using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class TopClientesController : ApiController
    {
       

        //GET: /api/TopClientes?dataIn={mes}&dataFm={ano}
        public IEnumerable<Lib_Primavera.Model.TopClientes> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.ListaTopClientes(dataIn, dataFm);
        }
    }
}
