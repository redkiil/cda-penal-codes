# API CDA - CODIGO PENAIS
Projeto back end de uma API para manutenção dos códigos penais do servidor.

# TECNOLOGIAS

- .NET 6
- EF CORE
- .NET WEB API
- MYSQL
- SWAGGER
- SWAGGER UI


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

Caso não abra o Swagger automaticamente entre no Link

```
https://localhost:7255/swagger
```