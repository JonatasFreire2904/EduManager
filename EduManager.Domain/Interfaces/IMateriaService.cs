using EduManager.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduManager.Domain.Interfaces
{
    public interface IMateriaService
    {
        Task<Materia> GetById(int id);
        Task<IEnumerable<Materia>> GetAll();
        Task<bool> AddMateria(Materia materia);
        Task<bool> AtualizarMateria(Materia materia);
        Task<bool> DeleteMateria(int id);
    }
}
