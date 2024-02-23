# Training Week 1
 2D platformer project as a training for LABGAT - Game Programmer

The sources in this project are taken from [this playlist](https://www.youtube.com/playlist?list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV)

## Key Features
- Various platforming mechanics such as Wall Jump, Double Jump, and Coyote Time
- Various traps and hazard
- 2 type of enemy, melee and ranged

## SOLID Principles
while the code is still far from perfect and needs lots of improvement, I have learned some implementation of SOLID principles while making this project.

### Single-Responsibility
The Player has many behavior and each behavior can be divided into its own class, for example the PlayerMovement, PlayerAttack, PlayerRespawn, and Health script handling each responsibility.

Improvement: the player class can be further seperated into an InputManager class focusing only on player input

### Open-Closed
Because we have several Traps and Enemy type each of them has their own script so that no one script has to handle every trap/enemy behaviour.

Improvement: The melee and ranged enemy share some behaviour, we could have used features such as inheritance so we dont need to copy and paste a lot of the code. Another part that can be improved is the Health script. While it is quite flexible being able to work on enemy and player, the script is quite bloated because it has to handle the behaviour difference of the player & enemy

### Liskov Substitution
in the script Health, we used a Behaviour type variable. Behaviours are Components that can be enabled or disabled, and the script in unity that inherits MonoBehaviour can be treated as a Behaviour this is because the MonoBehaviour itself inherits from Behaviour.

### Dependency Inversion
The volume setting (or options in general) in the game is controlled from a selection arrow and we have AudioManager script that handles all the logic, the abstraction layer is that the selection arrow doens't need to know how each option is implemented, rather the event on the Button component handles which method to execute. The button here acts as a layer of abstraction for the selection arrow and the logic of each options

Improvement: some of the scripts are still dependant to other scripts and introducing an abstraction layer can help if we want to scale up the game.

---
unity build: https://smtt.itch.io/labgat-week-1 (password: gat1)

## Controls
WASD - Move
Space - Jumps
Left Mouse Button - Fire/Attack
E - Select
Esc - Pause
