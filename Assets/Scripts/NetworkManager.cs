using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	const string VERSION = "v0.0.1";
	const string ROOMNAME = "DefaultRoom";
	const string PLAYERPREFABNAME = "Car";

	public Transform spawnPoint;

	void Start () {
		PhotonNetwork.ConnectUsingSettings (VERSION);
	}

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby () {
		RoomOptions roomOptions = new RoomOptions () { isVisible = false, maxPlayers = 2 };
		PhotonNetwork.JoinOrCreateRoom (ROOMNAME, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom() {
		PhotonNetwork.Instantiate (PLAYERPREFABNAME, spawnPoint.position, spawnPoint.rotation, 0);
	}
}
