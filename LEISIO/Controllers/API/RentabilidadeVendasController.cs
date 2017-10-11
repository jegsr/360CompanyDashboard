using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class RentabilidadeVendasController : ApiController
    {
     

        //GET: /api/PmRecebimentos?dataIn={mes}&dataFm={ano}
        public Lib_Primavera.Model.RentabilidadeVendas Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.RentabilidadeVendas(dataIn, dataFm);
        }
    }
}
