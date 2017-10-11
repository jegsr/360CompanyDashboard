using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class ValorStockController : ApiController
    {
       


        //Get:  api/ValorStock?dataIn={mes}&dataFm={ano}
        public Lib_Primavera.Model.ValorStock Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.ValorStock(dataIn, dataFm);

        }
    }
}
