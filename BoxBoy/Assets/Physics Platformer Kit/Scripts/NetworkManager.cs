using UnityEngine;
using System.Collections;


public class NetworkManager : MonoBehaviour {
	public delegate void SpawnMyPlayerHandler( GameObject myPlayer );
	public static event SpawnMyPlayerHandler SpawnedMyPlayer;
	
	public string version = "v 11";
	
	public string playerPrefabName = "Player";
	public Transform[] spawnPoints;

	public static NetworkManager instance;

	void Start () {
		if (instance == null) {
			instance = this;
			init ();
		} else if (instance != this) {
			SpawnMyPlayer(instance.gameObject.layer);
			Destroy(gameObject);
		}
	}

	void init () {
		DontDestroyOnLoad (gameObject);
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
		gameObject.layer = myPlayerObject.layer;

		if (SpawnedMyPlayer != null) {
			SpawnedMyPlayer( myPlayerObject );
		}
	}
	
	public void SpawnMyPlayer(int layer) {
		Debug.Log ("SpawnMyPlayer");

		int i = 0;
		while (i < spawnPoints.Length && spawnPoints[i].gameObject.layer != layer) 
			i++;
		
		Transform spawnPoint = spawnPoints[i];
		GameObject myPlayerObject = PhotonNetwork.Instantiate (playerPrefabName, spawnPoint.position, spawnPoint.rotation, 0);
		myPlayerObject.layer = spawnPoint.gameObject.layer;
		gameObject.layer = myPlayerObject.layer;
		
		if (SpawnedMyPlayer != null) {
			SpawnedMyPlayer( myPlayerObject );
		}
	}
}








