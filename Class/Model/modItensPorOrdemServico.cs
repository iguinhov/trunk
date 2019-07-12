using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modItensPorOrdemServico
    {
        private int _idNmSolitacao;
        private string _idProjeto;
        private string _nmVersao;
        private DateTime _dtCadastro;
        private int _idItem;
        private string _NomeSolicitante;
        private string _idDepartamento;
        private DateTime _dtAbertura;
        private string _descicao;
        private string _idDev;
        private string _idStatus;
        private string _idPrioridade;


        public modItensPorOrdemServico() { }


        [Display(Name = "OS")]
        public int idNmSolitacao
        {
            get { return _idNmSolitacao; }
            set { _idNmSolitacao = value; }
        }

        [Display(Name = "Projeto")]        
        public string idProjeto
        {
            get { return _idProjeto; }
            set { _idProjeto = value; }
        }
        [Display(Name = "Versão atual")]
        public string nmVersao
        {
            get { return _nmVersao; }
            set { _nmVersao = value; }
        }
        [Display(Name = "Cadastro")]
        public DateTime dtCadastro
        {
            get { return _dtCadastro; }
            set { _dtCadastro = value; }
        }

        [Display(Name = "Nº item")]
        public int idItem
        {
            get { return _idItem; }
            set { _idItem = value; }
        }

        [Display(Name = "Solicitante")]       
        public string NomeSolicitante
        {
            get { return _NomeSolicitante; }
            set { _NomeSolicitante = value; }
        }
        [Display(Name = "Departamento")]
        public string idDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }
        [Display(Name = "Abertura")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido.")]
        public DateTime dtAbertura
        {
            get { return _dtAbertura; }
            set { _dtAbertura = value; }
        }
        [Display(Name = "Descrição")]       
        public string descicao
        {
            get { return _descicao; }
            set { _descicao = value; }
        }
        [Display(Name = "Desenvolvedor Responsável")]
        public string idDev
        {
            get { return _idDev; }
            set { _idDev = value; }
        }
        [Display(Name = "Status")]
        public string idStatus
        {
            get { return _idStatus; }
            set { _idStatus = value; }
        }
        [Display(Name = "Prioridade")]
        public string idPrioridade
        {
            get { return _idPrioridade; }
            set { _idPrioridade = value; }
        }
    }
}
