using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modDepartamento
    {
        
        private int _idDepartamento;        
        private string _nome;

        public modDepartamento(){

        }

        [Display(Name = "Id departamento")]
        public int idDepartamento {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }
        [Display(Name = "Nome")]
        public string nome {
            get { return _nome; }
            set { _nome = value; }
        }
    }
}
