## SelfDestructButton
###### Version: v0.33
This is a mod for Rain World v1.5.

### Description
When grabbed by an enemy, the player can kill slugcat by holding the grab button. Lizards and Dropwigs try to grab the player whenever possible. Disables the emergency throw mechanic after being grabbed.
Can be combined with "JollyCoop" such that the other players can try to help out. Can be combined with the "Auto Destruction Mod" to create an explosion.

### Dependencies
- ConfigMachine.dll

### Installation
1. (ModLoader) `BepInEx` and `BOI` can be downloaded from [RainDB](https://www.raindb.net/) under `Tools`.  
  **NOTE:** Rain World's BepInEx is a modified version. Don't download it from GitHub.
2. (Dependency) The mod `ConfigMachine` can be downloaded from [RainDB](https://www.raindb.net/) under `Tools`.
3. Download the file  `SelfDestructButton.dll` from [Releases](https://github.com/SchuhBaum/SelfDestructButton/releases) and place it in the folder `[Steam]\SteamApps\common\Rain World\Mods`.
4. Start `[Steam]\SteamApps\common\Rain World\BOI\BlepOutIn.exe`.
5. Click `Select path` and enter the game's path `[Steam]\SteamApps\common\Rain World`. Enable the mod `SelfDestructButton.dll` and its dependencies. Then launch the game as normal. 

### Contact
If you have feedback, you can message me on Discord `@SchuhBaum#7246` or write an email to SchuhBaum71@gmail.com.

### License
There are two licenses available - MIT and Unlicense. You can choose which one you want to use.  

### Changelog
v0.2:
- Added an option interface to configure what mechanics get changed (needs ConfigMachine).

v0.3:
- Added support for AutoUpdate.
- Added an option (disabled by default) for a creature to insta-kill the player instead of trying to grab him when the creature is bleeding out (health < 0).
- Changed the emergency throw option to be disabled by default.

v0.33:
- Restructured code.
- Added mod description in the mod config.
- Added some debug logs.
- Added an option for red lizards (enabled by default). When enabled, red lizards are always deadly even when the Lizard Bite option is enabled.
- Changed the insta-kill option to be triggered when a creature's health is below 50%.