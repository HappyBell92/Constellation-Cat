using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishHover : MonoBehaviour
{
	[SerializeField] float hoverHeight =1f; 
	[SerializeField] float hoverBoost =1f; 
	[SerializeField] float hoverGravity =1f; 
	[SerializeField] LayerMask hoverGroundMask;
	Transform planetCore;
	Transform playerCat;
	Rigidbody rb;
	Vector3 compassTarget;
	void Start()
	{
		planetCore = PlanetCore.Instance.transform; // Get reference to planet core transform from PlanetCore static instance
		rb = GetComponent<Rigidbody>();
		playerCat = GameObject.FindWithTag("Player").transform; //to-do: avoid using Find
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		DoHoverHeight();
		DoCompassFacing();
	}

	void DoHoverHeight()
	{
		if(Physics.Raycast(transform.position, transform.forward, hoverHeight, hoverGroundMask))
		{
			rb.AddForce(transform.forward * -hoverBoost);
			Debug.DrawRay(transform.position, transform.forward * hoverHeight, Color.blue, Time.fixedDeltaTime);
		}
		else
		{
			rb.AddForce(transform.forward * hoverGravity);
			Debug.DrawRay(transform.position, transform.forward * hoverHeight, Color.green, Time.fixedDeltaTime);
		}
	}

	void DoCompassFacing()
	{
		compassTarget = transform.position + (playerCat.position - transform.position); // Vector from self to player
		compassTarget = Vector3.ProjectOnPlane(compassTarget - transform.position, -transform.forward) + transform.position; //Flatten vector against own up direction
		//transform.LookAt(compassTarget, transform.parent.transform.up); //Convert vector to transform rotation quaternion
		//rb.AddForce(compassTarget.normalized*2f); //doesn't work yet
		Debug.DrawLine(transform.position, compassTarget, Color.magenta, Time.fixedDeltaTime);
	}
}
