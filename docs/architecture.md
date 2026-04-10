# Arquitetura do Sistema

O **Vessel-Calc** utiliza a **Clean Architecture** para gerenciar a complexidade inerente aos cálculos de engenharia da norma ASME. O objetivo desta topologia é garantir que o Motor de Cálculo (Domínio) seja independente de frameworks web, interfaces de usuário ou bancos de dados.

## Estrutura da Solução

O repositório reflete as seguintes camadas:

1.  **VesselCalc.Domain (Núcleo):**
    * Contém as entidades de engenharia (`CylindricalShell`, `Head`, `Material`), *Value Objects* e interfaces estruturais.
    * **Nenhuma dependência externa** (exceto a biblioteca `UnitsNet` para rigor dimensional).
    * Utiliza o padrão *Strategy* para alternar dinamicamente entre equações de cascos finos (UG-27) e cascos espessos (App 1-2).

2.  **VesselCalc.Application (Casos de Uso):**
    * Orquestra os fluxos do usuário. Contém os *Command/Query Handlers*, DTOs e interfaces para geração do memorial de cálculo e acesso a repositórios.

3.  **VesselCalc.Infrastructure (Adaptadores Externos):**
    * Implementa o acesso a dados via **Entity Framework Core** com provedor **PostgreSQL**.
    * Responsável pelas integrações com bibliotecas de geração de PDF (Memorial de Cálculo) e serviços de e-mail.

4.  **VesselCalc.API (Apresentação Back-end):**
    * Pontos de entrada HTTP (Controllers/Minimal APIs) protegidos por autenticação.

5.  **VesselCalc.Web (Apresentação Front-end):**
    * SPA desenvolvida em **Blazor**, consumindo a API REST de forma reativa.

## Fluxo de Geração do Memorial

O Front-end envia um DTO com os parâmetros de entrada. A camada de `Application` busca os materiais necessários na `Infrastructure`, instancia as entidades no `Domain`, executa os métodos de cálculo (`CalculateMinimumRequiredThickness()`), gera um modelo de relatório em memória e delega a renderização do PDF para o gerador injetado na Infraestrutura.