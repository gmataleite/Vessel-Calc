# ADR 0004: Gestão Híbrida do Banco de Dados de Materiais

**Status:** Aceito  
**Data:** 09/04/2026

## Contexto
O software necessita calcular a espessura dos componentes baseando-se na tensão admissível ($S$) do material em uma dada temperatura (ASME II-D Tabela 1A). Prover todas as ligas existentes na norma torna o sistema pesado. Por outro lado, o projetista autônomo (público-alvo) frequentemente necessita usar materiais específicos ou realizar simulações teóricas que não estão pré-cadastradas no sistema.

## Decisão
Adotar uma arquitetura de banco de dados no PostgreSQL baseada em *Single Table* para materiais, suportando dados globais estáticos (Seeded) e dados dinâmicos por *Tenant* (Usuário).

A entidade `Material` conterá a propriedade booleana `IsAsmeStandard` e a propriedade anulável `UserId`.

## Consequências

* **Positivas:** * Flexibilidade total para o usuário final parametrizar os próprios projetos.
    * Simplifica a arquitetura de persistência utilizando apenas um `DbSet<Material>` no Entity Framework Core.
    * Facilidade de auditoria: materiais marcados como customizados aparecerão com alertas no Memorial de Cálculo, transferindo a responsabilidade técnica da propriedade mecânica informada para o engenheiro assinante.
* **Negativas:** * Requer interceptadores lógicos (*Query Filters*) consistentes no `Application` para impedir que o Usuário A acesse um material customizado criado pelo Usuário B.