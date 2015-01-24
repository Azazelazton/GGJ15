using UnityEngine;
using System.Collections;

public class StepAnim : MonoBehaviour {
    GameObject step, pillar;
    [SerializeField]
    StepAnim[] stairway; // Only have to set this at the first step;
<<<<<<< HEAD
    public float height = 0.2f;
    public float yVel = 1;
=======
    public float height = 0.3f;
    public float yVel = 0.5f;
>>>>>>> origin/master

    public int stepNr = 0;
    bool started = false;

    void Start()
    {
        GetItems();
        if(stairway.Length>0)
        getStepNr();

        //if (stepNr == 0)
    }
    void Update()
    {
        if (!started)
        {
            started = true;
            StartCoroutine(animate());
        }
    }
    void GetItems()
    {
        foreach (Transform obj in transform.GetComponentInChildren<Transform>())
        {
            if (obj.transform.name == "Step")
                step = obj.gameObject;
            if (obj.transform.name == "Pillar")
                pillar = obj.gameObject;
        }


    }

   
    public IEnumerator animate()
    {
<<<<<<< HEAD
        float desiredHeight = step.transform.position.y + height * stepNr+height;
        while (step.transform.position.y < desiredHeight)
=======
        float desiredHeight = (step.transform.position.y + height);
        desiredHeight *= 0.02f;
        desiredHeight += stepNr*height;
        desiredHeight += height;
        float yHeight = 0;
        while (yHeight < desiredHeight)
>>>>>>> origin/master
        {
            Vector3[] pillarCorners = pillar.GetComponent<MeshFilter>().mesh.vertices;
            float highestY = float.MinValue;
            foreach (Vector3 vertex in pillarCorners)
            {
                if (vertex.y > highestY) highestY = vertex.y;
            }
            for (int k = 0; k < pillarCorners.Length; k++)
            {
                Vector3 vertex = pillarCorners[k];
                if (vertex.y == highestY)
                {
                    pillarCorners[k] += new Vector3(0, yVel * Time.deltaTime, 0);
                }
            }
            pillar.GetComponent<MeshFilter>().mesh.vertices = pillarCorners;
            pillar.GetComponent<MeshFilter>().mesh.RecalculateBounds();

<<<<<<< HEAD
            step.transform.position += new Vector3(0, yVel*Time.deltaTime, 0);
=======
            Vector3[] stepCorners = step.GetComponent<MeshFilter>().mesh.vertices;
            for (int h = 0; h < stepCorners.Length; h++)
            {
                stepCorners[h] += new Vector3(0, yVel * Time.deltaTime, 0);
            }

            step.GetComponent<MeshFilter>().mesh.vertices = stepCorners;
            step.GetComponent<MeshFilter>().mesh.RecalculateBounds();

            yHeight += yVel * Time.deltaTime;
>>>>>>> origin/master

            yield return new WaitForEndOfFrame();
        }
        //if (stairway.Length >= (stepNr + 1))
        //    StartCoroutine(stairway[stepNr++].animate());
        
    }

    public void getStepNr()
    {
        if(stairway[0] == this)
            foreach (StepAnim other in stairway)
            {
                if (other != this)
                {
                    other.stairway = this.stairway;
                    other.getStepNr();
                }
            }

        for (int i = 0; i < stairway.Length; i++)
        {
            if (stairway[i] == this)
                this.stepNr = i;
        }
    }
}
