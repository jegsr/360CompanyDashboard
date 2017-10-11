using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class PmPagamentosController : ApiController
    {


        //GET: /api/PmPagamentos?dataIn='YYYY/MM/DD'&dataFm = 'YYYY/MM/DD'
        /// <summary>
        /// Valor em dias do prazo médio de pagamentos.
        /// </summary>
        /// <param name="dataIn">Data de Inicio no formato YYYY/MM/DD</param>
        /// <param name="dataFm">Data de Fim no formato YYYY/MM/DD</param>
        /// <returns></returns>
        public Lib_Primavera.Model.PmPagamentos Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.PrazoMedioPagamentos(dataIn, dataFm);
        }
    }
}
