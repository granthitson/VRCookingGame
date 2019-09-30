**********************************
***  NON CONVEX MESH COLLIDER  ***
**********************************


HOW TO USE 
**********

In Editor Mode:

	1. Select a game object that has a mesh
	2. Click 'Add Component' in the 'Inspector' window
	3. Type 'Non Convex Mesh Collider' and add the component
	4. Click 'Calculate' on the 'Non Convex Mesh Collider' component
	5. Optionally you can save your object as a prefab now.
	6. Done. Easy as that. :)

At Runtime:

	1. Use the following C# code to add the NonConvexMeshCollider component to a gameobject:	
	GameObject go = ...; //<- store your game object in local variable go
	var c = go.AddComponent<NonConvexMeshCollider>(); //<- add the NonCovexMeshCollider
	c.Calculate(); //<- Creates the colliders
	2. Done. Easy as that. :)


HOW DOES IT WORK?
*****************

The script uses unity compound colliders to mimic a cocanve mesh collider.
It computes the bounding box of your object and segments it into a configurable number of smaller boxes.
For each of these boxes it analyzes if it touches the mesh of your game object (or lies completely inside of the mesh).
The Non-Touching boxes are removed.
The surviving boxes are merged, to reduce the total number of colliders (improves performance).
The component creates a child game object called 'Colliders', for the game object you are using it for. So removing all that the component has modified is as 
simple as deleting this child object.
This process leads to an approximation only, of course. But the script is fast enough to even create a hundret segments per axis (means a million sub-boxes) 
in a fair amount of time. 
You most likeley won't notice any difference to a 'perfect' concave mesh collider, if you go that detailed. 
But be warned: more detail means less performance. A reasonabe deatail level seems to be somewhere between 10 and 30 boxes per axis (means 1000 to 27000 sub-boxes).

LIMITATIONS
***********

In really twisted multi level nested game object hierarhies, whith multiple transforms in the hierarchy having rotation and non uniform scaling, the 
collider may look / behave wrong.
But this is a limitation by the unity 'Compound Collider' approach, you probably won't easily find another solution that doesn't have the same limitation.
As the unity documentation states here:
Note, that primitive colliders will not work correctly with shear transforms - that means that if you use a combination of rotations and non-uniform scales 
in the tranform hierarchy so that the resulting shape would no longer match a primitive shape, the primitive collider will not be able to represent it correctly.
Accuracy is limited and so is performance. Try to find a good balance between the both by adjusting the 'Boxes Per Edge' parameter of the component. Higher values 
give more accuracy but less performance.