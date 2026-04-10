# ADR 0002: Uso do Padrão Strategy para Cálculo de Espessuras do Casco

**Status:** Aceito  
**Data:** 18/03/2026

## Contexto
O parágrafo UG-27 do ASME VIII Div. 1 define fórmulas de cascos cilíndricos para tensão circunferencial e longitudinal. No entanto, se a pressão exceder determinados limites (ex: $P > 0.385SE$) ou a espessura geométrica ultrapassar metade do raio interno ($t > 0.5R$), o componente é classificado como "casco espesso" (*thick shell*), exigindo o uso de equações de Lamé descritas no *Mandatory Appendix 1-2*. Centralizar a lógica de verificação e todas as fórmulas matemáticas dentro de um único método na entidade `CylindricalShell` gera uma classe monolítica e frágil.

## Decisão
Implementar o padrão de projeto **Strategy** no domínio de cálculo. 
Foi criada uma interface restrita `IShellThicknessCalculator` com implementações específicas (`CircumferentialThinShellCalculator`, `CircumferentialThickShellCalculator`, etc.). A entidade de domínio orquestra a execução, avaliando as condições limiares de pressão e instanciando dinamicamente a calculadora correta por meio de um método de seleção.

## Consequências
* **Positivas:**
    * Respeito ao Princípio do Aberto/Fechado (OCP). Adicionar novas teorias de cálculo não exige alterar as classes existentes.
    * Respeito ao Princípio da Responsabilidade Única (SRP). As calculadoras apenas computam álgebra; a entidade apenas gerencia o estado e aciona as calculadoras.
    * Testabilidade facilitada para blocos matemáticos isolados.
* **Negativas:**
    * Proliferação de arquivos e interfaces no diretório de Domínio, aumentando a complexidade estrutural visual da solução.