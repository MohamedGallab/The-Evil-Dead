# Resident Evil X Dead Space

## Overview

Resident Evil X Dead Space is a third-person survival horror game developed using Unity. Inspired by popular titles like Resident Evil and Dead Space.

## Demo

## Gameplay

You play as Leon, the protagonist, navigating through a series of rooms filled with enemies and limited resources. Your objective is to survive by utilizing various weapons, grenades, and items while managing your health and inventory. The entire gameplay lasts between 5 to 10 minutes.

## Installation

download it from [here](https://drive.google.com/file/d/1yKN9qM-T5HLneDZnSAgSt2Smglwi9NrF/view?usp=drive_link)

## Features

### Player Mechanics

- **Health Points and Movement**: Leon starts with 8 health points. Sprinting doubles his movement speed.
- **Primary Weapons**: Equip and switch between a Pistol, Assault Rifle, Shotgun, and Revolver, each with unique characteristics.
- **Grenades**: Use Hand Grenades and Flash Grenades to deal damage and incapacitate enemies.
- **Knife**: A versatile tool for stabbing enemies and breaking free from grapples.
- **Interactions**: Interact with objects and enemies using a contextual UI.

### Resource Management

- **Gold Coins**: Earn and spend gold in the store.
- **Inventory**: Manage up to 6 items, including weapons, grenades, herbs, and more.
- **Store**: Buy, sell, and store items. Repair your knife and manage inventory effectively.
- **Crafting**: Combine items to create more powerful mixtures and ammo.

### Level Design

- **Main Level**: Explore interconnected rooms, fight enemies, and gather resources.
- **Enemies**: Encounter various enemies with different attack patterns and health points.
- **Key Items**: Find key items to unlock new areas and progress through the level.

## Controls

1. **Movement**: `W`, `A`, `S`, `D` keys
2. **Sprint**: Hold `Shift` key
3. **Aim**: Hold `Right Mouse Button`
4. **Shoot**: `Left Mouse Button`
5. **Reload**: `R` key
6. **Interact**: `E` key
7. **Inventory**: `I` key
8. **Grenade**: `G` key
9. **Knife Attack**: `F` key

### Additional Controls

- The player controls the camera rotation with the mouse.
- The player controls Leon’s walking movements forward and backward using the arrow up and down keys as well as the “W” and “S” keys respectively.
- The player controls the left and right movements using the right and left keys or the “A” and “D” keys respectively.
- The player runs/sprints by holding down “left-shift” along with one of the movement keys.
- The player controls the upward and downward movements using the “Z” and “C” keys respectively.
- The player aims their currently equipped weapon by holding down the right mouse button.
- The player fires their currently equipped weapon by pressing the left mouse button.
- The player reloads their currently equipped weapon by pressing “R”.
- The player throws their currently equipped grenade by pressing the “G” button.
- The player interacts with specific objects by pressing the “E” button.
- The player uses the stasis ability by pressing the “F” button.
- The player uses the kinesis ability by holding the “Q” button.
- The player opens and closes their inventory by pressing the “Tab” button.
- The player can select items and buttons in the UI screens using the mouse movement and the left mouse button.
- The player pauses and resumes playing, as well as close UI screens by pressing the “ESC” button.

## Heads-Up Display (HUD)

The HUD is the display area where the player can see important information about the player’s status, such as health, equipped abilities, and ammo count.

- **Health Points**: The health points are displayed as a glowing segmented bar on the back of the player where each segment represents 1 health point.
- **Ammo Count**: The ammo count is displayed as anchored text to the weapon. It shows the current amount of ammo in the currently equipped weapon.
- **Stasis (Boss Level)**: The stasis is displayed as a circle on the back of the player. The circle angle represents the amount of stasis points left.
- **Oxygen Gauge (Zero-G)**: The oxygen supply is displayed as anchored text to Leon’s back. It shows the current amount of oxygen points.

## Screens

### Main Menu

- **Play**
  - New Game: Starts a new game from the Combat level.
  - Level Select: Allows the player to directly play a specific level if there are multiple levels.
- **Options**
  - Audio
    - Music level
    - Effects Level
  - Team Credits: Lists each team member’s name and project role.
  - Asset Credits: Lists all external assets and their sources.
- **Quit Game**

### Pause Screen

- Resume
- Restart Level
- Quit to Main Menu

### Game Over Screen

- Restart Level
- Quit to Main Menu

### Inventory Screen

All UI elements (e.g. text, buttons) relevant to the inventory (see section 3.2) should be visible.

### Store Screen

The store screen consists of multiple sub-screens. All UI elements (e.g. text, buttons) relevant to the store (see section 3.3) should be visible.

- Buy Tab
- Sell Tab
- Storage Tab
- Knife Repair Tab

## Graphics

### Models

You will need models for the environment, characters, and weapons. You can use models from the game or alternative models as long as they are fairly representative of the requirements.

### Animations

#### Leon Animations

- Idle
- Walking
- Floating (Zero-G)
- Sprinting
- Aiming
- Firing Weapon
- Throwing Grenade
- Using Knife
- Hit Reaction
- Being Grappled
- Dying

#### Enemy Animations

- Idle
- Walking
- Animations for Each Attack
- Hit Reaction
- Knocked-down
- Dying

#### Boss Animations

- Idle
- Walking
- Animations for Each Attack
- Stunned
- Dying

## Audio

### Sound Settings

The audio in your game should be divided into at least two independently controllable categories; Music and Sound effects (SFX).

### Sound Effects

#### Effects

- Footsteps of Leon as he moves.
- Footsteps of an enemy as he moves.
- Footsteps of the boss as he moves.

#### Feedback

- When a weapon is fired.
- When a grenade detonates.
- When an item is picked up.
- When a door is unlocked.
- When Leon is hit.
- When Leon dies.
- When an enemy is hit.
- When an enemy dies.
- When a turret detects Leon.
- When a turret fires laser.
- When a boss is hit.
- When a boss dies.

### Music

- Slow-paced track for the main and pause menus.
- A different track for each of the game level(s) depending on the atmosphere.

## Cheats

Implementing cheat codes is required, as they will help us to test individual aspects of your project, in case we were not able to test them throughout the normal gameplay.

- Heal: Increases Leon’s health by 4 health points.
- Toggle Invincibility: Prevents Leon from taking damage when enabled.
- Toggle Slow Motion: Makes the gameplay in half speed when enabled.
- Add Gold: Increases Leon’s gold count by 1000 gold coins.
- Door Unlock: Unlocks all locked doors enabling Leon to move to other rooms.

## Useful Resources

These sections contain links that might be helpful to you during your implementation. You are not restricted to using these resources and can choose to use different ones.
