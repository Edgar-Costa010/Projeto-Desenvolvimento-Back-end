// VidaPlus.Server/Models/Consulta.cs
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VidaPlus.Server.Models
{
    // Modelo para consultas médicas

    public class Consulta
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public string Observacoes { get; set; } = string.Empty;

        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; } // Navegação para o paciente associado

        public int ProfissionalId { get; set; }
        public Profissional? Profissional { get; set; } // Navegação para o profissional associado

        public bool Concluida { get; set; } = false;
    }
}
