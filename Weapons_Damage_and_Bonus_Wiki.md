# Wiki de Dano e Bônus de Armas - Ultima Adventures

## Índice

1. [Sistema de Cálculo de Dano](#sistema-de-cálculo-de-dano)
2. [Recursos de Materiais](#recursos-de-materiais)
3. [Bônus de Habilidades e Atributos](#bônus-de-habilidades-e-atributos)
4. [Modificadores de Arma](#modificadores-de-arma)
5. [Cenários de Simulação](#cenários-de-simulação)
6. [Tabelas Comparativas](#tabelas-comparativas)
7. [Guia de Escolha de Armas](#guia-de-escolha-de-armas)

---

## Sistema de Cálculo de Dano

### Sistema AOS (Age of Shadows)

O sistema de dano usa a fórmula AOS, que calcula o dano base e aplica múltiplos bônus e modificadores.

### Fórmula Completa de Dano

```
1. Dano Base = Random(MinDamage, MaxDamage) + MaterialDamage(Resource)
2. Bônus de Habilidades = Strength + Anatomy + Tactics + [Habilidades Específicas]
3. Bônus de Modificadores = QualityBonus + DamageLevelBonus + WeaponDamageAttribute
4. Total Bonus = Soma de todos os bônus (em decimal, ex: 0.79 = 79%)
5. Dano Total = Dano Base + (Dano Base × Total Bonus)
6. Dano Final = DiminishingReturns(Dano Total, Cap, 10)
```

### Componentes do Cálculo

#### 1. Dano Base
- **MinDamage** e **MaxDamage**: Valores base da arma (definidos por tipo de arma)
- **Material Damage**: Bônus adicional baseado no material usado (ver seção de Recursos)

#### 2. Bônus Físicos (Sem Limite)
- **Strength**: Bônus baseado na força do personagem
- **Anatomy**: Bônus baseado na habilidade de Anatomia
- **Tactics**: Bônus baseado na habilidade de Táticas
- **Habilidades Específicas**: Dependem do tipo de arma (ver seção de Bônus)

#### 3. Modificadores (Limitados)
- **Quality Bonus**: Bônus baseado na qualidade da arma
- **Damage Level Bonus**: Bônus baseado no nível de dano
- **Weapon Damage Attribute**: Bônus de atributos mágicos
- **Caps**: Modificadores são limitados por `DamageIncreaseCap()` (geralmente 100%)

#### 4. Diminishing Returns
- **Jogadores Regulares**: Cap de 53 de dano
- **Avatares**: Cap de 77 de dano
- **Criaturas**: Cap de 225 de dano
- Sistema de steps exponenciais após o threshold

---

## Recursos de Materiais

### Metais

Os metais são usados para armas corpo-a-corpo (espadas, machados, maças, etc.).

| Material | Bônus de Dano | Cor | Uso |
|----------|---------------|-----|-----|
| **Iron** | +0 | Cinza | Material base, sem bônus |
| **DullCopper** | +1 | Marrom escuro | Material básico melhorado |
| **ShadowIron** | +2 | Preto | Material intermediário |
| **Copper** | +3 | Laranja | Material intermediário |
| **Bronze** | +4 | Amarelo bronze | Material intermediário |
| **Gold** | +4 | Dourado | Material intermediário |
| **Platinum** | +4 | Prateado | Material intermediário |
| **Agapite** | +5 | Azul claro | Material avançado |
| **Verite** | +5 | Verde | Material avançado |
| **Valorite** | +6 | Azul escuro | Material avançado |
| **Nepturite** | +6 | Azul oceano | Material avançado |
| **Obsidian** | +6 | Preto brilhante | Material avançado |
| **Steel** | +7 | Cinza metálico | Material superior |
| **Brass** | +8 | Amarelo dourado | Material superior |
| **Mithril** | +9 | Prateado brilhante | Material superior |
| **Xormite** | +9 | Roxo | Material superior |
| **Titanium** | +15 | Cinza claro | Material elite |
| **Rosenium** | +15 | Rosa | Material elite |
| **Dwarven** | +18 | Cinza escuro | Material máximo |

### Madeiras

As madeiras são usadas para armas de madeira (arcos, bestas, cajados).

| Material | Bônus de Dano | Cor | Uso |
|----------|---------------|-----|-----|
| **RegularWood** | +0 | Marrom | Material base, sem bônus |
| **AshTree** | +1 | Cinza claro | Material básico melhorado |
| **CherryTree** | +1 | Rosa claro | Material básico melhorado |
| **EbonyTree** | +2 | Preto | Material intermediário |
| **GoldenOakTree** | +2 | Dourado | Material intermediário |
| **HickoryTree** | +3 | Marrom escuro | Material intermediário |
| **RosewoodTree** | +5 | Vermelho escuro | Material avançado |
| **ElvenTree** | +8 | Verde claro | Material máximo |

### Couros

Os couros são usados para algumas armas específicas (principalmente armas de couro).

| Material | Bônus de Dano | Cor | Uso |
|----------|---------------|-----|-----|
| **RegularLeather** | +0 | Marrom | Material base, sem bônus |
| **SpinedLeather** | +1 | Azul claro | Material básico melhorado |
| **HornedLeather** | +2 | Amarelo | Material intermediário |
| **BarbedLeather** | +3 | Verde | Material intermediário |
| **NecroticLeather** | +4 | Preto | Material avançado |
| **VolcanicLeather** | +4 | Vermelho | Material avançado |
| **FrozenLeather** | +5 | Azul gelo | Material avançado |
| **GoliathLeather** | +6 | Marrom escuro | Material superior |
| **DraconicLeather** | +8 | Vermelho escuro | Material superior |
| **HellishLeather** | +9 | Vermelho fogo | Material superior |
| **DinosaurLeather** | +10 | Verde escuro | Material elite |
| **AlienLeather** | +18 | Verde neon | Material máximo |

---

## Bônus de Habilidades e Atributos

### Bônus Universais (Todas as Armas)

#### 1. Strength (Força)
- **Fórmula**: `(Str × 0.100) / 100` se Str < 100
- **Fórmula**: `(Str × 0.100 + 3.00) / 100` se Str >= 100
- **Exemplos**:
  - Str 50 = 0.05 (5%)
  - Str 80 = 0.08 (8%)
  - Str 100 = 0.13 (13%)
  - Str 125 = 0.155 (15.5%)

#### 2. Anatomy (Anatomia)
- **Fórmula**: `(Anatomy × 0.300) / 100` se Anatomy < 100
- **Fórmula**: `(Anatomy × 0.300 + 3.00) / 100` se Anatomy >= 100
- **Exemplos**:
  - Anatomy 50 = 0.15 (15%)
  - Anatomy 80 = 0.24 (24%)
  - Anatomy 100 = 0.33 (33%)
  - Anatomy 120 = 0.39 (39%)

#### 3. Tactics (Táticas)
- **Fórmula**: `(Tactics × 0.300) / 100` se Tactics < 100
- **Fórmula**: `(Tactics × 0.300 + 3.00) / 100` se Tactics >= 100
- **Exemplos**:
  - Tactics 50 = 0.15 (15%)
  - Tactics 80 = 0.24 (24%)
  - Tactics 100 = 0.33 (33%)
  - Tactics 120 = 0.39 (39%)

#### 4. ArmsLore
- **BÔNUS DE DANO**: **NENHUM** - ArmsLore não fornece bônus de dano para nenhuma arma
- **Uso**: Apenas para identificar propriedades de armas

### Bônus Específicos por Tipo de Arma

#### 5. Lumberjacking (Carpintaria)
- **Aplicável**: Apenas para **Axes** (Machados)
- **Fórmula**: `(Lumberjacking × 0.200) / 100` se Lumberjacking < 100
- **Fórmula**: `(Lumberjacking × 0.200 + 10.00) / 100` se Lumberjacking >= 100
- **Exemplos**:
  - Lumberjacking 50 = 0.10 (10%)
  - Lumberjacking 100 = 0.30 (30%)
  - Lumberjacking 120 = 0.34 (34%)

#### 6. Mining (Mineração)
- **Aplicável**: Apenas para **Bashing** weapons (Armas de impacto)
- **Fórmula**: `(Mining × 0.200) / 100` se Mining < 100
- **Fórmula**: `(Mining × 0.200 + 10.00) / 100` se Mining >= 100
- **Exemplos**:
  - Mining 50 = 0.10 (10%)
  - Mining 100 = 0.30 (30%)
  - Mining 120 = 0.34 (34%)

#### 7. Fishing (Pesca)
- **Aplicável**: Apenas para **Harpoons** (Arpões)
- **Fórmula**: `(Fishing × 0.200) / 100` se Fishing < 100
- **Fórmula**: `(Fishing × 0.200 + 10.00) / 100` se Fishing >= 100
- **Exemplos**:
  - Fishing 50 = 0.10 (10%)
  - Fishing 100 = 0.30 (30%)
  - Fishing 120 = 0.34 (34%)

#### 8. Bushido
- **Aplicável**: Apenas para **Axes, Slashing, e Polearms**
- **Fórmula**: `(Bushido × 0.625) / 100` se Bushido < 100
- **Fórmula**: `(Bushido × 0.625 + 6.25) / 100` se Bushido >= 100
- **Exemplos**:
  - Bushido 50 = 0.3125 (31.25%)
  - Bushido 100 = 0.6875 (68.75%)
  - Bushido 120 = 0.75 (75%)

#### 9. Ninjitsu
- **Aplicável**: Armas específicas de Ninja
- **Fórmula**: `(Ninjitsu × 0.625) / 100` se Ninjitsu < 100
- **Fórmula**: `(Ninjitsu × 0.625 + 6.25) / 100` se Ninjitsu >= 100
- **Exemplos**:
  - Ninjitsu 50 = 0.3125 (31.25%)
  - Ninjitsu 100 = 0.6875 (68.75%)
  - Ninjitsu 120 = 0.75 (75%)

#### 10. Necromancy (Necromancia)
- **Aplicável**: Apenas para **WizardWands** e **Staves** especiais
- **Fórmula**: `(Necromancy × 0.625) / 100` se Necromancy < 100
- **Fórmula**: `(Necromancy × 0.625 + 6.25) / 100` se Necromancy >= 100
- **Exemplos**:
  - Necromancy 50 = 0.3125 (31.25%)
  - Necromancy 100 = 0.6875 (68.75%)
  - Necromancy 120 = 0.75 (75%)

#### 11. Magery (Magia)
- **Aplicável**: Apenas para **WizardWands** e **Staves** especiais
- **Fórmula**: `(Magery × 0.625) / 100` se Magery < 100
- **Fórmula**: `(Magery × 0.625 + 6.25) / 100` se Magery >= 100
- **Exemplos**:
  - Magery 50 = 0.3125 (31.25%)
  - Magery 100 = 0.6875 (68.75%)
  - Magery 120 = 0.75 (75%)

#### 12. Fletching (Flecharia)
- **Aplicável**: Apenas para **Wood Ranged** weapons (Arcos e Bestas de madeira)
- **Fórmula**: `(Fletching × 0.625) / 100` se Fletching < 100
- **Fórmula**: `(Fletching × 0.625 + 6.25) / 100` se Fletching >= 100
- **Exemplos**:
  - Fletching 50 = 0.3125 (31.25%)
  - Fletching 100 = 0.6875 (68.75%)
  - Fletching 120 = 0.75 (75%)

### Resumo de Bônus por Tipo de Arma

| Tipo de Arma | Strength | Anatomy | Tactics | Específico |
|--------------|----------|---------|---------|------------|
| **Espadas** | ✅ | ✅ | ✅ | - |
| **Machados** | ✅ | ✅ | ✅ | Lumberjacking, Bushido |
| **Maças** | ✅ | ✅ | ✅ | - |
| **Cajados** | ✅ | ✅ | ✅ | Necromancy/Magery (WizardWands) |
| **Arcos/Bestas** | ✅ | ✅ | ✅ | Fletching (madeira) |
| **Armas de Impacto** | ✅ | ✅ | ✅ | Mining |
| **Arpões** | ✅ | ✅ | ✅ | Fishing |
| **Armas Ninja** | ✅ | ✅ | ✅ | Ninjitsu |
| **Armas Bushido** | ✅ | ✅ | ✅ | Bushido |

---

## Modificadores de Arma

### Qualidade da Arma (Weapon Quality)

| Qualidade | Bônus de Dano | Descrição |
|-----------|---------------|-----------|
| **Low** | -10% | Arma de baixa qualidade, penaliza dano |
| **Regular** | 0% | Qualidade padrão, sem bônus |
| **Exceptional** | +10% | Qualidade excepcional, aumenta dano |

### Nível de Dano (Damage Level)

| Nível | Bônus de Dano | Descrição |
|-------|---------------|-----------|
| **Regular** | 0% | Nível padrão, sem bônus |
| **Ruin** | +5% | Nível básico melhorado |
| **Might** | +10% | Nível intermediário |
| **Force** | +12% | Nível avançado |
| **Power** | +15% | Nível superior |
| **Vanq** | +18% | Nível máximo |

### Nível de Precisão (Accuracy Level)

| Nível | Bônus de Precisão | Descrição |
|-------|-------------------|-----------|
| **Regular** | 0% | Precisão padrão |
| **Accurate** | +2% | Precisão melhorada |
| **Surpassingly** | +4% | Precisão superior |
| **Eminently** | +6% | Precisão avançada |
| **Exceedingly** | +8% | Precisão excelente |
| **Supremely** | +10% | Precisão máxima |

**Nota**: O nível de precisão afeta a chance de acerto, não o dano.

### Durabilidade

A durabilidade da arma afeta o dano quando a arma está danificada:

- **Fórmula**: `scale = 50 + ((50 × Hits) / MaxHits)`
- **Dano com 100% durabilidade**: 100% do dano
- **Dano com 50% durabilidade**: 75% do dano
- **Dano com 25% durabilidade**: 62.5% do dano

---

## Cenários de Simulação

### Cenário 1: Personagem Iniciante
**Stats**: Str 50, Skill Principal 50, Anatomy 0, Tactics 0  
**Arma**: Regular Quality, Regular Damage Level, Material Base (Iron/RegularWood)

#### Exemplo: Espada Longa (Base: 15-18)
- Dano Base: 15-18 (média 16.5)
- Strength Bonus: (50 × 0.100) / 100 = 0.05 (5%)
- Anatomy Bonus: 0%
- Tactics Bonus: 0%
- Quality Bonus: 0%
- Damage Level Bonus: 0%
- **Total Bonus**: 5%
- **Dano Calculado**: 16.5 × 1.05 = 17.325
- **Dano Final**: ~17-18

#### Exemplo: Machado (Base: 13-16)
- Dano Base: 13-16 (média 14.5)
- Strength Bonus: 5%
- Lumberjacking Bonus: 0% (assumindo 0 skill)
- **Total Bonus**: 5%
- **Dano Calculado**: 14.5 × 1.05 = 15.225
- **Dano Final**: ~15-16

#### Exemplo: QuarterStaff (Base: 11-14)
- Dano Base: 11-14 (média 12.5)
- Strength Bonus: 5%
- **Total Bonus**: 5%
- **Dano Calculado**: 12.5 × 1.05 = 13.125
- **Dano Final**: ~13-14

---

### Cenário 2: Personagem Intermediário
**Stats**: Str 80, Skill Principal 80, Anatomy 60, Tactics 60  
**Arma**: Regular Quality, Regular Damage Level, Material Intermediário (Copper/AshTree)

#### Exemplo: Espada Longa (Base: 15-18, Material +3)
- Dano Base: 15-18 + 3 = 18-21 (média 19.5)
- Strength Bonus: (80 × 0.100) / 100 = 0.08 (8%)
- Anatomy Bonus: (60 × 0.300) / 100 = 0.18 (18%)
- Tactics Bonus: (60 × 0.300) / 100 = 0.18 (18%)
- **Total Bonus**: 8% + 18% + 18% = 44%
- **Dano Calculado**: 19.5 × 1.44 = 28.08
- **Dano Final**: ~27-29

#### Exemplo: Machado (Base: 13-16, Material +3)
- Dano Base: 13-16 + 3 = 16-19 (média 17.5)
- Strength Bonus: 8%
- Anatomy Bonus: 18%
- Tactics Bonus: 18%
- Lumberjacking Bonus: (60 × 0.200) / 100 = 0.12 (12%)
- **Total Bonus**: 8% + 18% + 18% + 12% = 56%
- **Dano Calculado**: 17.5 × 1.56 = 27.3
- **Dano Final**: ~26-28

#### Exemplo: QuarterStaff (Base: 11-14, Material +1)
- Dano Base: 11-14 + 1 = 12-15 (média 13.5)
- Strength Bonus: 8%
- Anatomy Bonus: 18%
- Tactics Bonus: 18%
- **Total Bonus**: 44%
- **Dano Calculado**: 13.5 × 1.44 = 19.44
- **Dano Final**: ~19-20

---

### Cenário 3: Personagem Grandmaster
**Stats**: Str 100, Skill Principal 100, Anatomy 100, Tactics 100  
**Arma**: Regular Quality, Regular Damage Level, Material Avançado (Agapite/RosewoodTree)

#### Exemplo: Espada Longa (Base: 15-18, Material +5)
- Dano Base: 15-18 + 5 = 20-23 (média 21.5)
- Strength Bonus: (100 × 0.100 + 3.00) / 100 = 0.13 (13%)
- Anatomy Bonus: (100 × 0.300 + 3.00) / 100 = 0.33 (33%)
- Tactics Bonus: (100 × 0.300 + 3.00) / 100 = 0.33 (33%)
- **Total Bonus**: 13% + 33% + 33% = 79%
- **Dano Calculado**: 21.5 × 1.79 = 38.485
- **Dano Final**: ~37-40

#### Exemplo: Machado (Base: 13-16, Material +5)
- Dano Base: 13-16 + 5 = 18-21 (média 19.5)
- Strength Bonus: 13%
- Anatomy Bonus: 33%
- Tactics Bonus: 33%
- Lumberjacking Bonus: (100 × 0.200 + 10.00) / 100 = 0.30 (30%)
- **Total Bonus**: 13% + 33% + 33% + 30% = 109%
- **Dano Calculado**: 19.5 × 2.09 = 40.755
- **Dano Final**: ~39-42

#### Exemplo: QuarterStaff (Base: 11-14, Material +5)
- Dano Base: 11-14 + 5 = 16-19 (média 17.5)
- Strength Bonus: 13%
- Anatomy Bonus: 33%
- Tactics Bonus: 33%
- **Total Bonus**: 79%
- **Dano Calculado**: 17.5 × 1.79 = 31.325
- **Dano Final**: ~30-33

---

### Cenário 4: Personagem Máximo com Arma Excepcional
**Stats**: Str 125, Skill Principal 120, Anatomy 120, Tactics 120  
**Arma**: Exceptional Quality (+10%), Vanq Damage Level (+18%), Material Elite (Dwarven/ElvenTree)

#### Exemplo: Espada Longa (Base: 15-18, Material +18)
- Dano Base: 15-18 + 18 = 33-36 (média 34.5)
- Strength Bonus: (125 × 0.100 + 3.00) / 100 = 0.155 (15.5%)
- Anatomy Bonus: (120 × 0.300 + 3.00) / 100 = 0.39 (39%)
- Tactics Bonus: (120 × 0.300 + 3.00) / 100 = 0.39 (39%)
- Quality Bonus: +10%
- Damage Level Bonus: +18%
- **Total Bonus**: 15.5% + 39% + 39% + 10% + 18% = 121.5%
- **Dano Calculado**: 34.5 × 2.215 = 76.4175
- **Diminishing Returns**: Capped at 53 (jogadores regulares) ou 77 (avatares)
- **Dano Final**: ~50-53 (regular) ou ~75-77 (avatar)

#### Exemplo: Machado (Base: 13-16, Material +18)
- Dano Base: 13-16 + 18 = 31-34 (média 32.5)
- Strength Bonus: 15.5%
- Anatomy Bonus: 39%
- Tactics Bonus: 39%
- Lumberjacking Bonus: (120 × 0.200 + 10.00) / 100 = 0.34 (34%)
- Quality Bonus: +10%
- Damage Level Bonus: +18%
- **Total Bonus**: 15.5% + 39% + 39% + 34% + 10% + 18% = 155.5%
- **Dano Calculado**: 32.5 × 2.555 = 83.0375
- **Diminishing Returns**: Capped at 53 (regular) ou 77 (avatar)
- **Dano Final**: ~50-53 (regular) ou ~75-77 (avatar)

#### Exemplo: QuarterStaff (Base: 11-14, Material +8)
- Dano Base: 11-14 + 8 = 19-22 (média 20.5)
- Strength Bonus: 15.5%
- Anatomy Bonus: 39%
- Tactics Bonus: 39%
- Quality Bonus: +10%
- Damage Level Bonus: +18%
- **Total Bonus**: 121.5%
- **Dano Calculado**: 20.5 × 2.215 = 45.4075
- **Diminishing Returns**: Capped at 53 (regular) ou 77 (avatar)
- **Dano Final**: ~44-47 (regular) ou ~45-47 (avatar)

---

### Cenário 5: Comparação de Materiais

**Stats**: Str 100, Skill Principal 100, Anatomy 100, Tactics 100  
**Arma**: Exceptional Quality (+10%), Vanq Damage Level (+18%)  
**Arma Base**: Espada Longa (15-18)

#### Tabela de Comparação por Material

| Material | Bônus Mat. | Dano Base | Total Bonus | Dano Calc. | Dano Final |
|----------|------------|-----------|-------------|------------|------------|
| **Iron** | +0 | 15-18 | 121.5% | 36.5 | 36-37 |
| **Copper** | +3 | 18-21 | 121.5% | 46.5 | 46-47 |
| **Agapite** | +5 | 20-23 | 121.5% | 52.0 | 50-53* |
| **Steel** | +7 | 22-25 | 121.5% | 57.5 | 53* |
| **Mithril** | +9 | 24-27 | 121.5% | 63.0 | 53* |
| **Titanium** | +15 | 30-33 | 121.5% | 75.0 | 53* (77** avatar) |
| **Dwarven** | +18 | 33-36 | 121.5% | 81.5 | 53* (77** avatar) |

*Capped at 53 para jogadores regulares  
**Capped at 77 para avatares

**Observação**: Com stats máximos, materiais superiores ainda são limitados pelo cap de diminishing returns, mas oferecem mais margem para outros modificadores.

---

## Tabelas Comparativas

### Tabela de Bônus de Material por Tipo

#### Metais (Armas Corpo-a-Corpo)

| Material | Bônus | Tier | Recomendação |
|----------|-------|-----|--------------|
| Iron | +0 | Base | Iniciantes |
| DullCopper | +1 | 1 | Iniciantes |
| ShadowIron | +2 | 1 | Iniciantes |
| Copper | +3 | 2 | Intermediário |
| Bronze | +4 | 2 | Intermediário |
| Gold/Platinum | +4 | 2 | Intermediário |
| Agapite | +5 | 3 | Avançado |
| Verite | +5 | 3 | Avançado |
| Valorite | +6 | 3 | Avançado |
| Steel | +7 | 4 | Superior |
| Brass | +8 | 4 | Superior |
| Mithril | +9 | 4 | Superior |
| Titanium | +15 | 5 | Elite |
| Rosenium | +15 | 5 | Elite |
| Dwarven | +18 | 6 | Máximo |

#### Madeiras (Armas de Madeira)

| Material | Bônus | Tier | Recomendação |
|----------|-------|-----|--------------|
| RegularWood | +0 | Base | Iniciantes |
| AshTree/CherryTree | +1 | 1 | Iniciantes |
| EbonyTree/GoldenOakTree | +2 | 2 | Intermediário |
| HickoryTree | +3 | 2 | Intermediário |
| RosewoodTree | +5 | 3 | Avançado |
| ElvenTree | +8 | 4 | Máximo |

#### Couros (Armas Especiais)

| Material | Bônus | Tier | Recomendação |
|----------|-------|-----|--------------|
| RegularLeather | +0 | Base | Iniciantes |
| SpinedLeather | +1 | 1 | Iniciantes |
| HornedLeather | +2 | 2 | Intermediário |
| BarbedLeather | +3 | 2 | Intermediário |
| NecroticLeather | +4 | 3 | Avançado |
| VolcanicLeather | +4 | 3 | Avançado |
| FrozenLeather | +5 | 3 | Avançado |
| GoliathLeather | +6 | 4 | Superior |
| DraconicLeather | +8 | 4 | Superior |
| HellishLeather | +9 | 4 | Superior |
| DinosaurLeather | +10 | 5 | Elite |
| AlienLeather | +18 | 6 | Máximo |

### Tabela de Bônus de Habilidades

| Habilidade | Scalar | Threshold | Offset | Max Bonus (120) |
|------------|--------|-----------|--------|----------------|
| **Strength** | 0.100 | 100 | +3.00 | 15.5% |
| **Anatomy** | 0.300 | 100 | +3.00 | 39% |
| **Tactics** | 0.300 | 100 | +3.00 | 39% |
| **Lumberjacking** | 0.200 | 100 | +10.00 | 34% |
| **Mining** | 0.200 | 100 | +10.00 | 34% |
| **Fishing** | 0.200 | 100 | +10.00 | 34% |
| **Bushido** | 0.625 | 100 | +6.25 | 75% |
| **Ninjitsu** | 0.625 | 100 | +6.25 | 75% |
| **Necromancy** | 0.625 | 100 | +6.25 | 75% |
| **Magery** | 0.625 | 100 | +6.25 | 75% |
| **Fletching** | 0.625 | 0.625 | +6.25 | 75% |

### Tabela de Modificadores de Arma

| Modificador | Valores | Impacto |
|-------------|---------|---------|
| **Quality** | -10%, 0%, +10% | Dano |
| **Damage Level** | 0%, 5%, 10%, 12%, 15%, 18% | Dano |
| **Accuracy Level** | 0%, 2%, 4%, 6%, 8%, 10% | Precisão |

---

## Guia de Escolha de Armas

### Fatores de Decisão

1. **Tipo de Build**
   - **Warrior**: Foco em Strength, Anatomy, Tactics
   - **Bushido**: Foco em Bushido (armas específicas)
   - **Ninja**: Foco em Ninjitsu
   - **Mage-Warrior**: Foco em Magery/Necromancy (staves)

2. **Nível do Personagem**
   - **Iniciante (1-50)**: Materiais básicos (Iron, RegularWood)
   - **Intermediário (50-100)**: Materiais intermediários (Copper, AshTree)
   - **Avançado (100+)**: Materiais avançados (Agapite, RosewoodTree)
   - **Elite (120+)**: Materiais elite (Titanium, ElvenTree)

3. **Orçamento**
   - Materiais básicos: Baratos, fácil de obter
   - Materiais intermediários: Moderados
   - Materiais avançados: Caros
   - Materiais elite: Muito caros, raros

4. **Diminishing Returns**
   - Com stats máximos, o cap de 53 (ou 77 para avatares) limita o benefício de materiais superiores
   - Foque em qualidade, nível de dano e habilidades específicas

### Recomendações por Tipo de Arma

#### Espadas
- **Iniciante**: Iron, Regular Quality, Regular Damage
- **Intermediário**: Copper/Bronze, Regular Quality, Ruin/Might
- **Avançado**: Agapite/Verite, Exceptional Quality, Force/Power
- **Elite**: Steel/Mithril, Exceptional Quality, Vanq

#### Machados
- **Iniciante**: Iron, Regular Quality, Regular Damage
- **Intermediário**: Copper, Regular Quality, Ruin/Might
- **Avançado**: Agapite, Exceptional Quality, Force/Power
- **Elite**: Steel/Mithril, Exceptional Quality, Vanq
- **Nota**: Invista em Lumberjacking para bônus adicional

#### Cajados
- **Iniciante**: RegularWood, Regular Quality, Regular Damage
- **Intermediário**: AshTree/CherryTree, Regular Quality, Ruin/Might
- **Avançado**: RosewoodTree, Exceptional Quality, Force/Power
- **Elite**: ElvenTree, Exceptional Quality, Vanq
- **Nota**: Para mage-warriors, invista em Magery/Necromancy

#### Arcos/Bestas
- **Iniciante**: RegularWood, Regular Quality, Regular Damage
- **Intermediário**: AshTree, Regular Quality, Ruin/Might
- **Avançado**: RosewoodTree, Exceptional Quality, Force/Power
- **Elite**: ElvenTree, Exceptional Quality, Vanq
- **Nota**: Invista em Fletching para bônus adicional

### Estratégias de Otimização

1. **Maximizar Bônus de Habilidades**
   - Priorize Anatomy e Tactics (0.3% por ponto)
   - Strength é menos eficiente (0.1% por ponto)
   - Invista em habilidades específicas da arma

2. **Balancear Material e Modificadores**
   - Com stats baixos: Foque em material melhor
   - Com stats altos: Foque em Quality e Damage Level

3. **Considerar Diminishing Returns**
   - Jogadores regulares: Cap de 53
   - Avatares: Cap de 77
   - Materiais superiores podem não valer a pena se você já está no cap

4. **Manter Durabilidade**
   - Armas danificadas perdem dano
   - Repare regularmente para manter 100% de eficiência

---

## Conclusão

Este documento fornece uma visão completa do sistema de dano e bônus de armas no Ultima Adventures. Use estas informações para:

- **Entender** como o dano é calculado
- **Escolher** o material adequado para seu nível
- **Otimizar** seus stats e habilidades
- **Comparar** diferentes armas e configurações
- **Tomar decisões** informadas sobre upgrades

**Lembre-se**: O dano não é tudo! Considere também velocidade, habilidades especiais, durabilidade e custo-benefício.

---

*Documento baseado no código-fonte do servidor Ultima Adventures*  
*Sistema de cálculo: AOS (Age of Shadows)*  
*Última atualização: Baseado na versão refatorada do BaseWeapon.cs*

