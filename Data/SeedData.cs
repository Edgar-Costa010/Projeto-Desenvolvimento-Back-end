using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VidaPlus.Server.Models;

namespace VidaPlus.Server.Data
{

    // Classe para popular dados iniciais no banco de dados... Foi criado o administrador "admedgar"
    // com E-mail "admedgar@vidaplus.com" e a senha "12345" para acesso inicial ao sistema e depois desabilitado o seed.
    //para criar um novo administrador, tem que acessar com o admedgar e criar um novo usuário com perfil de administrador.
    // Na fase de testes acessei o admedgar e criei os adminstradores "Rita" e "Ana"
    // Todos os usuários criados na fase de testes tem dados de acesso "perfil+nome@vidaplus.com (ex.: admedgar@vidaplus.com; pacienteedgar@vidaplus.com; medicoedgar@vidaplus.com)" e senha "12345".

    public class SeedData
    {
        public async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var ctx = serviceProvider.GetRequiredService<VidaPlusDbContext>();

            // Admin
            if (!await ctx.Usuarios.AnyAsync(u => u.Email == "admin@vidaplus.com"))
            {
                ctx.Usuarios.Add(new UsuarioBase
                {
                    Nome = "Administrador",
                    Email = "admin@vidaplus.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Perfil = "Administrador"
                });

                await ctx.SaveChangesAsync();
            }
        }
    }
}