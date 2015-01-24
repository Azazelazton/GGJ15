using UnityEngine;
using System.Collections;

//ATTACH TO MAIN CAMERA, shows your health and coins
public class GUIManager : MonoBehaviour 
{	
	public GUISkin guiSkin;					//assign the skin for GUI display
	[HideInInspector]
	public int coinsCollected;

	private int coinsInLevel;
	private Health health;


	void Awake() {
		NetworkManager.SpawnedMyPlayer += setPlayer;
	}
	
	void OnDestroy(  ) {
		NetworkManager.SpawnedMyPlayer -= setPlayer;
	}
	
	void setPlayer( GameObject player ) {
		this.health = player.transform.GetComponent<Health>();
	}
	//setup, get how many coins are in this level
	void Start()
	{
		coinsInLevel = GameObject.FindGameObjectsWithTag("Coin").Length;		
	}
	
	//show current health and how many coins you've collected
	void OnGUI()
	{
		GUI.skin = guiSkin;
		GUILayout.Space(5f);
		
		if(health)
			GUILayout.Label ("Health: " + health.currentHealth);
		if(coinsInLevel > 0)
			GUILayout.Label ("Cubes: " + coinsCollected + " / " + coinsInLevel);
	}
}