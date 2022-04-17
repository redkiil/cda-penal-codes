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
git clone 
```

Crie um banco de dados com nome `CDA` em seu banco de dados MySQL.

Edite a Linha `79` do Arquivo `Program.cs` , e coloque as credencias do seu banco.


Entre na pasta raiz do projeto, abre um CMD e execute.

```cmd
dotnet run
```
Aguarde o projeto ser buildado e executado!

Rode as Migrations, para popular os dados iniciais.

```
dotnet ef update database
```

Caso não abra o Swagger automaticamente entre no Link

```
http://localhost:5001/swagger
```