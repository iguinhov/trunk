using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace Dal
{
    public class dalItens : Conexao
    {
        #region Cadastro de novo Item
        public void pubCadastraNovoItem(modItens item)
        {
            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_ITENS_CADASTRA_NOVO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_ITEM", item.idItem);
                    cmd.Parameters.AddWithValue("@ID_NM_SOLICITACAO", item.idNmSolicitacao);
                    cmd.Parameters.AddWithValue("@NOME_SOLICITANTE", item.NomeSolicitante);
                    cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", item.idDepartamento);
                    cmd.Parameters.AddWithValue("@DT_ABERTURA", item.dtAbertura);
                    cmd.Parameters.AddWithValue("@DT_PROGRAMADA", item.dtProgramada);
                    cmd.Parameters.AddWithValue("@DS_TITULO", item.tituloItem);
                    cmd.Parameters.AddWithValue("@DESCRICAO", item.descicao);
                    cmd.Parameters.AddWithValue("@ID_DEV", item.idDev);
                    cmd.Parameters.AddWithValue("@ID_TIPO_DESENV", item.idTipoDesenv);
                    cmd.Parameters.AddWithValue("@ID_STATUS", item.idStatus);                                        
                    cmd.Parameters.AddWithValue("@ID_PRIORIDADE", item.idPrioridade);
                    cmd.Parameters.AddWithValue("@DS_LOGIN_ABERTURA", item.idUsuarioLogado);                           

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw new Exception("USP_ITENS_CADASTRA_NOVO - : " + e.Message);
                    }
                }
                else
                {
                    throw new Exception("Problema ao tentar conexão com o banco de dados! ");
                }
            }
        }
        #endregion

        public void pubDesenvolvedorFinalizaItem(modItens item)
        {
            using(sqlCon = new SqlConnection(strCon))
            {
                if(strCon != null)
                {
                    cmd = new SqlCommand("USP_ITENS_FINALIZA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_ITEM", item.idItem);                   
                    cmd.Parameters.AddWithValue("@ID_STATUS", item.idStatus);
                    cmd.Parameters.AddWithValue("@CAMADAS_METODOS_DESCRICAO", item.camadasMetodos);
                    cmd.Parameters.AddWithValue("@PROCEDURES_NOMES", item.proceduresNomes);
                    cmd.Parameters.AddWithValue("@DESCRICAO_DESENVOLVIMENTO", item.descDesenvolvimento);
                    cmd.Parameters.AddWithValue("@DT_TERMINO", item.dtTermino);
                    cmd.Parameters.AddWithValue("@FL_COMMIT_ITEM", item.flCommit);
                    cmd.Parameters.AddWithValue("@TOTAL_HRS_ITEM", item.totalHoras);                    
                    cmd.Parameters.AddWithValue("@DT_PUBLICACAO", item.dtPublicacao);
                    cmd.Parameters.AddWithValue("@ID_USUARIO_LOGADO", item.idUsuarioLogado);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw new Exception("USP_ITENS_FINALIZA - : " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema ao tentar conexão com o banco de dados! ");
                }
            }
        }


        public void pubAdminAtualizaItensEmAberto(modItens item)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (strCon != null)
                {
                    cmd = new SqlCommand("USP_ITENS_EM_ABERTO_ADMIN_ATUALIZA_CAMPOS", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_ITEM", item.idItem);
                    cmd.Parameters.AddWithValue("@DS_TITULO", item.tituloItem);
                    cmd.Parameters.AddWithValue("@NOME_SOLICITANTE", item.NomeSolicitante);
                    cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", item.idDepartamento);
                    cmd.Parameters.AddWithValue("@ID_DEV", item.idDev);
                    cmd.Parameters.AddWithValue("@ID_TIPO_DESENV", item.idTipoDesenv);
                    cmd.Parameters.AddWithValue("@ID_STATUS", item.idStatus);
                    cmd.Parameters.AddWithValue("@ID_PRIORIDADE", item.idPrioridade);
                    cmd.Parameters.AddWithValue("@DT_PROGRAMADA", item.dtProgramada);
                    cmd.Parameters.AddWithValue("@DESCRICAO", item.descicao);                
                    cmd.Parameters.AddWithValue("@ID_USUARIO_LOGADO", item.idUsuarioLogado);           

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw new Exception("USP_ITENS_EM_ABERTO_ADMIN_ATUALIZA_CAMPOS - : " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema ao tentar conexão com o banco de dados! ");
                }
            }
        }

        public modItens pubBuscaItemPorId(int id)
        {
            objDr = null;

            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_ITENS_LISTA_TODOS_POR_ID", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_ITEM", id);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modItens item = null;

                        while (objDr.Read())
                        {
                            item = new modItens();

                            item.idItem = Convert.ToInt32(objDr["ID_ITEM"].ToString());
                            item.nomeProjeto = objDr["NOME_PROJETO"].ToString();
                            item.NomeSolicitante = objDr["NOME_SOLICITANTE"].ToString();
                            item.idDepartamento = Convert.ToInt32(objDr["ID_DEPARTAMENTO"].ToString());
                            item.dtAbertura = Convert.ToDateTime(objDr["DT_ABERTURA"].ToString());
                            item.tituloItem = objDr["DS_TITULO"].ToString();
                            item.descicao = objDr["DESCRICAO"].ToString();
                            item.idTipoDesenv = Convert.ToInt32(objDr["ID_TIPO_DESENV"].ToString());
                            item.idDev = Convert.ToInt32(objDr["ID_DEV"].ToString());                          
                            item.idStatus = Convert.ToInt32(objDr["ID_STATUS"].ToString());
                            item.dtProgramada = Convert.ToDateTime(objDr["DT_PROGRAMADA"].ToString());
                            item.camadasMetodos = objDr["CAMADAS_METODOS_DESCRICAO"].ToString();
                            item.proceduresNomes = objDr["PROCEDURES_NOMES"].ToString();                                           
                            item.idPrioridade = Convert.ToInt32(objDr["ID_PRIORIDADE"].ToString());
                        }

                        return item;
                    }
                    catch(SqlException ex)
                    {
                        throw new Exception("USP_ITENS_LISTA_TODOS_POR_ID - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao tentar realizar solicitação ! " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Ploblema com conexão ao banco de dados!");
                }
            }
        }

        public List<modItens> pubListaTodosOsItens()
        {
            List<modItens> itens = new List<modItens>();
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_ITENS_LISTA_TODOS", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;                    

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modItens item = null;

                        while (objDr.Read())
                        {
                            item = new modItens();

                            item.idItem = Convert.ToInt32(objDr["ID_ITEM"].ToString());
                            item.idNmSolicitacao = Convert.ToInt32(objDr["ID_NM_SOLICITACAO"].ToString());
                            item.NomeSolicitante = objDr["NOME_SOLICITANTE"].ToString();
                            item.idDepartamento = Convert.ToInt32(objDr["ID_DEPARTAMENTO"].ToString());
                            item.dtAbertura = Convert.ToDateTime(objDr["DT_ABERTURA"].ToString());
                            item.descicao = objDr["DESCRICAO"].ToString();
                            item.idDev = Convert.ToInt32(objDr["ID_DEV"].ToString());                           
                            item.idStatus = Convert.ToInt32(objDr["ID_STATUS"].ToString());                                            
                            item.idPrioridade = Convert.ToInt32(objDr["ID_PRIORIDADE"].ToString());

                            itens.Add(item);
                        }

                        return itens;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("USP_ITENS_LISTA_TODOS - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao tentar realizar solicitação ! " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Ploblema com conexão ao banco de dados!");
                }
            }
        }

        public modItens pubRegistraIdNovoItem(int id)
        {
            objDr = null;

            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_ITENS_RECEBE_ID_ORDEM_SERVICO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", id);
                  
                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modItens md = null;

                        while (objDr.Read())
                        {
                            md = new modItens();

                            md.idNmSolicitacao = Convert.ToInt32(objDr["ID_NM_SOLICITACAO"].ToString());
                            md.nomeProjeto = objDr["NOME_PROJETO"].ToString();
                        }

                        return md;
                    }
                    catch(SqlException ex)
                    {
                        throw new Exception("USP_ITENS_RECEBE_ID_ORDEM_SERVICO - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao processar solicitação " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }

                else
                {
                    throw new Exception("Problema ao tentar efetuar conexão com o banco de dados! ");
                }
            }
        }

        public List<modItensPorDesenvolvedorLogin> pubListaItensEmAbertoParaDesenvolvedor(string login)
        {
            objDr = null;

            List<modItensPorDesenvolvedorLogin> itens = new List<modItensPorDesenvolvedorLogin>();

            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_ITENS_LISTA_EM_ABERTO_POR_DESENVOLVEDOR", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LOGIN", login);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modItensPorDesenvolvedorLogin itm = null;

                        while (objDr.Read())
                        {
                            itm = new modItensPorDesenvolvedorLogin();

                            itm.idNmSolicitacao = Convert.ToInt32(objDr["ID_NM_SOLICITACAO"]);
                            itm.projeto = objDr["NOME_PROJETO"].ToString();
                            itm.nmVersao = objDr["NM_VERSAO_ATUAL"].ToString();
                            itm.idItem = Convert.ToInt32(objDr["ID_ITEM"]);                            
                            itm.solicitante = objDr["NOME_SOLICITANTE"].ToString();
                            itm.dtAbertura = Convert.ToDateTime(objDr["DT_ABERTURA"].ToString());
                            itm.dtProgramada = Convert.ToDateTime(objDr["DT_PROGRAMADA"].ToString());
                            itm.status = objDr["STATUS"].ToString();
                            itm.prioridade = objDr["PRIORIDADE_TIPO"].ToString();
                            itm.titulo = objDr["DS_TITULO"].ToString();
                            itm.descricao = objDr["DESCRICAO"].ToString();

                            itens.Add(itm);                            
                        }

                        return itens;
                    }
                    catch(SqlException ex)
                    {
                        throw new Exception("USP_ITENS_LISTA_EM_ABERTO_POR_DESENVOLVEDOR - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao tentar realizar requisição! " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema com a conexão ao banco de dados !");
                }
            }
        }

        public void pubDesenvolvedorAtualizaStatusItem(modItens item)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    objDr = null;
                    cmd = new SqlCommand("USP_ITENS_DESENVOLVEDOR_ALTERA_STATUS", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_ITEM", item.idItem);
                    cmd.Parameters.AddWithValue("@ID_STATUS", item.idStatus);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("USP_ITENS_DESENVOLVEDOR_ALTERA_STATUS - :" + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Problema ao tentar efetuar atualização do status." + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }

                else
                {
                    throw new Exception("Problema com a conexão ao banco de dados!");
                }
            }
        }

        public List <modItensListasPorStatus> pubListaOsStatusdosItensEmAberto(int idStatus)
        {
            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    List<modItensListasPorStatus> todosStatus = new List<modItensListasPorStatus>();

                    objDr = null;

                    cmd = new SqlCommand("USP_ITENS_LISTA_POR_STATUS_ID_PESQUISA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@ID_STATUS", idStatus);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modItensListasPorStatus lista = null;

                        while (objDr.Read())
                        {
                            lista = new modItensListasPorStatus();

                            lista.projeto = objDr["NOME_PROJETO"].ToString();
                            lista.solicitante = objDr["NOME_SOLICITANTE"].ToString();
                            lista.idItem = Convert.ToInt32(objDr["ID_ITEM"]);
                            lista.titulo = objDr["DS_TITULO"].ToString();
                            lista.desenvolvedor = objDr["NOME_DESENVOLVEDOR"].ToString();
                            lista.status = objDr["STATUS"].ToString();
                            lista.dtCadastro = Convert.ToDateTime(objDr["DT_ABERTURA"].ToString());
                            lista.dtProgramada = Convert.ToDateTime(objDr["DT_PROGRAMADA"].ToString());                            
                            lista.descricao = objDr["DESCRICAO"].ToString();

                            todosStatus.Add(lista);
                        }

                        return todosStatus;
                    }
                    catch(SqlException ex)
                    {
                        throw new Exception("[USP_ITENS_LISTA_POR_STATUS_ID_PESQUISA] - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao dar andamento na solicitação - : " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema ao tentar efetuar conexão com o banco de dados.");
                }               
            }
        }

        public List<modItensListasPorStatus> pubListaTodosItensEncerradosPorDesenvolvedor(int idDev)
        {
            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    objDr = null;

                    List<modItensListasPorStatus> listass = new List<modItensListasPorStatus>();

                    cmd = new SqlCommand("USP_LISTA_STATUS_FINALIZADO_POR_DESENVOLVEDOR_ITEM", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ID_DEV", idDev);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modItensListasPorStatus itens = null;

                        while (objDr.Read())
                        {
                            itens = new modItensListasPorStatus();

                            itens.projeto = objDr["NOME_PROJETO"].ToString();
                            itens.nmVersao = objDr["NM_VERSAO_ATUAL"].ToString();
                            itens.solicitante = objDr["NOME_SOLICITANTE"].ToString();
                            itens.idItem = Convert.ToInt32(objDr["ID_ITEM"]);
                            itens.titulo = objDr["DS_TITULO"].ToString();
                            itens.desenvolvedor = objDr["NOME_DESENVOLVEDOR"].ToString();
                            itens.status = objDr["STATUS"].ToString();
                            itens.dtCadastro = Convert.ToDateTime(objDr["DT_ABERTURA"].ToString());
                            itens.dtProgramada = Convert.ToDateTime(objDr["DT_PROGRAMADA"].ToString());
                            itens.dtFinalizado = Convert.ToDateTime(objDr["DT_TERMINO"].ToString());
                            itens.descricao = objDr["DESCRICAO"].ToString();

                            listass.Add(itens);
                        }
                        return listass;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("[USP_LISTA_OUTROS_STATUS_POR_DESENVOLVEDOR_ITEM] - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao dar andamento na solicitação - : " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }

                else
                {
                    throw new Exception("Problema ao efetuar conexão com banco de dados. ");
                }
            }
        }

        public modItensFinalizadosDetalhe pubItensFinalizadosDetalhe(int id) {

            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {                  
                    objDr = null;

                    cmd = new SqlCommand("USP_ITENS_FINALIZADOS_DETALHE", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_ITEM", id);

                    try
                    {
                        sqlCon.Open();

                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modItensFinalizadosDetalhe itemFim = null;

                        while (objDr.Read())
                        {
                            itemFim = new modItensFinalizadosDetalhe();

                            itemFim.projeto = objDr["NOME_PROJETO"].ToString();
                            itemFim.nmVersao = objDr["NM_VERSAO_ATUAL"].ToString();
                            itemFim.solicitante = objDr["NOME_SOLICITANTE"].ToString();
                            itemFim.departamento = objDr["DEPARTAMENTO"].ToString();
                            itemFim.idItem = Convert.ToInt32(objDr["ID_ITEM"]);
                            itemFim.titulo = objDr["DS_TITULO"].ToString();
                            itemFim.desenvolvedor = objDr["NOME_DESENVOLVEDOR"].ToString();
                            itemFim.status = objDr["STATUS"].ToString();
                            itemFim.prioridade = objDr["PRIORIDADE_TIPO"].ToString();
                            itemFim.dtAbertura = Convert.ToDateTime(objDr["DT_ABERTURA"].ToString());
                            itemFim.dtProgramada = Convert.ToDateTime(objDr["DT_PROGRAMADA"].ToString());
                            itemFim.dtTermino = Convert.ToDateTime(objDr["DT_TERMINO"].ToString());
                            itemFim.descricao = objDr["DESCRICAO"].ToString();
                            itemFim.descricaoDesenvolvedor = objDr["DESCRICAO_DESENVOLVIMENTO"].ToString();
                            itemFim.camadaMetodos = objDr["CAMADAS_METODOS_DESCRICAO"].ToString();
                            itemFim.proceduresNomes = objDr["PROCEDURES_NOMES"].ToString();
                            itemFim.flCommit = objDr["FL_COMMIT_ITEM"].ToString();                          
                        }

                        return itemFim;

                    }
                    catch(SqlException ex)
                    {
                        throw new Exception("[USP_ITENS_FINALIZADOS_DETALHE] - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch(Exception e)
                    {
                        throw new Exception("[Problema ao tentar efetuar consulta solicitada] - : " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema ao tentar efetuar conexão com o banco de dados!");
                }
            }
        }

        //EFETUA CONSULTA DO ITEM APÓS CADASTRO PARA QUE UTILIZE O RETORNO NO ENVIO DE E-MAIL
        //public List<modItensConsultaEnvioEmail> pubConsultaItemParaEnvioEmail()
        //{
        //    using(sqlCon = new SqlConnection(strCon))
        //    {
        //        if(sqlCon != null)
        //        {
        //            List<modItensConsultaEnvioEmail> emails = new List<modItensConsultaEnvioEmail>();

        //            objDr = null;

        //            SqlCommand cmd = new SqlCommand("USP_ITENS_CONSULTA_ULTIMO_ENVIA_EMAIL", sqlCon);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            try
        //            {
        //                sqlCon.Open();

        //                objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //                modItensConsultaEnvioEmail con = null;

        //                while (objDr.Read())
        //                {
        //                    con = new modItensConsultaEnvioEmail();

        //                    con.idItem = Convert.ToInt32(objDr["ID_ITEM"]);
        //                    con.nomeProjeto = objDr["PROJETO"].ToString();
        //                    con.versao = objDr["VERSAO"].ToString();
        //                    con.solicitante = objDr["NOME_SOLICITANTE"].ToString();
        //                    con.depto = objDr["DEPARTAMENTO"].ToString();
        //                    con.desc = objDr["DESCRICAO"].ToString();
        //                    con.prioridade = objDr["PRIORIDADE_TIPO"].ToString();
        //                    con.emailDev = objDr["EMAIL_DEV"].ToString();
        //                    con.emailUser = objDr["EMAIL_U"].ToString();
        //                    con.desenvolvedor = objDr["DESENVOLVEDOR"].ToString();

        //                    emails.Add(con);
        //                }

        //                return emails;
        //            }
        //            catch(SqlException ex)
        //            {
        //                throw new Exception("USP_ITENS_CONSULTA_ULTIMO_ENVIA_EMAIL - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
        //            }
        //            catch (Exception e)
        //            {

        //                throw new Exception("[Problema ao tentar efetuar consulta solicitada] - : " + e.Message);
        //            }
        //            finally
        //            {
        //                FechaConexao();
        //            }

        //        }
        //        else
        //        {
        //            throw new Exception("Problema ao se conectar com o banco de dados!");
        //        }
        //    }
        //}

        //public List<modItensConsultaEnvioEmail> pubConsultaPorIdItemEnvioEmail(int id)
        //{
        //    using (sqlCon = new SqlConnection(strCon))
        //    {
        //        if (sqlCon != null)
        //        {
        //            List<modItensConsultaEnvioEmail> emails = new List<modItensConsultaEnvioEmail>();

        //            objDr = null;

        //            SqlCommand cmd = new SqlCommand("USP_ITENS_CONSULTA_ID_ENVIA_EMAIL", sqlCon);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@ID_ITEM", id);

        //            try
        //            {
        //                sqlCon.Open();

        //                objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //                modItensConsultaEnvioEmail con = null;

        //                while (objDr.Read())
        //                {
        //                    con = new modItensConsultaEnvioEmail();

        //                    con.idItem = Convert.ToInt32(objDr["ID_ITEM"]);
        //                    con.nomeProjeto = objDr["PROJETO"].ToString();
        //                    con.versao = objDr["VERSAO"].ToString();
        //                    con.solicitante = objDr["NOME_SOLICITANTE"].ToString();
        //                    con.depto = objDr["DEPARTAMENTO"].ToString();
        //                    con.desc = objDr["DESCRICAO"].ToString();
        //                    con.prioridade = objDr["PRIORIDADE_TIPO"].ToString();
        //                    con.emailDev = objDr["EMAIL_DEV"].ToString();
        //                    con.emailUser = objDr["EMAIL_U"].ToString();
        //                    con.emailAlter = objDr["EMAIL_ALT"].ToString();
        //                    con.desenvolvedor = objDr["DESENVOLVEDOR"].ToString();
        //                    con.descricaoDev = objDr["DESCRICAO_DESENVOLVIMENTO"].ToString();
        //                    con.statusItem = objDr["TIPO_STATUS"].ToString();

        //                    emails.Add(con);
        //                }

        //                return emails;
        //            }
        //            catch (SqlException ex)
        //            {
        //                throw new Exception("USP_ITENS_CONSULTA_ID_ENVIA_EMAIL - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
        //            }
        //            catch (Exception e)
        //            {

        //                throw new Exception("[Problema ao tentar efetuar consulta solicitada] - : " + e.Message);
        //            }
        //            finally
        //            {
        //                FechaConexao();
        //            }

        //        }
        //        else
        //        {
        //            throw new Exception("Problema ao se conectar com o banco de dados!");
        //        }
        //    }
        //}

    }
}