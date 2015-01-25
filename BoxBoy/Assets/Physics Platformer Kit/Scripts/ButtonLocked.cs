using UnityEngine;
using System.Collections;

[RequireComponent( typeof( PhotonView ) )]
public abstract class ButtonLocked : MonoBehaviour {	
	[SerializeField]
	BoxButton[] buttons = new BoxButton[0];
	
	// Use this for initialization
	protected virtual void Awake () {	

		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].Pushed += buttonPressed;
			buttons[i].Released += buttonReleased;
		}

		if (buttons.Length == 0)
			Invoke("activate", 0.5f);
	}
	
	void OnDestroy() {
		for (int i = 0; i < buttons.Length; i++) {
			if (buttons[i] != null) {
				buttons[i].Pushed -= buttonPressed;
				buttons[i].Released -= buttonReleased;
			}
		}
	}

	bool isActivated = false;
	void buttonPressed () {
		Debug.Log ("BUTTONpRESS");
		bool allButtonsAreActivated = true;
		foreach(BoxButton button in buttons)
		if (!button.activated) {
			allButtonsAreActivated = false;
			break;
		}
		
		if (allButtonsAreActivated && !isActivated) {
			isActivated = true;

			activate ();
		}
	}
	
	void buttonReleased () {
		Debug.Log ("BUTTONrELEASE");
		if (isActivated) {
			isActivated = false;

			deactivate ();

		}
	}
	
	protected abstract void activate ();
	protected abstract void deactivate ();
}
