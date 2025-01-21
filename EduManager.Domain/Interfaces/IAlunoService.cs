using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduManager.Domain.Interfaces
{
    public interface IAlunoService
    {
        Task<IEnumerable<Nota>> ObterNotas(int alunoId);  // Relacionamento de notas
        Task<bool> AdicionarMateriaAoAluno(int alunoId, int materiaId); // Relacionamento de aluno com matérias
    }
}
