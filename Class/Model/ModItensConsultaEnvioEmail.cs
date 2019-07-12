using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ModItensConsultaEnvioEmail
    {
        private int _idItem;
        private string _nomeProjeto;
        private string _versao;
        private string _solicitante;
        private string _dpto;
        private string _desc;
        private string _prioridade;
        private string _emailDev;
        private string _emailUser;
        private string _desenvolvedor;
        private string _descricaoDev;
        private string _statusItem;
        private string _emailAlter;

        public ModItensConsultaEnvioEmail() { } // Método construtor da classe.

        public int idItem
        {
            get { return _idItem; }
            set { _idItem = value; }
        }

        public string nomeProjeto
        {
            get { return _nomeProjeto; }
            set { _nomeProjeto = value; }
        }

        public string versao
        {
            get { return _versao; }
            set { _versao = value; }
        }

        public string solicitante
        {
            get { return _solicitante; }
            set { _solicitante = value; }
        }

        public string depto
        {
            get { return _dpto; }
            set { _dpto = value; }
        }

        public string desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public string prioridade
        {
            get { return _prioridade; }
            set { _prioridade = value; }
        }
        public string emailDev
        {
            get { return _emailDev; }
            set { _emailDev = value; }
        }

        public string emailUser
        {
            get { return _emailUser; }
            set { _emailUser = value; }
        }

        public string desenvolvedor
        {
            get { return _desenvolvedor; }
            set { _desenvolvedor = value; }
        }

        public string descricaoDev
        {
            get { return _descricaoDev; }
            set { _descricaoDev = value; }
        }

        public string statusItem
        {
            get { return _statusItem; }
            set { _statusItem = value; }
        }

        public string emailAlter
        {
            get { return _emailAlter; }
            set { _emailAlter = value; }
        }
    }
}
