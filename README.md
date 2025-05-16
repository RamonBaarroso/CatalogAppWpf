# 🖥️ ProductCatalogApp - Catálogo de Produtos WPF

Aplicativo desktop WPF para catálogo de produtos, com funcionalidades para visualizar detalhes, salvar favoritos e persistir esses favoritos entre execuções.

## 📦 Visão Geral

O ProductCatalogApp é um sistema desenvolvido em C# utilizando WPF que consome uma API RESTful para listar produtos, permite visualizar detalhes, salvar favoritos e exibir uma lista de produtos favoritos com persistência local via cache JSON.

## 📦 Funcionalidades

- Exibição de lista de produtos com título, imagem e preço formatado com símbolo de moeda.

- Visualização detalhada do produto em uma janela separada.

- Marcação de produtos como favoritos.

- Persistência local dos favoritos utilizando um serviço de cache baseado em arquivo JSON.

- Aba separada para visualização dos favoritos.

- Arquitetura MVVM para separação clara das responsabilidades.

- Injeção de dependência via Microsoft.Extensions.DependencyInjection.

- Configuração externa da URL da API via appsettings.json.

## 🛠️Como Rodar

- Clone o repositório.

- Abra no Visual Studio 2022 ou superior com suporte a .NET 8.

- Restaure os pacotes NuGet.

- Ajuste o appsettings.json se necessário.

- Compile e execute o projeto.


## 🚀Possíveis Melhorias Futuras

- Refatorar para uso completo de comandos no MVVM (ICommand) eliminando code-behind.

- Implementar paginação e pesquisa na lista de produtos.

- Suporte a múltiplas categorias de produtos.

- Testes automatizados (unitários e UI).

- Melhoria da UI para responsividade e acessibilidade.