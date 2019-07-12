using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modStatus
    {
       
        private int _idStatus;        
        private string _descricao;

        public modStatus() { }

        [Display(Name = "Id status")]
        public int idStatus {
            get { return _idStatus; }
            set { _idStatus = value; }
        }
        [Display(Name = "Descrição")]
        public string descricao {
            get { return _descricao; }
            set { _descricao = value; }
        }
    }
}
