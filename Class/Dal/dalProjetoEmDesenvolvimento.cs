using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{
    public class dalProjetoEmDesenvolvimento : Conexao
    {
        public void pubCadastraNovoProjeto(modProjetoEmDesenvolvimento projetoEmDev) {

            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PRJ_DESENVOLVIMENTO_CADASTRA_NOVO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_NM_SOLICITACAO", projetoEmDev.idNmSolitacao);
                    cmd.Parameters.AddWithValue("@ID_PROJETO", projetoEmDev.idProjeto);
                    cmd.Parameters.AddWithValue("@NM_VERSAO_ATUAL", projetoEmDev.nmVersao);
                    cmd.Parameters.AddWithValue("@DT_CADASTRO", projetoEmDev.dtCadastro);
                    cmd.Parameters.AddWithValue("@FL_ENCERRADO", projetoEmDev.flEncerrado);
                    cmd.Parameters.AddWithValue("@FL_COMMIT_SOLUTION", projetoEmDev.flCommitSolution);
                    cmd.Parameters.AddWithValue("@FL_VS_EM_PROD", projetoEmDev.flVsProd);

                    try
                    {
                        sqlCon.Open();

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw new Exception("USP_PRJ_DESENVOLVIMENTO_CADASTRA_NOVO - : " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema ao tentar se conectar com o banco de dados! ");
                }
                
            }
        }

        public void pubAtualizaFinalizaProjetoEmDesenvolvimento(modProjetoEmDesenvolvimento projetoDev)
        {
            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PRJ_DESENVOLVIMENTO_ATUALIZA_FINALIZA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_NM_SOLICITACAO", projetoDev.idNmSolitacao);
                    cmd.Parameters.AddWithValue("@FL_ENCERRADO", projetoDev.flEncerrado);
                    cmd.Parameters.AddWithValue("@DT_FINALIZADO", projetoDev.dtFinalizado);
                    cmd.Parameters.AddWithValue("@NM_VERSAO_FINAL", projetoDev.nmVersaoFim);
                    cmd.Parameters.AddWithValue("@NM_VERSAO_ATUAL", projetoDev.nmVersao);
                    cmd.Parameters.AddWithValue("@FL_COMMIT_SOLUTION", projetoDev.flCommitSolution);
                    cmd.Parameters.AddWithValue("@FL_VS_EM_PROD", projetoDev.flVsProd);
                    cmd.Parameters.AddWithValue("@DT_PUBLICACAO", projetoDev.dtPublicacaoFim);
                    cmd.Parameters.AddWithValue("@ID_USUARIO_LOGADO", projetoDev.idUsuarioLogado);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw new Exception("USP_PRJ_DESENVOLVIMENTO_ATUALIZA_FINALIZA - : " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
            }
        }

        public List<modProjetoEmDesenvolvimento> pubListaTodosOsDesenvolvimento()
        {
            List<modProjetoEmDesenvolvimento> osProjetos = new List<modProjetoEmDesenvolvimento>();

            objDr = null;

            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PRJ_DESENVOLVIMENTO_LISTA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modProjetoEmDesenvolvimento prjDev = null;

                        while (objDr.Read())
                        {
                            prjDev = new modProjetoEmDesenvolvimento();

                            prjDev.idNmSolitacao = Convert.ToInt32(objDr["ID_NM_SOLICITACAO"]);
                            prjDev.nomeProjeto = objDr["NOME_PROJETO"].ToString();
                            prjDev.nmVersao = objDr["NM_VERSAO_ATUAL"].ToString();
                            prjDev.dtCadastro = Convert.ToDateTime(objDr["DT_CADASTRO"].ToString());
                            prjDev.flEncerrado = Convert.ToBoolean(objDr["FL_ENCERRADO"].ToString());
                            //prjDev.dtFinalizado = Convert.ToDateTime(objDr["DT_FINALIZADO"].ToString());
                            prjDev.nmVersaoFim = objDr["NM_VERSAO_FINAL"].ToString();
                            prjDev.flCommitSolution = Convert.ToBoolean(objDr["FL_COMMIT_SOLUTION"].ToString());
                            prjDev.flVsProd = Convert.ToBoolean(objDr["FL_VS_EM_PROD"].ToString());
                            //prjDev.dtPublicacaoFim = Convert.ToDateTime(objDr["DT_PUBLICACAO"].ToString());

                            osProjetos.Add(prjDev);
                        }

                        return osProjetos;
                    }
                    catch(SqlException ex)
                    {
                        throw new Exception("USP_PRJ_DESENVOLVIMENTO_LISTA -: " + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao tentar listar os projetos em desenvolvimento. " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema com a conexão ao banco de dados! ");
                }
            }
        }

        public modProjetoEmDesenvolvimento pubBuscaOsProjetoPorId(int id)
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PRJ_DESENVOLVIMENTO_LISTA_POR_ID", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_NM_SOLICITACAO", id);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modProjetoEmDesenvolvimento prjDev = null;

                        while (objDr.Read())
                        {
                            prjDev = new modProjetoEmDesenvolvimento();

                            prjDev.idNmSolitacao = Convert.ToInt32(objDr["ID_NM_SOLICITACAO"]);
                            prjDev.idProjeto = Convert.ToInt32(objDr["ID_PROJETO"]);
                            prjDev.nmVersao = objDr["NM_VERSAO_ATUAL"].ToString();
                            prjDev.dtCadastro = Convert.ToDateTime(objDr["DT_CADASTRO"].ToString());
                            prjDev.flEncerrado = Convert.ToBoolean(objDr["FL_ENCERRADO"].ToString());                           
                            prjDev.flCommitSolution = Convert.ToBoolean(objDr["FL_COMMIT_SOLUTION"].ToString());
                            prjDev.flVsProd = Convert.ToBoolean(objDr["FL_VS_EM_PROD"].ToString());
                          
                        }

                        return prjDev;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("[USP_PRJ_DESENVOLVIMENTO_LISTA_POR_ID] - : " + e.Number + " - Descrição: " + e.Message.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Problema com a consulta informada : " + ex.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema com a conexão ao banco de dados! ");
                }
            }
        }

        //CONSULTA ITENS RELACIONADOS A UM PROJETO/OS MATRIZ.
        public List<modItensPorOrdemServico> pubListaItensAtreladosNaOrdemdeServico(int idOs)
        {
            List<modItensPorOrdemServico> osProjetos = new List<modItensPorOrdemServico>();           

            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_ITENS_POR_ORDEM_SERVICO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_NM_SOLICITACAO", idOs);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modItensPorOrdemServico prjOs = null;                       

                        while (objDr.Read())
                        {
                            prjOs = new modItensPorOrdemServico();

                            prjOs.idNmSolitacao = Convert.ToInt32(objDr["ID_NM_SOLICITACAO"]);
                            prjOs.idProjeto = objDr["NOME_PROJETO"].ToString();
                            prjOs.nmVersao = objDr["NM_VERSAO_ATUAL"].ToString();
                            prjOs.dtCadastro = Convert.ToDateTime(objDr["DT_CADASTRO"].ToString());
                            prjOs.idItem = Convert.ToInt32(objDr["ID_ITEM"].ToString());
                            prjOs.NomeSolicitante = objDr["NOME_SOLICITANTE"].ToString();
                            prjOs.idDepartamento = objDr["NOME"].ToString();
                            prjOs.descicao = objDr["DESCRICAO"].ToString();
                            prjOs.dtAbertura = Convert.ToDateTime(objDr["DT_ABERTURA"].ToString());
                            prjOs.idDev = objDr["NOME_DESENVOLVEDOR"].ToString();
                            prjOs.idStatus = objDr["STATUS"].ToString();
                            prjOs.idPrioridade = objDr["PRIORIDADE_TIPO"].ToString();

                            osProjetos.Add(prjOs);                            
                        }

                        return osProjetos;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("USP_ITENS_POR_ORDEM_SERVICO -: " + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao tentar listar os projetos em desenvolvimento. " + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema com a conexão ao banco de dados! ");
                }
            }
        }

        //COMBO CARREGA NA MODAL LISTA DE PROJETOS EM DESENVOLVIMENTO PARA SELEÇÃO
        public List<modProjetoEmDesenvolvimento> pubComboListaProjetosEmDesenvolvimento()
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                List<modProjetoEmDesenvolvimento> lista = new List<modProjetoEmDesenvolvimento>();

                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PRJ_DESENVOLVIMENTO_CARREGA_COMBO_ATIVOS", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;                    

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modProjetoEmDesenvolvimento md = null;

                        while (objDr.Read())
                        {
                            md = new modProjetoEmDesenvolvimento();

                            md.idNmSolitacao = Convert.ToInt32(objDr["ID_NM_SOLICITACAO"].ToString());
                            md.nomeProjeto = objDr["NOME_PROJETO"].ToString();                          

                            lista.Add(md);
                        }

                        return lista;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("USP_PRJ_DESENVOLVIMENTO_CARREGA_COMBO_ATIVOS - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
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

        //ATUALIZA O PROJETO A SER INSERIDO O ITEM
        public void pubAtualizaItemParaOutraOS(modItens item)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    objDr = null;
                    cmd = new SqlCommand("USP_ITENS_ADMIN_ALTERA_OS_PROJETO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_ITEM", item.idItem);
                    cmd.Parameters.AddWithValue("@ID_NM_SOLICITACAO", item.idNmSolicitacao);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("USP_ITENS_ADMIN_ALTERA_OS_PROJETO - :" + ex.Number + " - Descrição: " + ex.Message.ToString());
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
    }
}
