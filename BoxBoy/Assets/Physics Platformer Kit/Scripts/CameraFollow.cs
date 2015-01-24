﻿using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	public Transform target;									//object camera will focus on and follow
	public Vector3 targetOffset =  new Vector3(0f, 3.5f, 7);	//how far back should camera be from the lookTarget
	public bool lockRotation;									//should the camera be fixed at the offset (for example: following behind the player)
	public float followSpeed = 6;								//how fast the camera moves to its intended position
	public float inputRotationSpeed = 100;						//how fast the camera rotates around lookTarget when you press the camera adjust buttons
	public bool mouseFreelook;									//should the camera be rotated with the mouse? (only if camera is not fixed)
	public float rotateDamping = 100;							//how fast camera rotates to look at target
	public GameObject waterFilter;								//object to render in front of camera when it is underwater
	public string[] avoidClippingTags;							//tags for big objects in your game, which you want to camera to try and avoid clipping with
	
	private Transform followTarget;
	private bool camColliding;
	private LayerMask layerMask;
	
	//setup objects
	void Awake()
	{
		followTarget = new GameObject().transform;	//create empty gameObject as camera target, this will follow and rotate around the player
		followTarget.name = "Camera Target";
		if(waterFilter)
			waterFilter.renderer.enabled = false;
		
		//don't smooth rotate if were using mouselook
		if(mouseFreelook)
			rotateDamping = 0f;

		NetworkManager.SpawnedMyPlayer += setCameraTarget;
	}

	void OnDestroy(  ) {
		NetworkManager.SpawnedMyPlayer -= setCameraTarget;
	}

	void setCameraTarget( GameObject target ) {
		this.target = target.transform;

		gameObject.layer = target.layer;
		layerMask = Physics.AllLayers;
		layerMask ^= 1 << LayerMask.NameToLayer ("Red");
		layerMask ^= 1 << LayerMask.NameToLayer("Blue");
		layerMask ^= 1 << LayerMask.NameToLayer("NonPlayerWall");

		string otherLayerName = LayerMask.LayerToName (target.layer) == "RedPlayer" ? "Blue" : "Red";
		int otherLayer = LayerMask.NameToLayer (otherLayerName);
		camera.cullingMask |= 1 << otherLayer;
	}
	
	//run our camera functions each frame
	void Update()
	{
		if (!target)
			return;
		
		SmoothFollow ();
		if(rotateDamping > 0)
			SmoothLookAt();
		else
			transform.LookAt(target.position);
	}

	//toggle waterfilter, is camera clipping walls?
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Water" && waterFilter)
			waterFilter.renderer.enabled = true;
	}
	
	//toggle waterfilter, is camera clipping walls?
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Water" && waterFilter)
			waterFilter.renderer.enabled = false;
	}
	
	//rotate smoothly toward the target
	void SmoothLookAt()
	{
		Quaternion rotation = Quaternion.LookRotation (target.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, rotateDamping * Time.deltaTime);
	}
		
	//move camera smoothly toward its target
	void SmoothFollow()
	{
		//move the followTarget (empty gameobject created in awake) to correct position each frame
		followTarget.position = target.position;
		followTarget.Translate(targetOffset, Space.Self);
		if (lockRotation)
			followTarget.rotation = target.rotation;
		
		if(mouseFreelook)
		{
			//mouse look
			float axisX = Input.GetAxis ("Mouse X") * inputRotationSpeed * Time.deltaTime;
			followTarget.RotateAround (target.position,Vector3.up, axisX);
			float axisY = Input.GetAxis ("Mouse Y") * inputRotationSpeed * Time.deltaTime;
			followTarget.RotateAround (target.position, transform.right, -axisY);
		}
		else
		{
			//keyboard camera rotation look
			float axis = Input.GetAxis ("CamHorizontal") * inputRotationSpeed * Time.deltaTime;
			followTarget.RotateAround (target.position, Vector3.up, axis);
		}
		
		//where should the camera be next frame?
		Vector3 nextFramePosition = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
		Vector3 direction = nextFramePosition - target.position;
		//raycast to this position
		RaycastHit hit;
		if(Physics.Raycast (target.position, direction, out hit, direction.magnitude + 0.3f, layerMask))
		{
			transform.position = nextFramePosition;
			foreach(string tag in avoidClippingTags)
				if(hit.transform.tag == tag)
					transform.position = hit.point - direction.normalized * 0.3f;
		}
		else
		{
			//otherwise, move cam to intended position
			transform.position = nextFramePosition;
		}
	}
}