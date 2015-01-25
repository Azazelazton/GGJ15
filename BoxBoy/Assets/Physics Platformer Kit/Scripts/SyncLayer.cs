using UnityEngine;
using System.Collections;

public class SyncLayer : MonoBehaviour {
	[SerializeField]
	Material blue = null
		, red = null;

	void Start() {
		PhotonView photonView = GetComponent<PhotonView>();

		setLayer (gameObject.layer);

		bool isMine = photonView.isSceneView ? PhotonNetwork.isMasterClient : photonView.isMine;
		if (isMine)
			photonView.RPC ("setLayer", PhotonTargets.OthersBuffered, gameObject.layer);
	}

	[RPC]
	void setLayer( int layer ) {
		gameObject.layer = layer;
		
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		for (int i = 0; i < renderers.Length; i++)
			renderers[i].material = (LayerMask.LayerToName (layer) == "Red" || LayerMask.LayerToName (layer) == "RedPlayer") ? red : blue;
	}
}
