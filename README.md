# Top Down 2D Game

A top-down 2D survival game developed using Unity 6.

This project was created as part of a Game Development technical assessment.

---

## Overview

The player explores a map, shoots enemies, avoids enemy attacks, and eliminates all enemies to achieve victory.

The project demonstrates gameplay systems, AI behaviour, object pooling, Scriptable Objects, UI management, and optimization techniques.

---

## Features

### Player System

- Top-down player movement
- Smooth player rotation
- Camera follow using Cinemachine
- Shooting mechanics
- Health system

### Projectile System

- Projectile damage
- Projectile speed
- Projectile range
- Projectile cooldown
- Projectile lifetime
- Projectile knockback
- Projectile pierce count
- Projectile color customization
- Projectile scale customization

### Enemy AI System

- Enemy patrol behaviour
- Enemy player detection
- Enemy chase behaviour
- Enemy search behaviour
- Enemy attack system
- Enemy health system
- Enemy death system

### Collision System

- Bullets collide with walls
- Bullets damage enemies only
- Walls block enemy vision

### UI System

- Health UI
- Victory Screen
- Game Over Screen
- Restart Button
- Exit Button

### Optimization

- Object Pooling system
- Scriptable Object based projectile configuration
- Runtime parameter editing

---

## Gameplay Flow

1. Player starts inside the level.
2. Enemies patrol their assigned areas.
3. Enemy detects player within view range.
4. Enemy chases and attacks player.
5. Player eliminates all enemies.
6. Victory screen appears.
7. If player health reaches zero, Game Over screen appears.

---

## Technologies Used

- Unity 6
- C#
- Cinemachine
- Scriptable Objects
- Object Pooling
- Git
- GitHub

---

## Project Structure

Assets

```
Assets
├── Scripts
│   ├── Player
│   ├── Enemy
│   ├── Projectile
│   ├── UI
│   └── Managers
│
├── ScriptableObjects
├── Sprites
├── Prefabs
├── Scenes
└── Settings
```

---

## Demo Video

Demo Video:

[(Add Google Drive Link Here)](https://drive.google.com/drive/folders/1xCw9LntrVAo6J4WGX9vnY-oMxQ5NM0nH?usp=drive_link)

Example:

https://drive.google.com/xxxxx

---

## GitHub Repository

Repository Link:

https://github.com/AsheedEliyangod/Top-Down-2D-Game

---

## How To Run

1. Clone the repository.

```bash
git clone https://github.com/AsheedEliyangod/Top-Down-2D-Game.git
```

2. Open the project using Unity 6.

3. Open MainScene.

4. Press Play.

---

## Future Improvements

- Better enemy pathfinding
- Additional enemy types
- More levels
- Sound effects
- Background music
- Better animations

---

## Developer

Asheed Eliyangod

Game Developer

GitHub:

https://github.com/AsheedEliyangod
