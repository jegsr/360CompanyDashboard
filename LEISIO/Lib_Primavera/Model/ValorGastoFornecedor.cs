using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEISIO.Lib_Primavera.Model
{
    /*Valor Gasto em Compras com cada Fornecedor*/
    public class ValorGastoFornecedor
    {
        public string NomeFornecedor
        {
            get;
            set;
        }

        public double ValorGasto
        {
            get;
            set;
        }

        public bool Sucess
        {
            get;
            set;
        }
    }
}