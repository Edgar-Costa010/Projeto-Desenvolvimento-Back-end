namespace VidaPlus.Server.Models
{

    // Modelo para prescrições médicas associadas a consultas

    public class Prescricao
    {
        public int Id { get; set; }
        public int ConsultaId { get; set; }
        public Consulta? Consulta { get; set; }
        public string Texto { get; set; } = string.Empty;
        public DateTime Data { get; set; } = DateTime.UtcNow; // Armazena a data e hora atuais do momento de criação da prescrição
    }
}