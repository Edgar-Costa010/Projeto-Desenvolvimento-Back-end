// VidaPlus.Server/Controllers/FinanceiroController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VidaPlus.Server.Data;
using VidaPlus.Server.Models;

namespace VidaPlus.Server.Controllers
{

    // Controlador para gerenciar registros financeiros. Acesso restrito a administradores.
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdministradorOnly")]

    public class FinanceiroController : ControllerBase
    {
        // Injeção do contexto do banco de dados
        private readonly VidaPlusDbContext _context;
        public FinanceiroController(VidaPlusDbContext context) { _context = context; }

        // Informações financeiras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Financeiro>>> GetAll()
        {
            return await _context.Financeiro.OrderByDescending(f => f.Data).ToListAsync();
        }

        // Lançamento financeiro especificado por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Financeiro>> Get(int id)
        {
            var item = await _context.Financeiro.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        // Lançamento financeiro (entrada ou saída)
        [HttpPost]
        [Authorize(Policy = "AdministradorOnly")]
        public async Task<ActionResult<Financeiro>> Post([FromBody] Financeiro model)
        {
            if (model == null || model.Valor <= 0) return BadRequest(new { mensagem = "Valor inválido" });
            model.Data = model.Data == default ? DateTime.UtcNow : model.Data;
            _context.Financeiro.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        // Edição de lançamento financeiro
        [HttpPut("{id}")]
        [Authorize(Policy = "AdministradorOnly")]
        public async Task<IActionResult> Put(int id, [FromBody] Financeiro model)
        {
            var f = await _context.Financeiro.FindAsync(id);
            if (f == null) return NotFound();
            f.Valor = model.Valor;
            f.Tipo = model.Tipo;
            f.Descricao = model.Descricao;
            f.Data = model.Data;
            f.Unidade = model.Unidade;
            _context.Entry(f).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Exclusão de lançamento financeiro
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdministradorOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var f = await _context.Financeiro.FindAsync(id);
            if (f == null) return NotFound();
            _context.Financeiro.Remove(f);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Informações resumidas do financeiro para visualização simples no relatório (entradas, saídas, saldo)
        [HttpGet("relatorio")]
        [Authorize(Policy = "AdministradorOnly")]
        public async Task<IActionResult> Relatorio()
        {
            var entradas = await _context.Financeiro
                .Where(x => x.Tipo.ToLower() == "entrada")
                .SumAsync(x => (decimal?)x.Valor) ?? 0m;
            var saidas = await _context.Financeiro
                .Where(x => x.Tipo.ToLower() == "saida" || x.Tipo.ToLower() == "saída")
                .SumAsync(x => (decimal?)x.Valor) ?? 0m;

            // Contabilidade simples para visualização do total de entradas, saídas e saldo final
            var resumo = new
            {
                receitas = entradas,
                despesas = saidas,
                saldo = entradas - saidas
            };

            return Ok(resumo);
        }
    }
}