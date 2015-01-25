using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Animator ) )]
public class AnimationLock : ButtonLocked {
	Animator animator;
	protected override void Awake () {
		base.Awake ();

		animator = GetComponent<Animator> ();
	}

	protected override void activate () {
		animator.SetTrigger ("activate");
	}

	protected override void deactivate () {
		animator.SetTrigger ("deactivate");
	}
}
