using SuaSaude.Contracts;
using SuaSaude.entities;

namespace SuaSaude.Repositories.DataAcess
{
    public class AtividadeRepository : IAtividadeRepository
    {
        private readonly SuaSaudeDbContext _context;
        public AtividadeRepository(SuaSaudeDbContext context) => _context = context;

        public List<Atividade> GetAtividadesByDia(DateTime data, int userID) =>_context.atividade.Where(atv => atv.Data.Date == data.Date && atv.Usuario_id == userID).ToList();

        public List<Atividade> GetAtividadesByMes(DateTime data, int userID) => _context.atividade .Where(atv => atv.Data.Month == data.Month && atv.Data.Year == data.Year && atv.Usuario_id == userID).ToList();

        public Atividade Create(string descricao, DateTime data, bool status, int userID)
        {
            var novaAtv = new Atividade()
            {
                Descricao = descricao,
                Data = data,
                Status = status,
                Usuario_id = userID
            };

            _context.atividade.Add(novaAtv);
            _context.SaveChanges();
            return novaAtv;
        }
        public bool Update(int atvID, string descricao, DateTime data)
        {
            var atividade = _context.atividade.FirstOrDefault(atv => atv.Id == atvID);
            if (atividade == null) return false;
            
            atividade.Descricao = descricao;
            atividade.Data = data;

            int changes = _context.SaveChanges();

            return changes > 0;
        }

        public bool Delete(int atvID)
        {
            var atividade = _context.atividade.FirstOrDefault(atv => atv.Id == atvID);
            if (atividade == null) return false;

            _context.atividade.Remove(atividade);
            int changes = _context.SaveChanges();

            return changes > 0;
        }

        public bool AtividadeCheck(int atvId)
        {
            var atividade = _context.atividade.FirstOrDefault(atv => atv.Id == atvId);
            if (atividade == null) return false;

            atividade.Status = !atividade.Status;
            int changes = _context.SaveChanges();

            return changes > 0;
        }
    }
}
