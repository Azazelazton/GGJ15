using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour {
	[SerializeField]
	float jumpHeight = 5;

	// Use this for initialization
	void OnTriggerEnter (Collider collider) {
		PlayerMove controller = collider.GetComponent<PlayerMove> ();
		if (controller != null) {
			if (true) {		//TODO check for moving toward object
				controller.jump (jumpHeight);
			}
		}
	}
}
