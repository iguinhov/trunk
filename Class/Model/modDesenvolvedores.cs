using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modDesenvolvedores
    {
        
        private int _idDev;        
        private int _idUsuario;        
        private string _nomeCompleto;

        
        public modDesenvolvedores() { }

        [Display(Name = "Id desenvolvedor")]
        public int idDev
        {
            get { return _idDev; }
            set { _idDev = value; }
        }
        [Display(Name = "Id usuário")]
        public int idUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        [Display(Name = "Nome completo")]
        public string nomeCompleto
        {
            get { return _nomeCompleto; }
            set { _nomeCompleto = value; }
        }
    }
}
