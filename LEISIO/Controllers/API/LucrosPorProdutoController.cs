using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class LucrosPorProdutoController : ApiController
    {
        //GET: /api/LucroPorProduto?dataIn='YYYY/MM/DD'&dataFm = 'YYYY/MM/DD'
        /// <summary>
        /// Lista de com o nome e correspondente lucro de todos os produtos do sistema.
        /// </summary>
        /// <param name="dataIn">Data de Inicio no formato YYYY/MM/DD</param>
        /// <param name="dataFm">Data de Fim no formato YYYY/MM/DD</param>
        /// <returns></returns>
        public IEnumerable<Lib_Primavera.Model.LucroPorProduto> Get(string dataIn, string dataFm)
        {
            return Lib_Primavera.PriIntegration.LucrosPorProduto(dataIn, dataFm);
        }
    }
}
