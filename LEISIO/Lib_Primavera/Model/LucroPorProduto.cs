using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEISIO.Lib_Primavera.Model
{
    public class LucroPorProduto
    {
        public string NomeProduto
        {
            get;
            set;
        }

        public double Lucro
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