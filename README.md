# Gerenciador de Tarefas com Autenticação (C# + ASP.NET MVC)

Projeto de estudo de desenvolvimento backend com C#, criado para praticar ASP.NET MVC, acesso a dados com MySQL **sem o uso do Entity Framework** e autenticação de múltiplos usuários.

---

## 1. Sobre o projeto

Este é um **gerenciador de tarefas** em que cada usuário possui sua própria conta e gerencia apenas as suas tarefas. A ideia nasceu de uma lista de projetos de estudo para C# com ASP.NET e MVC, e este foi o primeiro deles.

### Funcionalidades

- Cadastro e autenticação de diferentes usuários
- CRUD completo de tarefas (criar, listar, editar e excluir)
- Filtro de tarefas por status (concluída / pendente)

### Objetivos de aprendizado

- Implementar um CRUD manualmente, sem as facilidades do Entity Framework
- Validar dados no controlador com `ModelState.IsValid`
- Criar *ViewModels* para exibição de dados nas telas
- Aplicar injeção de dependência (sem uso de interfaces)

### Arquitetura

O projeto **não é dividido em camadas**. Foi mantida uma arquitetura simples, com a organização feita apenas por pastas dentro do projeto `Autenticacao.Web`. É um projeto com foco didático, e não um código em nível profissional.

### Estrutura do repositório

```
Autenticacao-csharp-aspnet/
├── Autenticacao.Web/      # Projeto ASP.NET MVC (controllers, models, viewmodels, views)
├── Autenticacao.sln       # Solução do Visual Studio
├── .gitignore
└── README.md
```

---

## 2. Tecnologias utilizadas

### Stack

| Camada        | Tecnologia                |
|---------------|---------------------------|
| Linguagem     | C#                        |
| Back-end      | ASP.NET (padrão MVC)      |
| Banco de dados| MySQL                     |
| Front-end     | Bootstrap (com Razor Views) |

### Frameworks

- **ASP.NET MVC**: estrutura da aplicação web (Models, Views e Controllers)
- **Bootstrap**: estilização e layout responsivo das páginas

### APIs e acesso a dados

- **ADO.NET com driver MySQL**: o acesso ao banco é feito com consultas SQL escritas manualmente, **sem Entity Framework**, por escolha de estudo
- **Data Annotations / `ModelState`**: validação dos dados recebidos nos controladores

### Ferramentas

- Visual Studio (solução `.sln`)
- Git e GitHub para versionamento

---

## 3. Como baixar e executar o projeto

### Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) compatível com o projeto (verifique o `TargetFramework` no arquivo `.csproj` de `Autenticacao.Web`)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) em execução
- [Visual Studio](https://visualstudio.microsoft.com/) com a carga de trabalho *ASP.NET e desenvolvimento para a Web* (opcional, pode-se usar apenas a CLI do .NET)
- [Git](https://git-scm.com/)

### Passo 1: Clonar o repositório

```bash
git clone https://github.com/CleverssonHames/Autenticacao-csharp-aspnet.git
cd Autenticacao-csharp-aspnet
```

### Passo 2: Criar o banco de dados

No MySQL, crie um schema (o nome fica a seu critério) e selecione-o:

```sql
CREATE DATABASE gerenciador_tarefas;
USE gerenciador_tarefas;
```

Em seguida, execute os scripts abaixo para criar as tabelas.

**Tabela `usuario`**

```sql
CREATE TABLE `usuario` (
  `idusuario` char(36) NOT NULL,
  `nome` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  `senha` varchar(8) NOT NULL,
  `ativo` tinyint NOT NULL DEFAULT '1',
  `datacadastro` datetime NOT NULL,
  PRIMARY KEY (`idusuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```

**Tabela `tarefas`**

```sql
CREATE TABLE `tarefas` (
  `Id` char(36) NOT NULL,
  `IdUsuario` char(36) NOT NULL,
  `Descricao` varchar(500) NOT NULL,
  `Concluida` tinyint NOT NULL,
  `DataCriacao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```

### Passo 3: Configurar a string de conexão

Abra o projeto `Autenticacao.Web` e localize onde a conexão com o MySQL está configurada (normalmente em `appsettings.json` ou na classe de acesso a dados). Ajuste com os dados do seu servidor:

```
Server=localhost;Port=3306;Database=gerenciador_tarefas;Uid=SEU_USUARIO;Pwd=SUA_SENHA;
```

### Passo 4: Executar

**Pelo Visual Studio**

1. Abra o arquivo `Autenticacao.sln`
2. Defina `Autenticacao.Web` como projeto de inicialização
3. Pressione `F5` (ou `Ctrl + F5` para rodar sem depuração)

**Pela linha de comando**

```bash
dotnet restore
dotnet run --project Autenticacao.Web
```

Após iniciar, acesse no navegador o endereço exibido no terminal (por exemplo, `https://localhost:5001`).

### Primeiros passos na aplicação

1. Cadastre um novo usuário
2. Faça login
3. Crie, edite, conclua e exclua tarefas
4. Use o filtro para visualizar tarefas por status

---

## Observações

- Projeto desenvolvido **para fins de estudo**.
- A coluna `senha` está definida com `varchar(8)`, o que indica que as senhas são armazenadas sem criptografia/hash. **Não utilize este projeto em produção** sem antes implementar hash de senha (por exemplo, com BCrypt ou ASP.NET Core Identity).

## Autor

Desenvolvido por [CleverssonHames](https://github.com/CleverssonHames).
