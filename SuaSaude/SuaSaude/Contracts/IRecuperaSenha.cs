namespace SuaSaude.Contracts
{
    public interface IRecuperaSenha
    {
        bool RecuperarSenha(string email, string senha, string confirmaSenha);
    }
}
