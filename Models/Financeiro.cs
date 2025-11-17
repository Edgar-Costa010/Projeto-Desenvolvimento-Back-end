// VidaPlus.Server/Models/Financeiro.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VidaPlus.Server.Models
{

    // Modelo para registros financeiros

    public class Financeiro
    {
        public int Id { get; set; }

        [Required]
        public DateTime? Data { get; set; } = DateTime.UtcNow; //Data e hora atuais do momento do lançamento

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public string Tipo { get; set; } = string.Empty; // "Entrada" ou "Saida"

        public string? Descricao { get; set; }

        public string? Unidade { get; set; } // Unidade responsável pelo lançamento
    }
}
