# Monster Chase Behaviour Controller

[中文](https://github.com/your-repo/monster-chase-behaviour-controller/blob/main/README.zh-cn.md)

This project provides a Unity script that creates a monster chase behaviour. When the player moves around the scene, the monster automatically chases and moves towards the player while avoiding other objects when it approaches them. This script should be mounted on the monster type objects.

## How to Install

In your Unity project, clone this repository at any location using Git.

## How to Use

Attach the `MonsterChaseBehaviourController` to the monster object you want to control the chase behaviour. In the Unity editor, you can set the following parameters:

- Movement speed (`speed`): This is the movement speed of the monster, the default value is 1.0.
- Avoidance strength (`avoidStrength`): This is the avoidance strength of the monster, the bigger it is, the farther it will avoid when approaching other objects, the default value is 0.5.
- Avoidance distance (`avoidDistance`): This is the distance at which the monster will start trying to avoid other objects, the default value is 1.0.

## Parameter Settings

In the Unity editor, you can set the following parameters:

- Movement speed (`speed`): This controls the movement speed of the monster.
- Avoidance strength (`avoidStrength`): This controls the avoidance strength of the monster. The larger this is, the farther the monster will avoid when it approaches other objects.
- Avoidance distance (`avoidDistance`): This controls the distance at which the monster starts trying to avoid other objects.

## Operating Principle

The Monster Chase Behaviour Controller updates the position of the monster at each frame, causing it to move towards the player and avoid other objects. When other objects approach the monster, the monster will calculate a force to avoid the object, this force is inversely proportional to the distance from the object to the monster. The final movement direction of the monster is the sum of the direction towards the player and all avoidance forces.

## Others

The Monster Chase Behaviour Controller uses `GameObject.FindWithTag("Player")` to find the player object. Therefore, you need to make sure that the tag of your player object is "Player".

## Copyright Information

This project uses the MIT open source license. Everyone is welcome to improve and use the project.
