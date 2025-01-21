using EduManager.Domain.Entitys;
using EduManager.Domain.Interfaces;
using EduManager.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduManager.Repository.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly EduManagerDbContext _context;

        public NotaRepository(EduManagerDbContext context)
        {
            _context = context;
        }

        // Obter nota por aluno e matéria
        public async Task<Nota> GetNotaPorAlunoEMateria(int alunoId, int materiaId)
        {
            return await _context.Notas
                .FirstOrDefaultAsync(n => n.AlunoId == alunoId && n.MateriaId == materiaId);
        }

        // Obter todas as notas de um aluno
        public async Task<IEnumerable<Nota>> GetNotasPorAluno(int alunoId)
        {
            return await _context.Notas
                .Where(n => n.AlunoId == alunoId)
                .ToListAsync();
        }

        // Obter todas as notas de uma matéria
        public async Task<IEnumerable<Nota>> GetNotasPorMateria(int materiaId)
        {
            return await _context.Notas
                .Where(n => n.MateriaId == materiaId)
                .ToListAsync();
        }

        // Adicionar uma nova nota
        public async Task<bool> AddNota(Nota nota)
        {
            await _context.Notas.AddAsync(nota);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Atualizar uma nota existente
        public async Task<bool> AtualizarNota(Nota nota)
        {
            var notaExistente = await _context.Notas.FindAsync(nota.Id);

            if (notaExistente == null)
                return false;

            _context.Entry(notaExistente).CurrentValues.SetValues(nota);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Deletar uma nota pelo ID
        public async Task<bool> DeleteNota(int id)
        {
            var nota = await _context.Notas.FindAsync(id);

            if (nota == null)
                return false;

            _context.Notas.Remove(nota);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }
    }
}
