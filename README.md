# Projeto: Desenvolvimento Back-end

Trabalho acadêmico para a disciplina Eletiva IV: Projeto Multidisciplinar de Análise e Desenvolvimento de Sistemas, do 5º período da graduação em ADS, da Universidade Uninter - Centro Universitário Internacional

# VidaPlus – Sistema de Gestão Hospitalar (SGHSS)

O **VidaPlus** é um Sistema de Gestão Hospitalar e Serviços de Saúde desenvolvido em **C#** com **.NET Core** no padrão **MVC**, utilizando **Entity Framework Core** e **SQLite** como tecnologias de persistência de dados.

## Funcionalidades Principais

### 🧑‍⚕️ Gestão de Pacientes
- Cadastro de pacientes com dados pessoais, históricos médicos e convênios.
- Agendamento, consulta e cancelamento de atendimentos.
- Prontuários eletrônicos.
- Acesso ao dashboard com consultas agendadas e opção para novos agendamentos.

### 🧑‍🔬 Gestão de Profissionais de Saúde
- Cadastro de médicos, enfermeiros e técnicos.
- Gerenciamento de agendas e horários.
- Registro de prescrições eletrônicas, com CRM validado.
- Apenas usuários logados com perfil administrativo consegue manipular os endpoints dos profissionais
  
### 🏢 Administração Hospitalar
- Controle de leitos (ocupados, disponíveis e total).
- Gerenciamento de estoque de suprimentos e medicamentos.
- Relatórios financeiros e administrativos.

### 🔐 Segurança e Compliance
- Controle de acesso baseado em perfis.
- Autenticação via token (JWT) no Swagger.
- Logs de ações críticas e conformidade com a LGPD.

-------------------------------------------------

## 🔧 Tecnologias Utilizadas

- **Linguagem**: C#
- **Plataforma**: .NET 8
- **Framework**: ASP.NET Core MVC
- **Banco de dados**: SQLite
- **ORM**: Entity Framework Core
- **API RESTful**: com documentação via Swagger
- **Segurança**: JWT + controle de acesso por perfil
- **Frontend (protótipo)**: Feito em React + JavaScript

-------------------------------------------------

## 📂 Estrutura do back-end do Projeto

📂 VidaPlus/
│
├── 📁 Controllers/
│   ├── AuthController.cs
│   ├── PacienteController.cs
│   ├── AdminController.cs
│
├── 📁 Models/
│   ├── UsuarioBase.cs
│   ├── Paciente.cs
│   ├── Profissional.cs
│   ├── Administrador.cs
│
├── 📁 Data/
│   └── VidaPlusContext.cs
│
├── 📁 Views/
│   ├── Paciente/
│   ├── Profissional/
│   └── Admin/
│
└── Program.cs


-------------------------------------------------

## Principais Endpoints da API

| Método | Endpoint                  | Descrição                        | Acesso          |
|--------|---------------------------|----------------------------------|-----------------|
| POST   | `/api/Auth/login`         | Login de usuário                 | Público         |
| POST   | `/api/Auth/register`      | Cadastro de usuário admin        | Admin           |
| GET    | `/api/Pacientes`          | Listar todos os pacientes        | Admin           |
| POST   | `/api/Consultas`          | Cadastrar nova consulta          | Pacientes       |
| GET    | `/api/Leitos`             | Verificar leitos                 | Admin           |
| GET    | `/api/Financeiro`         | Verificar finanças               | Admin           |

-------------------------------------------------

## 🧪 Testes e Autenticação

- Testes realizados via Swagger e protótipo frontend.
- Para endpoints protegidos, utilize o botão **Authorize** no Swagger e insira o token JWT gerado após o login.
- Usuários pré-cadastrados:
  - **Administrador**:  
    - Email: `admedgar@vidaplus.com`  
    - Senha: `12345`
