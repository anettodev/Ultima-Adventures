# Comparação: Armas de Metal vs Armas de Madeira

## Ajuste Implementado

**Multiplicador de Dano para Armas de Metal**: 1.25x (25% mais dano)

Armas de metal agora causam **25% mais dano** que armas de madeira após todos os cálculos de bônus. Este multiplicador é aplicado após o cálculo de todos os bônus de habilidades e modificadores, mas antes do diminishing returns.

---

## Fórmula Atualizada

```
1. Dano Base = Random(MinDamage, MaxDamage) + MaterialDamage(Resource)
2. Bônus de Habilidades = Strength + Anatomy + Tactics + [Habilidades Específicas]
3. Bônus de Modificadores = QualityBonus + DamageLevelBonus + WeaponDamageAttribute
4. Total Bonus = Soma de todos os bônus
5. Dano Total = Dano Base + (Dano Base × Total Bonus)
6. **MULTIPLICADOR METAL**: Se Resource = Metal, então Dano Total × 1.25
7. Dano Final = DiminishingReturns(Dano Total, Cap, 10)
```

---

## Cenários Comparativos

### Cenário 1: Personagem Iniciante
**Stats**: Str 50, Skill Principal 50, Anatomy 0, Tactics 0  
**Arma**: Regular Quality, Regular Damage Level

#### Espada Longa (Metal - Base: 15-18, Material: Iron +0)
- Dano Base: 15-18 (média 16.5)
- Strength Bonus: (50 × 0.100) / 100 = 0.05 (5%)
- Anatomy Bonus: 0%
- Tactics Bonus: 0%
- Quality Bonus: 0%
- Damage Level Bonus: 0%
- **Total Bonus**: 5%
- **Dano Calculado**: 16.5 × 1.05 = 17.325
- **Multiplicador Metal**: 17.325 × 1.25 = **21.656**
- **Dano Final**: ~21-22

#### QuarterStaff (Madeira - Base: 11-14, Material: RegularWood +0)
- Dano Base: 11-14 (média 12.5)
- Strength Bonus: 5%
- Anatomy Bonus: 0%
- Tactics Bonus: 0%
- Quality Bonus: 0%
- Damage Level Bonus: 0%
- **Total Bonus**: 5%
- **Dano Calculado**: 12.5 × 1.05 = 13.125
- **Multiplicador Metal**: Não aplicado (madeira)
- **Dano Final**: ~13-14

**Diferença**: 21.656 / 13.125 = **1.65x (65% mais dano)**  
*Nota: A diferença é maior que 25% porque a arma de metal também tem base de dano maior*

---

### Cenário 2: Personagem Intermediário
**Stats**: Str 80, Skill Principal 80, Anatomy 60, Tactics 60  
**Arma**: Regular Quality, Regular Damage Level, Material Intermediário

#### Espada Longa (Metal - Base: 15-18, Material: Copper +3)
- Dano Base: 15-18 + 3 = 18-21 (média 19.5)
- Strength Bonus: (80 × 0.100) / 100 = 0.08 (8%)
- Anatomy Bonus: (60 × 0.300) / 100 = 0.18 (18%)
- Tactics Bonus: (60 × 0.300) / 100 = 0.18 (18%)
- **Total Bonus**: 8% + 18% + 18% = 44%
- **Dano Calculado**: 19.5 × 1.44 = 28.08
- **Multiplicador Metal**: 28.08 × 1.25 = **35.1**
- **Dano Final**: ~34-36

#### QuarterStaff (Madeira - Base: 11-14, Material: AshTree +1)
- Dano Base: 11-14 + 1 = 12-15 (média 13.5)
- Strength Bonus: 8%
- Anatomy Bonus: 18%
- Tactics Bonus: 18%
- **Total Bonus**: 44%
- **Dano Calculado**: 13.5 × 1.44 = 19.44
- **Multiplicador Metal**: Não aplicado (madeira)
- **Dano Final**: ~19-20

**Diferença**: 35.1 / 19.44 = **1.80x (80% mais dano)**

---

### Cenário 3: Personagem Grandmaster
**Stats**: Str 100, Skill Principal 100, Anatomy 100, Tactics 100  
**Arma**: Regular Quality, Regular Damage Level, Material Avançado

#### Espada Longa (Metal - Base: 15-18, Material: Agapite +5)
- Dano Base: 15-18 + 5 = 20-23 (média 21.5)
- Strength Bonus: (100 × 0.100 + 3.00) / 100 = 0.13 (13%)
- Anatomy Bonus: (100 × 0.300 + 3.00) / 100 = 0.33 (33%)
- Tactics Bonus: (100 × 0.300 + 3.00) / 100 = 0.33 (33%)
- **Total Bonus**: 13% + 33% + 33% = 79%
- **Dano Calculado**: 21.5 × 1.79 = 38.485
- **Multiplicador Metal**: 38.485 × 1.25 = **48.106**
- **Dano Final**: ~47-49

#### QuarterStaff (Madeira - Base: 11-14, Material: RosewoodTree +5)
- Dano Base: 11-14 + 5 = 16-19 (média 17.5)
- Strength Bonus: 13%
- Anatomy Bonus: 33%
- Tactics Bonus: 33%
- **Total Bonus**: 79%
- **Dano Calculado**: 17.5 × 1.79 = 31.325
- **Multiplicador Metal**: Não aplicado (madeira)
- **Dano Final**: ~30-32

**Diferença**: 48.106 / 31.325 = **1.54x (54% mais dano)**

---

### Cenário 4: Personagem Máximo com Arma Excepcional
**Stats**: Str 125, Skill Principal 120, Anatomy 120, Tactics 120  
**Arma**: Exceptional Quality (+10%), Vanq Damage Level (+18%), Material Elite

#### Espada Longa (Metal - Base: 15-18, Material: Dwarven +18)
- Dano Base: 15-18 + 18 = 33-36 (média 34.5)
- Strength Bonus: (125 × 0.100 + 3.00) / 100 = 0.155 (15.5%)
- Anatomy Bonus: (120 × 0.300 + 3.00) / 100 = 0.39 (39%)
- Tactics Bonus: (120 × 0.300 + 3.00) / 100 = 0.39 (39%)
- Quality Bonus: +10%
- Damage Level Bonus: +18%
- **Total Bonus**: 15.5% + 39% + 39% + 10% + 18% = 121.5%
- **Dano Calculado**: 34.5 × 2.215 = 76.4175
- **Multiplicador Metal**: 76.4175 × 1.25 = **95.522**
- **Diminishing Returns**: Capped at 53 (regular) ou 77 (avatar)
- **Dano Final**: ~50-53 (regular) ou ~75-77 (avatar)

#### QuarterStaff (Madeira - Base: 11-14, Material: ElvenTree +8)
- Dano Base: 11-14 + 8 = 19-22 (média 20.5)
- Strength Bonus: 15.5%
- Anatomy Bonus: 39%
- Tactics Bonus: 39%
- Quality Bonus: +10%
- Damage Level Bonus: +18%
- **Total Bonus**: 121.5%
- **Dano Calculado**: 20.5 × 2.215 = 45.4075
- **Multiplicador Metal**: Não aplicado (madeira)
- **Diminishing Returns**: Capped at 53 (regular) ou 77 (avatar)
- **Dano Final**: ~44-47 (regular) ou ~45-47 (avatar)

**Diferença (antes do cap)**: 95.522 / 45.4075 = **2.10x (110% mais dano)**  
**Diferença (após cap regular)**: 53 / 45.4075 = **1.17x (17% mais dano)**  
**Diferença (após cap avatar)**: 77 / 45.4075 = **1.70x (70% mais dano)**

---

### Cenário 5: Comparação Direta - Mesmo Dano Base

Para demonstrar o efeito puro do multiplicador de 25%, vamos comparar armas com o mesmo dano base:

#### Arma de Metal (Base: 15-18, Material: Iron +0)
**Stats**: Str 100, Anatomy 100, Tactics 100, Regular Quality, Regular Damage
- Dano Base: 15-18 (média 16.5)
- Total Bonus: 79%
- **Dano Calculado**: 16.5 × 1.79 = 29.535
- **Multiplicador Metal**: 29.535 × 1.25 = **36.919**
- **Dano Final**: ~36-38

#### Arma de Madeira (Base: 15-18, Material: RegularWood +0)
**Stats**: Str 100, Anatomy 100, Tactics 100, Regular Quality, Regular Damage
- Dano Base: 15-18 (média 16.5)
- Total Bonus: 79%
- **Dano Calculado**: 16.5 × 1.79 = 29.535
- **Multiplicador Metal**: Não aplicado (madeira)
- **Dano Final**: ~29-30

**Diferença Exata**: 36.919 / 29.535 = **1.25x (25% mais dano)** ✅

---

## Tabela Comparativa Resumida

| Cenário | Arma Metal | Arma Madeira | Diferença | Multiplicador Aplicado |
|---------|------------|--------------|-----------|------------------------|
| **1. Iniciante** | 21.7 | 13.1 | 1.65x | ✅ 1.25x |
| **2. Intermediário** | 35.1 | 19.4 | 1.80x | ✅ 1.25x |
| **3. Grandmaster** | 48.1 | 31.3 | 1.54x | ✅ 1.25x |
| **4. Máximo (antes cap)** | 95.5 | 45.4 | 2.10x | ✅ 1.25x |
| **4. Máximo (após cap regular)** | 53 | 45.4 | 1.17x | ⚠️ Limitado pelo cap |
| **5. Mesmo Base** | 36.9 | 29.5 | 1.25x | ✅ 1.25x |

---

## Observações Importantes

### 1. Diferença Real vs Multiplicador
- O multiplicador de 25% é aplicado corretamente
- A diferença total entre armas de metal e madeira pode ser maior que 25% porque:
  - Armas de metal geralmente têm dano base maior
  - Materiais de metal podem ter bônus maiores
  - O multiplicador é aplicado após todos os bônus

### 2. Impacto do Diminishing Returns
- Com stats máximos, o cap de 53 (ou 77 para avatares) pode limitar a diferença
- Armas de metal ainda se beneficiam mais porque atingem o cap mais facilmente
- O multiplicador garante que armas de metal sempre terão vantagem, mesmo no cap

### 3. Balanceamento
- Armas de madeira ainda são viáveis para builds específicos (mages, arqueiros)
- Armas de metal são superiores para combate corpo-a-corpo
- A diferença de 25% garante que metal seja sempre a escolha preferida para warriors

### 4. Aplicação do Multiplicador
- O multiplicador é aplicado **após** todos os bônus de habilidades
- O multiplicador é aplicado **antes** do diminishing returns
- Isso garante que o bônus de 25% seja sempre significativo

---

## Conclusão

O ajuste implementado garante que **armas de metal causem 25% mais dano que armas de madeira** após todos os cálculos de bônus. Esta diferença é consistente em todos os níveis de personagem e garante que armas de metal sejam sempre superiores para combate corpo-a-corpo, enquanto armas de madeira mantêm sua utilidade para builds específicos (mages, arqueiros).

**Fórmula Final**:
```
Dano Metal = (Dano Base + Bônus) × 1.25
Dano Madeira = (Dano Base + Bônus) × 1.00
```

---

*Análise baseada no código atualizado do BaseWeapon.cs*  
*Multiplicador implementado: 1.25x (25% mais dano para armas de metal)*

