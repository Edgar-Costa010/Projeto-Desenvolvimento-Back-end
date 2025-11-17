namespace VidaPlus.Server.Models
{

    // Modelo para registros de log de auditoria de logins e ações do sistema
    public class AuditLog
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? Extra { get; set; }
    }
}