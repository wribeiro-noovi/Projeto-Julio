using SuaSaude.entities;
using SuaSaude.Enum;

namespace SuaSaude.Contracts
{
    public interface IUsuarioRepository
    {
        Usuario Create(string nome, string sexo, DateTime dataNasc, decimal altura, decimal peso, string email, string senha);
        Usuario? Get(int userID);
        AlteracaoSenhaStatus AlterSenha(int userID, string sAtual, string senha, string confirmaSenha);
        bool Delete(int userID);
    }
}
