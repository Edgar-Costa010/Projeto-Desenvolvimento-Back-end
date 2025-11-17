namespace VidaPlus.Server.DTOs;

public class AlterarSenhaDTO
{
    public string Email { get; set; }
    public string SenhaAtual { get; set; }
    public string NovaSenha { get; set; }
}