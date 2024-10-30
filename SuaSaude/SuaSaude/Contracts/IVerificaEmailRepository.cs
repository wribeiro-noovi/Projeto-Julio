namespace SuaSaude.Contracts
{
    public interface IVerificaEmailRepository
    {
        bool VerificarUsuarioComEmail(string email);
    }
}
