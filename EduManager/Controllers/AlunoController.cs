using EduManager.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController(IAlunoService alunoService) : ControllerBase
    {
        private IAlunoService _alunoService = alunoService;

        [HttpGet("notas/{alunoId}")]
        public async Task<IEnumerable<Nota>> GetNotasAsync(int alunoId)
        {
            return await _alunoService.ObterNotas(alunoId);
        }
    }
}
