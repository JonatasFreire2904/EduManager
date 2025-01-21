using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduManager.Domain.Interfaces
{
    public interface IProfessorRepositoy
    {
        Task<Nota?> AddNota(Nota nota);
        Task<Aluno?> GetAlunoByName(string name);
        Task<Aluno?> GetAlunoByCpf(string cpf);

    }
}
