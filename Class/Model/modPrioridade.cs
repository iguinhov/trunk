using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modPrioridade
    {
        
        private int _idPrioridade;        
        private string _descricao;

        public modPrioridade() { }

        [Display(Name = "Id prioridade")]
        public int idPrioridade
        {
            get { return _idPrioridade; }
            set { _idPrioridade = value; }
        }
        [Display(Name = "Descrição")]
        public string descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
    }
}
