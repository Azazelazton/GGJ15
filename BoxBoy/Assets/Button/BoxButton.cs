using UnityEngine;
using System.Collections;

public class BoxButton : MonoBehaviour {
	public delegate void EventHandler();
	public event EventHandler Pushed;
	public event EventHandler Released;

    Vector3 startPos;
    Vector3 myPos;
    public bool activated = false;
    bool ableToMove = true;
    [SerializeField]
    GameObject view;
    void Start()
    {
        startPos = transform.parent.transform.position;
        myPos = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        string myLayerName = LayerMask.LayerToName(gameObject.layer);
        if (layerName.Substring(0, 3) == myLayerName.Substring(0, 3) && ableToMove)
        {
            StopCoroutine(MoveUp());
            StartCoroutine(MoveDown());
        }
    }
    void OnCollisionStay(Collision collision)
    {
        OnCollisionEnter(collision);
    }
    void OnCollisionExit(Collision collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        string myLayerName = LayerMask.LayerToName(gameObject.layer);
        if (layerName.Substring(0, 3) == myLayerName.Substring(0, 3) && ableToMove)
        {
            StartCoroutine(MoveUp());
        }
    }


    IEnumerator MoveDown()
	{
		if (!activated){
			this.activated = true;
			if (Pushed != null) 
				Pushed ();
		}

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
		if (activated) {
			activated = false;
			if (Released != null) 
				Released ();
		}

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
