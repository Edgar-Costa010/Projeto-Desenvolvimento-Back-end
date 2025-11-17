// VidaPlus.Server/Models/Leito.cs
using System.ComponentModel.DataAnnotations;

namespace VidaPlus.Server.Models
{

    // Modelo para leitos hospitalares

    public class Leito
    {
        public int Id { get; set; }

        [Required]
        public string Unidade { get; set; } = string.Empty; // ex: "Hospital Central"

        [Required]
        public string Numero { get; set; } = string.Empty;  // ex: "101A"

        public bool Ocupado { get; set; } = false; // false = livre, true = ocupado
        public string? Tipo { get; set; } // ex: "UTI", "Enfermaria"
    }
}
