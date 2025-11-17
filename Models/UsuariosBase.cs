using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// Modelo base para autenticação e controle de usuários com as informações comuns a todos os perfis

namespace VidaPlus.Server.Models
{
    public class UsuarioBase
    {
        public int Id { get; set; }

        [Required] public string Nome { get; set; } = string.Empty;
        [Required] public string CPF { get; set; }
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        public string Perfil { get; set; } = "Paciente";
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow; // Armazena a data e hora atuais do momento de criação do usuário
        [JsonIgnore] public string SenhaHash { get; set; } // Hash da senha para autenticação segura
        public string Telefone { get; set; }
    }
}   