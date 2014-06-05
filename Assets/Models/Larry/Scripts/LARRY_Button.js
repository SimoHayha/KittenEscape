
function OnGUI () { //Make a background box GUI.Box (Rect (500,120,100,90), "");

//Make the first button
if (GUI.Button (Rect(25,25,115,50),  "Larry Hi"))
{
Application.LoadLevel ("Hi");
}
else if (GUI.Button (Rect(25,100,115,50),  "Larry Walk"))
{
//application.loadlevel()
Application.LoadLevel ("Walk");
}
else if (GUI.Button (Rect(25,175,115,50),  "Funny Dance"))
{
//application.loadlevel()
Application.LoadLevel ("Funny");
}
else if (GUI.Button (Rect(25,250,115,50),  "Jump Dance"))
{
//application.loadlevel()
Application.LoadLevel ("Jump");
}
else if (GUI.Button (Rect(25,325,115,50),  "Pathsala Dance"))
{
//application.loadlevel()
Application.LoadLevel ("Pathsala");
}
else if (GUI.Button (Rect(25,400,115,50),  "Run"))
{
//application.loadlevel()
Application.LoadLevel ("Run");
}
else if (GUI.Button (Rect(25,475,115,50),  "Larry Slips"))
{
//application.loadlevel()
Application.LoadLevel ("Slip");
}

//else if (GUI.Button (Rect(850,675,100,40),  "Main Menu"))
//{
//application.loadlevel()
//Application.LoadLevel ("Start_2nd");
//}
}