using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Persistence;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{
    public class dalDepartamento : Conexao
    {
        public List<modDepartamento> pubListaDepartamentos()
        {
            List<modDepartamento> dptos = new List<modDepartamento>();

            using(sqlCon = new SqlConnection(strCon))
            {
                cmd = new SqlCommand("USP_DEPARTAMENTOS_LISTA", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlCon.Open();

                    objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);                    

                    modDepartamento dp = null;

                    while (objDr.Read())
                    {
                        dp = new modDepartamento();

                        dp.idDepartamento = Convert.ToInt32(objDr["ID_DEPARTAMENTO"].ToString());
                        dp.nome = objDr["NOME"].ToString();

                        dptos.Add(dp);
                    }

                    return dptos;

                }
                catch (Exception e)
                {
                    throw new Exception("Problema ao listar departamentos: - " + e.Message);
                }

                finally
                {
                    FechaConexao();
                }
            }            
        }

        public void pubAtualizaDepartamento(modDepartamento dpto)
        {
            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_DEPARTAMENTOS_ATUALIZA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", dpto.idDepartamento);
                    cmd.Parameters.AddWithValue("@NOME", dpto.nome);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception e)
                    {
                        throw new Exception("USP_DEPARTAMENTOS_ATUALIZA - : " + e.Message);
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
        
        public void pubCadastraDepartamento(modDepartamento dpto)
        {
            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_DEPARTAMENTOS_CADASTRO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NOME", dpto.nome);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_DEPARTAMENTOS_CADASTRO = : " + e.Message);
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
        
        public void pubRemoveDepartamentoPorId(int id)
        {
            using(sqlCon = new SqlConnection(strCon))
            {
                if(strCon != null)
                {
                    cmd = new SqlCommand("USP_DEPARTAMENTO_DELETE", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", id);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("USP_DEPARTAMENTO_DELETE - :" + e.Message);
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
        
        public modDepartamento pubBuscaDetalhesPorId(int id)
        {          
            objDr = null;

            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_DEPARTAMENTOS_LISTA_POR_ID", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", id);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);                        

                        modDepartamento dpt = null;

                        while (objDr.Read())
                        {
                            dpt = new modDepartamento();

                            dpt.idDepartamento = Convert.ToInt32(objDr["ID_DEPARTAMENTO"]);
                            dpt.nome = objDr["NOME"].ToString();                           
                        }

                        return dpt;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("[USP_DEPARTAMENTOS_LISTA_POR_ID] - : " + e.Number + " - Descrição: " + e.Message.ToString());
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
