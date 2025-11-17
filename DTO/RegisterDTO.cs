// RegisterDto
using System.Text.Json.Serialization;


// DTO para registro de pacientes com campos necessários de preenchimento pelo usuário
public class RegisterDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string Perfil { get; set; } = "Paciente"; // Padrão para "Paciente"
    public string? CPF { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
    [JsonConverter(typeof(DateTimeConverter))] public DateTime? DataNascimento { get; set; } //Converte a data para o formato dd/MM/yyyy ou dd-MM-yyyy
}

// LoginDto
public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
