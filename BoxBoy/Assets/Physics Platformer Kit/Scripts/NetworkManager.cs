using UnityEngine;
using System.Collections;


public class NetworkManager : MonoBehaviour {
	public delegate void SpawnMyPlayerHandler( GameObject myPlayer );
	public static event SpawnMyPlayerHandler SpawnedMyPlayer;
	
	private string version = "vk";
	
	public string playerPrefabName = "Player";
	public Transform[] spawnPoints;

	public void Start () {
		Connect ();
	}

	private void Connect() {
		Debug.Log ("Connect");
		PhotonNetwork.ConnectUsingSettings (version);
	}
	
	public void OnJoinedLobby() {
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}
	
	public void OnPhotonRandomJoinFailed() {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
	}
	
	public void OnJoinedRoom() {
		Debug.Log ("OnJoinedRoom");
		SpawnMyPlayer ();
	}
	
	public void SpawnMyPlayer() {
		Debug.Log ("SpawnMyPlayer");

		Transform spawnPoint = spawnPoints[PhotonNetwork.playerList.Length - 1];
		GameObject myPlayerObject = PhotonNetwork.Instantiate (playerPrefabName, spawnPoint.position, spawnPoint.rotation, 0);
		myPlayerObject.layer = spawnPoint.gameObject.layer;

		if (SpawnedMyPlayer != null) {
			SpawnedMyPlayer( myPlayerObject );
		}
	}
}








