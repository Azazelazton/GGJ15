using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {
	int numberOfPlayersInCollider = 0;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("RedPlayer") || other.gameObject.layer == LayerMask.NameToLayer("BluePlayer")) {
			numberOfPlayersInCollider++;

			if (numberOfPlayersInCollider >= 2) 
				loadNextLevel ();
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("RedPlayer") || other.gameObject.layer == LayerMask.NameToLayer ("BluePlayer")) 
			numberOfPlayersInCollider--;
	}

	void loadNextLevel () {
		int nextLevel = Application.loadedLevel + 1;
		if (nextLevel > Application.levelCount)
			nextLevel = 0;

		Application.LoadLevel (nextLevel);
	}
}
