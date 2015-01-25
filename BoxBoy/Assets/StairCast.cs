using UnityEngine;
using System.Collections;

public class StairCast : MonoBehaviour {
    [SerializeField]
    PlayerMove controller;
    [SerializeField]
    float jumpHeight;

	void Update () {
        //Debug.DrawRay(transform.position, transform.forward);
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit, 1))
        //{
        //    if (hit.transform.tag == "Stair")
        //        Jump();
        //}
        
	}

    void OnTriggerStay(Collider col)
    {
		if (col.transform.tag == "Stair" && Mathf.Sign (controller.rigidbody.velocity.y) < 0.01f)
        	Jump();
    }

    void Jump(){
				controller.jump (jumpHeight);
    }
}
