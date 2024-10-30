using Microsoft.EntityFrameworkCore;
using SuaSaude.entities;

namespace SuaSaude.Repositories
{
    public class SuaSaudeDbContext : DbContext
    {
        public SuaSaudeDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> usuario { get; set; }
        public DbSet<TokenRecuperacao> token_Recuperacao { get; set; }
        public DbSet<Atividade> atividade { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(usuarios => usuarios.Id);
            modelBuilder.Entity<TokenRecuperacao>().HasKey(tk => tk.Id);
            modelBuilder.Entity<Atividade>().HasKey(atv => atv.Id);

            modelBuilder.Entity<Atividade>().HasOne(a => a.Usuario).WithMany(u => u.Atividades).HasForeignKey(a => a.Usuario_id);
        }
    }
}
