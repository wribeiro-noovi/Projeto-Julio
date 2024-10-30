namespace SuaSaude.Request.Atividade
{
    public class CreateAtividadeBodyJson
    {
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public bool Status { get; set; }
        public int Usuario_id { get; set; }
    }
}
