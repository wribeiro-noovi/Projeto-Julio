using SuaSaude.Contracts;
using SuaSaude.entities;

namespace SuaSaude.Repositories.DataAcess
{
    public class AutenticacaoRepository : IAutenticacaoRepository
    {
        private readonly SuaSaudeDbContext _context;
        public AutenticacaoRepository(SuaSaudeDbContext context) => _context = context;

        public Usuario Login(string email, string senha) 
        {
            var usuarioLogado = _context.usuario.FirstOrDefault(u => u.Email == email && u.Senha == senha);
            return usuarioLogado;
        }

        
    }
}
