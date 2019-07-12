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
    public class dalTipoDesenvolvimento : Conexao
    {
        public List<modTipoDesenvolvimento> pubListaTiposDesenvolvimento()
        {
            List<modTipoDesenvolvimento> tpsDesenvs = new List<modTipoDesenvolvimento>();

            using (sqlCon = new SqlConnection(strCon))
            {
                objDr = null;

                cmd = new SqlCommand("USP_TIPO_DESENVOLVIMENTOS_LISTA", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlCon.Open();

                    objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    modTipoDesenvolvimento tpdesen = null;

                    while (objDr.Read())
                    {
                        tpdesen = new modTipoDesenvolvimento();

                        tpdesen.idTipoDesenvolvimento = Convert.ToInt32(objDr["ID_TIPO_DESENV"].ToString());
                        tpdesen.descricao = objDr["DESCRICAO"].ToString();

                        tpsDesenvs.Add(tpdesen);
                    }

                    return tpsDesenvs;
                }
                catch (Exception e)
                {

                    throw new Exception("USP_TIPO_DESENVOLVIMENTOS_LISTA - : " + e.Message);
                }

                finally
                {
                    FechaConexao();
                }
            }
        }

        public void pubAtualizaTipoDesenvolvimento(modTipoDesenvolvimento tpDesenvolvimento)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_TIPO_DESENVOLVIMENTO_ATUALIZA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_TIPO_DESENV", tpDesenvolvimento.idTipoDesenvolvimento);
                    cmd.Parameters.AddWithValue("@DESCRICAO", tpDesenvolvimento.descricao);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_TIPO_DESENVOLVIMENTO_ATUALIZA - : " + e.Message);
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

        public void pubCadastraTipoDesenvolvimento(modTipoDesenvolvimento tpDesenvolvimento)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_TIPO_DESENVOLVIMENTO_CADASTRO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@DESCRICAO", tpDesenvolvimento.descricao);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_TIPO_DESENVOLVIMENTO_CADASTRO = : " + e.Message);
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

        public void pubRemoveTipoDesenvolvimentoPorId(modTipoDesenvolvimento tpDesenvolvimento)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (strCon != null)
                {
                    cmd = new SqlCommand("USP_TIPO_DESENVOLVIMENTO_DELETE", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_TIPO_DESENV", tpDesenvolvimento.idTipoDesenvolvimento);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_TIPO_DESENVOLVIMENTO_DELETE - :" + e.Message);
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

        public modTipoDesenvolvimento pubTipoDesenvolvimentoPorId(int id)
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_TIPO_DESENVOLVIMENTOS_LISTA_POR_ID", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_TIPO_DESENV", id);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modTipoDesenvolvimento tpDesenv = null;

                        while (objDr.Read())
                        {
                            tpDesenv = new modTipoDesenvolvimento();

                            tpDesenv.idTipoDesenvolvimento = Convert.ToInt32(objDr["ID_TIPO_DESENV"].ToString());
                            tpDesenv.descricao = objDr["DESCRICAO"].ToString();
                        }

                        return tpDesenv;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("[USP_TIPO_DESENVOLVIMENTOS_LISTA_POR_ID] - : " + e.Number + " - Descrição: " + e.Message.ToString());
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
