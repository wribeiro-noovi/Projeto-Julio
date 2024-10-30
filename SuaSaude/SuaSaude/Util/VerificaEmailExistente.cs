using SuaSaude.Contracts;
using SuaSaude.Repositories;
using System.Text.RegularExpressions;

namespace SuaSaude.Util
{
    public class VerificaEmailExistente : IVerificaEmailRepository
    {
        private readonly SuaSaudeDbContext _context;
        public VerificaEmailExistente(SuaSaudeDbContext context) => _context = context;

        public bool VerificarUsuarioComEmail(string email)
        {
            if (!_context.usuario.Any(u => u.Email == email)) return false;
            return true;
        }
    }
}
