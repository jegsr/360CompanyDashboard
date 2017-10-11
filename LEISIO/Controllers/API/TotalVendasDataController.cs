using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class TotalVendasDataController : ApiController
    {
      
        //GET: /api/TotalVendasData?dataIn={mes}&dataFm={ano}
        public IEnumerable<Lib_Primavera.Model.TotalVendasData> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.TotalVendasData(dataIn, dataFm);
        }
    }
}
