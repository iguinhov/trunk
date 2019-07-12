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
    public class dalPrioridade : Conexao
    {
        public List<modPrioridade> pubListaPrioridades()
        {
            List<modPrioridade> prioridades = new List<modPrioridade>();

            using (sqlCon = new SqlConnection(strCon))
            {
                objDr = null;

                cmd = new SqlCommand("USP_PRIORIDADES_LISTA", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlCon.Open();

                    objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    modPrioridade priorid = null;

                    while (objDr.Read())
                    {
                        priorid = new modPrioridade();

                        priorid.idPrioridade = Convert.ToInt32(objDr["ID"].ToString());
                        priorid.descricao = objDr["PRIORIDADE_TIPO"].ToString();

                        prioridades.Add(priorid);
                    }

                    return prioridades;
                }
                catch (Exception e)
                {

                    throw new Exception("USP_PRIORIDADES_LISTA - : " + e.Message);
                }

                finally
                {
                    FechaConexao();
                }
            }
        }

        public void pubAtualizaPrioridade(modPrioridade prioridade)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PRIORIDADE_ATUALIZA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", prioridade.idPrioridade);
                    cmd.Parameters.AddWithValue("@PRIORIDADE_TIPO", prioridade.descricao);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_PRIORIDADE_ATUALIZA - : " + e.Message);
                    }

                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema ao tentar executar requisição!");
                }
            }
        }

        public void pubCadastraPrioridade(modPrioridade prioridade)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PRIORIDADE_CADASTRO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PRIORIDADE_TIPO", prioridade.descricao);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_PRIORIDADE_CADASTRO = : " + e.Message);
                    }

                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema ao tentar executar requisição!");
                }
            }
        }

        public void pubRemovePrioridade(modPrioridade prioridade)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (strCon != null)
                {
                    cmd = new SqlCommand("USP_PRIORIDADE_DELETE", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", prioridade.idPrioridade);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_PRIORIDADE_DELETE - :" + e.Message);
                    }
                    finally
                    {
                        FechaConexao();
                    }
                }
                else
                {
                    throw new Exception("Problema ao tentar executar requisição!");
                }
            }
        }

        public modPrioridade pubBuscaPrioridadePorId(int id)
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PRIORIDADES_LISTA_POR_ID", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modPrioridade prt = null;

                        while (objDr.Read())
                        {
                            prt = new modPrioridade();

                            prt.idPrioridade = Convert.ToInt32(objDr["ID"]);
                            prt.descricao = objDr["PRIORIDADE_TIPO"].ToString();
                        }

                        return prt;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("[USP_PRIORIDADES_LISTA_POR_ID] - : " + e.Number + " - Descrição: " + e.Message.ToString());
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
    }
}
