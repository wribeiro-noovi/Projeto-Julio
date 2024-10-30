using SuaSaude.Contracts;
using SuaSaude.Repositories;

namespace SuaSaude.Util.RecuperaSenha
{
    public class RecuperaSenha : IRecuperaSenha
    {
        private readonly SuaSaudeDbContext _context;
        public RecuperaSenha(SuaSaudeDbContext context) => _context = context;

        public bool RecuperarSenha(string email, string senha, string confirmaSenha)
        {
            var usuario = _context.usuario.FirstOrDefault(u => u.Email == email);
            if (usuario is null) return false;

            if (!senha.Equals(confirmaSenha)) return false;

            usuario.Senha = confirmaSenha;
            _context.SaveChanges();
            return true;
        }
    }
}
