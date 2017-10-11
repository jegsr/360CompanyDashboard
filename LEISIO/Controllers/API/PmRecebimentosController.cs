using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class PmRecebimentosController : ApiController
    {
      

        //GET: /api/PmRecebimentos?dataIn=dataIn={mes}&dataFm={ano}
        public Lib_Primavera.Model.PmRecebimentos Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.PrazoMedioRecebimento(dataIn, dataFm);
        }
    }
}
