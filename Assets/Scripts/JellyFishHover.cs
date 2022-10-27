using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishHover : MonoBehaviour
{
	[SerializeField] float hoverHeight =1f; 
	[SerializeField] float hoverBoost =1f; 
	[SerializeField] float chaseSpeed =2f; 
	[SerializeField] float hoverGravity =1f; 
	[SerializeField] float detectionTimeDelay =0.33f; //Attempts to detect three times per second
	[SerializeField] float detectionRadius =5f; //Meters to detect
	[SerializeField] float aggroTimeMax =8f; //Time enemy stays angry after player exits detection radius
	[SerializeField] LayerMask hoverGroundMask;
	[SerializeField] LayerMask playerLayer;
	Collider[] playerCatCollider;
	bool playerFound = false;
	float detectionTimer = 1f; //Always counts down 1 to 0
	float aggroTimer;
	Transform planetCore;
	Transform playerCatTransform;
	Rigidbody rb;
	Vector3 chaseVector;
	void Start()
	{
		playerCatCollider = new Collider[1];
		planetCore = PlanetCore.Instance.transform; // Get reference to planet core transform from PlanetCore static instance
		rb = GetComponent<Rigidbody>();
		aggroTimer = aggroTimeMax;
	}
	void FixedUpdate()
	{
		DoHoverHeight();
		TryDetectPlayer();
		if(playerFound)
		{
			CheckAggro();
			DoChaseDirection();
		}
	}

	void DoHoverHeight()
	{
		if(Physics.Raycast(transform.position, transform.forward, hoverHeight, hoverGroundMask))
		{
			rb.AddForce(transform.forward * -hoverBoost); //If ground is detected, add force up (negative forward vector)
			Debug.DrawRay(transform.position, transform.forward * hoverHeight, Color.blue, Time.fixedDeltaTime); //visualize with a line
		}
		else
		{
			rb.AddForce(transform.forward * hoverGravity); //If ground is not detected, add force down (positive forward vector)
			Debug.DrawRay(transform.position, transform.forward * hoverHeight, Color.yellow, Time.fixedDeltaTime);
		}
	}

	void DoChaseDirection()
	{
		chaseVector = (playerCatTransform.position - transform.position); // Vector from self to player
		chaseVector = Vector3.ProjectOnPlane(chaseVector, -transform.forward); //Flatten vector against own up direction at world 0
		chaseVector = chaseVector.normalized*chaseSpeed; //Set vector length to 1f and then multiply by chase speed multiplier
		rb.AddForce(chaseVector*chaseSpeed); // move towards player
		Debug.DrawLine(transform.position, chaseVector+transform.position, Color.magenta, Time.fixedDeltaTime); //Visualize direction and magnitude of force
	}

	void TryDetectPlayer()
	{
		if(detectionTimer > 0f) //Timer spreads out attempts to detect over time
		{
			detectionTimer -= Time.fixedDeltaTime; // Timer ticks down 1f per second
		}
		else
		{
			detectionTimer = detectionTimeDelay;
			if (Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, playerCatCollider, playerLayer) > 0) //Radius check to detect player on layer Player
			{
				if(!playerFound) //Assigns player transform reference if not assigned yet
				{
					playerCatTransform = playerCatCollider[0].transform;
					playerFound = true;
					aggroTimer = aggroTimeMax;
				}
			}
		}
	}
	void CheckAggro()
	{
		if(aggroTimer > 0f)
		{
			aggroTimer -= Time.fixedDeltaTime; // Timer ticks down 1f per second
		}
		else
		{
			playerFound = false;
			Debug.Log(gameObject+" lost player, returning to idle");
		}
	}
	void OnDrawGizmosSelected()
    {
        // Red sphere to show detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
