using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class CustosPorProdutoController : ApiController
    {

        //GET: /api/CustosPorProduto?dataIn='YYYY/MM/DD'&dataFm = 'YYYY/MM/DD'
        public List<Lib_Primavera.Model.CustosPorProduto> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.CustosPorProduto(dataIn, dataFm);
        }
    }
}
