# Vessel-Calc

**Vessel-Calc** é uma aplicação web SaaS (Software as a Service) projetada para engenheiros e projetistas mecânicos autônomos. O sistema realiza o dimensionamento rigoroso de vasos de pressão baseado no código **ASME Boiler and Pressure Vessel Code (BPVC) Seção VIII, Divisão 1**, culminando na geração automatizada de memoriais de cálculo exportáveis.

Este projeto alia experiência prática em engenharia mecânica a padrões modernos de desenvolvimento de software, demonstrando a aplicação de **Domain-Driven Design (DDD)** para modelar domínios matemáticos e normativos complexos.

## 🚀 Arquitetura e Stack Tecnológica

O projeto foi estruturado seguindo os princípios da **Clean Architecture**, garantindo total isolamento entre as regras de negócio da norma ASME, a infraestrutura de dados e a interface do usuário.

* **Linguagem & Framework:** C# / .NET 9 (ASP.NET Core Web API)
* **Front-end:** Blazor WebAssembly
* **Banco de Dados:** SQL Server
* **ORM:** Entity Framework Core
* **Testes:** xUnit (Foco em testes unitários para o motor de cálculo)
* **Utilitários de Domínio:** UnitsNet (Garantia de consistência e conversão de grandezas físicas no SI)
* **CI/CD:** GitHub Actions com provisionamento via Docker.

## ⚙️ Funcionalidades Principais

* **Motor de Cálculo ASME:** Dimensionamento de espessuras de cascos cilíndricos considerando pressões internas e hidrostáticas (Regras UG-27 e Mandatory Appendix 1-2).
* **Gestão de Materiais Híbrida:** Banco de dados integrado com materiais padronizados do ASME BPVC Seção II-D, com suporte para cadastro de materiais customizados pelo projetista.
* **Geração de Relatórios:** Emissão de memorial de cálculo documentando o passo a passo algébrico e as referências normativas adotadas.

## 🛠️ Como Executar Localmente (Ambiente de Desenvolvimento)

A infraestrutura local utiliza contêineres Docker para o banco de dados e o Entity Framework Core para o mapeamento objeto-relacional.

### 1. Subindo o Banco de Dados (Docker)
É necessário ter o Docker instalado. Execute o comando abaixo para iniciar o contêiner do SQL Server:
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=VesselCalc@2026!" -p 1433:1433 --name sql_server_vesselcalc -d [mcr.microsoft.com/mssql/server:2022-latest](https://mcr.microsoft.com/mssql/server:2022-latest)