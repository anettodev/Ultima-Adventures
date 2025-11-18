# Healing & Bandages - Guia Completo

**Sistema:** Ultima Adventures - Sistema de Cura com Bandagens
**Última Atualização:** Dezembro, 2024
**Skills Relacionadas:** Healing (jogadores), Veterinary (criaturas), Anatomy/AnimalLore

---

## Índice

1. [Introdução ao Healing](#introdução-ao-healing)
2. [Como Usar Bandagens](#como-usar-bandagens)
3. [Como Funciona a Cura](#como-funciona-a-cura)
4. [Tipos de Bandagens](#tipos-de-bandagens)
5. [Fountain of Life](#fountain-of-life-fonte-da-vida)
6. [Treinamento de Healing](#treinamento-de-healing)
7. [Fórmulas de Cálculo](#fórmulas-de-cálculo)
8. [Interrupções e Penalidades](#interrupções-e-penalidades)
9. [Simulações](#simulações)

---

## Introdução ao Healing e Veterinary

Healing é a skill primária para curar jogadores usando bandagens. Veterinary é usada para curar criaturas (animais e monstros). Ambas funcionam de forma similar, mas requerem skills secundárias diferentes e têm algumas diferenças mecânicas.

### Conceitos Principais

- **Healing:** Cura jogadores e requer Anatomy como skill secundária
- **Veterinary:** Cura criaturas (animais e monstros) e requer AnimalLore como skill secundária
- **Seleção Automática:** O sistema escolhe automaticamente qual skill usar baseado no alvo
- **Bandagens:** Item consumível necessário para todas as ações de cura
- **Ações Bloqueadas:** Durante a cura, você não pode usar skills ou conjurar feitiços
- **Alcance:** Máximo de 2 tiles de distância (5 tiles se FriendsAvoidHeels estiver ativo)

### Quando Usar Cada Skill

**Healing + Anatomy:**
- Alvos: Jogadores (PlayerMobile)
- Qualquer criatura que não seja animal ou monstro

**Veterinary + AnimalLore:**
- Alvos: Criaturas com Body.IsMonster ou Body.IsAnimal
- Pets, animais selvagens, monstros
- **Nota:** O sistema detecta automaticamente o tipo de alvo

---

## Como Usar Bandagens

### Uso Básico

1. **Duplo-clique na bandagem** ou use `[bandself` / `[bandother`
2. **Selecione o alvo** (você mesmo ou outro jogador/criatura)
3. **Aguarde o tempo de aplicação** (1.3s a 7.2s)
4. **Resultado:** Cura HP, remove veneno, ou ressuscita (dependendo da situação)

### Requisitos

- **Bandagem na mochila:** Consumida ao iniciar a cura
- **Alvo válido:** Deve estar vivo (exceto para ressurreição), não ser Golem, não estar em estado Blessed
- **Fome mínima:** Alvo deve ter Hunger >= 5 para receber cura (apenas jogadores)
- **Skills mínimas:** 
  - **Healing:** 60+ Healing/Anatomy para curar veneno, 80+ para ressurreição
  - **Veterinary:** 60+ Veterinary/AnimalLore para curar veneno, 80+ para ressurreição

### Restrições Durante Cura

- **Bloqueio de ações:** Não pode usar skills ou conjurar feitiços enquanto cura
- **Distância:** Se o alvo se afastar mais de 2 tiles, a cura é cancelada
- **Paralisia:** Se você for paralisado ou congelado, a cura é cancelada
- **Morte:** Se você morrer, a cura é cancelada

---

## Como Funciona a Cura

### Tipos de Ação

**1. Cura de HP (Hit Points)**
- Requisito: Alvo vivo, com dano, não envenenado
- Fórmula: Baseada em Healing/Anatomy (jogadores) ou Veterinary/AnimalLore (criaturas)
- Sucesso: Chance baseada na skill
- Resultado: Restaura HP (mínimo 1, máximo 80)
- **Bônus para Criaturas:** Criaturas recebem bônus de HP baseado em HitsMax / 100

**2. Cura de Veneno**
- Requisito: Alvo envenenado, 60+ Healing/Anatomy ou Veterinary/AnimalLore
- Fórmula: Chance baseada no nível do veneno + skills
- Sucesso: Remove o veneno completamente
- Resultado: Alvo curado de todos os venenos

**3. Ressurreição**
- Requisito: Alvo morto, 80+ Healing/Anatomy ou Veterinary/AnimalLore
- Fórmula: Chance baseada em Healing/Veterinary - 68
- Sucesso: Abre gump de ressurreição (jogadores) ou ressurreição direta (pets)
- Resultado: Alvo pode escolher ressuscitar (jogadores) ou é ressuscitado automaticamente (pets do dono)

**4. Parar Sangramento**
- Requisito: Alvo com efeito Bleed
- Resultado: Remove o efeito de sangramento

**5. Ferida Mortal**
- Requisito: Alvo com Mortal Strike
- Resultado: Não pode ser curado (apenas mensagem)

---

## Tipos de Bandagens

### Bandagem Regular

**Propriedades:**
- Peso: 0.3 stones
- Stackable: Sim
- Hue: 0 (padrão)
- Onde obter: Vendores, loot, crafting

**Efeitos:**
- Cura HP baseada em skills
- Cura veneno baseada em skills
- Ressuscita baseado em skills
- Sem bônus especiais

### Bandagem Aprimorada (Enhanced Bandage)

**Propriedades:**
- Peso: 0.3 stones
- Stackable: Sim
- Hue: 0x8A5 (dourado)
- Propriedade: "essas bandagens foram aprimoradas"

**Como Obter:**
- **Fountain of Life:** Coloque bandagens regulares na fonte
- **Conversão:** 1 bandagem regular = 1 bandagem aprimorada (usa 1 carga da fonte)
- **Taxa de Sucesso:** 100% (garantido, desde que haja cargas)

**Bônus:**
- **+10 HP:** Adiciona 10 pontos de vida à cura
- **+5% chance de cura de veneno:** Para todos os níveis de veneno
- **-0.3s a -1.0s:** Redução aleatória no tempo de aplicação
- **Efeitos visuais:** Partículas verdes e som (como Greater Heal)

### Fountain of Life (Fonte da Vida)

**Descrição:**
A Fountain of Life é um addon especial que converte bandagens regulares em bandagens aprimoradas. É um item de casa que pode ser colocado em sua residência.

**Propriedades:**
- **Tipo:** Addon Container (não pode ser levantado)
- **Capacidade:** Até 125 itens
- **Cargas Máximas:** 15 cargas
- **Cargas Padrão:** 15 cargas ao criar
- **Recarga:** 1 dia (recarrega automaticamente)
- **Hue do Deed:** 0x8A5 (dourado, igual às bandagens aprimoradas)

**Como Funciona:**
1. **Coloque bandagens na fonte:** Arraste bandagens regulares para dentro da fonte
2. **Conversão automática:** As bandagens são convertidas imediatamente em enhanced bandages
3. **Consumo de cargas:** Cada bandagem convertida consome 1 carga
4. **Recarga diária:** A fonte recarrega automaticamente para 15 cargas a cada 24 horas

**Mecânica de Conversão:**
- **Taxa de sucesso:** 100% (garantido)
- **Conversão parcial:** Se você colocar mais bandagens do que cargas disponíveis, apenas as que podem ser convertidas serão processadas
- **Processamento:** Bandagens são processadas em ordem reversa (últimas primeiro)
- **Limite:** Conversão para quando as cargas acabam

**Exemplo de Uso:**
- Fonte com 15 cargas + 20 bandagens regulares = 15 enhanced bandages + 5 bandagens regulares restantes
- Fonte com 5 cargas + 3 bandagens regulares = 3 enhanced bandages + 2 cargas restantes

**Como Obter:**
- **Deed:** Use o Fountain of Life Deed (dourado) para colocar a fonte em sua casa
- **Deed hue:** 0x8A5 (mesma cor das enhanced bandages)
- **Colocação:** Funciona como qualquer outro addon de casa

**Dicas:**
- Verifique as cargas restantes na propriedade do item
- A fonte recarrega automaticamente, não precisa de manutenção
- Use as cargas estrategicamente - enhanced bandages são valiosas
- A fonte pode armazenar até 125 itens, útil para conversão em massa

---

## Treinamento de Healing

### Sistema de Ganho de Skill

**Ganhos baseados em skill e ação bem-sucedida:**

| Skill Range | Ação que Dá Ganho | Requisito |
|-------------|-------------------|-----------|
| 0.0 - 60.0 | Cura de HP | Deve ser bem-sucedida |
| 60.1 - 90.0 | Cura de Veneno (níveis 0-3) | Deve ser bem-sucedida |
| 80.1 - 110.0 | Ressurreição | Deve ser bem-sucedida |
| 110.1 - 120.0 | Cura de Veneno Lethal (nível 4) | Deve ser bem-sucedida |

### Dicas de Treinamento

**0-60 Skill:**
- **Healing:** Cure jogadores com dano
- **Veterinary:** Cure pets/criaturas com dano
- Use em si mesmo ou em outros
- Foque em curas bem-sucedidas

**60-90 Skill:**
- **Healing:** Cure venenos em jogadores (Lesser, Regular, Greater, Deadly)
- **Veterinary:** Cure venenos em criaturas (Lesser, Regular, Greater, Deadly)
- Use em alvos envenenados
- Mantenha Anatomy/AnimalLore alta

**80-110 Skill:**
- **Healing:** Ressuscite jogadores mortos
- **Veterinary:** Ressuscite pets mortos
- Requer 80+ em ambas as skills (Healing/Anatomy ou Veterinary/AnimalLore)

**110-120 Skill:**
- **Healing:** Cure veneno Lethal (nível 4) em jogadores
- **Veterinary:** Cure veneno Lethal (nível 4) em criaturas
- Mais difícil de encontrar alvos
- Requer alta skill em ambas

### Bônus de Veterinary

**Sistema de Ganho Extra de Skill:**
Ao curar seu pet (não invocado) enquanto ele está lutando contra criaturas mais fortes:
- **Ratio:** (HitsMax do inimigo) / (HitsMax do pet × 2)
- **Requisitos:**
  - Pet deve ser seu (ControlMaster == você)
  - Pet deve estar em combate (Combatant != null)
  - Inimigo deve ser BaseCreature
  - Ratio >= 2 para ativar bônus
- **Bônus:** Ganha chance extra de skill gain por cada incremento de ratio
- **Máximo:** Ratio de 10 (máximo bônus)
- **Mecânica:** Para cada ponto de ratio acima de 2, há 50% de chance de ganhar skill extra

**Exemplo:**
- Pet com 100 HP lutando contra criatura com 600 HP
- Ratio = 600 / (100 × 2) = 3
- Ratio efetivo = 3 (clampado em 10)
- Ganhos extras possíveis: 1 (ratio 3 - 2 = 1 incremento)

---

## Fórmulas de Cálculo

### Fórmula de Cura de HP

**Chance de Sucesso:**
```
// Para Healing (jogadores):
Chance = ((Healing + 10) / 100) - 0.10 - (Slips × 0.02)

// Para Veterinary (criaturas):
Chance = ((Veterinary + 10) / 100) - 0.10 - (Slips × 0.02)
```

**Quantidade Curada:**
```
// Para Healing (jogadores):
Min = (Anatomy/2) + (Healing/2) + 20
Max = (Anatomy/2) + (Healing/2) + 50

// Para Veterinary (criaturas):
Min = (AnimalLore/2) + (Veterinary/2) + 20
Max = (AnimalLore/2) + (Veterinary/2) + 50

HP = Random(Min, Max)

// Bônus para criaturas:
HP += HitsMax / 100

// Penalidades:
HP -= HP × Slips × 0.35 (AOS) ou HP -= Slips × 4 (Classic)
HP ×= 0.4 (redução base de 60%)
HP ×= (1.0 - DamageEvents × 0.10) (perda de concentração)

// Bônus Enhanced:
HP += 10 (se EnhancedBandage)

// Cap:
HP = Min(HP, 80)
```

### Fórmula de Cura de Veneno

**Chance de Sucesso:**
```
BaseChance = [50% (Lesser), 40% (Regular), 30% (Greater), 12% (Deadly), 3% (Lethal)]

// Para Healing (jogadores):
SkillBonus = (Healing/10 × 1%) + (Anatomy/10 × 1%)

// Para Veterinary (criaturas):
SkillBonus = (Veterinary/10 × 1%) + (AnimalLore/10 × 1%)

SlipPenalty = Slips × 2%
EnhancedBonus = 5% (se EnhancedBandage)

Chance = BaseChance + SkillBonus - SlipPenalty + EnhancedBonus
```

### Fórmula de Ressurreição

**Chance de Sucesso:**
```
// Para Healing (jogadores):
Chance = ((Healing - 68) / 50) - (Slips × 0.02)

// Para Veterinary (criaturas):
Chance = ((Veterinary - 68) / 50) - (Slips × 0.02)
```

### Fórmula de Tempo de Aplicação

**Tempo Base:**
```
// Healing (jogadores) - Outros:
Tempo = 1.0s a 2.4s (baseado em DEX, antes do multiplicador)
// Usa cálculo AOS ou Classic dependendo da era

// Veterinary (criaturas) - Outros:
Tempo = 3.0s (padrão) ou (dexseconds + 1.0s) se dexseconds >= 2.0s
// Tempo especial para Veterinary (AOS apenas)
// Bounded entre 1.0s e 2.4s

// Si mesmo (ambos):
Tempo = TempoOutros × 2.0

// Penalidade de velocidade:
Tempo ×= 1.3 (30% mais lento)

// Bônus Enhanced:
Tempo -= Random(0.3s, 1.0s)

// Limites finais:
Tempo = Clamp(Tempo, 1.3s, 7.2s)
```

**Fatores de DEX:**
- DEX cap: 300
- Bônus DEX: 30% mais forte (1.3x multiplicador)
- Alto DEX reduz significativamente o tempo
- **Veterinary:** Tem cálculo especial baseado em dexseconds (AOS apenas)

---

## Interrupções e Penalidades

### Slips (Dedos Escorregam)

**Quando Ocorre:**
- **Jogadores:** Dano >= 10% do HP máximo
- **Criaturas:** Dano >= 20 HP (de jogadores) ou >= 25 HP (de NPCs)
- **Midland:** Pode evitar com teste de Agility (Agility/2 > Random)

**Efeitos:**
- **Mensagem:** "Seus dedos escorregam!" (amarelo)
- **Penalidade de chance:** -2% por slip
- **Penalidade de HP:** -35% por slip (AOS) ou -4 HP por slip (Classic)
- **Conta como dano:** Slips também contam como eventos de dano (concentração)

### Perda de Concentração

**Quando Ocorre:**
- Cada vez que o curador recebe dano durante a aplicação
- Slips também contam como eventos de dano

**Efeito:**
- **Redução:** -10% de HP curado por evento de dano
- **Acumulativo:** Múltiplos eventos de dano reduzem mais
- **Mensagem:** Mostra % de perda de concentração no resultado

**Exemplo:**
- 1 evento de dano: -10% HP curado
- 2 eventos de dano: -20% HP curado
- 3 eventos de dano: -30% HP curado

### Cancelamento de Cura

**Causas:**
1. **Distância:** Alvo se afasta mais de 2 tiles
2. **Paralisia:** Curador fica paralisado ou congelado
3. **Morte:** Curador morre
4. **Deleção:** Curador ou alvo é deletado

**Mensagens:**
- Distância: "Your target moved too far away and the healing was cancelled."
- Paralisia: "A cura foi interrompida porque você foi paralizado!"

### Bloqueio de Ações

**Durante a Cura:**
- **Skills:** Bloqueadas (mensagem de erro)
- **Feitiços:** Bloqueados (mensagem: "Você está realizando uma ação de cura!")
- **Outras ações:** Bloqueadas até a cura terminar ou ser cancelada

---

## Simulações

### Cenário 1: Cura Básica (Skill 100, DEX 100)

**Configuração:**
- Healing: 100.0
- Anatomy: 100.0
- DEX: 100
- Alvo: Outro jogador
- Bandagem: Regular
- Sem slips, sem dano

**Resultados:**

| Item | Valor |
|------|-------|
| Tempo de Aplicação | ~2.0s |
| Chance de Sucesso | 100% |
| HP Curado (Min) | 70 HP |
| HP Curado (Max) | 100 HP |
| HP Curado (Médio) | 85 HP |
| HP Final (após redução 60%) | 34 HP |

**Fórmula Aplicada:**
```
Min = (100/2) + (100/2) + 20 = 120
Max = (100/2) + (100/2) + 50 = 150
HP = Random(120, 150) = 125 (exemplo)
HP × 0.4 = 50 HP
Cap = Min(50, 80) = 50 HP
```

### Cenário 2: Cura com Enhanced Bandage (Skill 120, DEX 150)

**Configuração:**
- Healing: 120.0
- Anatomy: 120.0
- DEX: 150
- Alvo: Outro jogador
- Bandagem: Enhanced
- Sem slips, sem dano

**Resultados:**

| Item | Valor |
|------|-------|
| Tempo de Aplicação | ~1.5s (com bônus -0.3s a -1.0s) |
| Chance de Sucesso | 120% (garantido) |
| HP Curado (Min) | 80 HP |
| HP Curado (Max) | 110 HP |
| HP Final (após redução + bônus) | 44-54 HP |
| Efeitos Visuais | Partículas verdes + som |

**Fórmula Aplicada:**
```
Min = (120/2) + (120/2) + 20 = 140
Max = (120/2) + (120/2) + 50 = 170
HP = Random(140, 170) = 155 (exemplo)
HP × 0.4 = 62 HP
HP + 10 (Enhanced) = 72 HP
Cap = Min(72, 80) = 72 HP
```

### Cenário 3: Cura com Penalidades (Skill 80, 1 Slip, 2 Eventos de Dano)

**Configuração:**
- Healing: 80.0
- Anatomy: 80.0
- DEX: 80
- Alvo: Si mesmo
- Bandagem: Regular
- 1 slip, 2 eventos de dano

**Resultados:**

| Item | Valor |
|------|-------|
| Tempo de Aplicação | ~5.0s (auto-cura 2x mais lenta) |
| Chance de Sucesso | 78% (80% - 2% slip) |
| HP Base (Min) | 100 HP |
| HP Base (Max) | 130 HP |
| HP após Slip | 65-84 HP (AOS: -35%) |
| HP após Redução Base | 26-34 HP |
| HP após Concentração | 21-27 HP (2 eventos: -20%) |
| Mensagem Final | "Você curou X pontos de vida. Devido ao dano sofrido, a perda de concentração foi de 20%." |

**Fórmula Aplicada:**
```
Base = Random(100, 130) = 115 HP
Slip = 115 × 0.35 = 40 HP perdidos → 75 HP
Redução = 75 × 0.4 = 30 HP
Concentração = 30 × 0.8 (2 eventos) = 24 HP
```

### Tabela Comparativa: Regular vs Enhanced (Healing)

| Skill | DEX | Tipo | Tempo | HP Curado | Chance Veneno (Lesser) |
|-------|-----|------|-------|-----------|------------------------|
| 50 | 50 | Regular | ~4.5s | 20-25 HP | 55% |
| 50 | 50 | Enhanced | ~3.8s | 30-35 HP | 60% |
| 80 | 100 | Regular | ~2.5s | 36-44 HP | 66% |
| 80 | 100 | Enhanced | ~1.8s | 46-54 HP | 71% |
| 100 | 150 | Regular | ~2.0s | 44-52 HP | 70% |
| 100 | 150 | Enhanced | ~1.3s | 54-62 HP | 75% |
| 120 | 200 | Regular | ~1.8s | 52-60 HP | 74% |
| 120 | 200 | Enhanced | ~1.3s | 62-70 HP | 79% |

### Tabela Comparativa: Regular vs Enhanced (Veterinary)

| Skill | DEX | Tipo | Tempo | HP Curado | Chance Veneno (Lesser) |
|-------|-----|------|-------|-----------|------------------------|
| 50 | 50 | Regular | ~3.0s | 20-25 HP + HitsMax/100 | 55% |
| 50 | 50 | Enhanced | ~2.3s | 30-35 HP + HitsMax/100 | 60% |
| 80 | 100 | Regular | ~2.5s | 36-44 HP + HitsMax/100 | 66% |
| 80 | 100 | Enhanced | ~1.8s | 46-54 HP + HitsMax/100 | 71% |
| 100 | 150 | Regular | ~2.0s | 44-52 HP + HitsMax/100 | 70% |
| 100 | 150 | Enhanced | ~1.3s | 54-62 HP + HitsMax/100 | 75% |
| 120 | 200 | Regular | ~1.8s | 52-60 HP + HitsMax/100 | 74% |
| 120 | 200 | Enhanced | ~1.3s | 62-70 HP + HitsMax/100 | 79% |

**Notas:**
- Tempos são para curar outros (auto-cura é 2x mais lenta)
- HP após redução base de 60%
- Enhanced sempre adiciona +10 HP e +5% chance de veneno
- Enhanced reduz tempo em 0.3-1.0s aleatoriamente
- **Veterinary:** Criaturas recebem bônus adicional de HitsMax/100
- **Veterinary:** Tempo pode variar baseado em dexseconds (AOS apenas)

---

## Referência Rápida

### Comandos
- `[bandself` - Curar a si mesmo
- `[bandother` - Curar outro alvo

### Skills Relacionadas
- **Healing:** Skill primária para jogadores
- **Veterinary:** Skill primária para criaturas
- **Anatomy:** Skill secundária para Healing
- **AnimalLore:** Skill secundária para Veterinary

### Limites Importantes
- **HP Máximo Curado:** 80 HP
- **Alcance Máximo:** 2 tiles (5 se FriendsAvoidHeels)
- **Tempo Mínimo:** 1.3 segundos
- **Tempo Máximo:** 7.2 segundos
- **Auto-cura:** 2x mais lenta que curar outros
- **Fountain of Life:** 15 cargas máximas, recarrega diariamente

### Dicas Estratégicas

**PvP:**
- Use Enhanced Bandages em situações críticas
- Mantenha distância durante a cura
- Evite curar enquanto recebe dano (perda de concentração)
- Alto DEX acelera significativamente a cura

**PvE:**
- **Healing:** Treine curando outros jogadores com dano constante
- **Veterinary:** Treine curando pets com dano constante (bônus extra em combate)
- Use Enhanced para venenos difíceis
- Auto-cura é mais lenta - considere curar outros primeiro
- **Veterinary:** Ganhe skill extra curando pets lutando contra inimigos mais fortes

**Treinamento Healing:**
- 0-60: Foque em curas bem-sucedidas em jogadores
- 60-90: Cure venenos em jogadores regularmente
- 80-110: Ressuscite jogadores mortos
- 110-120: Cure veneno Lethal em jogadores quando possível

**Treinamento Veterinary:**
- 0-60: Foque em curas bem-sucedidas em pets/criaturas
- 60-90: Cure venenos em criaturas regularmente
- 80-110: Ressuscite pets mortos
- 110-120: Cure veneno Lethal em criaturas quando possível
- **Dica:** Cure pets em combate contra inimigos mais fortes para ganho extra de skill

---

**Última Atualização:** Dezembro, 2024
**Versão:** 1.0
**Status:** Completo e Atual

