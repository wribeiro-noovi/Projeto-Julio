using SuaSaude.entities;

namespace SuaSaude.Contracts
{
    public interface IAutenticacaoRepository
    {
        Usuario Login(string email, string senha);
    }
}
