using UnityEngine;
using System.Collections;

[RequireComponent( typeof( PhotonView ) )]
public abstract class ButtonLocked : MonoBehaviour {	
	PhotonView photonView;

	bool isMine {
		get {
			return LayerMask.LayerToName(NetworkManager.instance.gameObject.layer).Substring(0, 3) != LayerMask.LayerToName(gameObject.layer).Substring(0, 3);
		}
	}

	[SerializeField]
	BoxButton[] buttons = new BoxButton[0];
	
	// Use this for initialization
	protected virtual void Awake () {	
		photonView = GetComponent<PhotonView> ();

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
	
	void buttonPressed () {
		bool allButtonsAreActivated = true;
		foreach(BoxButton button in buttons)
		if (!button.activated) {
			allButtonsAreActivated = false;
			break;
		}
		
		if (allButtonsAreActivated && isMine)
			photonView.RPC ("activateRPC", PhotonTargets.AllBuffered);
	}
	
	void buttonReleased () {
		if (isMine)
			photonView.RPC ("deactivateRPC", PhotonTargets.AllBuffered);
	}

	[RPC]
	protected void activateRPC() {
		activate ();
	}
	
	[RPC]
	protected void deactivateRPC() {
		deactivate ();
	}
	
	protected abstract void activate ();
	protected abstract void deactivate ();
}
