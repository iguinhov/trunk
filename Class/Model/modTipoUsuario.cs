using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modTipoUsuario
    {
       
        private Int32 _idTipoUsuario;        
        private string _descricao;

        public modTipoUsuario() { }

        [Display(Name = "Id tipo usuário")]
        public Int32 idTipoUsuario {
            get { return _idTipoUsuario; }
            set { _idTipoUsuario = value; }
        }
        [Display(Name = "Descrição")]
        public string descricao {
            get { return _descricao; }
            set { _descricao = value; } 
        }
    }
}
