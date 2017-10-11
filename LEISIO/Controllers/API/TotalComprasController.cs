using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class TotalComprasController : ApiController
    {
        
        //GET: /api/TotalCompras?dataIn={mes}&dataFm={ano}
        public Lib_Primavera.Model.TotalCompras Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.TotalCompras(dataIn, dataFm);
        }


    }
}
