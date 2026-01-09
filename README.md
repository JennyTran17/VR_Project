# Witchcraft VR  
## Reflective Technical Report on a Virtual Reality Application Prototype

---

## 1. Introduction

Witchcraft VR is a small-scale virtual reality experience developed using Unity and the XR Interaction Toolkit. The purpose of this project was to explore immersive interaction design through VR magic mechanics, potion crafting, and spatial gameplay. The application focuses on embodied interaction, encouraging players to physically engage with the world through hand movement, object manipulation, and spell casting.

This report documents the technical development of the project and reflects on the challenges encountered during implementation. The following sections outline the core gameplay concept, key technical systems, design decisions, and a reflection on learning outcomes.

---

## 2. Project Overview and Gameplay Concept

The core gameplay of Witchcraft VR revolves around exploration, material collection, and potion crafting. Players fly through the environment to gather ingredients and return to a central crafting area to create potions. There are four unique potion types, each representing a different magical domain.

The player interacts primarily through a VR wand, which supports drawing magic in the air, casting projectile spells, and enabling free-hand magic depending on whether the wand is held. This design encourages natural VR interaction and reinforces the fantasy of becoming a witch through physical gestures.

As players successfully craft all four unique potion types, the game recognizes their progression and unlocks a final ritual event. A magical light beam appears and a cutscene plays, symbolizing the player becoming a fully awakened witch.

---

## 3. Technical Implementation and Systems

This section outlines the major technical systems developed during the project and explains how they contribute to gameplay.

---

### 3.1 VR Wand Interaction System

The wand system is the primary interaction tool in the experience. It was implemented using XR Interaction Toolkit components, allowing it to work seamlessly with XR Direct Interactors and sockets.

Key features of the wand system include:
- Drawing magical lines in 3D space using a Line Renderer
- Shooting projectile-based spells from the wand tip
- Detecting which hand is currently holding the wand
- Enabling free-hand magic when the wand is not held
- Supporting dynamic XR attach points instead of fixed parenting

A significant technical challenge was handling the fact that XR Interaction Toolkit does not use traditional transform parenting. This meant that the wand was not a child of the hand transform. The issue was resolved by detecting the active XR interactor at runtime and using XR attach points to correctly align magic effects with the hand.

**Screenshot placeholder:**  
![Wand interaction and spell casting](Screenshots/wand_interaction.png)

---

### 3.2 Potion Crafting System

The potion system is built around data-driven design using Scriptable Objects. Each potion type is represented by a `PotionData` asset, making the system flexible and easy to extend.

Each potion includes:
- A potion type enum
- A list of required ingredients
- A descriptive text explaining the potion's purpose

A `PotionsManager` script handles potion selection and updates the UI using TextMeshPro. When the player selects a potion, the UI dynamically updates to show the potion name, description, and ingredient list.

This separation of data and logic improved code clarity and made it easier to modify or add new potion types without changing existing systems.

**Screenshot placeholder:**  
![Potion UI and ingredient list](Screenshots/potion_ui.png)

---

### 3.3 Unique Potion Completion Tracking

To give the game a clear goal, a completion system was introduced that tracks whether the player has crafted all four unique potion types. Instead of simply counting potion objects in the scene, a `HashSet` is used to store unique potion identifiers.

This ensures that duplicate potions do not count toward progression. Once all four potion types are registered:
- A light beam GameObject is activated
- A video cutscene is played
- A final completion event is triggered

This system provides narrative closure and reinforces the idea of mastery through experimentation and crafting.

**Screenshot placeholder:**  
![Final ritual and light beam event](Screenshots/final_event.png)

---

## 4. Technical Challenges and Problem Solving

Several technical difficulties were encountered throughout development.

One major challenge was handling dynamic XR attachment. Because XR objects are attached at runtime, relying on transform hierarchy caused issues when spawning magic effects relative to the hand. This was solved by querying XR interactors and their attach points directly.

Another issue involved tracking game state reliably. Early approaches allowed duplicate potions to incorrectly trigger the completion event. Switching to a HashSet-based system ensured accurate progression tracking and improved robustness.

These challenges encouraged iterative testing and reinforced the importance of understanding how VR frameworks manage object relationships.

---

## 5. Reflection on Learning and Development

This project significantly deepened my understanding of VR interaction design and XR system architecture. I gained practical experience working with XR Interaction Toolkit, Scriptable Objects, and event-driven gameplay systems.

The development process highlighted the importance of designing for player feedback and motivation. Initially, the experience lacked a clear goal, but introducing the potion completion event gave the game a stronger sense of purpose.

From a technical perspective, I improved my ability to debug complex interaction issues and structure code in a modular, maintainable way.

---

## 6. Limitations and Future Development

While the project meets its core objectives, there are several limitations:
- Potion effects are largely symbolic rather than mechanically impactful
- There are no enemy encounters or time-based challenges
- The narrative is minimal outside the final event

Potential future improvements include:
- Expanding potion effects to influence player stats or abilities
- Adding enemy encounters or environmental hazards
- Introducing quest-based progression
- Expanding the final ritual into a multi-stage event

---

## 7. Conclusion

Witchcraft VR demonstrates a successful exploration of immersive VR interaction through magic systems and crafting mechanics. The project showcases clear technical achievement, problem-solving skills, and reflective engagement with the learning outcomes of the module.

The experience provides a solid foundation for future expansion while effectively demonstrating the principles of VR design and interaction.

---

## Appendix

### Technologies Used
- Unity
- C#
- XR Interaction Toolkit
- Universal Render Pipeline
- TextMeshPro
- Scriptable Objects
- Unity Video Player