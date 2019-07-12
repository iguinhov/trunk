using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Conexao
    {
        protected string strCon = ConfigurationManager.ConnectionStrings["cxSQLSistemas"].ConnectionString;
        protected SqlConnection sqlCon;
        protected SqlCommand cmd;
        protected SqlDataReader objDr;
        protected DataSet objDataSet;
        protected SqlDataAdapter objDa;

        protected void AbreConexao()
        {
            try
            {
                sqlCon = new SqlConnection(strCon);

                sqlCon.Open();
            }

            catch (SqlException ex) {

                throw new Exception("Problema de conexão com o servidor SQL - " + ex.Number + " - Descrição: " + ex.Message.ToString());
            }

            catch (Exception e)
            {
                throw new Exception("Erro : - " + e.Message);
            }
            
        }

        protected void FechaConexao()
        {
            try
            {
                sqlCon.Close();
            }
            catch(SqlException ex)
            {
                throw new Exception("[Erro ao encerrar conexão com o banco de dados] - : " + ex.Number + " - Descrição: " + ex.Message.ToString());
            }

            catch (Exception e)
            {

                throw new Exception ("Problema ao encerrar conexão:  - " + e.Message);
            }
        }
    }
}
