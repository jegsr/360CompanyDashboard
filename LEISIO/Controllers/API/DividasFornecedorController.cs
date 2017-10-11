using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class DividasFornecedorController : ApiController
    {

        //Get:  api/DividasFornecedor?dataIn='YYYY/MM/DD'&dataFm = 'YYYY/MM/DD'
        /// <summary>
        /// Valor que corresponde a dividas que nós temos para com os Fornecedores.
        /// </summary>
        /// <param name="dataIn">Data de Inicio no formato YYYY/MM/DD</param>
        /// <param name="dataFm">Data de Fim no formato YYYY/MM/DD</param>
        /// <returns></returns>
        public IEnumerable<Lib_Primavera.Model.DividasFornecedor> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.ListarPagamentosPendentes(dataIn, dataFm);

        }

    }
}
