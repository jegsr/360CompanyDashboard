using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class TotalVendasController : ApiController
    {
       

        //GET: /api/TotalVendas?dataIn={mes}&dataFm={ano}
        public Lib_Primavera.Model.TotalVendas Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.TotalVendas(dataIn, dataFm);
        }
    }
}
