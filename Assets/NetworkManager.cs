using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Connect();
	}

    void Connect()
    {
        Debug.Log("Connect");
        PhotonNetwork.ConnectUsingSettings("0.1.0");

    }

    void OnDisconnectedFromPhoton()
    {
        Debug.Log("Re-Connect");
        PhotonNetwork.ConnectUsingSettings("0.1.0");
    }

    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomRoomJoinFailed");
        PhotonNetwork.CreateRoom("world1");
    }

    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        spawnPlayer();
    }

    void spawnPlayer()
    {
        Debug.Log("spawning player");
        GameObject myPlayerGO = PhotonNetwork.Instantiate("_Player", new Vector3(-4f, 31f, -7.5f), Quaternion.identity, 0);
        myPlayerGO.GetComponentInChildren<RPG_Controller>().enabled = true;
        myPlayerGO.GetComponentInChildren<RPG_Camera>().enabled = true;
        myPlayerGO.GetComponentInChildren<Camera>().enabled = true;
        myPlayerGO.GetComponentInChildren<CharacterController>().enabled = true;
        myPlayerGO.GetComponentInChildren<PlayerObject>().enabled = true;
        myPlayerGO.GetComponentInChildren<Player>().enabled = true;
        myPlayerGO.GetComponentInChildren<Canvas>().enabled = true;
        myPlayerGO.GetComponentInChildren<GuiFunction>().enabled = true;
        myPlayerGO.GetComponentInChildren<AudioListener>().enabled = true;
        Transform playerChar = myPlayerGO.transform.FindChild("PlayerChar");
    }
}
