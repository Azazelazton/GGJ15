using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {
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
		int nextLevel = Application.loadedLevel + 1;
		if (nextLevel > Application.levelCount)
			nextLevel = 0;

		Application.LoadLevel (nextLevel);
	}
}
