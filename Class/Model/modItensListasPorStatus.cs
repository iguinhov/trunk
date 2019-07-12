using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modItensListasPorStatus
    {
        private string _projeto;
        private string _nmVersao;
        private string _solicitante;
        private int _idItem;
        private string _titulo;
        private string _desenvolvedor;
        private string _status;
        private DateTime _dtCadastro;
        private DateTime _dtProgramada;
        private DateTime _dtFinalizado;
        private string _descricao;

        public modItensListasPorStatus() { }

        public string projeto {
            get { return _projeto; }
            set { _projeto = value; }
        }
        public string solicitante {
            get { return _solicitante; }
            set { _solicitante = value; }
        }
        public int idItem {
            get { return _idItem; }
            set { _idItem = value; }
        }
        public string titulo {
            get { return _titulo; }
            set { _titulo = value; }
        }
        public string desenvolvedor {
            get { return _desenvolvedor; }
            set { _desenvolvedor = value; }
        }
        public string status {
            get { return _status; }
            set { _status = value; }
        }
        public DateTime dtCadastro {
            get { return _dtCadastro; }
            set { _dtCadastro = value; }
        }
        public DateTime dtProgramada {
            get { return _dtProgramada; }
            set { _dtProgramada = value; }
        }
        public DateTime dtFinalizado {
            get { return _dtFinalizado; }
            set { _dtFinalizado = value; }
        }
        public string descricao {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public string nmVersao
        {
            get { return _nmVersao; }
            set { _nmVersao = value; }
        }        
    }
}
