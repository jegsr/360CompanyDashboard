using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class TopFornecedoresController : ApiController
    {

        //GET: /api/TopFornecedores?mes=<Valor_mes>&ano=<Valor_ano>
        public IEnumerable<Lib_Primavera.Model.TopFornecedores> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.TopFornecedores(dataIn, dataFm);
        }
    }
}
