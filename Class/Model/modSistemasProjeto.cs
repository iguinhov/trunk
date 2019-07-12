using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modSistemasProjeto
    {
        
        private int _idProjeto;       
        private string _nomeProjeto;

        public modSistemasProjeto() { }

        [Display(Name = "Id Projeto")]
        public int idProjeto
        {
            get { return _idProjeto; }
            set { _idProjeto = value; }
        }
        [Display(Name = "Nome projeto")]
        public string nomeProjeto
        {
            get { return _nomeProjeto; }
            set { _nomeProjeto = value; }
        }
    }
}
