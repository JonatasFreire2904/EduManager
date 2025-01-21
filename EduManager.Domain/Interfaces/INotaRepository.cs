using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduManager.Domain.Interfaces
{
    public interface INotaRepository
    {
        Task<Nota> GetNotaPorAlunoEMateria(int alunoId, int materiaId);
        Task<IEnumerable<Nota>> GetNotasPorAluno(int alunoId);
        Task<IEnumerable<Nota>> GetNotasPorMateria(int materiaId);
        Task<bool> AddNota(Nota nota);
        Task<bool> AtualizarNota(Nota nota);
        Task<bool> DeleteNota(int id);
    }
}
