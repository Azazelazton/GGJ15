using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PhotonView))]
public class BoxButton : MonoBehaviour {
	public delegate void EventHandler();
	public event EventHandler Pushed;
	public event EventHandler Released;

	[SerializeField]
	Material redPressedColor
		, bluePressedColor;
	Material origonalColor;

	PhotonView photonView;

	bool isMine {
		get {
			return LayerMask.LayerToName(NetworkManager.instance.gameObject.layer).Substring(0, 3) != LayerMask.LayerToName(gameObject.layer).Substring(0, 3);
		}
	}

    Vector3 startPos;
    Vector3 myPos;
    public bool activated = false;
    bool ableToMove = true;
    [SerializeField]
    GameObject view;

	List<GameObject> objectsAbove;

	void Awake () {
		NetworkManager.SpawnedMyPlayer += onPlayerSpawn;
	}

    void Start()
    {
		photonView = GetComponent<PhotonView> ();

        startPos = transform.parent.transform.position;
        myPos = transform.position;

		objectsAbove = new List<GameObject> ();
    }

	void OnDestroy () {
		NetworkManager.SpawnedMyPlayer -= onPlayerSpawn;
	}

	void onPlayerSpawn (GameObject player) {
		Renderer[] renderers = transform.parent.parent.GetComponentsInChildren<Renderer>();
		origonalColor = renderers[0].material;
	}

	void setColor (Material color) {
		Renderer[] renderers = transform.parent.parent.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < renderers.Length; i++)
			renderers[i].material = color;
	}

	[RPC]
	void open () {
		this.activated = true;
		GetComponent<AudioController>().PlayClip(0);

		setColor (LayerMask.LayerToName(gameObject.layer).Substring(0, 3) == "Red"? redPressedColor: bluePressedColor);

		//StopCoroutine(MoveUp());
		//StartCoroutine(MoveDown());
		
		if (Pushed != null) 
			Pushed ();
	}
	[RPC]
	void close () {
		activated = false;
		GetComponent<AudioController>().PlayClip(0);
		
		setColor (origonalColor);
		
		//StartCoroutine(MoveUp());
		
		if (Released != null) 
			Released ();
	}

    void OnCollisionEnter(Collision collision)
    {
		if (isMine) {
	        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
	        string myLayerName = LayerMask.LayerToName(gameObject.layer);
	        if (layerName.Substring(0, 3) == myLayerName.Substring(0, 3) && ableToMove && collision.transform.tag != "Button")
	        {
				
				if (!objectsAbove.Contains(collision.gameObject)) {
					objectsAbove.Add(collision.gameObject);
					
					if (!activated && objectsAbove.Count == 1){
						open();
						photonView.RPC("open", PhotonTargets.OthersBuffered);
					}
				}
	        }
		}
    }

    void OnCollisionExit(Collision collision)
    {
		if (isMine) {
	        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
	        string myLayerName = LayerMask.LayerToName(gameObject.layer);
	        if (layerName.Substring(0, 3) == myLayerName.Substring(0, 3) && ableToMove)
			{
				if (objectsAbove.Contains(collision.gameObject)) {
					objectsAbove.Remove(collision.gameObject);
					
					if (objectsAbove.Count == 0 && activated) {
						close ();
						photonView.RPC("close", PhotonTargets.OthersBuffered);
					}
				}
	        }
		}
    }


	IEnumerator MoveDown()
	{
        ableToMove = false;
        while (transform.parent.position.y > startPos.y - 0.064f)
        {
            transform.parent.position += new Vector3(0, -0.02f, 0);
            transform.position = myPos;
            yield return new WaitForEndOfFrame();
        }
		transform.parent.position = new Vector3(transform.parent.position.x, startPos.y - 0.1f, transform.parent.position.z);
        ableToMove = true;
    }

	IEnumerator MoveUp()
	{
        ableToMove = false;
        while (transform.parent.position.y < startPos.y)
        {
            transform.parent.position += new Vector3(0, 0.02f, 0);
            transform.position = myPos;
            yield return new WaitForEndOfFrame();
        }
        transform.parent.position = new Vector3(transform.parent.position.x, startPos.y, transform.parent.position.z);
        ableToMove = true;
    }
}
