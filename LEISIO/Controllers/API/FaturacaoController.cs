using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class FaturacaoController : ApiController
    {

        //GET: /api/Faturacao?dataIn='YYYY/MM/DD'&dataFm = 'YYYY/MM/DD'
        /// <summary>
        /// Valor da Facturação.
        /// </summary>
        /// <param name="dataIn">Data de Inicio no formato YYYY/MM/DD</param>
        /// <param name="dataFm">Data de Fim no formato YYYY/MM/DD</param>
        /// <returns></returns>
        public Lib_Primavera.Model.Faturacao Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.Faturacao(dataIn, dataFm);
        }

    }
}
