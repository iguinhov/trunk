using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modUsuariosLogados
    {
        private int _idLogon;
        private string _loginUsuario;

        public modUsuariosLogados() { }

        public int idLogon
        {
            get { return _idLogon; }
            set { _idLogon = value; }
        }

        public string loginUsuario
        {
            get { return _loginUsuario; }
            set { _loginUsuario = value; }
        }
    }
}
