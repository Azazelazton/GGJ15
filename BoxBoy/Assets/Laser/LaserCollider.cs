using UnityEngine;
using System.Collections;

public class LaserCollider : MonoBehaviour {
    [SerializeField]
    GameObject laser;
    GameObject tempLaser;
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
            if (hit.transform.tag == "Mirror")
            {
               
                if (hit.point != null)
                {
                    Vector3 dot = Vector3.Dot(hit.normal, transform.up) * hit.normal;
                    Vector3 reflected = transform.up - 2 * dot;
                    if(tempLaser == null || tempLaser.transform.forward != reflected)
                    {
                        if (tempLaser != null)
                            Destroy(tempLaser);

                        tempLaser = GameObject.Instantiate(laser, hit.point, Quaternion.identity) as GameObject;
                        tempLaser.transform.forward = reflected;
                    }
                } 
                else if (tempLaser != null)
                    Destroy(tempLaser);

            }
        }
        else
        {
            Destroy(tempLaser);
        }
    }
}
