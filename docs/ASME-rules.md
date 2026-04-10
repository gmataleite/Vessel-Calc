# Modelagem das Regras ASME no Domínio

Este documento serve como ponte entre a engenharia mecânica (ASME VIII Div. 1) e o modelo orientado a objetos do software.

## O Desafio Normativo
A norma ASME não é um algoritmo linear. Ela define cenários, limites de tensão e exceções geométricas. Traduzir isso para código exige encapsulamento rigoroso para evitar que dados inconsistentes gerem vasos de pressão inseguros.

## Mapeamento de Conceitos

### 1. Separação de Grandezas Físicas
A manipulação de unidades (MPa, psi, mm, polegadas) é um vetor comum de bugs em softwares de engenharia.
* **Decisão:** Utilização da biblioteca `UnitsNet`. As assinaturas de métodos não recebem tipos primitivos (`double pressure`), mas estruturas tipadas (`Pressure pressure`). O Domínio padroniza os cálculos internos utilizando o Sistema Internacional (Pascals, Metros).

### 2. O Problema do "Casco Espesso" (Thick Shell)
As equações primárias de projeto (UG-27) falham matematicamente se a pressão for excessiva ou se a espessura exceder a metade do raio interno.
* **Modelagem no Código:** A entidade `CylindricalShell` avalia as condições $P > 0.385SE$ internamente. Utilizando o padrão *Strategy*, o método extraído injeta a classe `CircumferentialThickShellCalculator` em tempo de execução, garantindo a transição automática e imperceptível para a regra do Mandatory Appendix 1-2.

### 3. Margem de Corrosão e Fabricação
Sobreespessura de corrosão ($CA$) não altera as tensões estruturais, apenas a dimensão final geométrica.
* **Modelagem no Código:** O $CA$ é exigido no construtor da entidade, mas aplicado exclusivamente na última etapa do cálculo (`CalculateMinimumRequiredThickness()`), evitando sua interferência nas validações lógicas das regras do ASME.