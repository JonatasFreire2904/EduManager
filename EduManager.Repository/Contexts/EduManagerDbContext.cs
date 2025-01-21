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
            // Configuração para Aluno
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Cpf).IsRequired().HasMaxLength(14); // CPF formatado
                entity.Property(a => a.DataNascimento).IsRequired();

                // Relação com Nota (1:N)
                entity.HasMany(a => a.Notas)
                      .WithOne(n => n.Aluno)
                      .HasForeignKey(n => n.AlunoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração para Diretor
            modelBuilder.Entity<Diretor>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            });

            // Configuração para Materia
            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name).IsRequired().HasMaxLength(100);

                // Relação com Professor (1:N)
                entity.HasOne(m => m.Professor)
                      .WithMany(p => p.MateriasResponsaveis)
                      .HasForeignKey(m => m.ProfessorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração para Nota
            modelBuilder.Entity<Nota>(entity =>
            {
                entity.HasKey(n => n.Id);
                entity.Property(n => n.Notas).IsRequired().HasColumnType("decimal(5,2)");

                // Relação com Aluno
                entity.HasOne(n => n.Aluno)
                      .WithMany(a => a.Notas)
                      .HasForeignKey(n => n.AlunoId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relação com Materia
                entity.HasOne(n => n.Materia)
                      .WithMany()
                      .HasForeignKey(n => n.MateriaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração para Professor
            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);

                // Relação com Materias (1:N)
                entity.HasMany(p => p.MateriasResponsaveis)
                      .WithOne(m => m.Professor)
                      .HasForeignKey(m => m.ProfessorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
