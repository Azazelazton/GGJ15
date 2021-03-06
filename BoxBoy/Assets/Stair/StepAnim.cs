﻿using UnityEngine;
using System.Collections;

public class StepAnim : MonoBehaviour {
	GameObject step, pillar;
	[SerializeField]
	StepAnim[] stairway; // Only have to set this at the first step;
	public float height = 0.3f;
	public float yVel = 0.5f;
	
	public int stepNr = 0;
	bool started = false;

	void Start()
	{
		GetItems();
		if(stairway.Length>0)
			getStepNr();

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

    void playSound()
    {

        GetComponent<AudioController>().PlayClip(1);
        GetComponent<AudioController>().PlayClip(0);
    }

	public IEnumerator animate()
    {
       
            if (stepNr == 0)
                foreach (StepAnim ani in stairway)
                    if (ani != this)
                        StartCoroutine(ani.animate());


            GetComponent<AudioController>().PlayClip(0);


            float desiredHeight = (step.transform.position.y + height);
            desiredHeight *= 0.02f;
            desiredHeight += stepNr * height;
            desiredHeight += height;
            float yHeight = 0;
            while (yHeight < desiredHeight)
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
                pillar.GetComponent<BoxCollider>().center += new Vector3(0, yVel * Time.deltaTime, 0);

                Vector3[] stepCorners = step.GetComponent<MeshFilter>().mesh.vertices;
                for (int h = 0; h < stepCorners.Length; h++)
                {
                    stepCorners[h] += new Vector3(0, yVel * Time.deltaTime, 0);
                }

                step.GetComponent<MeshFilter>().mesh.vertices = stepCorners;
                step.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                step.GetComponent<BoxCollider>().center += new Vector3(0, yVel * Time.deltaTime, 0);


                yHeight += yVel * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            GetComponent<AudioController>().PlayClip(1);

        
	}
	public IEnumerator deanimate()
    {
       
            if (stepNr == 0)
                foreach (StepAnim ani in stairway)
                    if (ani != this)
                        StartCoroutine(ani.deanimate());


            GetComponent<AudioController>().PlayClip(0);

            float desiredHeight = (step.transform.position.y + height);
            desiredHeight *= 0.02f;
            desiredHeight += stepNr * height;
            desiredHeight += height;
            float yHeight = 0;
            while (yHeight < desiredHeight)
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
                        pillarCorners[k] += new Vector3(0, -yVel * Time.deltaTime, 0);
                    }
                }
                pillar.GetComponent<MeshFilter>().mesh.vertices = pillarCorners;
                pillar.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                pillar.GetComponent<BoxCollider>().center += new Vector3(0, -yVel * Time.deltaTime, 0);

                Vector3[] stepCorners = step.GetComponent<MeshFilter>().mesh.vertices;
                for (int h = 0; h < stepCorners.Length; h++)
                {
                    stepCorners[h] += new Vector3(0, -yVel * Time.deltaTime, 0);
                }

                step.GetComponent<MeshFilter>().mesh.vertices = stepCorners;
                step.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                step.GetComponent<BoxCollider>().center += new Vector3(0, -yVel * Time.deltaTime, 0);

                yHeight += yVel * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            GetComponent<AudioController>().PlayClip(1);
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