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
    public class dalStatus : Conexao
    {
        public List<modStatus> pubListaStatus()
        {
            List<modStatus> statuss = new List<modStatus>();

            using (sqlCon = new SqlConnection(strCon))
            {
                objDr = null;

                cmd = new SqlCommand("USP_STATUS_LISTA", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlCon.Open();

                    objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    modStatus sts = null;

                    while (objDr.Read())
                    {
                        sts = new modStatus();

                        sts.idStatus = Convert.ToInt32(objDr["ID_STATUS"].ToString());
                        sts.descricao = objDr["DESCRICAO"].ToString();

                        statuss.Add(sts);
                    }

                    return statuss;
                }
                catch (Exception e)
                {

                    throw new Exception("USP_STATUS_LISTA - : " + e.Message);
                }

                finally
                {
                    FechaConexao();
                }
            }
        }

        public void pubAtualizaStatus(modStatus status)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_STATUS_ATUALIZA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_STATUS", status.idStatus);
                    cmd.Parameters.AddWithValue("@DESCRICAO", status.descricao);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_STATUS_ATUALIZA - : " + e.Message);
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

        public void pubCadastraStatus(modStatus status)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_STATUS_CADASTRO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DESCRICAO", status.descricao);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_STATUS_CADASTRO = : " + e.Message);
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

        public void pubRemoveStatusPorId(modStatus status)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (strCon != null)
                {
                    cmd = new SqlCommand("USP_STATUS_DELETE", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_STATUS", status.idStatus);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_STATUS_DELETE - :" + e.Message);
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

        public modStatus pubBuscaStatusPorId(int id)
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_STATUS_LISTA_POR_ID", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_STATUS", id);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modStatus sts = null;

                        while (objDr.Read())
                        {
                            sts = new modStatus();

                            sts.idStatus = Convert.ToInt32(objDr["ID_STATUS"]);
                            sts.descricao = objDr["DESCRICAO"].ToString();
                        }

                        return sts;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("[USP_STATUS_LISTA_POR_ID] - : " + e.Number + " - Descrição: " + e.Message.ToString());
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
