namespace VidaPlus.Server.DTOs
{

    // DTO com os campos para a criação de usuários Administradores
    public class AdministradorDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
    }
}
