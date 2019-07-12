using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modItensFinalizadosDetalhe
    {
        private string _projeto;
        private string _nmVersao;
        private string _solicitante;
        private string _departamento;
        private int _idItem;
        private string _titulo;
        private string _desenvolvedor;
        private string _status;
        private string _prioridade;
        private DateTime _dtAbertura;         
        private DateTime _dtProgramada;
        private DateTime _dtTermino;
        private string _descricao;
        private string _descricaoDesenvolvedor;
        private string _camadaMetodos;
        private string _proceduresNomes;
        private string _flCommit;

        public modItensFinalizadosDetalhe() { }

        public string projeto
        {
            get { return _projeto; }
            set { _projeto = value; }
        }
        public string nmVersao
        {
            get { return _nmVersao; }
            set { _nmVersao = value; }
        }
        public string solicitante
        {
            get { return _solicitante; }
            set { _solicitante = value; }
        }
        public string departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }
        public int idItem
        {
            get { return _idItem; }
            set { _idItem = value; }
        }
        public string titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }
        public string desenvolvedor
        {
            get { return _desenvolvedor; }
            set { _desenvolvedor = value; }
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
        public DateTime dtTermino
        {
            get { return _dtTermino; }
            set { _dtTermino = value; }
        }
        public string descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public string descricaoDesenvolvedor
        {
            get { return _descricaoDesenvolvedor; }
            set { _descricaoDesenvolvedor = value; }
        }
        public string camadaMetodos
        {
            get { return _camadaMetodos; }
            set { _camadaMetodos = value; }
        }
        public string proceduresNomes
        {
            get { return _proceduresNomes; }
            set { _proceduresNomes = value; }
        }
        public string flCommit
        {
            get { return _flCommit; }
            set { _flCommit = value; }
        }

    }
}
