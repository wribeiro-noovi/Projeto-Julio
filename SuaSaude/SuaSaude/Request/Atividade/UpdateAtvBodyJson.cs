namespace SuaSaude.Request.Atividade
{
    public class UpdateAtvBodyJson
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }
}
