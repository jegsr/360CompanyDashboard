using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEISIO.Lib_Primavera.Model
{
    /*Produto fornecido por cada Fornecedor(Lista)*/

    public class ProdutosPorFornecedor
    {
        public string Nome
        {
            get;
            set;
        }

        public string NomeProduto
        {
            get;
            set;
        }

        public double Quantidade
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
