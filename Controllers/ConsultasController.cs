// VidaPlus.Server/Controllers/ConsultasController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VidaPlus.Server.Data;
using VidaPlus.Server.Models;

namespace VidaPlus.Server.Controllers
{
    // Controlador para gerenciar consultas médicas
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : ControllerBase
    {
        // Injeção do contexto do banco de dados
        private readonly VidaPlusDbContext _context;
        public ConsultasController(VidaPlusDbContext context)
        {
            _context = context;
        }

        // Listar todas as consultas com detalhes do paciente e profissional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetAll()
        {
            return await _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Profissional)
                .OrderByDescending(c => c.Data)
                .ToListAsync();
        }

        // Obter detalhes de uma consulta específica pelo ID com paciente e profissional
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Consulta>> Get(int id)
        {
            var c = await _context.Consultas
                .Include(x => x.Paciente)
                .Include(x => x.Profissional)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (c == null) return NotFound();
            return c;
        }

        // Listar consultas por paciente
        [HttpGet("paciente/{pacienteId:int}")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetByPaciente(int pacienteId)
        {
            var list = await _context.Consultas
                .Where(x => x.PacienteId == pacienteId)
                .Include(x => x.Profissional)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
            return list;
        }

        // Listar consultas por profissional
        [HttpGet("profissional/{profissionalId:int}")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetByProfissional(int profissionalId)
        {
            var list = await _context.Consultas
                .Where(x => x.ProfissionalId == profissionalId)
                .Include(x => x.Paciente)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
            return list;
        }

        // Criar uma nova consulta, validando paciente e profissional
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Consulta>> Post([FromBody] ConsultaDto dto)
        {
            // valida paciente e profissional existirem
            var paciente = await _context.Pacientes.FindAsync(dto.PacienteId);
            if (paciente == null) return BadRequest(new { mensagem = "Paciente não encontrado." });

            var profissional = await _context.Profissionais.FindAsync(dto.ProfissionalId);
            if (profissional == null) return BadRequest(new { mensagem = "Profissional não encontrado." });

            // cria a consulta
            var consulta = new Consulta
            {
                Data = dto.Data,
                Observacoes = dto.Observacoes ?? string.Empty,
                PacienteId = dto.PacienteId,
                ProfissionalId = dto.ProfissionalId,
                Concluida = false
            };

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            // retornar a entidade completa com relações
            await _context.Entry(consulta).Reference(c => c.Profissional).LoadAsync();
            await _context.Entry(consulta).Reference(c => c.Paciente).LoadAsync();

            return CreatedAtAction(nameof(Get), new { id = consulta.Id }, consulta);
        }

        // Editar uma consulta existente
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] ConsultaDto dto)
        {
            // valida se a consulta existe
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null) return NotFound(new { mensagem = "Consulta não encontrada." });

            if (dto.Data != default) consulta.Data = dto.Data;
            if (dto.Observacoes != null) consulta.Observacoes = dto.Observacoes;
            if (dto.ProfissionalId != 0) consulta.ProfissionalId = dto.ProfissionalId;

            _context.Entry(consulta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Marcar uma consulta como concluída
        [HttpPut("{id:int}/concluir")]
        [Authorize]
        public async Task<IActionResult> Concluir(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null) return NotFound(new { mensagem = "Consulta não encontrada." });

            consulta.Concluida = true;
            _context.Entry(consulta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Deletar uma consulta
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null) return NotFound();
            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    // DTO para criar ou atualizar consultas
    public class ConsultaDto
    {
        public DateTime Data { get; set; }
        public string? Observacoes { get; set; }
        public int PacienteId { get; set; }
        public int ProfissionalId { get; set; }
    }
}