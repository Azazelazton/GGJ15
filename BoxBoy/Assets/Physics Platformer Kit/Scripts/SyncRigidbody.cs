using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Rigidbody ) )]
public class SyncRigidbody : Photon.MonoBehaviour {

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(rigidbody);
			stream.SendNext(transform.position);
		}
		else
		{
			rigidbody.velocity = (Vector3) stream.ReceiveNext();
			transform.position = (Vector3) stream.ReceiveNext();
		}
	}
}
