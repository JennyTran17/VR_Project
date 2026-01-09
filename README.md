# Witchcraft VR  
**VR Application Development Report & Reflection**

---

## 1. Introduction
Witchcraft VR is a virtual reality prototype developed using Unity (v 2022.3.62f1) and the XR Interaction Toolkit. The project was created as part of a VR Application Development module, with the aim of exploring interaction design, locomotion, and object-based gameplay within a VR environment.

The application allows the player to move freely through a virtual space, collecting ingredients and crafting potions via cauldron-based system. The experience prioritises interaction and systems over scripted events, encouraging exploration and experimentation. Over the course of development, several core mechanics were implemented, refined, and evaluated, including wand interaction, potion crafting, broom-based flying, and an end-game completion event.

This report documents the technical achievements of the project, highlights challenges encountered during development, and reflects on the learning outcomes achieved.

---

## 2. Project Overview and Design Goals
The primary design goal of Witchcraft VR was to create a small but complete VR experience with a clear gameplay loop. This loop consists of:
1. Exploring the environment  
2. Collecting ingredients  
3. Crafting potions  
4. Unlocking progression through player actions  

Progression is driven by player interaction with systems rather than linear scripting. Each mechanic was designed to feel physically grounded in VR, making use of hand-based interaction, spatial positioning, and visual feedback.

Another key objective was extensibility. Systems such as potion crafting and interaction logic were designed so that additional content could be added without significant refactoring.

---

## 3. Technical Implementation
### 3.1 Wand Interaction System
The wand acts as the primary interaction tool within the application. It uses the XR Grab Interactable component, allowing it to be picked up and released using XR Direct Interactors.

The wand system supports the following interactions:
- Drawing lines in 3D space using LineRenderer  
- Shooting projectile-based spells from the wand tip  
- Detecting which hand is currently holding the wand  
- Disabling wand-based interactions when the wand is not held (bugs prevention)  

A major challenge was ensuring that only the hand holding the wand could trigger drawing and shooting actions. This required avoiding assumptions about transform parenting, as XR Interaction Toolkit manages object attachment dynamically. Instead, interaction state and XR events were used to determine behaviour.

When the wand is not held, the player can still perform magic actions, but limited. This maintains player control while clearly separating wand-based and hand-based interactions.

**Screenshots:**
<table border="0">
  <tr>
    <td><img src="https://github.com/user-attachments/assets/fac82f50-6089-4f08-be03-e7efc143893a" width="400"></td>
    <td><img src="https://github.com/user-attachments/assets/509736ce-650d-4bff-978c-769a6c7f28a3" width="500"></td>
  </tr>
</table>

---

### 3.2 Potion Crafting System
The potion crafting system is built using ScriptableObjects and a central manager class. Each potion is defined by a `PotionData` ScriptableObject, which contains:
- Potion type  
- Required ingredients  
- A short description  

The `PotionsManager` handles potion selection and updates the user interface using TextMeshPro. Ingredient lists and descriptions are presented clearly to the player to support decision-making during crafting.

Ingredients collected in the world can be dropped into a cauldron. When this occurs, the system checks whether the added ingredients match a valid recipe. Recipes are matched in an order-independent manner, allowing flexibility in how ingredients are added.

A limitation of the current system is that it requires an exact match in both ingredients and quantity. So while this keeps the logic simple and reliable, it reduces flexibility and is something to be considered for future development.

**Screenshots:**
<table border="0">
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/d699fa80-6a68-4641-ab74-9e0aed874ab3" width="80"></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/4bbcdb26-6993-41da-9726-ff124270ffb7" width="400"></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/0de17be9-7205-47b2-82c4-b7614f6f53dc" width="80"></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/a50fd248-c12a-4bbc-8593-b1d9eb3c41d8" width="400"></td>
  </tr>
</table>

<table border="0">
  <tr>
    <th colspan="4" align="center"><p2>4 Scriptable Objects</p2></th>
  </tr>
  
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/56127bae-999b-477f-82d1-58b95db90fe1" width="80"></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/7638ce21-308c-4dfd-b787-3f605abfa26e" width="80"></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/926fc665-5536-4335-8e4b-55e405fa0882" width="80"></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/de4fef05-9e8e-4c4f-9660-bf1b03acc054" width="80"></td>
  </tr>
</table>

---

### 3.3 Broom Flying Mechanic
Player movement is primarily handled through a broom-based flying mechanic, which allows for free traversal in the environment without relying only on teleportation. 

The broom is implemented as a grabbable object. When held, forward movement is applied based on controller orientation and player input. This maintains immersion and gives the player direct control over movement.

Key considerations during implementation included:
- Smoothing/gradual acceleration to reduce motion discomfort  
- Avoiding sudden changes in velocity  
- Maintaining stable height control  

The flying system integrates with existing XR Locomotion rather than replacing it entirely, allowing players to switch between grounded interaction and flight. While effective, the system could benefit from additional constraints, such as speed limits or environmental boundaries.

**Screenshots:**

---

### 3.4 Unique Potion Completion Event
To provide a clear goal for the crafting system, a completion mechanic was implemented. The system tracks which potion types have been crafted using a `HashSet`, ensuring that only unique potion types are counted.

Each time a potion prefab is instantiated, it is registered with a completion checker. Once all four potion types have been crafted:

- A light beam GameObject is activated  
- A video cutscene begins playing  
- Further completion checks are disabled  

This approach avoids relying solely on object counts or tags, which could lead to incorrect completion if duplicates are created. Using a `HashSet` ensures the logic remains robust and scalable.

This event serves as a clear conclusion to the experience and provides feedback that the player has completed the main objectives.

**Screenshots:**
<table border="0">
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/fd2386dc-a0da-43e0-b46f-de45a1f19eec" width="400"></td>
  </tr>
</table>

---

## 4. Technical Challenges and Solutions
One of the main challenges encountered was working with XR Interaction Toolkit’s dynamic attachment system. Objects are not always parented in a traditional transform hierarchy, making hand detection and attachment logic more complex.

This issue was addressed by:
- Relying on XR interaction events instead of transform checks  
- Avoiding hard-coded parenting assumptions  
- Separating interaction logic based on interaction state  

Another challenge involved keeping systems modular and loosely coupled. This was resolved by ensuring systems communicate through clearly defined methods rather than direct dependencies.

---

## 5. Reflection
This project highlighted the importance of designing interactions specifically for VR rather than adapting non-VR patterns. Mechanics that appear simple in traditional games often require additional consideration when physical interaction and player comfort are involved.

A personal key learning outcome was gaining a deeper understanding of XR Interaction Toolkit and its approach to interaction management. Early assumptions about object parenting led to issues that required redesigning parts of the interaction logic.

Overall, the project demonstrates a solid foundation in VR interaction design and system-based gameplay. While limited in scope, it delivers a complete and coherent experience with clear goals and meaningful interaction.

---

## 6. Future Work
Potential future considerations include:
- Expanding potion effects to influence gameplay more directly  
- Introducing environmental challenges/more objectives  
- Adding progression systems linked to potion usage  
- Improving player experience with more audio and visual feedback

These improvements would build upon the existing systems while maintaining the project’s modular structure.

---
