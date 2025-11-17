using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VidaPlus.Server.Models
{
    // Modelo para pacientes com informações específicas
    // Não herdando de UsuarioBase para manter simplicidade e evitar complexidade desnecessária
    public class Paciente
    {
        public int Id { get; set; }
        [Required] public string Nome { get; set; } = string.Empty;
        [Required] public string CPF { get; set; } = string.Empty;
        [JsonConverter(typeof(DateTimeConverter))] public DateTime? DataNascimento { get; set; } //Converte a data para o formato dd/MM/yyyy ou dd-MM-yyyy
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        [Required][JsonIgnore] public string SenhaHash { get; set; } = string.Empty;
        [JsonIgnore] public ICollection<Consulta>? Consultas { get; set; } // Navegação para consultas associadas ao paciente
    }
}