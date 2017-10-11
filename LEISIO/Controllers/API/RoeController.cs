using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class RoeController : ApiController
    {
       

        //GET: /api/Roe?dataIn={mes}&dataFm={ano}
        public Lib_Primavera.Model.Roe Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.Roe(dataIn, dataFm);
        }

    }
}
