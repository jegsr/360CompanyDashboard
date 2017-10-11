using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class RoaController : ApiController
    {
       

        //GET: /api/Roa?dataIn={mes}&dataFm={ano}
        public Lib_Primavera.Model.Roa Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.Roa(dataIn, dataFm);
        }

    }
}
