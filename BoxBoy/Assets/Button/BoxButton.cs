using UnityEngine;
using System.Collections;

public class BoxButton : MonoBehaviour {

    Vector3 startPos;
    public bool activated = false;
    bool ableToMove = true;
    void Start()
    {
        startPos = transform.parent.transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && ableToMove)
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
        if (collision.gameObject.tag == "Player" && ableToMove)
        {
            StartCoroutine(MoveUp());
        }
    }
    IEnumerator MoveDown()
    {
        ableToMove = false;
        while (transform.parent.position.y > startPos.y - 0.1f)
        {
            transform.parent.position += new Vector3(0, -0.02f, 0);
            yield return new WaitForEndOfFrame();
        }
        transform.parent.position = new Vector3(transform.parent.position.x, startPos.y - 0.1f, transform.parent.position.z);
        activated = true;
        ableToMove = true;
    }
    IEnumerator MoveUp()
    {
        ableToMove = false;
        while (transform.parent.position.y < startPos.y)
        {
            transform.parent.position += new Vector3(0, 0.02f, 0);
            yield return new WaitForEndOfFrame();
        }
        transform.parent.position = new Vector3(transform.parent.position.x, startPos.y, transform.parent.position.z);
        activated = false;
        ableToMove = true;
    }
}
