using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class TotalCustosController : ApiController
    {
        
        //GET: /api/TotalCustos?mes=<Valor_mes>&ano=<Valor_ano>
        public List<Lib_Primavera.Model.TotalCustos> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.TotalCustos(dataIn, dataFm);
        }
    }
}
