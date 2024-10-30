using SuaSaude.entities;

namespace SuaSaude.Contracts
{
    public interface IAtividadeRepository
    {
        List<Atividade> GetAtividadesByDia(DateTime data, int userID);
        List<Atividade> GetAtividadesByMes(DateTime data, int userID);
        Atividade Create(string descricao, DateTime data, bool status, int userID);
        bool Update(int atvID, string descricao, DateTime data);
        bool Delete(int atvID);
        bool AtividadeCheck(int atvId);
    }
}
