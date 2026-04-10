# ADR 0001: Propósito do Projeto e Adoção de TDD

**Status:** Aceito  
**Data:** 06/03/2026

## Contexto
O projeto Vessel-Calc nasce da necessidade de unir o conhecimento empírico e normativo de projetos mecânicos (ASME VIII Div. 1) a práticas modernas de engenharia de software corporativa. Softwares de engenharia lidam com cálculos críticos onde falhas representam risco à vida e prejuízos materiais. Para que o sistema seja confiável para engenheiros autônomos e indústrias, a exatidão das equações físicas não pode sofrer regressões estruturais durante a evolução do código.

## Decisão
1. **Propósito:** O sistema será desenvolvido como um SaaS multitenant focado na automatização rigorosa do cálculo mecânico e geração de relatórios.
2. **Engenharia de Qualidade:** O núcleo de cálculo (Camada de Domínio) será desenvolvido sob a estrita adoção do **Test-Driven Development (TDD)**. Os testes unitários (via xUnit) ditarão a arquitetura das entidades físicas (Cascos, Tampos, Bocais), garantindo que cada regra da norma ASME possua cobertura de teste antes de sua implementação funcional.

## Consequências
* **Positivas:**
    * Criação de um motor de cálculo blindado contra regressões. Modificações futuras para suportar novas divisões do ASME não quebrarão os cálculos base.
    * O processo de TDD força a criação de classes menores, coesas e desacopladas, aderindo naturalmente aos princípios SOLID.
    * Documentação viva do comportamento esperado da norma através dos testes.
* **Negativas:**
    * Maior tempo de concepção e desenvolvimento inicial.
    * Necessidade de *mocks* e dados de teste extremamente precisos, extraídos manualmente de memoriais de cálculo previamente validados na engenharia.