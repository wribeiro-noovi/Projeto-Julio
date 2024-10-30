namespace SuaSaude.entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public DateTime DataNasc { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public virtual ICollection<Atividade>? Atividades { get; set; }
    }
}
