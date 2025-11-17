# Projeto-Backend

# 🏥 VidaPlus – Sistema de Gestão Hospitalar

O **VidaPlus** é um Sistema de Gestão Hospitalar e Serviços de Saúde desenvolvido em **C#** com **.NET Core** no padrão **MVC**, utilizando **Entity Framework Core** e **SQLite** como tecnologias de persistência. Ele foi projetado para otimizar e integrar processos administrativos, clínicos e operacionais de instituições de saúde.

---

## 🚀 Funcionalidades Principais

### 🧑‍⚕️ Gestão de Pacientes
- Cadastro de pacientes com dados pessoais, históricos médicos e convênios.
- Agendamento, consulta e cancelamento de atendimentos.
- Prontuários eletrônicos com vinculação de exames e prescrições.
- Acesso ao dashboard com consultas agendadas e telemedicina.

### 🧑‍🔬 Gestão de Profissionais de Saúde
- Cadastro de médicos, enfermeiros e técnicos.
- Gerenciamento de agendas e horários.
- Registro de prescrições eletrônicas, com CRM validado.

### 🏢 Administração Hospitalar
- Controle de leitos (ocupados, disponíveis e em limpeza).
- Gerenciamento de estoque de suprimentos e medicamentos.
- Relatórios financeiros e administrativos com filtros por período.

### 🔐 Segurança e Compliance
- Controle de acesso baseado em papéis (RBAC).
- Autenticação via token (JWT) no Swagger.
- Logs de ações críticas e conformidade com a LGPD.

---

## 🔧 Tecnologias Utilizadas

- **Linguagem**: C#
- **Plataforma**: .NET 8
- **Framework**: ASP.NET Core MVC
- **Banco de dados**: SQLite
- **ORM**: Entity Framework Core
- **API RESTful**: com documentação via Swagger
- **Segurança**: JWT + controle de acesso por perfil
- **Frontend (protótipo)**: Razor Views

---

## 📂 Estrutura do Projeto

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


---

## 🔗 Endpoints Principais da API

| Método | Endpoint                  | Descrição                        | Acesso          |
|--------|---------------------------|----------------------------------|-----------------|
| POST   | `/api/Auth/login`         | Login de usuário                 | Público         |
| POST   | `/api/Auth/register`      | Cadastro de usuário admin        | Admin           |
| GET    | `/api/Pacientes`          | Listar todos os pacientes        | Admin           |
| POST   | `/api/Consultas`          | Cadastrar nova consulta          | Paciente/Profissional |
| GET    | `/api/Leitos`             | Verificar leitos                 | Admin/Médicos   |
| GET    | `/api/Financeiro`         | Verificar finanças               | Admin           |

📌 **Detalhes completos e documentação Swagger disponível em**: `/swagger`

---

## 🧪 Testes e Autenticação

- Testes realizados via Swagger e protótipo frontend.
- Para endpoints protegidos, utilize o botão **Authorize** no Swagger e insira o token JWT gerado após o login.
- Usuários pré-cadastrados:
  - **Administrador**:  
    - Email: `admedgar@vidaplus.com`  
    - Senha: `12345`

---

## 📚 Referências

- TROELSEN, Andrew; JAPIKSE, Philip. _Pro C# 7: with .NET and .NET Core_. 8. ed. New York: Apress, 2017.
- GIARETTA, Ricardo. _API CRUD completa com C# e .NET 8 do zero | Passo a passo_. YouTube, 2024.  
  Disponível em: https://www.youtube.com/watch?v=UXMKOgmQ7zI

---

## 🛠️ Como Executar este Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/edgar010/VidaPlus.git

   
---

## 🔗 Endpoints Principais da API

| Método | Endpoint                  | Descrição                        | Acesso          |
|--------|---------------------------|----------------------------------|-----------------|
| POST   | `/api/Auth/login`         | Login de usuário                 | Público         |
| POST   | `/api/Auth/register`      | Cadastro de usuário admin        | Admin           |
| GET    | `/api/Pacientes`          | Listar todos os pacientes        | Admin           |
| POST   | `/api/Consultas`          | Cadastrar nova consulta          | Paciente/Profissional |
| GET    | `/api/Leitos`             | Verificar leitos                 | Admin/Médicos   |
| GET    | `/api/Financeiro`         | Verificar finanças               | Admin           |

📌 **Detalhes completos e documentação Swagger disponível em**: `/swagger`

---

## 🧪 Testes e Autenticação

- Testes realizados via Swagger e protótipo frontend.
- Para endpoints protegidos, utilize o botão **Authorize** no Swagger e insira o token JWT gerado após o login.
- Usuários pré-cadastrados:
  - **Administrador**:  
    - Email: `admedgar@vidaplus.com`  
    - Senha: `12345`

---

## 📚 Referências

- TROELSEN, Andrew; JAPIKSE, Philip. _Pro C# 7: with .NET and .NET Core_. 8. ed. New York: Apress, 2017.
- GIARETTA, Ricardo. _API CRUD completa com C# e .NET 8 do zero | Passo a passo_. YouTube, 2024.  
  Disponível em: https://www.youtube.com/watch?v=UXMKOgmQ7zI

---

## 🛠️ Como Executar este Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/VidaPlus.git

