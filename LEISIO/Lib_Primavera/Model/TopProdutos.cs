using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEISIO.Lib_Primavera.Model
{
    /*
     Nome do Produto e Quantidade Vendida*/
    public class TopProdutos
    {
        public string CodProduto
        {
            get;
            set;
        }

        public string NomeProduto
        {
            get;
            set;
        }

        public double QuantidadeProduto
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