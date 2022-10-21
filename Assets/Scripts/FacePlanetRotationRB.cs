using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlanetRotationRB : MonoBehaviour
{
	Transform planetCore;
	Rigidbody rb;
	void Start()
	{
		planetCore = PlanetCore.Instance.transform; // Get reference to planet core transform from PlanetCore static instance
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		rb.MoveRotation(Quaternion.LookRotation(planetCore.position - transform.position, transform.up)); // Rotate towards planet core with respect to rigidbody interpolation
	}
}
