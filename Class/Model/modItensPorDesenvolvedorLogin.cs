using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modItensPorDesenvolvedorLogin
    {
        private int _idNmSolicitacao;
        private int _idItem;
        private string _nmVersao;
        private string _projeto;
        private string _solicitante;
        private DateTime _dtAbertura;
        private DateTime _dtProgramada;
        private string _titulo;
        private string _status;
        private string _prioridade;
        private string _descricao;

        public modItensPorDesenvolvedorLogin()
        {

        }

        public int idNmSolicitacao
        {
            get { return _idNmSolicitacao; }
            set { _idNmSolicitacao = value; }
        }

        public int idItem
        {
            get { return _idItem; }
            set { _idItem = value; }
        }

        public string projeto
        {
            get { return _projeto; }
            set { _projeto = value; }
        }

        public string solicitante
        {
            get { return _solicitante; }
            set { _solicitante = value; }
        }

        public DateTime dtAbertura
        {
            get { return _dtAbertura; }
            set { _dtAbertura = value; }
        }
        public DateTime dtProgramada
        {
            get { return _dtProgramada; }
            set { _dtProgramada = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string prioridade
        {
            get { return _prioridade; }
            set { _prioridade = value; }
        }
        public string descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public string titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string nmVersao
        {
            get { return _nmVersao; }
            set { _nmVersao = value; }
        }

    }
}
