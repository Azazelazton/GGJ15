using UnityEngine;
using System.Collections;

public class TriggerCube : MonoBehaviour {

    public enum TriggerType { touch, laser, move}
    public TriggerType myTriggerType;
    //Touch = if touched something happens
    //Laser = if laser hits, something happens
    //Move = if moved something happens

    public delegate void CubeTrigger();
    public event CubeTrigger CubeTriggerEvent;

    void Start()
    {
        if (myTriggerType == TriggerType.move || myTriggerType == TriggerType.laser)
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.rigidbody.freezeRotation = true;
            if (myTriggerType == TriggerType.move)
                StartCoroutine(checkForMovement());
        }
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (myTriggerType == TriggerType.touch)
        Action();
    }

    IEnumerator checkForMovement()
    {
        Vector3 originalPosition = gameObject.transform.position;
        bool moved = false;

        while (!moved)
        {
            float deltaX = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x - originalPosition.x, 2));
            float deltaY = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.y - originalPosition.y, 2));
            float deltaZ = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.z - originalPosition.z, 2));
            if (deltaX >= 1 || deltaY >= 1 || deltaZ >= 1)
                moved = true;
            yield return new WaitForEndOfFrame();
        }
        Action();
    }

    void Action()
    {
        if (CubeTriggerEvent != null)
            CubeTriggerEvent();
    }

}
