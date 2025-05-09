# ProductControl

## Descrição
ProductControl é uma aplicação ASP.NET Core MVC (.NET 8) que permite o controle de produtos por loja. Com autenticação por e-mail, as lojas podem cadastrar, editar, excluir e finalizar produtos, além de visualizar seu estoque.

## Tecnologias
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- PostgreSQL
- Docker

## Como rodar localmente
1. Clone o repositório
2. Certifique-se de ter o .NET 8 SDK instalado
3. Configure a string de conexão no arquivo `appsettings.json` (veja a seção Configuração)
4. Execute os comandos:
   ```bash
   dotnet restore
   dotnet run
   ```
5. Acesse a aplicação em `http://localhost:5000`

## Como rodar com Docker
1. Clone o repositório
2. Certifique-se de ter o Docker instalado
3. Configure a string de conexão no arquivo `appsettings.json` (veja a seção Configuração)
4. Execute os comandos:
   ```bash
   docker build -t productcontrol .
   docker run -p 5000:80 productcontrol
   ```
5. Acesse a aplicação em `http://localhost:5000`

## Estrutura do banco/modelos
- **Product**: Modelo que representa um produto, com campos como nome, quantidade, data de fabricação, validade, etc.
- **Loja**: Modelo que representa uma loja, com campos como nome e e-mail, e uma lista de produtos.

## Funcionalidades principais
- Cadastro e autenticação de lojas por e-mail
- CRUD completo de produtos
- Finalização de produtos
- Visualização de produtos por loja

## Configuração
A string de conexão do PostgreSQL deve ser configurada no arquivo `appsettings.json`
