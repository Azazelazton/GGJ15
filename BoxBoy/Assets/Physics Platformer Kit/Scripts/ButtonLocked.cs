﻿using UnityEngine;
using System.Collections;

public abstract class ButtonLocked : MonoBehaviour {	
	[SerializeField]
	BoxButton[] buttons = new BoxButton[0];
	
	// Use this for initialization
	protected virtual void Awake () {		
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
	
	protected abstract void activate ();
	protected abstract void deactivate ();
}