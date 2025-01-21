using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduManager.Domain.Interfaces;
using EduManager.Repository.Contexts;
using Microsoft.EntityFrameworkCore;


namespace EduManager.Repository.Repositories
{
    public class AlunoRepository : IAlunoRepository

    {
        private readonly EduManagerDbContext _context;

        public AlunoRepository(EduManagerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AdicionarMateriaAoAluno(int alunoId, int materiaId)
        {
            var aluno = await _context.Alunos.FindAsync(alunoId);
            var materia = await _context.Materias.FindAsync(materiaId);

            if (aluno == null || materia == null)
                return false;

            // Aqui você pode criar uma lógica de relacionamento entre aluno e matéria, por exemplo:
            var nota = new Nota
            {
                AlunoId = alunoId,
                MateriaId = materiaId,
                Notas = 0 // Adiciona uma nota inicial
            };

            // Adicionando a nota
            await _context.Notas.AddAsync(nota);
            var resultado = await _context.SaveChangesAsync();

            return resultado > 0;
        }

        public async Task<IEnumerable<Nota>> ObterNotas(int alunoId)
        {
              return await _context.Notas
             .Where(n => n.AlunoId == alunoId)
             .ToListAsync();
        }
    }
}
