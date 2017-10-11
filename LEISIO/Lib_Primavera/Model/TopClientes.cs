using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEISIO.Lib_Primavera.Model
{
    /*
     Nome do Cliente e Quantidade por ele comprada*/
    public class TopClientes
    {
        public string CodCliente
        {
            get;
            set;
        }

        public string NomeCliente
        {
            get;
            set;
        }

        public double QuantidadeVendida
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