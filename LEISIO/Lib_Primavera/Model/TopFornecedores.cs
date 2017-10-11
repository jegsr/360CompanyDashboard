using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEISIO.Lib_Primavera.Model
{
    /*
     Nome do Fornecedor e Quantidade que compramos, Os 5/6 maiores*/
    public class TopFornecedores
    {
        public string CodFornecedor
        {
            get;
            set;
        }

        public string NomeFornecedor
        {
            get;
            set;
        }

        public double QuantidadeComprada
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