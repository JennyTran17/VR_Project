# Witchcraft VR - Project Summary
Witchcraft VR is a small magical VR experience built with Unity and XR Interaction Toolkit. The core gameplay focuses on flying through the world, collecting ingredients, and crafting four unique potions. Each potion uses a custom data system that displays its ingredients, name, and description to the player.

The player interacts with a VR wand capable of drawing magic, casting spells, and performing free-hand magic depending on which hand is holding it. The wand system supports natural VR interaction, dynamic attach points, and responsive spell casting.

As players gather materials and successfully craft all four potion types, the game recognizes each unique potion created and unlocks a final event. A magical light beam appears and a cutscene plays, symbolizing the player becoming a fully awakened witch.

The project combines exploration, light crafting mechanics, and interactive VR magic to create a short but engaging magical experience.

### Features:

### **1. VR Wand Interaction**
The wand system supports:
- Drawing magical lines in 3D space  
- Shooting wand projectiles  
- Dynamic detection of which hand is holding the wand  
- Free-hand magic that spawns when the wand is not held  
- Compatibility with XR Direct Interactors and dynamic attach points

### **2. Potion Crafting System**
Players can craft four main potion types:

- **Healing**  
- **Mana**  
- **Strength**  
- **Love**

Each potion includes:
- A `PotionData` ScriptableObject containing ingredients and description  
- A `PotionsManager` handling selection and UI updates  
- Ingredient text display using TextMeshPro  
- Clear extensibility for adding future potion types

### **3. Unique Potion Completion Event**
The game tracks all crafted potion types using a **HashSet**, ensuring that duplicates do not count toward completion.

Once all four potion types are crafted:
- A **light beam** GameObject activates  
- A **video cutscene** begins playing  
- A final event triggers, signaling the player's transformation into a master witch

### Future Development:
- Extend spells and wand attacks
- Introduce a quest progression system
- Add enemy encounters or challenges
- Additional end-game cinematics
