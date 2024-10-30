using SuaSaude.Repositories;
using System.Text.RegularExpressions;

namespace SuaSaude.Util.Cadastro
{
    public class ValidarDadosCadastrais
    {
        private readonly SuaSaudeDbContext _context;
        public ValidarDadosCadastrais(SuaSaudeDbContext context) => _context = context;

        public Dictionary<string, string> ValidarDados(string nome, string sexo, DateTime dataNasc, decimal altura, decimal peso, string email, string senha)
        {
            var erros = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(nome) || nome.Length < 2)
                erros["Nome"] = "Nome inválido";

            if (sexo != "Masculino" && sexo != "Feminino" && sexo != "Outros")
                erros["Sexo"] = "Sexo inválido";

            if (dataNasc >= DateTime.Now)
                erros["DataNascimento"] = "Data de nascimento inválida";

            if (altura < 1.3M)
                erros["Altura"] = "Altura inválida";

            if (peso < 30)
                erros["Peso"] = "Peso inválido";

            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                erros["Email"] = "E-mail inválido";

            if(_context.usuario.Any(e => e.Email == email))
            {
                erros["EmailRepetido"] = "E-mail já cadastrado";
            }

            if (string.IsNullOrEmpty(senha))
                erros["Senha"] = "Senha inválida";

            return erros;
        }
        

    }
}
