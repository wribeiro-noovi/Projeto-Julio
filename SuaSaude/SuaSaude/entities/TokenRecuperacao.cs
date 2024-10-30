namespace SuaSaude.entities
{
    public class TokenRecuperacao
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; }
    }
}
