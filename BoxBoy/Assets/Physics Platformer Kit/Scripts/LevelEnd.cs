using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {
	int levelCount = 4;

	bool blueEntered = false;
	bool redEntered = false;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("RedPlayer"))
			redEntered = true;
		else if (other.gameObject.layer == LayerMask.NameToLayer ("BluePlayer")) 
			blueEntered = true;

		if (blueEntered && redEntered) 
			loadNextLevel ();
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("RedPlayer"))
			redEntered = false;
		else if (other.gameObject.layer == LayerMask.NameToLayer ("BluePlayer")) 
			blueEntered = false;
	}

	void loadNextLevel () {
		int currentLevelID = int.Parse (Application.loadedLevelName.Remove (0, 6));
		int nextLevel = currentLevelID + 1;
		if (currentLevelID >= levelCount)
			nextLevel = 0;

		Application.LoadLevel ("Level "+nextLevel);
	}
}
