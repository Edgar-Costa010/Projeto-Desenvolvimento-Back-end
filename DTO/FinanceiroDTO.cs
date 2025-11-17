using System;

namespace VidaPlus.Server.DTOs
{
    public class FinanceiroDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Tipo { get; set; } = "Receita";
        public decimal Valor { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public string Observacoes { get; set; } = string.Empty;
    }
}