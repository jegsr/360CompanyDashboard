using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEISIO.Lib_Primavera.Model
{
    /*
     Total de gasto em cada Produto*/
    public class CustosPorProduto
    {
        public double Total
        {
            get;
            set;
        }

        public string NomeProduto
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