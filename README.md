# Witchcraft VR  
**VR Application Development Report & Reflection**


## 1. Introduction
Witchcraft VR is a virtual reality application developed using Unity (v 2022.3.62f1) and the XR Interaction Toolkit with the aim is to have a gameplay with interaction design, locomotion inside a VR environment. Player can move freely through a virtual space, collect ingredients and craft potions.

This report will document the technical achievements of the project, highlights challenges encountered during development, and learning outcomes reflection.
<table border="0">
  <tr align="center">
    <td><img src="https://github.com/user-attachments/assets/28c0be3a-c603-4d14-9a37-ae30053d3c2c" width="400"></td>
  </tr>
</table>


## 2. Project Overview and Design Goals
The primary design goal of Witchcraft VR was to create a small but complete VR experience with a clear gameplay loop. This consists of:
1. Exploring the environment  
2. Collecting ingredients  
3. Crafting potions  
4. Unlocking progression through player actions  

Different mechanics were designed to make use of the VR hand interaction, spatial positioning, and visual feedback. (Will be discussed later)

The project also focuses on extensibility in which features like potion crafting and interaction logic were designed as additional content without affecting the existing structure.


## 3. Technical Implementation, Challenges and Solution
### 3.1 Wand Interaction System
It uses the XR Grab Interactable component, allowing the wand to be picked up and released using XR Ray Interactors.

The wand system supports the following interactions:
- Drawing lines in 3D space using LineRenderer  
- Shooting projectile-based spells from the wand tip  
- Detecting which hand is currently holding the wand  
- Detecting which hand for additional mechanics like shooting and power ignition.

A significant technical challenge was that it doesnot detect the correct interacting hand and causes error while trying to perform the mechanic. This is because XR Interaction Toolkit does not use traditional transform parenting and the focus was on getting the incorrect transform. The wand was not a child of the hand transform.

The issue was resolved by detecting the active XR interactor at runtime instead and using XR attach points to correctly align magic effects with the hand.

**Screenshots:**
<table border="0">
  <tr>
    <td><img src="https://github.com/user-attachments/assets/fcb11410-6b6f-4772-a53f-4f379dbd00b1" width="700"></td>
    <td><img src="https://github.com/user-attachments/assets/fac82f50-6089-4f08-be03-e7efc143893a" width="500"></td>
    <td><img src="https://github.com/user-attachments/assets/509736ce-650d-4bff-978c-769a6c7f28a3" width="500"></td>
  </tr>
</table>


### 3.2 Potion Crafting System
The potion crafting system uses ScriptableObjects and a central manager class. Each potion is defined by a `PotionData` ScriptableObject, which contains:
- Potion type  
- Required ingredients  
- A short description  

The `PotionsManager` handles potion selection and updates the user interface using TextMeshPro which will be shown in the magic book. Ingredient lists and descriptions are presented clearly to the player to support decision-making during crafting.

Ingredients collected in the world can be dropped into a cauldron. When this happens, the system checks whether the added ingredients match a valid recipe. Recipes are matched in order, allowing flexibility in how ingredients are added.

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


### 3.3 Broom Flying Mechanic
Player movement also includes flying mechanic using an attached broom, which allows for free traversal in the environment without relying only on teleportation. 

The broom is implemented as a grabbable object. When held in socket close to the body,forward movement is applied based on right controller orientation and player input trigger. This maintains immersion and gives the player direct control over movement.  

The flying system integrates with existing XR Locomotion rather than replacing it entirely, allowing players to switch between grounded interaction and flight.

There are several technical challenges. The movement system was initially resulted in unstable and overly sensitive controls. Small wrist movements caused sudden changes in direction, making the broom difficult to stee, turn and uncomfortable to use.

This led to key considerations during implementation included:
- Smoothing/gradual acceleration to reduce motion discomfort  
- Avoiding sudden changes in velocity  
- Maintaining stable height control

**Screenshots:**
<table border="0">
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/a60a528b-2f26-4a08-b3c6-e82d1161588f" width="500"></td>
    <td align="center"><img src="https://github.com/user-attachments/assets/06cbce3c-1fe0-4385-94fe-467d8407a80f" width="500"></td>
  </tr>
</table>


### 3.4 Unique Potion Completion Event
To provide a clear goal for the crafting system, a completion system was implemented. The system tracks which potion types have been crafted and ensures that only unique potion types are counted.

Each time a potion prefab is instantiated, it is registered with a completion checker. Once all four potion types have been crafted:
- A light beam GameObject is activated  
- A video cutscene begins playing  
- Further completion checks are disabled  

**Screenshots:**
<table border="0">
  <tr>
    <td align="center"><img src="https://github.com/user-attachments/assets/fd2386dc-a0da-43e0-b46f-de45a1f19eec" width="400"></td>
  </tr>
</table>

## 4. Reflection
This project significantly deepened my understanding of VR interaction design and XR system architecture. I gained practical experience working with XR Interaction Toolkit, Scriptable Objects, and event-driven gameplay systems.

The development process highlighted the importance of designing for player feedback and motivation. Initially, I noticed the game experience lacked a clear goal, but later when I introduce the potion completion event, this gave the game a stronger sense of purpose.

From a technical perspective, I  believe I improved my ability to debug much more complex interaction issues and structure code in a modular, maintainable way.

One key area for improvement is interaction polish. More time should be spent on tuning physics values, hand input smoothing, and visual feedback because this would improve the overall immersion and reduce player fatigue or confusion.

Moreover, adding structured challenges, consequences would strengthen player motivation. For example, a few guided objectives, some enemy encounters, or time based challenges would give the potion system more impact within the game world.

## 5. Future Work Consideration
- Expanding potion effects to influence gameplay more directly  
- Introducing environmental challenges/more objectives  
- Adding progression systems linked to potion usage  
- Improving player experience with more audio and visual feedback

These improvements would build upon the existing systems.
