# Project PROJECT_NAME

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Aiden Grieshaber
-   Section: 1

## Game Design

-   Camera Orientation: Top-down
-   Camera Movement: No camera movement
-   Player Health: Lives
-   End Condition: Defeat the boss
-   Scoring: Destroying enemies rewards points

### Game Description

Welcome to Strafe Space! Pilot a space ship through groups of enemies and take down as many as you can! A formidable foe awaits you at the end of your journey!

### Controls

-   Movement
    -   Up: Up Arrow
    -   Down: Down Arrow
    -   Left: Left Arrow
    -   Right: Right Arrow
-   Shoot: Z

## Make It Your Own

There will be a boss enemy at the end of the stage. Player can pick up energy items dropped by enemies to increase the power and quantity of their shots, power is lost upon taking damage. Enemy spawns are in a pre-determined stage, which is the same every time. There 5 types of normal enemies. There is an easy mode that the player can select at the start; in easy mode the player starts at 2 power, cannot lose power, and has infinite lives.

Enemies:
Type 1: Moves in an arc before moving downward. Only shoots down.
Type 2: Zig-zags across the screen while shooting directed shots at the player.
Type 3: Rarely moves and has lots of health. Alternates between three modes of firing: rows of 5 shots, a fan of dense bullets, three fast bullets.
Type 4: Moves straight down and shoots horizontally.
Type 5: Dashes about the screen. It stops to charge a homing projectile.
Boss: Has 3 phases of bullet hell madness. Uses some of the attacks of the other enemy types amidst its own attacks.

## Sources

Asset Credit:
Player spritesheet - https://opengameart.org/content/enemy-sprite
Enemies - https://opengameart.org/content/shmup-enemies
        - https://opengameart.org/content/sci-fi-shoot-em-up-object-images
Projectiles - https://opengameart.org/content/bullet-collection-1-m484
        - https://opengameart.org/content/m484-lightning-weapon
        - https://opengameart.org/content/explosion-animated
        - https://opengameart.org/content/explosion-set-1-m484-games
        - https://opengameart.org/content/sci-fi-shoot-em-up-object-images
Background - https://opengameart.org/content/galaxy-for-shmup

## Known Issues

Fourth enemy type occasionally throws an error. I can't find a cause, but it doesn't seem to affect gameplay.

### Requirements not completed

No visual when the player hits an enemy with a bullet (aside from the bullet disappearing or enemy death). I didn't realize this was a requirement until too late.

