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
    public class dalTipoUsuario : Conexao
    {
        public List<modTipoUsuario> pubListaTipoUsuarios()
        {
            List<modTipoUsuario> tpUsuarios = new List<modTipoUsuario>();

            using(sqlCon = new SqlConnection(strCon))
            {
                objDr = null;

                cmd = new SqlCommand("USP_TIPO_USUARIO_LISTA", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlCon.Open();

                    objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    modTipoUsuario tpUsuario = null;

                    while (objDr.Read())
                    {
                        tpUsuario = new modTipoUsuario();

                        tpUsuario.idTipoUsuario = Convert.ToInt32(objDr["ID_TIPO_USUARIO"].ToString());
                        tpUsuario.descricao = objDr["DESCRICAO"].ToString();

                        tpUsuarios.Add(tpUsuario);
                    }

                    return tpUsuarios;
                }
                catch (Exception e)
                {

                    throw new Exception("USP_TIPO_USUARIO_LISTA - : " + e.Message);
                }

                finally
                {
                    FechaConexao();
                }
            }
        }

        public void pubAtualizaTipoUsuario(modTipoUsuario tpUsuario)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_TIPO_USUARIO_ATUALIZA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_TIPO_USUARIO", tpUsuario.idTipoUsuario);
                    cmd.Parameters.AddWithValue("@DESCRICAO", tpUsuario.descricao);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_TIPO_USUARIO_ATUALIZA - : " + e.Message);
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

        public void pubCadastraTipoUsuario(modTipoUsuario tpUsuario)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_TIPO_USUARIO_CADASTRO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DESCRICAO", tpUsuario.descricao);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_TIPO_USUARIO_CADASTRO = : " + e.Message);
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

        public void pubRemoveUsuarioPorId(modTipoUsuario tpUsuario)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (strCon != null)
                {
                    cmd = new SqlCommand("USP_TIPO_USUARIO_DELETE", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_TIPO_USUARIO", tpUsuario.idTipoUsuario);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_TIPO_USUARIO_DELETE - :" + e.Message);
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

        public modTipoUsuario pubTipoUsuarioPorId(int id)
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_TIPO_USUARIO_LISTA_POR_ID", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_TIPO_USUARIO", id);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modTipoUsuario tpUsuario = null;

                        while (objDr.Read())
                        {
                            tpUsuario = new modTipoUsuario();

                            tpUsuario.idTipoUsuario = Convert.ToInt32(objDr["ID_TIPO_USUARIO"].ToString());
                            tpUsuario.descricao = objDr["DESCRICAO"].ToString();
                        }

                        return tpUsuario;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("[USP_TIPO_USUARIO_LISTA_POR_ID] - : " + e.Number + " - Descrição: " + e.Message.ToString());
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
