# Super Mario Bros

A Unity recreation of the classic Super Mario Bros game 1-1 level (to be expanded letter).

## Features

- Playable Mario-style character controller.
- Walk, jump, high jump, sprint, and crouch controls.
- Small, big, and fire states for Mario.
- Goomba enemies with movement and stomp behavior.
- Breakable blocks for big Mario.
- Collectable coins from tilemaps and mystery blocks.
- Pipe teleport behavior for vertical and horizontal pipes.
- Scrolling camera
- Level bounds.

## Controls

| Action                | Keys                            |
| --------------------- | ------------------------------- |
| Move left/right       | `A` / `D` or Left / Right Arrow |
| Jump                  | `Space`, `W`, or Up Arrow       |
| High jump             | Hold `Space`, `W`, or Up Arrow  |
| Sprint                | `Shift` or `Ctrl`               |
| Crouch                | `S` or Down Arrow               |
| Enter vertical pipe   | Crouch on the pipe              |
| Enter horizontal pipe | Move into the pipe              |

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

- Unity `6000.4.5f1` or latter`*`
- .Net

`*built in 6000.4.5f1`

## Setup

Open the project folder in Unity Hub

```text
Super-Mario-Bros
```

select a unity version above 6000.4

## Note

This is a fan-made learning project.

_please don't send a DMCA Nintendo_
