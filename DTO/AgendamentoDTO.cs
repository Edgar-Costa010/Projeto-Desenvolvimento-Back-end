// Classe DTO (Data Transfer Object) para agendamento simplificado

using System.Text.Json.Serialization;

namespace VidaPlus.Server.DTOs
{
    public class AgendamentoSimplificadoDTO
    {
        public string MedicoNome { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DataConsulta { get; set; }
        public string? Observacoes { get; set; } = string.Empty;
    }
}
