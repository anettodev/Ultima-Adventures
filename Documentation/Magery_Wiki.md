# Magery & Spells - Guia Completo

**Sistema:** Ultima Adventures - Sistema de Magia
**Última Atualização:** 6 de Novembro, 2025
**Total de Feitiços:** 64 (8 circles × 8 feitiços cada)

---

## Índice

1. [Introdução ao Magery](#introdução-ao-magery)
2. [Como Usar Feitiços](#como-usar-feitiços)
3. [Como os Feitiços Funcionam](#como-os-feitiços-funcionam)
4. [Categorias e Tipos de Feitiços](#categorias-e-tipos-de-feitiços)
5. [Requisitos dos Feitiços](#requisitos-dos-feitiços)
6. [Fórmulas de Cálculo de Dano](#fórmulas-de-cálculo-de-dano)
7. [Fórmulas de Cálculo de Benefícios](#fórmulas-de-cálculo-de-benefícios)
8. [Visão Geral dos Circles](#visão-geral-dos-circles)
9. [Informações de Balanceamento](#informações-de-balanceamento)
10. [Referência de Tipos de Dano](#referência-de-tipos-de-dano)
11. [Estratégia e Dicas](#estratégia-e-dicas)

---

## Introdução ao Magery

Magery é a principal skill de magia no Ultima Adventures, permitindo aos jogadores conjurar 64 feitiços diferentes através de 8 circles de poder. Cada circle contém 8 feitiços, com circles superiores exigindo mais skill e fornecendo efeitos mais poderosos.

### Conceitos Principais

- **Circles:** Os feitiços são organizados em 8 circles (1º ao 8º)
- **Custo de Mana:** Cada feitiço consome mana baseado na tabela por circle (crescimento polinomial balanceado):
  - 1º Circle: 4 mana
  - 2º Circle: 7 mana
  - 3º Circle: 11 mana
  - 4º Circle: 16 mana
  - 5º Circle: 22 mana
  - 6º Circle: 28 mana
  - 7º Circle: 36 mana
  - 8º Circle: 48 mana
- **Reagents:** A maioria dos feitiços requer reagents específicos para conjurar
- **Requisitos de Skill:** Feitiços de circles superiores requerem maior skill de Magery
- **Tempo de Conjuração:** Feitiços de circles superiores levam mais tempo para conjurar

---

## Como Usar Feitiços

### Conjuração Básica

1. **Equipar um Spellbook:** Você precisa de um spellbook equipado para conjurar feitiços
2. **Ter Reagents:** Certifique-se de ter os reagents necessários na sua mochila
3. **Ter Mana:** Verifique se você tem mana suficiente para o feitiço
4. **Conjurar o Feitiço:** Use a interface do spellbook ou digite `[cast <número do feitiço>`

### Métodos de Conjuração

- **Interface do Spellbook:** Clique duas vezes no seu spellbook e selecione um feitiço
- **Linha de Comando:** Digite `[cast 1` para Magic Arrow, `[cast 12` para Fireball, etc.
- **Spell Scrolls:** Use scrolls para conjurar feitiços sem reagents (consome o scroll)

### Movimento Durante Conjuração

- Você pode se mover durante a conjuração, mas o movimento é limitado
- **Limites de Passos:** Baseado na sua skill de Magery:
  - 0-20 Magery: 2 passos
  - 21-40: 4 passos
  - 41-60: 6 passos
  - 61-80: 8 passos
  - 81-100: 10 passos
  - 101-110: 15 passos
  - 111-120: 20 passos
- **Sistema de Avisos:** Você receberá avisos conforme se aproxima do limite de passos
- **Falha do Feitiço:** Mover-se demais causará a falha do feitiço

### Interrupção de Feitiço

- Receber dano durante a conjuração pode interromper seu feitiço
- **Feitiço Protection:** Fornece proteção contra interrupção (veja Informações de Balanceamento)
- Feitiços de circles superiores são mais vulneráveis à interrupção

---

## Como os Feitiços Funcionam

### Mecânicas de Feitiço

1. **Seleção de Alvo:** A maioria dos feitiços requer selecionar um alvo
2. **Linha de Visão:** Você deve ter linha de visão até o seu alvo
3. **Alcance:** Feitiços têm alcances máximos (varia por feitiço e era)
4. **Resistência:** Alvos podem resistir efeitos de feitiços usando a skill Magic Resist
5. **Reflexão:** Alguns feitiços podem ser refletidos de volta ao conjurador

### Componentes do Feitiço

- **Custo de Mana:** Baseado na tabela de mana por circle (não é simplesmente 4 × circle)
- **Reagents:** Materiais necessários (veja descrições individuais dos feitiços)
- **Tempo de Conjuração:** Aumenta com o nível do circle
- **Duração:** Feitiços benéficos têm durações baseadas em skills
- **Cooldown:** 0.75 segundos entre conjurações de feitiços

### Interações de Skills

- **Magery:** Skill primária, determina quais feitiços você pode conjurar
- **EvalInt (Evaluate Intelligence):** Afeta o dano e efetividade dos feitiços
- **Inscription:** Afeta a duração dos feitiços e alguns cálculos de dano
- **Magic Resist:** Permite que alvos resistam aos efeitos de feitiços

---

## Categorias e Tipos de Feitiços

### Feitiços de Ataque
**Propósito:** Causar dano aos alvos

**Exemplos:**
- Magic Arrow (1º Circle) - Rápido, baixo dano
- Fireball (3º Circle) - Dano em área
- Energy Bolt (6º Circle) - Alto dano em alvo único
- Flamestrike (7º Circle) - Dano muito alto

**Características:**
- Causam dano baseado em fórmulas (veja Cálculo de Dano)
- Podem ser resistidos
- Podem ter efeitos de área
- Diferentes tipos de dano (Fire, Energy, Cold, etc.)

### Feitiços Benéficos
**Propósito:** Ajudar você mesmo ou aliados

**Subcategorias:**
- **Cura:** Restaura pontos de vida (Heal, Greater Heal)
- **Buffs de Atributos:** Aumenta atributos (Strength, Agility, Cunning)
- **Defensivos:** Fornecem proteção (Protection, Reactive Armor)
- **Utilidade:** Fornecem habilidades especiais (Night Sight, Teleport)

**Características:**
- Têm durações baseadas em skills
- Não podem se acumular consigo mesmos (anti-stacking)
- Podem ter efetividade baseada em skill

### Feitiços de Maldição
**Propósito:** Debuff em inimigos

**Exemplos:**
- Weaken (1º Circle) - Reduz Strength
- Clumsy (1º Circle) - Reduz Dexterity
- Feeblemind (1º Circle) - Reduz Intelligence

**Características:**
- Reduzem atributos do alvo
- Podem ser resistidos
- Duração baseada nas skills do conjurador e do alvo

### Feitiços de Utilidade
**Propósito:** Fornecer funcionalidade fora de combate

**Exemplos:**
- Create Food (1º Circle) - Gera comida
- Teleport (5º Circle) - Viagem instantânea
- Recall (6º Circle) - Viagem para rune
- Gate Travel (7º Circle) - Cria portal

**Características:**
- Sem efeitos de combate
- Frequentemente não têm duração
- Úteis para exploração e conveniência

### Feitiços de Campo (Field)
**Propósito:** Criar áreas com dano

**Exemplos:**
- Fire Field (4º Circle) - Dano de fogo ao longo do tempo
- Poison Field (5º Circle) - Dano de veneno ao longo do tempo
- Energy Field (6º Circle) - Dano de energia ao longo do tempo

**Características:**
- Criam áreas de dano persistentes
- Dano ao longo do tempo
- Podem ser atravessadas (com dano)

### Feitiços de Invocação
**Propósito:** Invocar criaturas para lutar

**Exemplos:**
- Summon Creature (4º Circle) - Invocação básica
- Energy Vortex (8º Circle) - Invocação poderosa
- Summon Daemon (8º Circle) - Invocação muito poderosa

**Características:**
- Criaturas invocadas lutam por você
- Duração baseada em skills
- Classe Sorcerer recebe bônus de duração

---

## Requisitos dos Feitiços

### Requisitos de Skill e Mana

| Circle | Magery Mínimo | Custo de Mana | Faixa Típica de Skill |
|--------|---------------|---------------|-----------------------|
| 1º | 0.0 | 4 | 0-20 |
| 2º | 8.1 | 7 | 8-30 |
| 3º | 16.2 | 11 | 16-40 |
| 4º | 24.3 | 16 | 24-50 |
| 5º | 32.4 | 22 | 32-60 |
| 6º | 40.5 | 28 | 40-70 |
| 7º | 48.6 | 36 | 48-80 |
| 8º | 56.7 | 48 | 56-100 |

**Nota:** Os custos de mana seguem uma progressão polinomial balanceada (~0.5×circle² + 1.5×circle + 2), projetada para crescimento controlado nos circles superiores.

### Requisitos de Reagents

A maioria dos feitiços requer reagents. Reagents comuns incluem:
- **Bloodmoss** - Usado em muitos feitiços
- **Black Pearl** - Componente comum
- **Garlic** - Usado em cura e utilidade
- **Ginseng** - Usado em cura e buffs
- **Mandrake Root** - Usado em circles superiores
- **Nightshade** - Usado em maldições
- **Spider's Silk** - Usado em feitiços defensivos
- **Sulfurous Ash** - Usado em feitiços de fogo

### Outros Requisitos

- **Linha de Visão:** Necessária para feitiços com alvo
- **Alvo Deve Estar Vivo:** Não pode mirar criaturas mortas
- **Espaço na Mochila:** Necessário para feitiços que criam itens
- **Alvo Válido:** Alguns feitiços têm requisitos específicos de alvo

---

## Fórmulas de Cálculo de Dano

### Sistema NMS (New Magic System) - Recomendado

**Fórmula:**
```
Base Damage = Dice(dice, sides, bonus)
EvalInt Benefit = NMSUtils.getDamageEvalBenefit(Caster)
Final Damage = Floor(Base Damage × EvalInt Benefit)
```

**Exemplo:**
```
Magic Arrow:
- Base: Dice(1, 3) + 2 = 3-5 dano
- Com EvalInt 100: ~1.0x multiplicador
- Final: 3-5 dano (limitado a 8)
```

**Características:**
- Cálculo mais simples
- Baseado principalmente em EvalInt
- Usado pela maioria dos feitiços modernos
- Dano mais previsível

### Sistema AOS (Age of Shadows) - Legado

**Fórmula:**
```
Base Damage = Dice(dice, sides, bonus) × 100
Damage Bonus = Inscribe + Intelligence + SDI (com limites PvP)
Scaled Damage = Scale(Base, 100 + Damage Bonus)
EvalInt Scaled = Apply EvalInt scaling
Final Damage = (Scaled × Scalar) / 100
```

**Componentes:**
- **Inscription Bonus:** Baseado na skill Inscription
- **Intelligence Bonus:** Baseado no atributo INT
- **SDI (Spell Damage Increase):** De equipamentos
- **EvalInt Scaling:** Multiplicador adicional

**Características:**
- Cálculo mais complexo
- Múltiplos modificadores
- Potencial de dano maior
- Usado por alguns feitiços legados

### Tipos de Dano

Os feitiços causam diferentes tipos de dano:
- **Physical:** Dano físico direto
- **Fire:** Dano baseado em fogo
- **Cold:** Dano de gelo/congelamento
- **Poison:** Dano tóxico (frequentemente DoT)
- **Energy:** Dano de energia mágica

**Distribuição de Tipos de Dano:**
- Fire: 35% dos feitiços (Magic Arrow, Fireball, Flamestrike, etc.)
- Energy: 24% dos feitiços (Lightning, Energy Bolt, Chain Lightning)
- Cold: 12% dos feitiços (Harm, Mind Blast)
- Physical: 6% dos feitiços (Earthquake)
- Poison: 12% dos feitiços (Poison, Poison Field)

---

## Fórmulas de Cálculo de Benefícios

### Feitiços de Cura

**Fórmula:**
```
Heal Amount = NMSUtils.getBeneficialMageryInscribePercentage(Caster) / 3
Bonus = 20% ao curar outros (não a si mesmo)
Final Heal = Heal Amount × (1.0 ou 1.2)
```

**Exemplo:**
```
Heal com Magery 100, Inscription 100:
- Base: ~33 HP
- Curando outros: ~40 HP (bônus de 20%)
```

### Feitiços de Buff de Atributos (Strength, Agility, Cunning)

**Fórmula Base:**
```
Percent = 1 + (Inscription.Fixed / 100)
Percent = Percent × 0.01
Percent = Percent × (0.8 se Inscription >= 120 senão 0.6)
Base Bonus = Target Stat × Percent
```

**Modificações de Balanceamento (2º Circle):**
```
Reduction Factor = Random(0.50, 0.70)  // 50-70% do valor original
Final Bonus = Base Bonus × Reduction Factor
Minimum = 1 ponto (se original > 0)
```

**Fórmula de Duração:**
```
Base = Random(10, 30)
Bonus = Ceiling(Inscription × 0.25) + TierBonus
PreCap = (Base + Bonus) × 0.70  // Redução de 30%
Final = Clamp(PreCap, 15, 60)   // Mín 15s, Máx 60s
```

**Anti-Stacking:**
- Não pode reconjurar enquanto o buff está ativo
- Mensagem: "O alvo já está sob efeito deste feitiço."

**Exemplo:**
```
Strength em alvo STR 150, Inscription 120:
- Cálculo Base: 15 STR
- Após Redução: 7-10 STR (aleatório 50-70%)
- Duração: 56-60 segundos (limitado)
```

### Feitiço Protection

**Penalidades de Resistência:**
```
Each Resistance = Random(-8, -2)
Total Penalty = Soma de 5 valores aleatórios
Average Total = ~-25 pontos de resistência
```

**Duração:**
```
Igual aos buffs de atributos: redução de 30%, limite de 15-60s
```

**Proteção Contra Interrupção:**
```
Primeiro Golpe: 100% de prevenção
Golpes Subsequentes: 50% de prevenção
```

---

## Visão Geral dos Circles

### 1º Circle (Feitiços 1-8)
**Custo de Mana:** 4
**Dificuldade:** Iniciante (0-20 skill)

**Feitiços:**
1. Clumsy - Reduz Dexterity
2. Create Food - Gera comida
3. Feeblemind - Reduz Intelligence
4. Heal - Restaura pontos de vida
5. Magic Arrow - Feitiço de ataque básico
6. Night Sight - Fornece visão noturna
7. Reactive Armor - Buff defensivo
8. Weaken - Reduz Strength

**Características:**
- Baixo custo de mana
- Conjuração rápida
- Utilidade e combate básicos

### 2º Circle (Feitiços 9-16)
**Custo de Mana:** 7
**Dificuldade:** Aprendiz (8-30 skill)

**Feitiços Principais:**
- Strength - Aumenta Strength (balanceado)
- Agility - Aumenta Dexterity (balanceado)
- Cunning - Aumenta Intelligence (balanceado)
- Protection - Buff defensivo com proteção contra interrupção (balanceado)

**Notas de Balanceamento:**
- Buffs de atributos: 50-70% de efetividade, duração de 15-60s, sem acúmulo
- Protection: Resistências aleatórias, proteção no primeiro golpe, 50% depois

### 3º Circle (Feitiços 17-24)
**Custo de Mana:** 11
**Dificuldade:** Novato (16-40 skill)

**Feitiços Principais:**
- Fireball - Dano em área
- Bless - Aumenta todos os atributos (reservado para balanceamento futuro)
- Magic Lock - Tranca contêineres
- Unlock - Destranca contêineres

### 4º Circle (Feitiços 25-32)
**Custo de Mana:** 16
**Dificuldade:** Adepto (24-50 skill)

**Feitiços Principais:**
- Fire Field - Dano de área ao longo do tempo
- Greater Heal - Cura forte
- Lightning - Dano de energia
- Summon Creature - Invocação básica

### 5º Circle (Feitiços 33-40)
**Custo de Mana:** 22
**Dificuldade:** Especialista (32-60 skill)

**Feitiços Principais:**
- Poison Field - Dano de veneno em área
- Teleport - Viagem instantânea
- Dispel - Remove criaturas invocadas
- Explosion - Alto dano em área

### 6º Circle (Feitiços 41-48)
**Custo de Mana:** 28
**Dificuldade:** Mestre (40-70 skill)

**Feitiços Principais:**
- Energy Bolt - Alto dano em alvo único
- Energy Field - Dano de energia em área
- Magic Reflect - Reflete feitiços
- Recall - Viagem para rune

### 7º Circle (Feitiços 49-56)
**Custo de Mana:** 36
**Dificuldade:** Grão-Mestre (48-80 skill)

**Feitiços Principais:**
- Flamestrike - Dano muito alto
- Gate Travel - Cria portal
- Mass Dispel - Dispel em área
- Meteor Swarm - Dano massivo em área

### 8º Circle (Feitiços 57-64)
**Custo de Mana:** 48
**Dificuldade:** Lendário (56-100 skill)

**Feitiços Principais:**
- Energy Vortex - Invocação poderosa
- Summon Daemon - Invocação muito poderosa
- Resurrection - Revive jogadores mortos
- Chain Lightning - Dano de energia em cadeia

---

## Informações de Balanceamento

### Mudanças de Balanceamento do 2º Circle (6 de Novembro, 2025)

Todos os feitiços de buff benéfico do 2º Circle foram rebalanceados para criar uma jogabilidade mais dinâmica.

#### Buffs de Atributos (Strength, Agility, Cunning)

**Mudanças:**
1. **Redução de Bônus de Atributos:** 30-50% de redução aleatória (50-70% do valor original)
2. **Redução de Duração:** 30% de redução com limites rígidos (15-60 segundos)
3. **Anti-Stacking:** Não pode reconjurar enquanto o buff está ativo

**Impacto:**
- **Antes:** Mago mestre dando +15-20 pontos de atributo por 80-100 segundos
- **Depois:** Mago mestre dando +7-10 pontos de atributo por 56-60 segundos
- **Efetividade:** ~65% de redução no valor total

**Exemplo:**
```
ANTIGO: Inscription 120 + STR 150 = +15 STR por 80-100s
NOVO: Inscription 120 + STR 150 = +7-10 STR por 56-60s
```

#### Feitiço Protection

**Mudanças:**
1. **Penalidades de Resistência:** Aleatórias -2 a -8 por resistência (em vez de -8 fixo)
2. **Duração:** Redução de 30% com limite rígido de 60 segundos
3. **Proteção Contra Interrupção:** 100% primeiro golpe, depois 50% de chance

**Impacto:**
- **Penalidade de Resistência:** 37% menos severa (média -25 vs -40)
- **Duração:** 40-50% mais curta (máx 60s vs 100s)
- **Interrupção:** Primeiro golpe sempre protegido, golpes subsequentes arriscados

**Exemplo:**
```
ANTIGO: Todas as resistências -8 (total -40), 100% de prevenção de interrupção
NOVO: Cada resistência -2 a -8 aleatório (média -25), 100% primeiro golpe, 50% depois
```

### Impacto no Gameplay

**PvP:**
- Buffs são úteis mas não obrigatórios
- Combate mais rápido
- Mais tomada de decisão tática
- Menos enrolação com buffs perfeitos

**PvE:**
- Gerenciamento de buff mais ativo
- Gerenciamento de mana mais importante
- Necessidade de reconjurar 2-3 vezes por andar de dungeon
- Ainda úteis mas não excessivamente poderosos

**Grupos:**
- Mago de buff fornece suporte sólido (não esmagador)
- Outros papéis de suporte permanecem valiosos
- Composições de grupo mais diversas viáveis

---

## Referência de Tipos de Dano

### Feitiços de Dano Fire
- Magic Arrow (1º Circle)
- Fireball (3º Circle)
- Fire Field (4º Circle)
- Explosion (5º Circle)
- Flamestrike (7º Circle)
- Meteor Swarm (7º Circle) - 35% fire

**Total:** ~35% dos feitiços de ataque

### Feitiços de Dano Energy
- Lightning (4º Circle)
- Energy Bolt (6º Circle)
- Energy Field (6º Circle)
- Chain Lightning (8º Circle)

**Total:** ~24% dos feitiços de ataque

### Feitiços de Dano Cold
- Harm (4º Circle)
- Mind Blast (5º Circle)

**Total:** ~12% dos feitiços de ataque

### Feitiços de Dano Physical
- Earthquake (6º Circle)

**Total:** ~6% dos feitiços de ataque

### Feitiços de Dano Poison
- Poison (3º Circle) - DoT
- Poison Field (5º Circle)

**Total:** ~12% dos feitiços de ataque

---

## Estratégia e Dicas

### Dicas Gerais

1. **Gerenciar Mana:** Feitiços de circles superiores custam mais mana
2. **Carregar Reagents:** Sempre tenha reagents para seus feitiços mais usados
3. **Usar Circles Inferiores:** Não use sempre seus feitiços de circle mais alto
4. **Movimento:** Esteja ciente dos limites de passos durante a conjuração
5. **Resistência:** Skill Magic Resist ajuda a resistir feitiços inimigos

### Estratégia PvP

1. **Buffs:** Use buffs de atributos antes de lutas importantes, mas não dependa deles
2. **Protection:** Use Protection para iniciar conjurações importantes, mas saiba que não é absoluto
3. **Interrupções:** Tente interromper conjuradores inimigos
4. **Reflexão:** Fique atento ao Magic Reflect nos inimigos
5. **Gerenciamento de Mana:** Não desperdice mana em alvos de baixo valor

### Estratégia PvE

1. **Cura:** Mantenha Heal/Greater Heal prontos para emergências
2. **Feitiços de Área:** Use Fireball/Explosion para grupos de inimigos
3. **Buffs:** Use buffs de atributos antes de lutas contra chefes
4. **Utilidade:** Use Teleport/Recall para viagem eficiente
5. **Invocações:** Use invocações para tankar dano

### Estratégia de Buff de Atributos

**Quando Usar:**
- Antes de lutas PvP importantes
- Antes de lutas contra chefes em PvE
- Quando você tem atributos base altos (melhor escalonamento)
- Quando a skill Inscription é alta (melhor duração)

**Quando NÃO Usar:**
- Limpando mobs comuns (desperdício de mana)
- Quando já está bufado (não pode acumular)
- Quando está com pouca mana
- Quando o bônus de atributo seria mínimo

**Dicas Profissionais:**
- Temporize buffs para momentos críticos
- Atributos base maiores = melhor valor de buff
- Não conjure spam (desperdício de mana)
- Coordene com o grupo para timing

### Estratégia de Protection

**Quando Usar:**
- Antes de conjurar feitiços de circles altos
- Quando em desvantagem numérica em PvP
- Quando enfrentando inimigos focados em melee
- Iniciando sequências longas de dungeon

**Quando NÃO Usar:**
- Quando não está conjurando ativamente
- Quando enfrentando conjuradores (penalidade de resistência prejudica)
- Quando já está bufado
- Para exploração estendida (duração curta)

**Dicas Profissionais:**
- Primeira interrupção sempre prevenida (muito valioso)
- Não confie em prevenção de múltiplas interrupções
- Penalidade de resistência varia (fator de sorte)
- Reconjure antes de conjurações importantes

---

## Tabelas de Referência Rápida

### Buffs de Atributos (Mago Mestre - Inscription 120)

| Atributo Alvo | Bônus Antigo | Bônus Novo | Duração Antiga | Duração Nova | Efetividade |
|---------------|--------------|------------|----------------|--------------|-------------|
| 80 | +2-3 | +1 | 80-100s | 56-60s | -66% a -83% |
| 100 | +5 | +2-3 | 80-100s | 56-60s | -55% a -72% |
| 120 | +7-8 | +3-4 | 80-100s | 56-60s | -50% a -70% |
| 150 | +15-16 | +7-10 | 80-100s | 56-60s | -62% a -72% |
| 200 | +20-21 | +10-14 | 80-100s | 56-60s | -58% a -73% |

**Nerf Médio:** ~65% de redução

### Feitiço Protection

| Inscription | Perda Resist Antiga | Perda Resist Nova | Duração Antiga | Duração Nova | Interrupção ANTIGA | Interrupção NOVA |
|-------------|---------------------|-------------------|----------------|--------------|---------------------|-------------------|
| 50 | -40 | ~-25 (-37%) | 30-45s | 15-22s | 100% todas | 100% → 50% |
| 80 | -40 | ~-25 (-37%) | 50-75s | 24-36s | 100% todas | 100% → 50% |
| 100 | -40 | ~-25 (-37%) | 65-85s | 31-41s | 100% todas | 100% → 50% |
| 120 | -40 | ~-25 (-37%) | 80-100s | 39-60s | 100% todas | 100% → 50% |

---

## Recursos Adicionais

### Documentação Relacionada
- **Magery_Spells_Complete_Guide.md** - Referência detalhada feitiço por feitiço (64 feitiços)
- **Magic_Trap_Spell_Guide.md** - Sistema Magic Trap (separado de Magery)

### Comandos no Jogo
- `[cast <número>` - Conjurar feitiço por número
- `[skills` - Ver suas skills
- `[stats` - Ver suas estatísticas

### Treinamento de Skills
- **Magery:** Conjure feitiços para ganhar skill
- **EvalInt:** Use feitiços que requerem avaliação
- **Inscription:** Crie spell scrolls
- **Magic Resist:** Resista aos feitiços inimigos

---

**Última Atualização:** 6 de Novembro, 2025
**Versão:** 1.0
**Status:** Completo e Atual
