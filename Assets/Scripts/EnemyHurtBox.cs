using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
	[SerializeField] GameObject prefabParentObject;
	[SerializeField] AudioClip JellyDeathSound;
	public ParticleSystem deathCloud;
	public float knockBackStrength = 10.0f;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
			Vector3 awayFromEnemy = other.gameObject.transform.position - transform.position;
			Instantiate(deathCloud, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
			AudioSource.PlayClipAtPoint(JellyDeathSound, transform.position);

			playerRigidbody.velocity = playerRigidbody.transform.up * knockBackStrength;


			Destroy(prefabParentObject);
		}
	}
}
