using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Rigidbody ) ),
 RequireComponent( typeof( PhotonView ) )]
public class SyncRigidbody : Photon.MonoBehaviour {
	bool isMine = false;

	void Start () {
		NetworkManager.SpawnedMyPlayer += requestOwnerShip;
	}

	void OnDestroy () {
		NetworkManager.SpawnedMyPlayer -= requestOwnerShip;
	}

	void requestOwnerShip (GameObject myPlayer) {
		if (LayerMask.LayerToName(gameObject.layer).Substring(0, 3) == LayerMask.LayerToName(NetworkManager.instance.gameObject.layer).Substring(0, 3)) {
			isMine = true;
		} else 
			isMine = false;
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (isMine)
		{
			photonView.RPC("sync", PhotonTargets.Others, new object[]{transform.position, transform.rotation, rigidbody.velocity});
		}
	}

	[RPC]
	void sync(Vector3 position, Quaternion rotation, Vector3 velocity) {
		transform.position = position;
		transform.rotation = rotation;
		rigidbody.velocity = velocity;
	}
}
