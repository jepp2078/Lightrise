using UnityEngine;
using System.Collections;

public class Marble : Photon.MonoBehaviour {

    public float movementSpeed = 25;

	void FixedUpdate () {
        if (!photonView.isMine)
            return;

        Vector3 movement = (Input.GetAxis("Horizontal") * -Vector3.left * movementSpeed) + (Input.GetAxis("Vertical") * Vector3.forward *movementSpeed);
        GetComponent<Rigidbody>().AddForce(movement);

	}
}
