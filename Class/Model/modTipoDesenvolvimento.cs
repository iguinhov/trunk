using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modTipoDesenvolvimento
    {
        
        private int _idTipoDesenvolvimento;        
        private string _descricao;

        public modTipoDesenvolvimento() { }

        [Display(Name = "Id tipo desenvolvimento")]
        public int idTipoDesenvolvimento
        {
            get { return _idTipoDesenvolvimento; }
            set { _idTipoDesenvolvimento = value; }
        }
        [Display(Name = "Descrição")]
        public string descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
    }
}
