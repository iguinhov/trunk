using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;
using Repository;

namespace Dal
{
    public class dalUsuarios : Conexao
    {
        usuarioCriptografiaRepository cript = new usuarioCriptografiaRepository();

        public void pubCadastraNovoUsuario(modUsuarios usuarios)
        {
            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_USUARIOS_CADASTRO", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NOME", usuarios.nome);
                    cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", usuarios.idDepartamento);
                    cmd.Parameters.AddWithValue("@EMAIL",usuarios.email);
                    cmd.Parameters.AddWithValue("@LOGIN",usuarios.login);
                    cmd.Parameters.AddWithValue("@SENHA", cript.codificaSenha(usuarios.senha));
                    cmd.Parameters.AddWithValue("@DT_CADASTRO", usuarios.dtCadastro);
                    cmd.Parameters.AddWithValue("@ID_TIPO_USUARIO", usuarios.idTipoUsuario);
                    cmd.Parameters.AddWithValue("@FL_ATIVO", usuarios.flAtivo);
                    cmd.Parameters.AddWithValue("@FL_DESENVOLVEDOR", usuarios.flDev);
                    cmd.Parameters.AddWithValue("@IMG_PERFIL", usuarios.imagem);                                        

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Erro ao tentar efetuar cadastro do usuário: - " + e.Message);
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

        public void pubAtualizaUsuario(modUsuarios usuarios)
        {
            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_USUARIOS_ATUALIZA", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_USUARIO", usuarios.idUsuario);
                    cmd.Parameters.AddWithValue("@NOME", usuarios.nome);
                    cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", usuarios.idDepartamento);
                    cmd.Parameters.AddWithValue("@EMAIL", usuarios.email);
                    cmd.Parameters.AddWithValue("@LOGIN", usuarios.login);
                    cmd.Parameters.AddWithValue("@SENHA", cript.codificaSenha(usuarios.senha));
                    cmd.Parameters.AddWithValue("@ID_TIPO_USUARIO", usuarios.idTipoUsuario);
                    cmd.Parameters.AddWithValue("@FL_ATIVO", usuarios.flAtivo);
                    cmd.Parameters.AddWithValue("@FL_DESENVOLVEDOR", usuarios.flDev);                    
                    cmd.Parameters.AddWithValue("@DT_ALTERACAO", usuarios.dtAlteracao);
                    cmd.Parameters.AddWithValue("@IMG_PERFIL", usuarios.imagem);

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao tentar atualizar usuário - : " + e.Message);
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

        public List<modUsuarios> pubListaTodosOsUsuarios()
        {
            List<modUsuarios> usuarios = new List<modUsuarios>();

            objDr = null;

            using(sqlCon = new SqlConnection(strCon))
            {
                cmd = new SqlCommand("USP_USUARIOS_LISTA_TODOS", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    sqlCon.Open();
                    objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);                    

                    modUsuarios usr = null;

                    while (objDr.Read())
                    {
                        usr = new modUsuarios();

                        usr.idUsuario = Convert.ToInt32(objDr["ID_USUARIO"].ToString());
                        usr.nome = objDr["NOME"].ToString();
                        usr.departamentoRetorno = objDr["DEPARTAMENTO"].ToString();
                        usr.email = objDr["EMAIL"].ToString();
                        usr.login = objDr["LOGIN"].ToString();
                        usr.senha = objDr["SENHA"].ToString();
                        usr.dtCadastro = Convert.ToDateTime(objDr["DT_CADASTRO"].ToString());
                        usr.tipoUsuarioRetorno = objDr["PERMISSAO"].ToString();
                        usr.flAtivoRetorno = objDr["FL_ATIVO"].ToString();
                        usr.flDesenvolvedorRetorno = objDr["FL_DESENVOLVEDOR"].ToString();                     

                        usuarios.Add(usr);
                    }

                    return usuarios;
                }
                catch (Exception e)
                {
                    throw new Exception("Problema ao tentar listar usuários cadastrados! - : " + e.Message);
                }

                finally
                {
                    FechaConexao();
                }
            }
        }

        public modUsuarios pubBuscaUsuarioPorId(int id)
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_USUARIOS_LISTA_TODOS_POR_ID", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_USUARIO", id);

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modUsuarios usuario = null;

                        while (objDr.Read())
                        {
                            usuario = new modUsuarios();

                            usuario.idUsuario = Convert.ToInt32(objDr["ID_USUARIO"].ToString());
                            usuario.nome = objDr["NOME"].ToString();
                            usuario.idDepartamento = Convert.ToInt32(objDr["ID_DEPARTAMENTO"].ToString());
                            usuario.email = objDr["EMAIL"].ToString();
                            usuario.login = objDr["LOGIN"].ToString();
                            usuario.senha = objDr["SENHA"].ToString();
                            usuario.dtCadastro = Convert.ToDateTime(objDr["DT_CADASTRO"].ToString());
                            usuario.idTipoUsuario = Convert.ToInt32(objDr["ID_TIPO_USUARIO"].ToString());
                            usuario.flAtivo = Convert.ToBoolean(objDr["FL_ATIVO"].ToString());
                            usuario.flDev = Convert.ToBoolean(objDr["FL_DESENVOLVEDOR"].ToString());                           
                        }

                        return usuario;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("[USP_USUARIOS_LISTA_TODOS_POR_ID] - : " + e.Number + " - Descrição: " + e.Message.ToString());
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

        //EFETUA LOGIN DO USUÁRIO E ARMAZENA NA TABELA DE LOGON
        public modUsuarios pubUsuarioLogar(string login, string senha)
        {
            objDr = null;

            using(sqlCon = new SqlConnection(strCon))
            {
                if(sqlCon != null)
                {
                    cmd = new SqlCommand("USP_USUARIOS_LOGAR", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LOGIN", login);
                    cmd.Parameters.AddWithValue("@SENHA", cript.codificaSenha(senha));

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modUsuarios usuario = null;

                        while (objDr.Read())
                        {
                            usuario = new modUsuarios();

                            usuario.idUsuario = Convert.ToInt32(objDr["ID_USUARIO"].ToString());
                            usuario.login = objDr["LOGIN"].ToString();
                            usuario.senha = objDr["SENHA"].ToString();                       
                            usuario.imagem = (byte[])objDr["IMG_PERFIL"];                                                     
                        }

                        return usuario;
                    }
                    catch(SqlException ex)
                    {
                        throw new Exception("[USP_USUARIOS_LOGAR] - : " + ex.Number + " - Descrição; " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao tentar efetuar login do usuário. " + e.Message);
                    }
                }
                else
                {
                    throw new Exception("Problema com a conexão ao banco de dados! ");
                }
            }
        }

        //DESLOGA USUÁRIO DO SISTEMA (ALTERA FLAG PARA 0)
        public void pubUsuarioDeslogar(int id)
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_USUARIOS_DESLOGAR", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_USUARIO", id);                   

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
         
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("[USP_USUARIOS_LOGAR] - : " + ex.Number + " - Descrição; " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao tentar efetuar login do usuário. " + e.Message);
                    }
                }
                else
                {
                    throw new Exception("Problema com a conexão ao banco de dados! ");
                }
            }
        }

        //CONSULTA USUÁRIO PELO LOGIN DA SESSÃO PARA ALTERAR PERFIL
        public modUsuarios pubUsuarioPropriedadesPerfil(string login)
        {
            objDr = null;

            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_USUARIO_CONSULTA_LOGIN", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LOGIN", login);                   

                    try
                    {
                        sqlCon.Open();
                        objDr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        modUsuarios usuario = null;

                        while (objDr.Read())
                        {
                            usuario = new modUsuarios();

                            usuario.nome = objDr["NOME"].ToString();
                            usuario.login = objDr["LOGIN"].ToString();
                            usuario.senha = objDr["SENHA"].ToString();
                            usuario.imagem = (byte[])objDr["IMG_PERFIL"];

                        }

                        return usuario;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("[USP_USUARIO_CONSULTA_LOGIN] - : " + ex.Number + " - Descrição; " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao tentar efetuar login do usuário. " + e.Message);
                    }
                }
                else
                {
                    throw new Exception("Problema com a conexão ao banco de dados! ");
                }
            }
        }

        //USUÁRIO ALTERA NOME E SENHA.
        public void pubAtualizaPerfilUsuarioNomeSenha(modUsuarios usuarios)
        {
            using (sqlCon = new SqlConnection(strCon))
            {
                if (sqlCon != null)
                {
                    cmd = new SqlCommand("USP_USUARIO_ALTERA_DADOS_PERFIL", sqlCon);
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.AddWithValue("@NOME", usuarios.nome);
                    cmd.Parameters.AddWithValue("@LOGIN", usuarios.login);
                    cmd.Parameters.AddWithValue("@SENHA", cript.codificaSenha(usuarios.senha));
                    cmd.Parameters.AddWithValue("@IMG_PERF", usuarios.imagem);                   

                    try
                    {
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex) {
                        throw new Exception("[USP_USUARIO_ALTERA_DADOS_PERFIL] - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Problema ao tentar atualizar usuário - : " + e.Message);
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
