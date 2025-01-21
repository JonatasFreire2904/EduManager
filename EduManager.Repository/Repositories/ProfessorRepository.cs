using EduManager.Domain.Entitys;
using EduManager.Domain.Interfaces;
using EduManager.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduManager.Repository.Repositories
{
    public class ProfessorRepository : IProfessorRepositoy
    {
        private readonly EduManagerDbContext _context;

        public ProfessorRepository(EduManagerDbContext context)
        {
            _context = context;
        }

        // Adicionar uma nova nota
        public async Task<Nota?> AddNota(Nota nota)
        {
            try
            {
                await _context.Notas.AddAsync(nota);
                await _context.SaveChangesAsync();
                return nota;
            }
            catch
            {
                return null; // Retorna null em caso de falha
            }
        }

        // Obter aluno por nome
        public async Task<Aluno?> GetAlunoByName(string name)
        {
            return await _context.Alunos
                .FirstOrDefaultAsync(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Obter aluno por CPF
        public async Task<Aluno?> GetAlunoByCpf(string cpf)
        {
            return await _context.Alunos
                .FirstOrDefaultAsync(a => a.Cpf == cpf);
        }
    }
}
