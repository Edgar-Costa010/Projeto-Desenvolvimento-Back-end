using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VidaPlus.Server.Data;
using VidaPlus.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using VidaPlus.Server.DTOs;

namespace VidaPlus.Server.Controllers
{
    // Controlador para gerenciar administradores do sistema
    // Todas as rotas são protegidas e requerem o papel de "Administrador"
    // Apenas quem já está logado como administrador pode gerenciar outros administradores
    // Inicialmente foi criado o administrador "admedgar@vidaplus", com a senha "12345" diretamente no banco de dados para acesso inicial ao sistema
    // Posteriormente, novos administradores foram cirados, para testes, e novos podem ser criados via API ou interface administrativa

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class AdministradoresController : ControllerBase
    {
        private readonly VidaPlusDbContext _context;

        public AdministradoresController(VidaPlusDbContext context)
        {
            _context = context;
        }

        // Lista todos os administradores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioBase>>> GetAdministradores()
        {
            return await _context.Usuarios
                .Where(u => u.Perfil == "Administrador")
                .ToListAsync();
        }

        // Lista um administrador específico por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioBase>> GetAdministrador(int id)
        {
            var admin = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id && u.Perfil == "Administrador");

            if (admin == null)
                return NotFound(new { mensagem = "Administrador não encontrado." });

            return admin;
        }

        // Insere um novo administrador
        [HttpPost]
        public async Task<ActionResult<UsuarioBase>> PostAdministrador([FromBody] AdministradorDTO dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                return BadRequest(new { mensagem = "Email já cadastrado." });

            var novoAdmin = new UsuarioBase
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                Email = dto.Email,
                Telefone = dto.Telefone,
                Perfil = "Administrador",
                DataCadastro = DateTime.UtcNow,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
            };

            _context.Usuarios.Add(novoAdmin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdministrador), new { id = novoAdmin.Id }, new
            {
                novoAdmin.Id,
                novoAdmin.Nome,
                novoAdmin.CPF,
                novoAdmin.Email,
                novoAdmin.Telefone,
                novoAdmin.Perfil,
                mensagem = "Administrador cadastrado com sucesso!"
            });
        }

        // Atualiza os dados de um administrador existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrador(int id, [FromBody] AdministradorUpdateDto dto)
        {
            var admin = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id && u.Perfil == "Administrador");
            if (admin == null)
                return NotFound(new { mensagem = "Administrador não cadastrado." });

            admin.Nome = dto.Nome ?? admin.Nome;
            admin.CPF = dto.CPF ?? admin.CPF;
            admin.Email = dto.Email ?? admin.Email;
            admin.Telefone = dto.Telefone ?? admin.Telefone;

            if (!string.IsNullOrEmpty(dto.NovaSenha))
                admin.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);

            await _context.SaveChangesAsync();
            return NoContent();
        }


        // Exclui um administrador
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrador(int id)
        {
            var admin = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id && u.Perfil == "Administrador");
            if (admin == null)
                return NotFound(new { mensagem = "Administrador não encontrado." });

            _context.Usuarios.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Visualização do relatório financeiro
        [HttpGet("RelatorioFinanceiro")]
        public async Task<IActionResult> GetRelatorioFinanceiro()
        {
            var receitas = await _context.Movimentacoes
                .Where(m => m.Tipo == "Receita")
                .SumAsync(m => m.Valor);

            var despesas = await _context.Movimentacoes
                .Where(m => m.Tipo == "Despesa")
                .SumAsync(m => m.Valor);

            var saldo = receitas - despesas;

            return Ok(new
            {
                receitas,
                despesas,
                saldo,
                dataGeracao = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            });
        }

        // Visualização do resumo do sistema (quantidade de pacientes, profissionais, leitos)
        [HttpGet("ResumoSistema")]
        public async Task<IActionResult> GetResumoSistema()
        {
            var totalPacientes = await _context.Pacientes.CountAsync();
            var totalProfissionais = await _context.Profissionais.CountAsync();
            var totalLeitos = await _context.Leitos.CountAsync();

            return Ok(new
            {
                totalPacientes,
                totalProfissionais,
                totalLeitos
            });
        }
    }
}