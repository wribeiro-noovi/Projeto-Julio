using SuaSaude.Contracts;
using SuaSaude.entities;
using SuaSaude.Enum;

namespace SuaSaude.Repositories.DataAcess
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SuaSaudeDbContext _context;
        public UsuarioRepository(SuaSaudeDbContext context) => _context = context;

        public Usuario Create(string nome, string sexo, DateTime dataNasc, decimal altura, decimal peso, string email, string senha)
        {
            var usuario = new Usuario()
            {
                Nome = nome,
                Sexo = sexo,
                Altura = altura,
                Peso = peso,
                DataNasc = dataNasc,
                Email = email,
                Senha = senha
            };

            _context.usuario.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }
        public Usuario? Get(int userID) => _context.usuario.FirstOrDefault(u => u.Id == userID);

        public AlteracaoSenhaStatus AlterSenha(int userID, string sAtual, string senha, string confirmaSenha)
        {
            var user = Get(userID);
            if(user is null) return AlteracaoSenhaStatus.UsuarioNaoEncontrado;

            if(user.Senha == sAtual)
            {
                if(senha == confirmaSenha)
                {
                    user.Senha = senha;
                    var change = _context.SaveChanges();

                    return AlteracaoSenhaStatus.Sucesso;
                }
                return AlteracaoSenhaStatus.SenhasNaoConferem;
            }
            return AlteracaoSenhaStatus.SenhaAtualIncorreta;
        }

        public bool Delete(int userID)
        {
            var user = Get(userID);
            if(user is null) return false;

            _context.usuario.Remove(user);
            var change = _context.SaveChanges();

            return change > 0;
        }
    }
}
