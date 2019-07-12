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
    public class dalSistemasProjetos : Conexao
    {
        public List<modSistemasProjeto> pubListaProjetos()
        {
            List<modSistemasProjeto> projetos = new List<modSistemasProjeto>();

            using (sqlCon = new SqlConnection(strCon))
            {
                objDr = null;

                cmd = new SqlCommand("USP_PROJETOS_SISTEMAS_LISTA", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlCon.Open();

                    objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    modSistemasProjeto prj = null;

                    while (objDr.Read())
                    {
                        prj = new modSistemasProjeto();

                        prj.idProjeto = Convert.ToInt32(objDr["ID_PROJETO"].ToString());
                        prj.nomeProjeto = objDr["NOME_PROJETO"].ToString();

                        projetos.Add(prj);
                    }

                    return projetos;
                }
                catch (Exception e)
                {

                    throw new Exception("USP_PROJETOS_SISTEMAS_LISTA - : " + e.Message);
                }

                finally
                {
                    FechaConexao();
                }
            }
        }

        public void pubAtualizaProjeto(modSistemasProjeto projetos)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PROJETOS_SISTEMAS_ATUALIZA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_PROJETO", projetos.idProjeto);
                    cmd.Parameters.AddWithValue("@NOME_PROJETO", projetos.nomeProjeto);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_PROJETOS_SISTEMAS_ATUALIZA - : " + e.Message);
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

        public void pubCadastraProjeto(modSistemasProjeto projetos)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PROJETOS_SISTEMAS_CADASTRO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NOME_PROJETO", projetos.nomeProjeto);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_PROJETOS_SISTEMAS_CADASTRO = : " + e.Message);
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

        public void pubRemoveProjeto(modSistemasProjeto projetos)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (strCon != null)
                {
                    cmd = new SqlCommand("USP_PROJETOS_SISTEMAS_DELETE", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_PROJETO", projetos.idProjeto);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_PROJETOS_SISTEMAS_DELETE - :" + e.Message);
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

        public modSistemasProjeto pubBuscaProjetoPorId(int id)
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_PROJETOS_SISTEMAS_LISTA_POR_ID", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_PROJETO", id);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modSistemasProjeto prj = null;

                        while (objDr.Read())
                        {
                            prj = new modSistemasProjeto();

                            prj.idProjeto = Convert.ToInt32(objDr["ID_PROJETO"]);
                            prj.nomeProjeto = objDr["NOME_PROJETO"].ToString();
                        }

                        return prj;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("[USP_PROJETOS_SISTEMAS_LISTA_POR_ID] - : " + e.Number + " - Descrição: " + e.Message.ToString());
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
