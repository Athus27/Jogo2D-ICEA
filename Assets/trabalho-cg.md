
# Jogo 2D – ICEA / Unity

Projeto de jogo digital 2D desenvolvido em **Unity**, conforme especificações do trabalho acadêmico do ICEA.

---

## 📁 Estrutura de Pastas

```

Assets/
├── Scenes/        # Cenas do jogo (Menu, Jogo, GameOver)
├── Scripts/       # Scripts C# (movimento, menu, inimigos, etc.)
├── Sprites/       # Sprites e sprite sheets (player, inimigos, cenários)
├── Animations/    # Animações e Animator Controllers
├── Prefabs/       # Prefabs reutilizáveis (Player, inimigos, UI)
├── UI/            # Elementos de interface (menus, HUD, botões)
├── Audio/         # Efeitos sonoros e músicas
└── Materials/     # Materiais (uso pontual em 2D)

```



---

## 🎮 Funcionalidades Previstas

- Jogo digital **2D**
- **Single-player** (offline)
- Desenvolvido em **Unity**
- Mecânicas obrigatórias:
  - Andar (A/D ou setas)
  - Pular (Espaço)
  - Atacar (com animação e som)
- Placar de pontuação
- Menu inicial com opções clássicas:
  - Jogar
  - Opções
  - Sair
- Jogo inspirado na jornada de um aluno rumo à formatura

---

## 🔧 Ferramentas Utilizadas

- Unity (URP 2D)
- LibreSprite (criação de sprites e animações)
- Git + GitHub (controle de versão)

---

## 🔁 Fluxo de Trabalho (Git / GitHub)

### Inicialização
```bash
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin <URL_DO_REPOSITORIO>
git push -u origin main
````

### Desenvolvimento

* Criar branch para cada funcionalidade:

```bash
git checkout -b feature/menu
```

* Commits pequenos e objetivos:

```bash
git commit -m "Add main menu UI"
```

* Finalizar funcionalidade:

```bash
git checkout main
git merge feature/menu
```

### Boas práticas

* Não versionar arquivos temporários
* Commits claros e frequentes
* Organização de pastas respeitada

---

## 👤 Autoria

Projeto acadêmico desenvolvido para a disciplina de Computação Gráfica

```
```
