# About

Bonfire is a 3D-Action RPG inspired by the Soulslike genre. Its purpose is to showcase my proficency to code general features and complex mechanics for Videogames.  

## Mechanics and Features

#### Enemy-AI
###### Bonfire features two different enemy behaviours. 
1.One will aggro when the player enters its cone of vision or its trigger radius. He will alarm other enemies in a small radius as well.  
2.The other one is non-aggressive but will aggro when another enemy in his alarm radius (which is quite big).  
He then alarms every enemy in a large radius.

#### Combat System
##### XP-System
Defeating enemies rewards the player with exp which can be used to increase stats while resting at a shrine.  
However unspent exp will be dropped when the player dies.
#### Mesh-Generation
The map is a proceduraly generated plane, shaped by a noise with customizable settings and a falloff on the edges.
#### Engine-Tool Development
#### Shaders
Terrain Shader: A Terrain Shader takes up to two textures and blends them depending on customizable parameters like normal-direction height.
Water Shader: The Water Shader takes a normal-texture
#### Visual Effects

