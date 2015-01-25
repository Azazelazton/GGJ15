using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Animator ) )]
public class AnimationLock : ButtonLocked {
	Animator animator;
	protected override void Awake () {
		animator = GetComponent<Animator> ();
		
		base.Awake ();
	}

	protected override void activate () {
		animator.SetTrigger ("activate");
	}

	protected override void deactivate () {
		animator.SetTrigger ("deactivate");
	}
}
