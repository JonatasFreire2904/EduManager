using EduManager.Domain.Entitys;
using EduManager.Domain.Interfaces;
using EduManager.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EduManager.Repository.Repositories
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly EduManagerDbContext _context;

        public MateriaRepository(EduManagerDbContext context)
        {
            _context = context;
        }

        // Obter matéria pelo ID
        public async Task<Materia> GetById(int id)
        {
            return await _context.Materias.FindAsync(id);
        }

        // Obter todas as matérias
        public async Task<IEnumerable<Materia>> GetAll()
        {
            return await _context.Materias.ToListAsync();
        }

        // Adicionar uma nova matéria
        public async Task<bool> AddMateria(Materia materia)
        {
            await _context.Materias.AddAsync(materia);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Atualizar uma matéria existente
        public async Task<bool> AtualizarMateria(Materia materia)
        {
            var materiaExistente = await _context.Materias.FindAsync(materia.Id);

            if (materiaExistente == null)
                return false;

            _context.Entry(materiaExistente).CurrentValues.SetValues(materia);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }

        // Deletar uma matéria pelo ID
        public async Task<bool> DeleteMateria(int id)
        {
            var materia = await _context.Materias.FindAsync(id);

            if (materia == null)
                return false;

            _context.Materias.Remove(materia);
            var resultado = await _context.SaveChangesAsync();
            return resultado > 0;
        }
    }
}
