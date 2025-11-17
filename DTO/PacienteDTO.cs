using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VidaPlus.Server.Models;

namespace VidaPlus.Server.DTO
{

    // DTO para transferência de dados de Paciente

    public class PacienteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, digite seu nome!")] public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O CPF é obrigatório.")] public string CPF { get; set; } = string.Empty;
        [JsonConverter(typeof(DateTimeConverter))] public DateTime? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Endereco { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(5, ErrorMessage = "A senha deve ter no mínimo 5 caracteres.")]
        public string Senha { get; set; } = string.Empty; // Senha em texto puro para transferência segura e depois é convertida para hash no backend
        // Senha não deve ser exposta em respostas, apenas usada para criação/atualização
    }
}
