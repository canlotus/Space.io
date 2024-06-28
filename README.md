[https://canlotus.itch.io/space-io]
Space.io 2D
Space.io 2D is a captivating 2D space-themed game developed in Unity. The player navigates through levels, defeats enemies, and collects experience points to unlock new weapons and abilities. The game starts from a main menu and restarts from the beginning if the player dies.

Table of Contents
Game Description
Features
Installation
How to Play
Controls
Development
Credits
Game Description
Space.io 2D is a thrilling arcade game set in space. The objective is to survive as long as possible while defeating waves of enemies. Players can level up and unlock new weapons with unique abilities to aid in their survival. The game features a main menu where players can start the game, and upon death, they return to this main menu.

Features
Level Progression: Players gain experience points (XP) and level up.
Weapon Unlocks: New weapons unlock as players level up.
Main Menu: Start the game from a main menu.
Game Over: Return to the main menu upon player death.
Installation
To play the game locally, follow these steps:

Clone the repository:

sh
Kodu kopyala
git clone [https://github.com/canlotus/Space.io]
Open the project in Unity (version 2020.3 or later recommended).

Ensure the necessary scenes are added to the build settings:

StartScene
GameScene
Build the game for WebGL or your preferred platform:

Go to File > Build Settings
Select your target platform (WebGL recommended)
Add the necessary scenes to the build
Click Build
How to Play
Launch the game and click the Start button on the main menu.
Use the controls to navigate your character and defeat enemies.
Gain experience points by defeating enemies to level up and unlock new weapons.
If your health reaches zero, the game will return to the main menu.
Controls
Movement: Arrow keys or WASD
Fire Weapon: Spacebar or mouse click
Select Weapon: 1, 2, 3 keys (for different weapon types)
Development
Key Scripts
PlayerHealth.cs: Manages player health, damage, and death.
ExperienceSystem.cs: Handles player experience points and leveling.
WeaponSelectionUI.cs: Manages the weapon selection user interface.
GameManager.cs: Controls the game state, including starting and restarting the game.
Development Setup
Unity Version: 2020.3 or later
Platform: WebGL recommended for easy sharing and deployment
Adding New Features
To add new features or modify existing ones:

Open the project in Unity.
Edit or add new scripts in the Scripts folder.
Test changes in the Unity Editor.
Rebuild the project for your target platform.
Credits
Development: Your Name
Art and Assets: List any third-party assets used or created by team members.
Sound and Music: List any sound or music credits if applicable.
