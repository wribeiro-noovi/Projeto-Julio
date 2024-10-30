namespace SuaSaude.entities
{
    public class Atividade
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public bool Status { get; set; }
        public int Usuario_id { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
