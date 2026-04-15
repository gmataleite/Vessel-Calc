# Roadmap do Vessel-Calc

Este documento rastreia os épicos planejados para a evolução do motor de cálculo e da aplicação web.

## Fase 1: Domínio Básico ASME VIII Div. 1 (Em Andamento)
- [x] Cascos Cilíndricos (UG-27 / App 1-2)
- [x] Tampos Elipsoidais (UG-32 / App 1-4)
- [x] Tampos Toro-esféricos (UG-32 / App 1-4)
- [x] Tampos Hemisféricos (UG-32 / App 1-3)

## Fase 2: Gestão de Materiais e Dados
- [ ] Implementar base de dados relacional para ASME II-D (Tabela 1A)
- [ ] Interceptadores no EF Core para isolamento multitenant de materiais customizados.

## Fase 3: Apresentação e Exportação
- [ ] UI em Blazor WebAssembly
- [ ] Geração de Memorial de Cálculo em PDF (QuestPDF)

## Fase 4: Estabilidade Estrutural e Colapso
- [ ] **Módulo de Pressão Externa e Vácuo (UG-28 / UG-33):** Implementar algoritmo iterativo para verificação de flambagem de cascos e tampos.
- [ ] **Digitalização de Gráficos ASME Sec II-D:** Criar serviço de interpolação bi-logarítmica para extração automatizada dos Fatores Geométricos (A) e Fatores de Material (B).
- [ ] **Anéis de Reforço (Stiffening Rings - UG-29):** Dimensionamento do momento de inércia requerido para anéis de reforço contra pressão externa.