
function OnGUI () { //Make a background box GUI.Box (Rect (500,120,100,90), "");

//Make the first button
if (GUI.Button (Rect(25,25,115,50),  "Larry Hi"))
{
animation.Play("hi");
}
}