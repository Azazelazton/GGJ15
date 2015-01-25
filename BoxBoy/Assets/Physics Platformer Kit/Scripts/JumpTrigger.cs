using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour {
	[SerializeField]
	float jumpHeight = 2.5F;

	// Use this for initialization
	void OnTriggerStay (Collider collider) {
		PlayerMove controller = collider.GetComponent<PlayerMove> ();
		if (controller != null) {
			if (Vector3.Dot(-transform.forward, controller.rigidbody.velocity) > 0.4f) {		//TODO check for moving toward object
				controller.jump (jumpHeight);
			}
		}
	}
}
