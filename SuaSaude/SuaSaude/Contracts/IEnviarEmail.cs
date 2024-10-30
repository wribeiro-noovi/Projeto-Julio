using SuaSaude.entities;

namespace SuaSaude.Contracts
{
    public interface IEnviarEmail
    {
        string GerarToken();
        TokenRecuperacao EnviarToken(string emailDestino);
        TokenRecuperacao validarToken(string token, string email);
    }
}
