#pragma strict
var chTexture : Texture2D;
var positionch : Rect;
static var ch = true;
  
function Update() // If we don't do this, we can't update the size.
{
    positionch = Rect((Screen.width - chTexture.width) / 2, (Screen.height - 
        chTexture.height) /2, chTexture.width, chTexture.height);
}
//We need to draw the texture on the gui 
function OnGUI()
{
    if(ch == true)
    {
        GUI.DrawTexture(positionch, chTexture);
    }
}