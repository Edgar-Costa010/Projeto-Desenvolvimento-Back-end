namespace VidaPlus.Server.DTOs
{
    /// DTO para atualização de dados do Administrador
    public class AdministradorUpdateDto
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string? NovaSenha { get; set; }
    }
}