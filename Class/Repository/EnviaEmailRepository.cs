using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EnviaEmailRepository
    {
        /// <summary>
        /// Smtp default
        /// </summary>
        private string smtp = "iggorcarreiro2@gmail.com";

        /// <summary>
        /// Login default
        /// </summary>
        private string login = "SISTEMA DE OS :: OS :: <iggorcarreiro2@gmail.com";

        /// <summary>
        /// Senha default
        /// </summary>
        private string senha = "avatar11";
        /// <summary>
        /// inica a classe
        /// </summary>
        /// <param name="config">string com as configurações de e-mail</param>
        public EnviaEmailRepository(string config = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(config))
                {
                    var itens = config.Split(';');
                    this.login = itens[0].Split('=')[1];
                    this.senha = itens[1].Split('=')[1];
                    this.smtp = itens[2].Split('=')[1];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Função para enviar um e-mail
        /// </summary>
        /// <param name="to">Parametro tipo String que define para quem vai o e-mail</param>
        /// <param name="subject">Parametro tipo String que define qual o assunto do e-mail</param>
        /// <param name="body">Parametro tipo String que define o corpo do e-mail</param>
        /// <param name="msg">Parametro tipo String que retorna uma menssagem apenas se o e-mail gerar uma exceção</param>
        /// <param name="from">Parametro tipo String que define de quem é o e-mail possue um valor default</param>
        /// <param name="arquivo">Parametro tipo String que recebe o caminho do arquivo para anexo possue um valor default</param>
        /// <returns>Retorna true caso o e-mail tenha sido enviado com sucesso e false caso tenha ocorrido algum problema</returns>
        /// <exception cref="System.Exception">Lança uma exeção caso não seja possivel enviar e-mail</exception>
        /// <example>
        ///     var   ee = new EnviaEmail();
        ///     bool mando = ee.MandaEmail(EMAIL_PARA, TITULO_DO_EMAIL, MENSSAGEM_DO_EMAIL, out VARIAVEL_TIPO_STRING, EMAIL_DE (OPCIONAL), CAMINHO_DO_ARQUIVO_PARA_ANEXO (OPCIONAL));
        /// </example>
        public bool MandaEmail(string to, string subject, string body, out string msg, string from = "", string arquivo = "")
        {
            from = string.IsNullOrWhiteSpace(from) ? this.login : from;
            //Verifica se falta algum parametro para envio de e-mail
            if (string.IsNullOrWhiteSpace(this.smtp) ||/* string.IsNullOrWhiteSpace(this.login) || string.IsNullOrWhiteSpace(this.senha) ||*/ string.IsNullOrWhiteSpace(from))
            {
                string listItens = string.Empty;
                listItens = string.Concat(string.IsNullOrWhiteSpace(this.smtp) ? " SMTP vazio. " : string.Empty);
                listItens = string.Concat(string.IsNullOrWhiteSpace(this.login) ? " LOGIN vazio. " : string.Empty);
                listItens = string.Concat(string.IsNullOrWhiteSpace(this.senha) ? " SENHA vazio. " : string.Empty);
                listItens = string.Concat(string.IsNullOrWhiteSpace(from) ? " FROM vazio. " : string.Empty);
                msg = string.Format("Falta os seguintes itens para enviar e-mail: {0}", listItens);
                return false;
            }

            //cria o e-mail
            MailMessage mailMsg = new MailMessage();
            //configura o e-mail
            MailAddress mailAd = new MailAddress(from);
            //configura o smtp
            SmtpClient smtpClient = new SmtpClient(this.smtp);

            try
            {
                var emails = to.ToString().Split(';');
                foreach (var i in emails)
                {
                    //define para quem será enviado
                    mailMsg.To.Add(i.Trim());
                }
                //define de quem é o e-mail
                mailMsg.From = mailAd;
                //define o assunto
                mailMsg.Subject = subject;
                //define o corpo do e-mail
                mailMsg.Body = body;

                mailMsg.IsBodyHtml = true;

                mailMsg.Priority = MailPriority.Normal;

                if (!string.IsNullOrEmpty(arquivo))
                    if (File.Exists(arquivo))
                        mailMsg.Attachments.Add(new Attachment(arquivo));

                //habilita o ssl (essa configuração depende do servidor smtp)
                smtpClient.EnableSsl = false;
                //configura as credenciais para enviar o e-mail
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential(login, senha);
                //define as credenciais
                smtpClient.Credentials = cred;
                //envia o e-mail
                smtpClient.Send(mailMsg);
                //limpa a menssagem de retorno
                msg = string.Empty;
                //retorna verdadeiro pois enviou o e-mail
                return true;
            }

            catch (Exception exp)
            {
                //retorna a menssagem de erro
                msg = exp.Message;
                //retorna falso pois não enviou o e-mail
                return false;
            }
        }
    }
}
