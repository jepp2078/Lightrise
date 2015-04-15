
function Update () {
var model1 = GameObject.Find("armor");
var model2 = GameObject.Find("vest");
var model3 = GameObject.Find("Sword");
var model4 = GameObject.Find("shirt");
var model5 = GameObject.Find("Armlet");
var model6 = GameObject.Find("Shield");


if (Input.GetKeyDown ("d")) {

model1.GetComponent.<Renderer>().enabled=true;

}


if (Input.GetKeyDown ("f")) {
model1.GetComponent.<Renderer>().enabled=false;
}

if (Input.GetKeyDown ("q")) {

model2.GetComponent.<Renderer>().enabled=true;

}


if (Input.GetKeyDown ("w")) {
model2.GetComponent.<Renderer>().enabled=false;
}
if (Input.GetKeyDown ("a")) {

model3.GetComponent.<Renderer>().enabled=true;

}


if (Input.GetKeyDown ("s")) {
model3.GetComponent.<Renderer>().enabled=false;
}
if (Input.GetKeyDown ("e")) {

model4.GetComponent.<Renderer>().enabled=true;

}


if (Input.GetKeyDown ("r")) {
model4.GetComponent.<Renderer>().enabled=false;
}
if (Input.GetKeyDown ("g")) {

model5.GetComponent.<Renderer>().enabled=true;

}


if (Input.GetKeyDown ("h")) {
model5.GetComponent.<Renderer>().enabled=false;
}
if (Input.GetKeyDown ("z")) {

model6.GetComponent.<Renderer>().enabled=true;

}


if (Input.GetKeyDown ("x")) {
model6.GetComponent.<Renderer>().enabled=false;
}

}
