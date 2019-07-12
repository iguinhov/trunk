using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modProjetoEmDesenvolvimento
    {
        
        private int _idNmSolitacao;
        private int _idProjeto;       
        private string _nmVersao;       
        private DateTime _dtCadastro;       
        private bool? _flEncerrado;       
        private DateTime _dtFinalizado;       
        private string _nmVersaoFim;       
        private bool? _flCommitSolution;       
        private bool? _flVsProd;        
        private DateTime _dtPublicacaoFim;       
        private string _idUsuarioLogado;
        private string _nomeProjeto;
        
        //Método construtor da classe
        public modProjetoEmDesenvolvimento() { }

        [Display(Name = "Número da Ordem de Serviço")]
        public int idNmSolitacao {
            get { return _idNmSolitacao; }
            set { _idNmSolitacao = value; }
        }

        [Display(Name = "Projeto")]
        [Required(ErrorMessage = "O projeto é obrigatório.", AllowEmptyStrings = false)]
        public int idProjeto {
            get { return _idProjeto; }
            set { _idProjeto = value; }
        }
        [Display(Name = "Número da versão atual")]
        public string nmVersao {
            get { return _nmVersao; }
            set { _nmVersao = value; }
        }
        [Display(Name = "Data de cadastro")]
        public DateTime dtCadastro {
            get { return _dtCadastro; }
            set { _dtCadastro = value; }
        }
        [Display(Name = "Encerrado")]
        public bool? flEncerrado {
            get { return _flEncerrado; }
            set { _flEncerrado = value; }
        }
        [Display(Name = "Data finalizado")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido.")]
        public DateTime dtFinalizado {
            get { return _dtFinalizado; }
            set { _dtFinalizado = value; }
        }
        [Display(Name = "Número da versão final")]
        public string nmVersaoFim {
            get { return _nmVersaoFim; }
            set { _nmVersaoFim = value; }
        }
        [Display(Name = "Commit na solution")]
        public bool? flCommitSolution {
            get { return _flCommitSolution; }
            set { _flCommitSolution = value; }
        }
        [Display(Name = "Versão em produção")]
        public bool? flVsProd {
            get { return _flVsProd; }
            set { _flVsProd = value; }
        }
        [Display(Name = "Data final da publicação")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido.")]
        public DateTime dtPublicacaoFim {
            get { return _dtPublicacaoFim; }
            set { _dtPublicacaoFim = value; }
        }
        [Display(Name = "Projeto")]
        public string nomeProjeto
        {
            get { return _nomeProjeto; }
            set { _nomeProjeto = value; }
        }

        public string idUsuarioLogado
        {
            get { return _idUsuarioLogado; }
            set { _idUsuarioLogado = value; }
        }
    }
}
