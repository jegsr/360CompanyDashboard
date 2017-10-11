using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.StdBE800;
using Interop.GcpBE800;
using Interop.ErpBS800;

namespace LEISIO.Lib_Primavera
{
    public class PriIntegration
    {

        /*
         Método que retorna o Número Contribuinte de um dado Cliente a partir do Nome de Cliente passado ao método.*/
        public static String NifByClientName(String name)
        {

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {
                StdBELista objList;

                //Vai a tabela Clientes procurar por um Cliente que tenha o mesmo Nome que o nome passado para o método e que retorna o Número de Contribuinte.
                objList = PriEngine.Engine.Consulta("SELECT dbo.Clientes.NumContrib FROM dbo.Clientes WHERE dbo.Clientes.Nome ='" + name + "'");

                return objList.Valor("NumContrib");
            }

            return null;
        }




        /*
                Método que retorna um array com o total de dinheiro ganho com as vendas divido por mes do ano*/
        public static List<Model.TotalVendasData> TotalVendasData(string dataIn, string dataFm)
        {
            StdBELista objList;


            List<Model.TotalVendasData> listArts = new List<Model.TotalVendasData>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {
                // CONVERT(VARCHAR(7), dbo.LinhasDoc.DataSaida, 111) -> Converter a data para o formato YYYY/MM/DD e pegar apenas nos 7 primeiros Char para ficar com apenas ano e mes
                objList = PriEngine.Engine.Consulta("SELECT  CONVERT(VARCHAR(7), dbo.CabecDoc.Data, 111) as Data,SUM(dbo.CabecDoc.TotalMerc + dbo.CabecDoc.TotalIva + dbo.CabecDoc.TotalOutros + dbo.CabecDoc.TotalDesc) as ValorTotal From dbo.CabecDoc Where TipoDoc = 'FA' AND CONVERT(VARCHAR(10), dbo.CabecDoc.Data, 111) BETWEEN " + dataIn + "AND" + dataFm + "GROUP BY CONVERT(VARCHAR(7), dbo.CabecDoc.Data, 111)");


                while (!objList.NoFim())
                {
                    Model.TotalVendasData art = new Model.TotalVendasData();
                    art.Data = objList.Valor("Data");
                    art.Total = objList.Valor("ValorTotal");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();

                }


                return listArts;
            }
            else
            {
                return null;
            }
        }

        /*
         Método que retorna um array com valor comercial dos produtos existentes em stock*/
        public static List<Model.ValorStockData> ValorStockData(string dataIn, string dataFm)
        {
            StdBELista objList;


            List<Model.ValorStockData> listArts = new List<Model.ValorStockData>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT a.Data , SUM(a.ValorStock) as ValorStock FROM (SELECT dbo.Artigo.Artigo, CONVERT(VARCHAR(7),dbo.LinhasSTK.Data, 111) as Data,  SUM(dbo.Artigo.STKActual)/COUNT(*) * SUM(dbo.Artigo.PCMedio)/COUNT(*) as ValorStock   FROM dbo.LinhasSTK  INNER JOIN dbo.Artigo ON dbo.Artigo.Artigo = dbo.LinhasSTK.Artigo WHERE (dbo.LinhasSTK.TipoDoc = 'SI' OR dbo.LinhasSTK.TipoDoc = 'ES') AND CONVERT(VARCHAR(10),dbo.LinhasSTK.Data, 111) BETWEEN " + dataIn + "AND" + dataFm + "GROUP BY dbo.Artigo.Artigo, CONVERT(VARCHAR(7),dbo.LinhasSTK.Data, 111)) a GROUP BY a.Data");


                while (!objList.NoFim())
                {
                    Model.ValorStockData art = new Model.ValorStockData();
                    art.Data = objList.Valor("Data");
                    art.Total = objList.Valor("ValorStock");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();

                }


                return listArts;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Lista de Top Produtos de determinado mes de um ano.
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static List<Model.LucroPorProduto> LucrosPorProduto(string dataIn, string dataFm)
        {
            StdBELista objList;


            List<Model.LucroPorProduto> listArts = new List<Model.LucroPorProduto>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT dbo.LinhasDoc.Descricao,SUM((dbo.LinhasDoc.PrecUnit-dbo.Artigo.PCMedio)*dbo.LinhasDoc.Quantidade) as ValorStock FROM dbo.CabecDoc INNER JOIN dbo.LinhasDoc ON dbo.CabecDoc.Id = dbo.LinhasDoc.IdCabecDoc RIGHT JOIN dbo.Artigo ON dbo.Artigo.Descricao = dbo.LinhasDoc.Descricao Where TipoDoc = 'FA' AND  CONVERT(VARCHAR(10),dbo.LinhasDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + " GROUP BY dbo.LinhasDoc.Descricao");


                while (!objList.NoFim())
                {
                    Model.LucroPorProduto art = new Model.LucroPorProduto();
                    art.NomeProduto = objList.Valor("Descricao");
                    art.Lucro = objList.Valor("ValorStock");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();

                }


                return listArts;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// Lista de Top Produtos de determinado mes de um ano.
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static List<Model.ProdutosPorCliente> ProdutosPorCliente(string dataIn, string dataFm)
        {
            StdBELista objList;


            List<Model.ProdutosPorCliente> listArts = new List<Model.ProdutosPorCliente>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT dbo.CabecDoc.Nome,dbo.LinhasDoc.Descricao, SUM(dbo.LinhasDoc.Quantidade) as Quantidade  FROM dbo.CabecDoc INNER JOIN dbo.LinhasDoc ON dbo.CabecDoc.Id = dbo.LinhasDoc.IdCabecDoc RIGHT JOIN dbo.Artigo ON dbo.Artigo.Descricao = dbo.LinhasDoc.Descricao Where TipoDoc = 'FA' AND  CONVERT(VARCHAR(10),dbo.CabecDoc.Data,111)  BETWEEN " + dataIn + " AND " + dataFm + " GROUP BY dbo.CabecDoc.Nome,dbo.LinhasDoc.Descricao ORDER BY dbo.CabecDoc.Nome ASC");


                while (!objList.NoFim())
                {
                    Model.ProdutosPorCliente art = new Model.ProdutosPorCliente();
                    art.NomeProduto = objList.Valor("Descricao");
                    art.Nome = objList.Valor("Nome");
                    art.Quantidade = (objList.Valor("Quantidade") < 0) ? objList.Valor("Quantidade") * -1 : objList.Valor("Quantidade");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();

                }


                return listArts;
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// Lista de Top Produtos de determinado mes de um ano.
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static List<Model.ValorGanhoCliente> ValorGanhoCliente(string dataIn, string dataFm)
        {
            StdBELista objList;


            List<Model.ValorGanhoCliente> listArts = new List<Model.ValorGanhoCliente>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT dbo.CabecDoc.Nome,SUM(dbo.CabecDoc.TotalMerc + dbo.CabecDoc.TotalIva + dbo.CabecDoc.TotalOutros + dbo.CabecDoc.TotalDesc) as ValorLucro  FROM dbo.CabecDoc Where TipoDoc = 'FA' AND  CONVERT(VARCHAR(10),dbo.CabecDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + "GROUP BY dbo.CabecDoc.Nome");


                while (!objList.NoFim())
                {
                    Model.ValorGanhoCliente art = new Model.ValorGanhoCliente();
                    art.Valor = objList.Valor("ValorLucro");
                    art.NomeCliente = objList.Valor("Nome");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();

                }


                return listArts;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Lista de Top Produtos de determinado mes de um ano.
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static List<Model.QuantidadeProdutos> QuantidadeProdutos(string dataIn, string dataFm)
        {
            StdBELista objList;


            List<Model.QuantidadeProdutos> listArts = new List<Model.QuantidadeProdutos>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("Select dbo.Artigo.Descricao, dbo.Artigo.STKActual as Quantidade FROM dbo.Artigo INNER JOIN dbo.LinhasSTK ON dbo.LinhasSTK.Artigo = dbo.Artigo.Artigo WHERE (dbo.LinhasSTK.TipoDoc = 'SI' OR dbo.LinhasSTK.TipoDoc = 'ES') AND CONVERT(VARCHAR(10),dbo.LinhasSTK.Data,111) BETWEEN " + dataIn + " AND " + dataFm + " GROUP BY dbo.Artigo.Descricao,dbo.Artigo.STKActual  ORDER BY Quantidade Desc ");


                while (!objList.NoFim())
                {
                    Model.QuantidadeProdutos art = new Model.QuantidadeProdutos();
                    art.NomeProduto = objList.Valor("Descricao");
                    art.Quantidade = objList.Valor("Quantidade");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();

                }


                return listArts;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Lista de Top Produtos de determinado mes de um ano.
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static List<Model.ValorProduto> ValorProdutos(string dataIn, string dataFm)
        {
            StdBELista objList;


            List<Model.ValorProduto> listArts = new List<Model.ValorProduto>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT dbo.Artigo.Descricao,  SUM(dbo.Artigo.STKActual)/COUNT(*) * SUM(dbo.Artigo.PCMedio)/COUNT(*) as ValorStock   FROM dbo.LinhasSTK  INNER JOIN dbo.Artigo ON dbo.Artigo.Artigo = dbo.LinhasSTK.Artigo WHERE (dbo.LinhasSTK.TipoDoc = 'SI' OR dbo.LinhasSTK.TipoDoc = 'ES') AND CONVERT(VARCHAR(10),dbo.LinhasSTK.Data, 111) BETWEEN " + dataIn + " AND " + dataFm + "  GROUP BY dbo.Artigo.Descricao ORDER BY ValorStock DESC");


                while (!objList.NoFim())
                {
                    Model.ValorProduto art = new Model.ValorProduto();
                    art.NomeProduto = objList.Valor("Descricao");
                    art.Valor = objList.Valor("ValorStock");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();

                }


                return listArts;
            }
            else
            {
                return null;
            }
        }




        /*
         * com mes e ano
         * 
         */
        public static List<Model.DividasCliente> ListarRecebimentosPendentes(string dataIn, string dataFm)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.DividasCliente rec = new Model.DividasCliente();
            List<Model.DividasCliente> listPags = new List<Model.DividasCliente>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT dbo.Pendentes.Serie,dbo.Pendentes.DataDoc,  dbo.Pendentes.ValorPendente, dbo.Clientes.Nome, dbo.Pendentes.NumDoc FROM dbo.Pendentes INNER JOIN dbo.Clientes ON dbo.Pendentes.Entidade = dbo.Clientes.Cliente  WHERE CONVERT(VARCHAR(10),DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + "");


                while (!objList.NoFim())
                {
                    try
                    {
                        rec = new Model.DividasCliente();

                        rec.Nome = objList.Valor("Nome");
                        rec.Serie = objList.Valor("Serie");
                        rec.Valor = Math.Round(objList.Valor("ValorPendente"), 2);
                        rec.NumDocumento = objList.Valor("NumDoc");
                        rec.Sucess = true;
                        listPags.Add(rec);
                        objList.Seguinte();
                    }
                    catch (Exception e)
                    {
                        rec.Sucess = false;
                    }

                }

                return listPags;

            }
            else
            {
                return null;

            }
        }




        /*
               * com mes e ano
               * 
               */
        public static List<Model.DividasFornecedor> ListarPagamentosPendentes(string dataIn, string dataFm)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.DividasFornecedor pag = new Model.DividasFornecedor();
            List<Model.DividasFornecedor> listPags = new List<Model.DividasFornecedor>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {


                objList = PriEngine.Engine.Consulta("SELECT dbo.Pendentes.Serie, dbo.Pendentes.DataDoc,  dbo.Pendentes.ValorPendente, dbo.Fornecedores.Nome, dbo.Pendentes.NumDoc FROM dbo.Pendentes INNER JOIN dbo.Fornecedores ON dbo.Pendentes.Entidade = dbo.Fornecedores.Fornecedor WHERE CONVERT(VARCHAR(10),DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + "");

                while (!objList.NoFim())
                {
                    try
                    {
                        pag = new Model.DividasFornecedor();

                        pag.Nome = objList.Valor("Nome");
                        pag.Serie = objList.Valor("Serie");
                        pag.Valor = -1 * Math.Round(objList.Valor("ValorPendente"), 2);
                        pag.NumDocumento = objList.Valor("NumDoc");
                        pag.Sucess = true;
                        listPags.Add(pag);
                        objList.Seguinte();
                    }
                    catch (Exception)
                    {
                        pag.Sucess = false;
                    }


                }

                return listPags;

            }
            else
            {
                return null;

            }

        }

        /// <summary>
        /// Valor total de stock num determinado mes de um ano
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static Model.ValorStock ValorStock(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.ValorStock art = new Model.ValorStock();
            try
            {
                if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
                {

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.Artigo.STKActual * dbo.Artigo.PCMedio) AS TotalStock FROM dbo.Artigo WHERE CONVERT(VARCHAR(10),dbo.Artigo.DataUltEntrada,111) BETWEEN " + dataIn + " AND " + dataFm + "");

                    art.Valor_Stock = objList.Valor("TotalStock");
                    art.Sucess = true;
                    return art;
                }
                else
                {
                    art.Sucess = false;
                    return null;
                }
            }
            catch (Exception ex)
            {
                art.Sucess = false;
                return null;
            }
        }
        /*
        Método que retorna o valor total feito com as compras de produtos*/
        public static Model.TotalCompras TotalCompras(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.TotalCompras art = new Model.TotalCompras();
            try
            {
                if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
                {

                    objList = PriEngine.Engine.Consulta("SELECT SUM(-1*(dbo.CabecCompras.TotalMerc + dbo.CabecCompras.TotalIva + dbo.CabecCompras.TotalOutros + dbo.CabecCompras.TotalDespesasAdicionais - dbo.CabecCompras.TotalDesc)) AS TotalCompras FROM dbo.CabecCompras Where TipoDoc = 'VFA' and CONVERT(VARCHAR(10),dbo.CabecCompras.DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + "");

                    art.ValorTotalCompras = objList.Valor("TotalCompras");
                    art.Sucess = true;
                    return art;
                }
                else
                {
                    art.Sucess = false;
                    return null;
                }
            }
            catch (Exception e)
            {
                art.Sucess = false;
                return null;
            }
        }

        /// <summary>
        /// Dias que em media demora-mos a pagar aos Fornecedores, tendo em conta um determinado mes de um ano
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static Model.PmPagamentos PrazoMedioPagamentos(string dataIn, string dataFm)
        {

            StdBELista objList;

            Model.PmPagamentos art = new Model.PmPagamentos();


            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT dbo.Historico.DataDoc, dbo.Historico.DataLiq FROM dbo.Historico WHERE dbo.Historico.TipoEntidade = 'F' AND dbo.Historico.TipoDoc = 'VFA' AND CONVERT(VARCHAR(10),dbo.Historico.DataLiq,111) BETWEEN " + dataIn + " AND " + dataFm + "");


                double somaDifDays = 0;
                int contador = 0;
                while (!objList.NoFim())
                {
                    DateTime tmpDataDoc = objList.Valor("DataDoc");

                    try
                    {
                        DateTime tmpDataLiq = objList.Valor("DataLiq");

                        somaDifDays += (Convert.ToDouble(tmpDataLiq.Day.ToString()) - Convert.ToDouble(tmpDataDoc.Day.ToString()));
                        contador++;
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
                    {
                        somaDifDays += 0;
                    }

                    objList.Seguinte();

                }

                art.ValorPmPagamentos = somaDifDays / contador;
                art.Sucess = true;

                return art;

            }
            else
            {
                art.Sucess = false;
                return null;
            }

        }

        /// <summary>
        /// Valor total de vendas de um determinado mes de um ano
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static Model.TotalVendas TotalVendas(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.TotalVendas art = new Model.TotalVendas();
            try
            {
                if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
                {

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.CabecDoc.TotalMerc + dbo.CabecDoc.TotalIva + dbo.CabecDoc.TotalOutros - dbo.CabecDoc.TotalDesc) AS TotalVendas FROM dbo.CabecDoc Where TipoDoc = 'FA' and CONVERT(VARCHAR(10),dbo.CabecDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + "");

                    art.ValorTotalVendas = objList.Valor("TotalVendas");
                    art.Sucess = true;
                    return art;
                }
                else
                {
                    art.Sucess = false;
                    return null;
                }
            }
            catch (Exception e)
            {
                art.Sucess = false;
                return null;
            }
        }

        /// <summary>
        /// Dias que em media os Clientes demoram a pagar, tendo em conta um determinado mes de um ano
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static Model.PmRecebimentos PrazoMedioRecebimento(string dataIn, string dataFm)
        {

            StdBELista objList;

            Model.PmRecebimentos art = new Model.PmRecebimentos();


            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT dbo.Historico.DataDoc, dbo.Historico.DataLiq FROM dbo.Historico WHERE dbo.Historico.TipoEntidade = 'C' AND dbo.Historico.TipoDoc = 'FA' AND CONVERT(VARCHAR(10),dbo.Historico.DataLiq,111) BETWEEN " + dataIn + " AND " + dataFm + "");


                double somaDifDays = 0;
                int contador = 0;
                while (!objList.NoFim())
                {
                    DateTime tmpDataDoc = objList.Valor("DataDoc");

                    try
                    {
                        DateTime tmpDataLiq = objList.Valor("DataLiq");

                        somaDifDays += (Convert.ToDouble(tmpDataLiq.Day.ToString()) - Convert.ToDouble(tmpDataDoc.Day.ToString()));
                        contador++;
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
                    {
                        somaDifDays += 0;
                    }

                    objList.Seguinte();

                }

                art.ValorPmRecebimentos = somaDifDays / contador;
                art.Sucess = true;
                return art;

            }
            else
            {
                art.Sucess = false;
                return null;
            }

        }

        /// <summary>
        /// Valor Rentabilidade de Vendas de um determinado mes de um ano
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static Model.RentabilidadeVendas RentabilidadeVendas(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.RentabilidadeVendas art = new Model.RentabilidadeVendas();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT dbo.LinhasDoc.Descricao,SUM(dbo.LinhasDoc.PrecUnit - dbo.LinhasCompras.PrecUnit)/SUM(dbo.LinhasCompras.Quantidade * dbo.LinhasCompras.PrecUnit) AS Rent FROM dbo.LinhasDoc LEFT JOIN dbo.LinhasCompras ON dbo.LinhasDoc.Artigo = dbo.LinhasCompras.Artigo INNER JOIN dbo.Artigo ON dbo.Artigo.Descricao = dbo.LinhasDoc.Descricao WHERE CONVERT(VARCHAR(10),dbo.LinhasDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + " GROUP BY dbo.LinhasDoc.Descricao");

                double countRentabilidade = 0;
                int count = 0;
                while (!objList.NoFim())
                {

                    try
                    {
                        countRentabilidade += Convert.ToDouble(objList.Valor("Rent"));
                        count++;
                    }
                    catch (FormatException e)
                    {
                        countRentabilidade += 0;
                    }


                    objList.Seguinte();

                }

                art.ValorRentabilidadeVendas = (countRentabilidade / count) * 100;
                art.Sucess = true;

                return art;

            }
            else
            {
                art.Sucess = false;
                return null;
            }

        }
        /*****************************************FINANCEIRO************************************
        NOTA :nas finanças uso o capital social como o valor de inicio da caixa. */
        /**
       *LiquidezGeral=ativo corrente/passivo corrente
       * 
       * Liquidez Geral=(valor total das Faturas passadas aos clientes-o que já paguei aos fornecedores)+ capitalSocial/o que ainda não paguei aos fornecedores
       * 
       */
        public static Model.LiquidezGeral LiquidezGeral(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.LiquidezGeral art = new Model.LiquidezGeral();
            try
            {



                if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
                {

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.CabecDoc.TotalMerc + dbo.CabecDoc.TotalIva + dbo.CabecDoc.TotalOutros - dbo.CabecDoc.TotalDesc) AS TotalVendas FROM dbo.CabecDoc Where TipoDoc = 'FA' and CONVERT(VARCHAR(10),dbo.CabecDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + "");

                    //valor total das Faturas passadas aos clientes
                    double faturasPassadasAosClientes = objList.Valor("TotalVendas");

                    objList = PriEngine.Engine.Consulta("SELECT SUM(-1*(dbo.CabecCompras.TotalMerc + dbo.CabecCompras.TotalIva + dbo.CabecCompras.TotalOutros + dbo.CabecCompras.TotalDespesasAdicionais - dbo.CabecCompras.TotalDesc)) AS TotalCompras FROM dbo.CabecCompras Where TipoDoc = 'VFA' and CONVERT(VARCHAR(10),dbo.CabecCompras.DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + "");

                    // valor total do que já paguei aos fornecedores
                    double oQpagamosAosFornecedores = objList.Valor("TotalCompras");

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.Pendentes.ValorTotal) AS Soma FROM dbo.Pendentes WHERE TipoDoc = 'VFA'  AND  CONVERT(VARCHAR(10),DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + "");

                    //valor total do que ainda não paguei aos fornecedores
                    double oQaindaNaoPagamosFornecedores = -1 * objList.Valor("Soma");

                    objList = PriEngine.Engine.Consulta("SELECT dbo.TMP_Empresas.ICCapitalSocial AS capital FROM dbo.TMP_Empresas");
                    //valor da capital social
                    double capitalSocial = objList.Valor("capital");

                    art.ValorLiquidezGeral = Math.Round(((faturasPassadasAosClientes - oQpagamosAosFornecedores) + capitalSocial) / oQaindaNaoPagamosFornecedores, 2);

                    art.Sucess = true;
                    return art;

                }
                else
                {

                    art.Sucess = false;
                    return art;
                }
            }
            catch (Exception e)
            {

                art.Sucess = false;
                return art;

            }

        }
        /*
        * faturacao =soma do  valor das faturas passadas aos clientes
        * 
        */
        public static Model.Faturacao Faturacao(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.Faturacao art = new Model.Faturacao();
            try
            {


                if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
                {

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.CabecDoc.TotalMerc + dbo.CabecDoc.TotalIva + dbo.CabecDoc.TotalOutros + dbo.CabecDoc.TotalDesc) AS Soma FROM dbo.CabecDoc Where TipoDoc = 'FA' and CONVERT(VARCHAR(10),dbo.CabecDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + "");

                    //valor total das Faturas passadas aos clientes
                    art.ValorFaturacao = Math.Round(objList.Valor("Soma"), 2);
                    art.Sucess = true;
                    return art;

                }
                else
                {
                    art.Sucess = false;
                    return art;
                }
            }
            catch (Exception e)
            {
                art.Sucess = false;
                return art;

            }

        }

        /*
         * Roe=resultado liquido/capital proprio
         * resultado liquido=(rendimentos(o que vendi)-gastos(apenas no q comprei e vendi))*0.75
         * capital proprio=capital social+(resultado liquido)
         * 
         */
        public static Model.Roe Roe(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.Roe art = new Model.Roe();
            try
            {


                if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
                {


                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.CabecDoc.TotalMerc + dbo.CabecDoc.TotalIva + dbo.CabecDoc.TotalOutros - dbo.CabecDoc.TotalDesc) AS TotalVendas FROM dbo.CabecDoc Where TipoDoc = 'FA' and CONVERT(VARCHAR(10),dbo.CabecDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + "");
                    //valor total do que vendi(rendimentos)
                    double rendimentos = objList.Valor("TotalVendas");

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.LinhasDoc.Quantidade * dbo.LinhasDoc.PCM ) AS Soma FROM dbo.LinhasDoc  WHERE CONVERT(VARCHAR(10),Data,111) BETWEEN " + dataIn + " AND " + dataFm + "");
                    //valor total de gastos(quanto custaram os produtos que já foram vendidos)
                    double gastos = objList.Valor("Soma");

                    double resultadoLiquido = (rendimentos - gastos) * 0.75;

                    objList = PriEngine.Engine.Consulta("SELECT dbo.TMP_Empresas.ICCapitalSocial AS capital FROM dbo.TMP_Empresas");
                    //valor da capital social
                    double capitalSocial = objList.Valor("capital");

                    double capitalProprio = capitalSocial + resultadoLiquido;

                    art.ValorRoe = Math.Round(resultadoLiquido / capitalProprio, 2);
                    art.Sucess = true;
                    return art;

                }
                else
                {
                    art.Sucess = false;
                    return art;
                }
            }
            catch (Exception e)
            {
                art.Sucess = false;
                return art;

            }

        }

        /*
       * Roa =Resultado liquido/ativo
       * Roa=(rendimentos(o que vendi)-gastos(apenas no q comprei e vendi)*0,75)/(valor total das Faturas passadas aos clientes-o que já paguei aos fornecedores)+ capital social
       * 
       */
        public static Model.Roa Roa(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.Roa art = new Model.Roa();
            try
            {


                if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
                {


                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.CabecDoc.TotalMerc + dbo.CabecDoc.TotalIva + dbo.CabecDoc.TotalOutros - dbo.CabecDoc.TotalDesc) AS TotalVendas FROM dbo.CabecDoc Where TipoDoc = 'FA' and CONVERT(VARCHAR(10),dbo.CabecDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + "");
                    //valor total do que vendi(rendimentos)
                    double rendimentos = objList.Valor("TotalVendas");

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.LinhasDoc.Quantidade * dbo.LinhasDoc.PCM ) AS Soma FROM dbo.LinhasDoc WHERE CONVERT(VARCHAR(10),Data,111) BETWEEN " + dataIn + " AND " + dataFm + "");
                    //valor total de gastos(quanto custaram os produtos que já foram vendidos)
                    double gastos = objList.Valor("Soma");

                    double resultadoLiquido = (rendimentos - gastos) * 0.75;

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.CabecDoc.TotalMerc + dbo.CabecDoc.TotalIva + dbo.CabecDoc.TotalOutros - dbo.CabecDoc.TotalDesc) AS TotalVendas FROM dbo.CabecDoc Where TipoDoc = 'FA' and CONVERT(VARCHAR(10),dbo.CabecDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + "");
                    //valor total das Faturas passadas aos clientes
                    double faturasPassadasAosClientes = objList.Valor("TotalVendas");

                    objList = PriEngine.Engine.Consulta("SELECT SUM(-1*(dbo.CabecCompras.TotalMerc + dbo.CabecCompras.TotalIva + dbo.CabecCompras.TotalOutros + dbo.CabecCompras.TotalDespesasAdicionais - dbo.CabecCompras.TotalDesc)) AS TotalCompras FROM dbo.CabecCompras Where TipoDoc = 'VFA' and CONVERT(VARCHAR(10),dbo.CabecCompras.DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + "");
                    // valor total do que já paguei aos fornecedores
                    double oQpagamosAosFornecedores = objList.Valor("TotalCompras");

                    objList = PriEngine.Engine.Consulta("SELECT dbo.TMP_Empresas.ICCapitalSocial AS capital FROM dbo.TMP_Empresas");
                    //valor da capital social
                    double capitalSocial = objList.Valor("capital");


                    double ativo = (faturasPassadasAosClientes - oQpagamosAosFornecedores) + capitalSocial;
                    double roa = resultadoLiquido / ativo;

                    art.ValorRoa = Math.Round(roa, 2);
                    art.Sucess = true;
                    return art;

                }
                else
                {
                    art.Sucess = false;
                    return art;
                }
            }
            catch (Exception e)
            {
                art.Sucess = false;
                return art;

            }

        }

        /*
         * Autonomia financeira=capital proprio/ativo
         * 
         * Autonomia financeira=capital social+(resultado liquido)/((valor total das Faturas passadas aos clientes-o que já paguei aos fornecedores)+ capital social)
         * resultado liquido=rendimentos(o que vendi)-gastos(apenas no q comprei e vendi)*0.75
         * *0.75 porque em media 25% vai para o estado
         * 
         */
        public static Model.AutoFinanceira AutoFinanceira(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.AutoFinanceira art = new Model.AutoFinanceira();
            try
            {



                if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
                {

                    objList = PriEngine.Engine.Consulta("SELECT dbo.TMP_Empresas.ICCapitalSocial AS capital FROM dbo.TMP_Empresas");
                    //valor da capital social
                    double capitalSocial = objList.Valor("capital");

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.CabecDoc.TotalMerc + dbo.CabecDoc.TotalIva + dbo.CabecDoc.TotalOutros - dbo.CabecDoc.TotalDesc) AS TotalVendas FROM dbo.CabecDoc Where TipoDoc = 'FA' and CONVERT(VARCHAR(10), dbo.CabecDoc.Data, 111) BETWEEN " + dataIn + " AND " + dataFm + "");
                    //valor total do que vendi(rendimentos)
                    double rendimentos = objList.Valor("TotalVendas");

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.LinhasDoc.Quantidade * dbo.LinhasDoc.PCM ) AS Soma FROM dbo.LinhasDoc  WHERE CONVERT(VARCHAR(10),Data,111) BETWEEN " + dataIn + " AND " + dataFm + "");
                    //valor total de gastos(quanto custaram os produtos que já foram vendidos)
                    double gastos = objList.Valor("Soma");

                    double capitalProprio = capitalSocial + ((rendimentos - gastos) * 0.75);

                    objList = PriEngine.Engine.Consulta("SELECT SUM(dbo.CabecDoc.TotalMerc + dbo.CabecDoc.TotalIva + dbo.CabecDoc.TotalOutros - dbo.CabecDoc.TotalDesc) AS TotalVendas FROM dbo.CabecDoc Where TipoDoc = 'FA' and CONVERT(VARCHAR(10), dbo.CabecDoc.Data, 111) BETWEEN " + dataIn + " AND " + dataFm + "");
                    //valor total das Faturas passadas aos clientes
                    double faturasPassadasAosClientes = objList.Valor("TotalVendas");

                    objList = PriEngine.Engine.Consulta("SELECT SUM(-1*(dbo.CabecCompras.TotalMerc + dbo.CabecCompras.TotalIva + dbo.CabecCompras.TotalOutros + dbo.CabecCompras.TotalDespesasAdicionais - dbo.CabecCompras.TotalDesc)) AS TotalCompras FROM dbo.CabecCompras Where TipoDoc = 'VFA' and CONVERT(VARCHAR(10),dbo.CabecCompras.DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + "");
                    // valor total do que já paguei aos fornecedores
                    double oQpagamosAosFornecedores = objList.Valor("TotalCompras");

                    double ativo = (faturasPassadasAosClientes - oQpagamosAosFornecedores) + capitalSocial;
                    double autonomia = capitalProprio / ativo;


                    art.ValorAutoFinanceira = Math.Round(autonomia, 2);
                    art.Sucess = true;
                    return art;

                }
                else
                {
                    art.Sucess = false;
                    return art;
                }
            }
            catch (Exception e)
            {
                art.Sucess = false;
                return art;

            }

        }

        /// <summary>
        /// Top de Clientes de um determinado mes de um ano
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static List<Model.TopClientes> ListaTopClientes(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.TopClientes art = new Model.TopClientes();
            List<Model.TopClientes> listArts = new List<Model.TopClientes>();



            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {


                objList = PriEngine.Engine.Consulta("SELECT TOP 10 dbo.CabecDoc.Nome,SUM(dbo.LinhasDoc.PrecoLiquido + dbo.LinhasDoc.TotalIva) AS Quantidade FROM dbo.CabecDoc INNER JOIN dbo.LinhasDoc ON dbo.CabecDoc.Id = dbo.LinhasDoc.IdCabecDoc WHERE dbo.CabecDoc.TipoDoc = 'FA' and CONVERT(VARCHAR(10),dbo.LinhasDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + " GROUP BY dbo.CabecDoc.Nome ORDER BY Quantidade DESC");
                while (!objList.NoFim())
                {
                    art = new Model.TopClientes();
                    art.NomeCliente = objList.Valor("Nome");
                    art.CodCliente = NifByClientName(art.NomeCliente.ToString());
                    art.QuantidadeVendida = objList.Valor("Quantidade");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();

                }

                return listArts;
            }
            else
            {
                art.Sucess = false;
                return null;
            }
        }

        /// <summary>
        /// Lista de Top Produtos mais vendidos, quantidade,de determinado mes de um ano.
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <returns></returns>
        public static List<Model.TopProdutos> ListaTopProduto(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.TopProdutos art = new Model.TopProdutos();
            List<Model.TopProdutos> listArts = new List<Model.TopProdutos>();


            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT TOP 10  dbo.Artigo.Artigo,dbo.LinhasDoc.Descricao, SUM(dbo.LinhasDoc.Quantidade) AS Soma FROM dbo.CabecDoc INNER JOIN dbo.LinhasDoc ON dbo.CabecDoc.Id = dbo.LinhasDoc.IdCabecDoc INNER JOIN dbo.Artigo ON dbo.Artigo.Descricao = dbo.LinhasDoc.Descricao  WHERE CONVERT(VARCHAR(10),dbo.LinhasDoc.Data,111) BETWEEN " + dataIn + " AND " + dataFm + " GROUP BY dbo.LinhasDoc.Descricao,dbo.Artigo.Artigo ORDER BY Soma DESC");
                while (!objList.NoFim())
                {

                    art = new Model.TopProdutos();
                    art.CodProduto = objList.Valor("Artigo");
                    art.NomeProduto = objList.Valor("Descricao");
                    art.QuantidadeProduto = objList.Valor("Soma");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();

                }

                return listArts;
            }
            else
            {
                art.Sucess = false;
                return null;
            }
        }

        public static List<Model.CustosPorProduto> CustosPorProduto(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.CustosPorProduto art = new Model.CustosPorProduto();
            List<Model.CustosPorProduto> listArts = new List<Model.CustosPorProduto>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Descricao, PCM AS Custos FROM dbo.LinhasDoc  WHERE dbo.LinhasDoc.Artigo != '' AND CONVERT(VARCHAR(10),Data,111) BETWEEN " + dataIn + " AND " + dataFm + " GROUP BY Descricao,PCM ORDER BY PCM DESC");
                while (!objList.NoFim())
                {
                    art = new Model.CustosPorProduto();
                    art.Total = Math.Round(objList.Valor("Custos"), 2);
                    art.NomeProduto = objList.Valor("Descricao");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();
                }
                return listArts;
            }
            else
                return null;
        }


        public static List<Model.ProdutosPorFornecedor> ProdutosPorFornecedor(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.ProdutosPorFornecedor art = new Model.ProdutosPorFornecedor();
            List<Model.ProdutosPorFornecedor> listArts = new List<Model.ProdutosPorFornecedor>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Distinct dbo.CabecCompras.Nome AS nome, dbo.LinhasCompras.Descricao AS descricao, Sum(dbo.LinhasCompras.Quantidade) as Quantidade from dbo.LinhasCompras INNER JOIN dbo.CabecCompras ON dbo.LinhasCompras.NumDocExterno = dbo.CabecCompras.NumDocExterno where dbo.CabecCompras.TipoDoc = 'VFA' and dbo.CabecCompras.NumDocExterno != '1' AND CONVERT(VARCHAR(10),dbo.LinhasCompras.DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + " Group by dbo.CabecCompras.Nome, dbo.LinhasCompras.Descricao order by dbo.CabecCompras.Nome");
                while (!objList.NoFim())
                {
                    art = new Model.ProdutosPorFornecedor();
                    art.Nome = objList.Valor("nome");
                    art.NomeProduto = objList.Valor("descricao");
                    art.Quantidade = (objList.Valor("Quantidade") < 0) ? objList.Valor("Quantidade") * -1 : objList.Valor("Quantidade");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();
                }
                return listArts;
            }
            else
                return null;
        }


        public static List<Model.ValorGastoFornecedor> ValorGastoFornecedor(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.ValorGastoFornecedor art = new Model.ValorGastoFornecedor();
            List<Model.ValorGastoFornecedor> listArts = new List<Model.ValorGastoFornecedor>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Nome, SUM(-1*(dbo.CabecCompras.TotalMerc + dbo.CabecCompras.TotalIva + dbo.CabecCompras.TotalOutros + dbo.CabecCompras.TotalDespesasAdicionais - dbo.CabecCompras.TotalDesc)) AS Total FROM dbo.CabecCompras Where TipoDoc = 'VFA' AND CONVERT(VARCHAR(10),dbo.CabecCompras.DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + " GROUP BY dbo.CabecCompras.Nome ORDER BY Total DESC");
                while (!objList.NoFim())
                {
                    art = new Model.ValorGastoFornecedor();
                    art.NomeFornecedor = objList.Valor("Nome");
                    art.ValorGasto = Math.Round(objList.Valor("Total"), 2);
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();
                }
                return listArts;
            }
            else
                return null;
        }

        public static String IdByFornecedorName(String name)
        {

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {
                StdBELista objList;

                //Vai a tabela Clientes procurar por um Cliente que tenha o mesmo Nome que o nome passado para o método e que retorna o Número de Contribuinte.
                objList = PriEngine.Engine.Consulta("SELECT dbo.Fornecedores.Fornecedor FROM dbo.Fornecedores WHERE dbo.Fornecedores.Nome = '" + name + "'");

                return objList.Valor("Fornecedor");
            }

            return null;
        }


        public static List<Model.TopFornecedores> TopFornecedores(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.TopFornecedores art = new Model.TopFornecedores();
            List<Model.TopFornecedores> listArts = new List<Model.TopFornecedores>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT TOP 10 dbo.CabecCompras.Nome,SUM(-1*(dbo.CabecCompras.TotalMerc + dbo.CabecCompras.TotalIva + dbo.CabecCompras.TotalOutros + dbo.CabecCompras.TotalDespesasAdicionais - dbo.CabecCompras.TotalDesc)) AS Quantidade FROM dbo.CabecCompras INNER JOIN dbo.LinhasCompras ON dbo.CabecCompras.Id = dbo.LinhasCompras.IdCabecCompras WHERE CONVERT(VARCHAR(10),dbo.LinhasCompras.DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + " GROUP BY dbo.CabecCompras.Nome ORDER BY Quantidade DESC");
                while (!objList.NoFim())
                {
                    art = new Model.TopFornecedores();
                    art.CodFornecedor = IdByFornecedorName(objList.Valor("Nome"));
                    art.QuantidadeComprada = Math.Round(objList.Valor("Quantidade"), 2);
                    art.NomeFornecedor = objList.Valor("Nome");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();
                }
                return listArts;
            }
            else
                return null;
        }

        public static List<Model.TotalCustos> TotalCustos(string dataIn, string dataFm)
        {
            StdBELista objList;

            Model.TotalCustos art = new Model.TotalCustos();
            List<Model.TotalCustos> listArts = new List<Model.TotalCustos>();

            if (PriEngine.InitializeCompany(LEISIO.Properties.Settings.Default.Empresa.Trim(), LEISIO.Properties.Settings.Default.User.Trim(), LEISIO.Properties.Settings.Default.Pass.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT MONTH(DataDoc) AS Mes, YEAR(DataDoc) AS Ano, SUM(-1*(dbo.CabecCompras.TotalMerc + dbo.CabecCompras.TotalIva + dbo.CabecCompras.TotalOutros + dbo.CabecCompras.TotalDespesasAdicionais - dbo.CabecCompras.TotalDesc)) AS Soma FROM dbo.CabecCompras Where TipoDoc = 'VFA' AND CONVERT(VARCHAR(10),DataDoc,111) BETWEEN " + dataIn + " AND " + dataFm + " GROUP BY YEAR(DataDoc), MONTH(DataDoc)");
                while (!objList.NoFim())
                {
                    art = new Model.TotalCustos();
                    art.Total = Math.Round(objList.Valor("Soma"), 2);
                    art.Data = objList.Valor("Ano") + "/" + objList.Valor("Mes");
                    art.Sucess = true;
                    listArts.Add(art);
                    objList.Seguinte();
                }
                return listArts;
            }
            else
                return null;
        }
    }
}