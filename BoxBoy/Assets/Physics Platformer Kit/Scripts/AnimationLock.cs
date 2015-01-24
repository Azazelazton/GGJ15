using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Animator ) )]
public class AnimationLock : MonoBehaviour {
	Animator animator;

	int activatedButtons;

	[SerializeField]
	BoxButton[] buttons = new BoxButton[0];

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();

		activatedButtons = 0;

		for (int i = 0; i < buttons.Length; i++) {
			//buttons[i].EVENT1 += buttonPressed;
			//buttons[i].EVENT2 += buttonReleased;
		}
	}

	void OnDestroy() {
		for (int i = 0; i < buttons.Length; i++) {
			if (buttons[i] != null) {
				//buttons[i].EVENT1 -= buttonPressed;
				//buttons[i].EVENT2 -= buttonReleased;
			}
		}
	}

	void buttonPressed () {
		activatedButtons++;

		if (activatedButtons == buttons.Length)
			activate();
	}
	
	void buttonReleased () {
		activatedButtons--;

		deactivate ();
	}
	
	void activate () {
		animator.SetTrigger ("activate");
	}

	void deactivate () {
		animator.SetTrigger ("deactivate");
	}
}
