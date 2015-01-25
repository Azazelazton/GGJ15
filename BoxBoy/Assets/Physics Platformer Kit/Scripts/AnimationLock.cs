using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Animator ) )]
public class AnimationLock : MonoBehaviour {
	Animator animator;

	[SerializeField]
	BoxButton[] buttons = new BoxButton[0];

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();

		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].Pushed += buttonPressed;
			buttons[i].Released += buttonReleased;
		}
	}

	void OnDestroy() {
		for (int i = 0; i < buttons.Length; i++) {
			if (buttons[i] != null) {
				buttons[i].Pushed -= buttonPressed;
				buttons[i].Released -= buttonReleased;
			}
		}
	}

	void buttonPressed () {
		bool allButtonsAreActivated = true;
		foreach(BoxButton button in buttons)
			if (!button.activated) {
				allButtonsAreActivated = false;
				break;
			}

		if (allButtonsAreActivated)
			activate();
	}
	
	void buttonReleased () {		
		deactivate ();
	}
	
	void activate () {
		animator.SetTrigger ("activate");
	}

	void deactivate () {
		animator.SetTrigger ("deactivate");
	}
}
