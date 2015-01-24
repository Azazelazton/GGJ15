using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour {
	[SerializeField]
	float jumpHeight = 5;

	// Use this for initialization
	void OnTriggerStay (Collider collider) {
		PlayerMove controller = collider.GetComponent<PlayerMove> ();
		if (controller != null) {
			Debug.Log(Vector3.Dot(-transform.forward, controller.rigidbody.velocity));
			if (Vector3.Dot(transform.forward, controller.rigidbody.velocity) > 1) {		//TODO check for moving toward object
				controller.jump (jumpHeight);
			}
		}
	}
}
