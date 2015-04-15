#pragma strict

var changeAnimationKey : KeyCode = KeyCode.Space;
var currentAnimation : int = 0;
var forceLoop : boolean = false;

var playOrder : String[];

function Update () {
	var animationID : int = 0;
	if(Input.GetKeyDown(changeAnimationKey)){
		if(forceLoop){
			GetComponent.<Animation>()[playOrder[currentAnimation]].wrapMode = WrapMode.Loop;
		}
		GetComponent.<Animation>().CrossFade(playOrder[currentAnimation]);
		currentAnimation ++;
		if(currentAnimation >= playOrder.Length){
			currentAnimation = 0;
		}
	}

}