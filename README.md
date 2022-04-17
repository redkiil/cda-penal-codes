<p align="center"><img src="https://i.ibb.co/v3sy4xM/Screenshot-3.png"/></p>


# API CDA - CODIGO PENAIS
Projeto Back End de uma API para manutenção dos códigos penais do servidor.

# TECNOLOGIAS

- .NET 6
- EF CORE
- .NET WEB API
- MYSQL
- SWAGGER
- SWAGGER UI

# PRÉ-REQUISITOS

- Banco de Dados MySQL, com uma database criada.

# FEATURES

- [x] Autenticação de Usuário e Senha.
- [x] Listagem de Códigos Penais com paginação, filtro e ordenação por todos os campos.
- [x] Inclusão de Código Penal.
- [x] Exclusão de Código Penal.
- [x] Edição de Código Penal.
- [x] Visualização do Código Penal pelo ID.



# COMO EXECUTAR A API

Caso não tenha instalado, Faça Download do .NET SDK e .NET RUNTIME

https://dotnet.microsoft.com/en-us/download


Faça o download do projeto,

```cmd
git clone https://github.com/redkiil/cda-penal-codes
```

Crie um banco de dados com nome `CDA` em seu banco de dados MySQL.

Edite a Linha `79` do Arquivo `Program.cs` , e coloque as credencias do seu banco.

Rode as Migrations, para popular os dados iniciais.

```
dotnet ef database update
```

Entre na pasta raiz do projeto, abre um CMD e execute.

```cmd
dotnet run
```
Aguarde o projeto ser buildado e executado!

Caso não abra o Swagger automaticamente entre no Link `HTTPS`.

```
https://localhost:7255/swagger
```