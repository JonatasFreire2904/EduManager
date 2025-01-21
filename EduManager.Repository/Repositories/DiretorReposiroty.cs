using EduManager.Domain.Entitys;
using EduManager.Domain.Interfaces;
using EduManager.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduManager.Repository.Repositories
{
    public class DiretorRepository : IDiretorRepository
    {
        private readonly EduManagerDbContext _context;

        public DiretorRepository(EduManagerDbContext context)
        {
            _context = context;
        }

        // Obter aluno pelo nome
        public async Task<Aluno?> GetAlunoByName(string name)
        {
            return await _context.Alunos
                .FirstOrDefaultAsync(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Obter aluno pelo CPF
        public async Task<Aluno?> GetAlunoByCpf(string cpf)
        {
            return await _context.Alunos
                .FirstOrDefaultAsync(a => a.Cpf == cpf);
        }

        // Obter professor pelo nome
        public async Task<Professor?> GetProfessorByName(string name)
        {
            return await _context.Professores
                .FirstOrDefaultAsync(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Adicionar um professor
        public async Task<bool> AddProfessor(Professor professor)
        {
            await _context.Professores.AddAsync(professor);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Remover um professor pelo nome
        public async Task<bool> RemoveProfessor(string name)
        {
            var professor = await _context.Professores
                .FirstOrDefaultAsync(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (professor == null)
                return false;

            _context.Professores.Remove(professor);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Adicionar um aluno
        public async Task<bool> AddAluno(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Remover um aluno pelo nome
        public async Task<bool> RemoveAluno(string name)
        {
            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (aluno == null)
                return false;

            _context.Alunos.Remove(aluno);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Atualizar informações de um professor
        public async Task<bool> AtualizaProfessor(Professor professor)
        {
            var professorExistente = await _context.Professores.FindAsync(professor.Id);

            if (professorExistente == null)
                return false;

            _context.Entry(professorExistente).CurrentValues.SetValues(professor);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Atualizar informações de um aluno
        public async Task<bool?> AtualizaAluno(Aluno aluno)
        {
            var alunoExistente = await _context.Alunos.FindAsync(aluno.Id);

            if (alunoExistente == null)
                return null;

            _context.Entry(alunoExistente).CurrentValues.SetValues(aluno);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }
    }
}
