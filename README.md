# 1 - Projeto gerenciador de tarefas

- [x] Projeto concluido

Primeiro, a idéia desse projeto surgiu quando eu solicitei ao chatgpt projetos de estudo para a linguagem C# usando ASP.NET e MVC, e o chatgpt me sugeriu 5 projetos e esse é o primeiro.

**Tecnologogias:**
- ASP.NET MVC
- MYSQL
- BOOTSTRAP

Ele sugeriu o Entity Framework, mas eu queria fazer esse sem usar o entity.

**Funcionalidades:**
- CRUD de tarefas
- Filtro por status
- Autenticação de diferentes usuários

Esse projeto não separado em cadamadas, foi um projeto mais simples com relação a arquitetura, criei umas divisões por pastas apenas para ficar mais organizado.

Aprendi com esse projeto a fazer um CRUD sem as facilidades do Entity Framework, fiz validações utilizando o Model.IsValid no controlador, criei view models para exibição de dados, fiz injeção de dopendencia sem interface.
Enfim, sei que o código não está em nível profissional, mas pra fins de aprendizado para esse primeiro projeto eu achei valido.

Caso queira baixar e testar, vou deixar abaixo aquery de criação das tabelas usadas no mysql, para facilitar.

**Tabela tarefas**
```
CREATE TABLE `tarefas` (
  `Id` char(36) NOT NULL,
  `IdUsuario` char(36) NOT NULL,
  `Descricao` varchar(500) NOT NULL,
  `Concluida` tinyint NOT NULL,
  `DataCriacao` datetime NOT NULL,
  `DataAlteracao` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
```

**Tabela usuario**
```
CREATE TABLE `usuario` (
  `idusuario` char(36) NOT NULL,
  `nome` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  `senha` varchar(8) NOT NULL,
  `ativo` tinyint NOT NULL DEFAULT '1',
  `datacadastro` datetime NOT NULL,
  PRIMARY KEY (`idusuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
```
