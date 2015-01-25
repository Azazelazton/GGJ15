using UnityEngine;
using System.Collections;

public class FirstPersonCam : MonoBehaviour {

    float movementSpeed=10;
    float oldMouseX, oldMouseY, mouseX, mouseY, deltaX, deltaY;

    void Update()
    {
        oldMouseX = mouseX;
        oldMouseY = mouseY;
        mouseY = Input.mousePosition.y;
        mouseX = Input.mousePosition.x;
        deltaX = mouseX - oldMouseX;
        deltaY = mouseY - oldMouseY;
        deltaX = Input.GetAxis("Mouse X") * 5;
        deltaX = Input.GetAxis("Mouse Y");
        Debug.Log("X:" + deltaX + " , Y:" + deltaY);
        transform.Rotate(new Vector3(deltaY * movementSpeed * Time.deltaTime,deltaX * movementSpeed * Time.deltaTime,0));
    }
}
