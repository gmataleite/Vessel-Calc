# ADR 0003: Separação Lógica entre Espessura Requerida e Nominal

**Status:** Aceito  
**Data:** 09/04/2026

## Contexto
O processo de especificação de uma chapa de aço passa por três etapas lógicas distintas:
1. **Espessura Requerida de Projeto:** A física estrutural estrita baseada em pressão, geometria e tensão (fórmulas ASME).
2. **Espessura Final Mínima:** A espessura requerida somada a margens de desgaste (Sobreespessura de Corrosão - $CA$) e margens de conformação mecânica (UG-79 / UG-16).
3. **Espessura Nominal (Comercial):** A adaptação do valor final mínimo para a espessura de chapa mais próxima disponível no mercado ou em estoque do fabricante (ex: arredondar $28.69\text{ mm}$ para a chapa comercial de $1.25\text{ pol}$).

## Decisão
As entidades do núcleo do Domínio (`CylindricalShell`, `Head`) estão restritas **apenas às etapas 1 e 2**. Elas retornarão o valor analítico rigoroso através do método `CalculateMinimumRequiredThickness()`. 
A etapa 3 (tradução para Espessura Comercial) é uma regra de Cadeia de Suprimentos (Supply Chain), não de física mecânica. Portanto, a Etapa 3 foi delegada a um Serviço de Domínio / Serviço de Aplicação externo (ex: `PlateThicknessResolverService`), que recebe o valor do núcleo e consulta o banco de dados de chapas.

## Consequências
* **Positivas:**
    * O *Core Domain* não é contaminado por lógica comercial ou disponibilidade de mercado, mantendo pureza matemática.
    * O sistema pode operar internacionalmente: a mesma entidade mecânica pode passar por um `PlateThicknessResolverService` que devolve chapas em polegadas (mercado US) ou outro que devolve milímetros redondos (mercado Europeu/Brasileiro).
* **Negativas:**
    * A camada de Aplicação precisa de injeção de dependência e orquestração extra antes de devolver o DTO final para renderização na API/Front-end.