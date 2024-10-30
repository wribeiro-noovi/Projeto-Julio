namespace SuaSaude.Request.Usuario
{
    public class UsuarioBodyJson
    {
        public string Nome { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public DateTime DataNasc { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
