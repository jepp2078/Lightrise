using UnityEngine;
using System.Collections;

public class NetworkCharacterMine :Photon.MonoBehaviour {
    Vector3 realPosition = Vector3.zero;
    Quaternion realRotation = Quaternion.identity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine)
        {

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, Time.deltaTime * 5);
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
