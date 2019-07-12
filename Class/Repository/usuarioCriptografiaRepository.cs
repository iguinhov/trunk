using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Security.Cryptography;
using Repository.Interface;

namespace Repository
{
    public class usuarioCriptografiaRepository : interfCriptografiaRepository
    {
        //public string senhaCriptografia(string senha)
        //{
        //    return FormsAuthentication.HashPasswordForStoringInConfigFile(senha, "MD5");
        //}

        public string codificaSenha(string texto)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes.
            byte[] data = md5Hash.ComputeHash(Encoding.Default.GetBytes(texto));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }
            //retorna string com formato convertido para hash MD5
            return sBuilder.ToString();
        }

        public string decodificaSenha(string texto)
        {
            // Convert the input string to a byte array and compute the hash.
            Byte[] b = Convert.FromBase64String(texto);
            return System.Text.ASCIIEncoding.ASCII.GetString(b);
        }

    }
}
