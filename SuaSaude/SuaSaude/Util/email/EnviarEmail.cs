using SuaSaude.Contracts;
using SuaSaude.entities;
using SuaSaude.Repositories;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SuaSaude.Util.Email
{
    public class EnviarEmail : IEnviarEmail
    {
        private readonly SuaSaudeDbContext _context;
        public EnviarEmail(SuaSaudeDbContext context) => _context = context;


        private string smtpServer = "smtp.gmail.com"; // Servidor SMTP do Gmail
        private int smtpPort = 587; // Porta SMTP para TLS
        private string smtpUser = "teste.suasaude@gmail.com"; // Seu e-mail do Gmail
        private string smtpPass = "urud oric pxuw wkwx"; // Sua senha do Gmail ou App Password

        public string GerarToken()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            StringBuilder token = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                token.Append(chars[random.Next(chars.Length)]);
            }

            return token.ToString();
        }

        public TokenRecuperacao EnviarToken(string emailDestino)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
                {
                    var token = GerarToken();
                    client.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    client.EnableSsl = true;

                    // Criação da mensagem
                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpUser),
                        Subject = "Recuperação de Senha",
                        Body = $"Seu token para recuperação de senha é: {token}",
                        IsBodyHtml = true
                    };

                     mailMessage.To.Add(emailDestino); // Adiciona o destinatário

                        // Envio da mensagem
                        client.Send(mailMessage);

                        TokenRecuperacao tokenEnviado = new TokenRecuperacao()
                        {
                            Token = token,
                            Email = emailDestino,
                            DataEnvio = DateTime.Now
                        };
                        _context.token_Recuperacao.Add(tokenEnviado);
                        _context.SaveChanges();
                        return tokenEnviado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TokenRecuperacao validarToken(string token, string email)
        {
           var tokenLocalizado = _context.token_Recuperacao.FirstOrDefault(tr => tr.Token == token && tr.Email.Equals(email));
           if (tokenLocalizado == null) return null;
           return tokenLocalizado;
        }
    }
}
