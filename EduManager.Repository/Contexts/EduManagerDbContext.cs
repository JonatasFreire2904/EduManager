using EduManager.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace EduManager.Repository.Contexts
{
    public class EduManagerDbContext : DbContext
    {
        public EduManagerDbContext(DbContextOptions<EduManagerDbContext> options) : base(options) { }

        // DbSets
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Diretor> Diretores { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Professor> Professores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureAluno(modelBuilder);
            ConfigureDiretor(modelBuilder);
            ConfigureMateria(modelBuilder);
            ConfigureNota(modelBuilder);
            ConfigureProfessor(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureAluno(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(a => a.Cpf)
                      .IsRequired()
                      .HasMaxLength(14); // CPF formatado

                entity.Property(a => a.DataNascimento)
                      .IsRequired();

                // Relação com Nota (1:N)
                entity.HasMany(a => a.Notas)
                      .WithOne(n => n.Aluno)
                      .HasForeignKey(n => n.AlunoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureDiretor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diretor>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }

        private void ConfigureMateria(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                // Relação com Professor (1:N)
                entity.HasOne(m => m.Professor)
                      .WithMany(p => p.MateriasResponsaveis)
                      .HasForeignKey(m => m.ProfessorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureNota(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nota>(entity =>
            {
                entity.HasKey(n => n.Id);

                entity.Property(n => n.Notas)
                      .IsRequired()
                      .HasColumnType("decimal(5,2)");

                // Relação com Aluno
                entity.HasOne(n => n.Aluno)
                      .WithMany(a => a.Notas)
                      .HasForeignKey(n => n.AlunoId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relação com Materia
                entity.HasOne(n => n.Materia)
                      .WithMany(a => a.Notas)
                      .HasForeignKey(n => n.MateriaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }


        private void ConfigureProfessor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                // Relação com Materias (1:N)
                entity.HasMany(p => p.MateriasResponsaveis)
                      .WithOne(m => m.Professor)
                      .HasForeignKey(m => m.ProfessorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
