<h2>How to Access & Review this Project</h2>

<br>

<h3>Access Where? </h3> 
Latest commit from Moving-to-Unity branch.<br>

<br>
<br>

<h3>Review How?</h3> 

Just hit Play. <br>
<br>

*If MainTestScene doesn't open by default, select scene from [SCENES] folder.*  <br>
<br>

**Adjust to liking:**
- Click on any tile in scene to have access to it's Tile (script) in Inspector, from where you can physically adjust the block state.
- In the SceneManager you can adjust the spore's max population, initial spawnsize and spawn intervals from the Spawner script.
- Clicking on the preafabs in scene you can fiddle with the Nutritional Value variable in Unit script & Movement speed variable in npc controller script.

<br>

<ins>***Note***</ins> -  If you click on prefabs in scene you should be able to see quite a lot of adjustable variables, other than Nutritional Value in Unit script & Movement speed in npc controller script I don't recommend touching them. For instance the connections list is there to see if the list is actually filling up. The chosen action should display the prefabs current action and the location variable should show it's ingame location.

<br>

<ins>***Due to a Critical Bug***</ins>   - Creatures show odd behaviour in scene, since collision is never detected even when it should be (OnCollisionEnter never called), units will make one decision, navigate to target, sit on them and won't make another decision.

 <br>
<h3>Sub-Folders of [ASSETS]: </h3><br>
<br>

>[PREFABS] <br>
⦁	**CreatureA** <br>
⦁	**CreatureB** <br>
⦁	**Spore** <br>
⦁	*Tile can be ignored* <br>
<br>

>[SCENES] <br>
⦁	**MainTestScene** - Currently setup with a 8x8 tile map, one CreatureA prefab, one CreatureB prefab. <br>
<br>

>[SCRIPTS] <br>
⦁	**GridPathfinding** - BFS Algorithim. <br>
⦁	**Spawner** - Controls Spore population. <br>
⦁	**UnitManager** - Handles relationship between prefabs. <br>
 
>>[UNUSED SCRIPTS] <br>
*Scripts aren't used anywhere so they too can be ignored.* <br>
 
>>[GRID SYSTEM] <br>
⦁	**GridManager** - Sets up the grid.  <br>
⦁	**GridNode** - Holds stats for virtual objects needed for the pathfinder.  <br>
⦁	**Labeller** - Adds colour and cords for the Tiles. <br>
⦁	**Tile** - Holds position & blocked status for GridNodes. <br>
>>

>>[NPC] <br>
⦁	**Action** - Outline for action children. <br>
⦁	**Consideration** - Outline for consideration children. <br>
⦁	**NpcController** - Holds the functions needed for executing actions. <br>
⦁	**Unit** - Holds prefab's stats like health, location, type & nutrition value. <br>
⦁	**UtilityAI** - Scores & chooses actions. <br>
>>

>>>[Considerations] <br>
⦁	**ConEatClosest** - Values taken into account for scoring EatClosest. <br>
⦁	**ConEatDesired** - Values taken into account for scoring EatDesired. <br>
⦁	**ConIdle** - Values taken into account for scoring Idle. <br>
>>> 
>>>>[ScriptableObjects] <br>
Here the response curves for Considerations are created. <br>
>>>
>>>[Actions] <br>
⦁	**EatClosest** - How to execute EatClosest. <br>
⦁	**EatDesired** - How to execute EatDesired. <br>
⦁	**Idle** - How to execute Idle. <br>
>>>>[Scriptable Objects] <br>
Here Considerations are linked to Actions.



