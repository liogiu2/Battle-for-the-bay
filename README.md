Battle for the Bay
=============

Battle for the Bay is a computer game designed and developed in the [SC-SC-T-624-CGDD][1] three weeks course tought by David Thue and Marco Bancale.
The game is essentially a pirate-era MOBA game where the player controls a pirate ship and is accompanied by his minions to conquer the enemy base.

[1]: http://cadia.ru.is/wiki/public:t-624-cgdd-17-3:main

![Screenshot](https://i.gyazo.com/1f94240d16fecea14b859605beb923f4.png?raw=true)

<img src="https://i.gyazo.com/87bc4e036a0bfa2c26b6107450fd810f.jpg?raw=true" width="240">&nbsp;
<img src="https://i.gyazo.com/a802e8aeb3b0c46caffc7e763a643a12.jpg?raw=true" width="240">

Features
--------
- Player movement and special abilities
- Player upgrade system
- Player and enemy AI minion system
- Attacking towers and structure health
- Tower upgrade system
- Player base upgrade system
- HUD & Minimap
- ...more

Assets
------------
- [mrford3d](https://sketchfab.com/models/a08042ec0d6d492e8c2bddfe20fab855) Shark prefab, licensed under CC0
- [Rafael](https://sketchfab.com/models/6f3afdd50a464a4ab5476e100161c2e1) Stone Hammer prefab, licensed under CC0
- [mrsCrash](https://sketchfab.com/models/92a8d45d2d5c43b8b0b5bf7d97816ada) Level 1 chest, licensed under CC0
- [rimanah](https://sketchfab.com/models/e380d82e638e4ccfabf1030d88f5deec) Level 2 chest, licensed under CC0
- [Prographer](https://sketchfab.com/models/42e14fc80d4f4a76aaac082641c6487c) Level 3 chest, licensed under CC0
- [Dustyroom](https://www.assetstore.unity3d.com/en/#!/content/54116) General game sounds, licensed under CC0
- [FullTiltBoogie](https://www.assetstore.unity3d.com/en/#!/content/83179) Starter Particle Pack
- [Lylek Games](http://u3d.as/aM5) Medieval Town Exteriors
- [Digital Ruby (Jeff Johnson)](http://u3d.as/g72) Fire & Spell Effects
- [Tornado Bandits Studio](http://u3d.as/ufT) Low Poly Free Pack
- [PolySquid](http://u3d.as/WRr) Pirate Ships
- [freesound.org](https://freesound.org/) Various sound effects
- [Freepik]() AOE aiming sprite


Instructions
------------

If you just want to play the game on Windows, use the [available binaries for
each release][2] are what you need.  Just click the green button under a
release to download the archive and run the `*.exe` file. This might require a
newer Windows version.

For development, there are Visual Studio 2013 project files included. I also
provide prebuild libraries for the Visual C++ compiler. You need to build the
dependencies and put thier static LIB files into the `bin` directory first.

[2]: https://github.com/danijar/computer-game/releases

Built With
---------

Savegames are stored in `save/`. Modify the settings file located at
`script/settings/init.js` to specify which save game should be loaded at
startup. The default name is `world`. If the name doesn't exist yet, a new
world is created.

    Savegame: [ 'string', 'world' ],

There are also scripting functions available for creating new worlds and
switching between them at run time.

Built with
---------

The scripting language is JavaScript. You can access the in-game scripting
console by pressing `Tab`. It's only visible in debug mode, though. To enable
debug mode, blindly press `Tab`, type in `debug()` and hit `Enter`. You can now
see debug information on the screen like the scripting console, frames per
second, number of loaded meshes, coordinates of the camera, and so on.

Documentation can be found in the [List of Scripting Functions][3].

[3]: https://github.com/danijar/computer-game/wiki/List-of-Scripting-Functions

Controls
--------

### Settings and global actions

| Key   | Action                                       |
| :---: | -------------------------------------------- |
| `Esc` | Exit the application.                        |
| `F1`  | Toggle mouse capture for camera movement.    |
| `F2`  | Toggle wire frame mode.                      |
| `F3`  | Toggle collision shapes debug draw.          |
| `F4`  | Toggle vertical sync.                        |
| `F11` | Toggle between fullscreen and windowed mode. |
| `Tab` | Toggle scripting console.                    |

### Interaction with the world

| Key            | Action                               |
| :------------: | ------------------------------------ |
| `Mouse Left`   | Mine selected block.                 |
| `Mouse Right`  | Place a block.                       |
| `Mouse Middle` | Pick type of selected block.         |
| `1` to `9`     | Change type of block getting placed. |

### Inserting test objects

| Key      | Action                                                           |
| :------: | ---------------------------------------------------------------- |
| `Ctrl+C` | Insert a capsule body.                                           |
| `Ctrl+V` | Shoot a cube in view direction.                                  |
| `Ctrl+B` | Place barrel on the floor.                                       |
| `Ctrl+N` | Insert a big rock.                                               |
| `Ctrl+M` | Set a shrine.                                                    |
| `Ctrl+X` | Insert a big cube with the material from the `texture` variable. |
| `HOUSE`  | Insert a procedurally generated house.                           |

Known Bugs
----------

- Switch to fullscreen doen't work
- Testing on intel tablet, the screen pass doesn't work after resize
- Material live reload is not reliable
- Two unneeded framebuffer creations fail at startup
- Stencil doesn't work in all cases
- Out of range chunks aren't freed completely
- One can sometimes jump when in air

Authors
----------

- Switch to fullscreen doen't work
- Testing on intel tablet, the screen pass doesn't work after resize
- Material live reload is not reliable
- Two unneeded framebuffer creations fail at startup
- Stencil doesn't work in all cases
- Out of range chunks aren't freed completely
- One can sometimes jump when in air


License
------------
This software is under MIT license: https://opensource.org/licenses/MIT Copyright 2017 LiquidDreams

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
