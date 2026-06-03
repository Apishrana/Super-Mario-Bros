# Super Mario Bros 1-1 Copy

A 2D Unity recreation of the classic Super Mario Bros World 1-1 level.

This project is built as a fan-made Unity copy of Mario 1-1, with tilemap-based level design, Mario movement, enemies, power-ups, mystery blocks, coins, pipes, and camera boundaries.

## Features

- Playable Mario-style character controller.
- Walk, jump, high jump, sprint, and crouch controls.
- Small, big, and fire Mario states.
- Goomba enemies with patrol movement and stomp behavior.
- Mystery blocks that can spawn coins, mushrooms, flowers, or stars.
- Breakable blocks for big Mario.
- Collectable coins from tilemaps and mystery blocks.
- Pipe teleport behavior for vertical and horizontal pipes.
- Side-scrolling camera movement with level bounds.
- 2D sprites, tile palettes, animations, prefabs, and URP 2D rendering.

## Controls

| Action | Keys |
| --- | --- |
| Move left/right | `A` / `D` or Left / Right Arrow |
| Jump | `Space`, `W`, or Up Arrow |
| High jump | Hold `Space`, `W`, or Up Arrow |
| Sprint | `Shift` or `Ctrl` |
| Crouch | `S` |
| Enter vertical pipe | Crouch while standing on a pipe |
| Enter horizontal pipe | Move right into a pipe |

## Project Structure

```text
.
├── Assets
│   ├── Animations
│   │   ├── Clips
│   │   └── Controllers
│   ├── Editor
│   ├── Prefab
│   ├── Scenes
│   │   └── Lvls
│   │       └── 1-1.unity
│   ├── Scripts
│   │   ├── CameraAspectFix.cs
│   │   ├── GoombaController.cs
│   │   ├── HeadCheck.cs
│   │   ├── MoveCam.cs
│   │   ├── MushroomController.cs
│   │   ├── MystryBolckConroller.cs
│   │   ├── Pipe.cs
│   │   ├── PlayerAnimate.cs
│   │   ├── PlayerMovement.cs
│   │   ├── SpawnController.cs
│   │   └── VoidController.cs
│   ├── Settings
│   ├── Sprites
│   ├── Tiles
│   └── TilePalette
├── Packages
│   ├── manifest.json
│   └── packages-lock.json
├── ProjectSettings
│   └── ProjectVersion.txt
└── README.md
```

## Requirements

- Unity `6000.4.5f1`
- Unity Input System
- Universal Render Pipeline
- 2D Tilemap packages

The required Unity packages are listed in `Packages/manifest.json`.

## Setup

Open the project folder in Unity Hub:

```text
Super Mario Bros
```

Unity should install the packages from `Packages/manifest.json` automatically.

## Running the Game

1. Open the project in Unity.
2. Open the scene:

```text
Assets/Scenes/Lvls/1-1.unity
```

3. Press the Play button in the Unity Editor.

## Main Scripts

- `PlayerMovement.cs` handles player input, movement, jumping, crouching, power-ups, coins, enemy collision, and pipe entry.
- `PlayerAnimate.cs` controls Mario animation states.
- `HeadCheck.cs` handles hitting mystery blocks and breakable blocks from below.
- `MystryBolckConroller.cs` controls mystery block rewards and block animations.
- `GoombaController.cs` controls Goomba movement and death behavior.
- `MushroomController.cs` controls mushroom movement.
- `MoveCam.cs` controls side-scrolling camera limits.
- `Pipe.cs` stores pipe teleport and camera boundary data.

## Assets

The project includes:

- Mario, Goomba, mushroom, flower, coin, and block sprites.
- Overworld and underground tile sprites.
- Tile assets for building the 1-1 level.
- Player, enemy, block, power-up, pipe, and spawn prefabs.
- Animation clips and controllers for Mario, Goombas, and mystery blocks.

## Note

This is a fan-made learning project inspired by Super Mario Bros. It is not an official Nintendo project.
