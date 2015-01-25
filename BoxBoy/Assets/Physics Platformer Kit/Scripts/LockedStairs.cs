using UnityEngine;
using System.Collections;

public class LockedStairs : ButtonLocked {
	StepAnim anim;

	protected override void Awake () {		
		anim = GetComponent<StepAnim> ();
		
		base.Awake ();
	}
	
	protected override void activate () {
		StartCoroutine(anim.animate ());
	}
	
	protected override void deactivate () {
		StartCoroutine(anim.deanimate ());
	}
}
