read and review

mark down guide https://www.markdownguide.org/basic-syntax/#overview

latest commit from Move-to-Unity branch

⦁	block tiles
click on any tile in scene to have access to its tile (script) from where you can tick blocked to block the tile or not.

⦁	Spwaning spores
In the SceneManager you can adjust the spore max population, initial spawnsize and spawn intervals from the spawner script

clicking on the preafabs in scene you should be able to see quite a lot of adjustable variables, other than Nutritional Value in Unit script & Movement speed in npc controller script I don't recommend touching them.

For instance the connections list is there to make sure the list is actually filling up, the chosen action, should in theory display the preafbs current action and the location shows it's ingame location.

All scripts in use

name - location - use 

[ASSETS]

>[PREFABS]
⦁	CreatureA
⦁	CreatureB
⦁	Spre
⦁	Tile

>[SCENES]
⦁	MainTestScene

>[SCRIPTS]
⦁	GridPathfinding
⦁	SPawner
⦁	UnitManager

>>[UNUSED SCRIPTS]

>>[GRID SYSTEM]
⦁	GridManager
⦁	GridNode
⦁	Labeller
⦁	Tile

>>[NPC]
⦁	Action
⦁	Consideration
⦁	NpcController
⦁	Unit
⦁	UtilityAI

>>>[Considerations]
⦁	ConEatClosest
⦁	ConEatDesired
⦁	ConIdle
>>>>[ScriptableObjects]

>>>[Actions]
⦁	EatClosest
⦁	EatDesired
⦁	Idle
>>>>[Scriptable Objects]

Scene will be setup with a 8x8 tile map, one A prefab, one B prefab

===================
creatures show odd behaviour in scene
because collision is never being detected, units will make one decision, navigate to target, sit on them and won't make another decision.
