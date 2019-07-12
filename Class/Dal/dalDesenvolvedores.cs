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
    public class dalDesenvolvedores : Conexao
    {
        public List<modDesenvolvedores> pubListaDesenvolvedores()
        {
            List<modDesenvolvedores> desenvs = new List<modDesenvolvedores>();

            using (sqlCon = new SqlConnection(strCon))
            {
                objDr = null;

                cmd = new SqlCommand("USP_DESENVOLVEDORES_LISTA", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlCon.Open();

                    objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    modDesenvolvedores dev = null;

                    while (objDr.Read())
                    {
                        dev = new modDesenvolvedores();

                        dev.idDev = Convert.ToInt32(objDr["ID_DEV"].ToString());
                        dev.idUsuario = Convert.ToInt32(objDr["ID_USUARIO"].ToString());
                        dev.nomeCompleto = objDr["NOME_DESENVOLVEDOR"].ToString();

                        desenvs.Add(dev);
                    }

                    return desenvs;
                }
                catch (Exception e)
                {

                    throw new Exception("USP_DESENVOLVEDORES_LISTA - : " + e.Message);
                }

                finally
                {
                    FechaConexao();
                }
            }
        }       

        public void pubRemoveDesenvolvedor(modDesenvolvedores desenvolvedor)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (strCon != null)
                {
                    cmd = new SqlCommand("USP_DESENVOLVEDOR_DELETE", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_DEV", desenvolvedor.idDev);

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

        public modDesenvolvedores pubBuscaDesenvolvedorPorId(int id)
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_DESENVOLVEDORES_LISTA_POR_ID", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_DEV", id);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modDesenvolvedores dev = null;

                        while (objDr.Read())
                        {
                            dev = new modDesenvolvedores();

                            dev.idDev = Convert.ToInt32(objDr["ID_DEV"]);
                            dev.idUsuario = Convert.ToInt32(objDr["ID_USUARIO"]);
                            dev.nomeCompleto = objDr["NOME_DESENVOLVEDOR"].ToString();
                        }

                        return dev;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("[USP_DESENVOLVEDORES_LISTA_POR_ID] - : " + e.Number + " - Descrição: " + e.Message.ToString());
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
