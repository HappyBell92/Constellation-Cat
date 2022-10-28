using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RigidbodyWalker : MonoBehaviour
{
	[SerializeField] LayerMask groundMask;
	[SerializeField] float groundCheckLength = 1f;
	[SerializeField] private int stamps;
	[SerializeField] AudioSource catAudio;
	[SerializeField] AudioClip hyppyClip, zappedClip;
	[SerializeField] AudioClip[] stepClips;
	[SerializeField] Animator catAnim;
	[SerializeField] PlayerProperties playerProperties;
	public ParticleSystem starJump;
	public ParticleSystem dust;
	public float speed = 5.0f;
	public bool canJump = true;
	public float jumpCooldown = 0.3f;
	public float jumpHeight = 2.0f;
	public int maxJumps = 2;
	public int jumps;
	public float damageJumpHeight = 2.0f;
	public float enemyDamageUp = 1.0f;
	public float enemyDamageBack = 2.0f;
	public Camera playerCamera;
	public float lookSpeed = 2.0f;
	public float lookXLimit = 60.0f;
	private bool isColliding = false;
	private float iFrames = 1f;
	private bool invincible = false;

	public float rotationSpeed;
	private float frozenTime = 1;
	
	public bool frozen = false;
	public bool grounded = false;
	Rigidbody r;
	Vector2 rotation = Vector2.zero;
	float maxVelocityChange = 10.0f;


	void Awake()
	{
		r = GetComponent<Rigidbody>();
		r.freezeRotation = true;
		r.useGravity = false;
		r.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		rotation.y = transform.eulerAngles.y;

		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
	}

	private void Start()
	{
		stamps = 0;
		maxJumps = 1;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && canJump && jumps > 0 != frozen)
		{
			Jump();
		}

		if (Input.GetKeyDown(KeyCode.Space) && canJump && jumps >0 != frozen != grounded)
		{
			//Debug.Log("Double Jump!");
		}

			if (playerProperties.doubleJumpActive == true)
		{
			maxJumps = 2;
		}

	}

	void FixedUpdate()
	{
		//RaycastHit hit;
		if(Physics.Raycast(transform.position, -transform.up, groundCheckLength, groundMask))
		{
			//Debug.Log("Hit Ground");
			grounded = true;
			catAnim.SetBool("Grounded", true);
			if (canJump)
			{
				jumps = maxJumps;
			}
			
		}

		else
		{
			grounded = false;
			catAnim.SetBool("Grounded", false);
		}

		Debug.DrawRay(transform.position, -transform.up * groundCheckLength, Color.yellow);

		if (frozen == false)
		{
			// Calculate how fast we should be moving
			Vector3 forwardDir = Vector3.Cross(transform.up, -playerCamera.transform.right).normalized;
			Vector3 rightDir = Vector3.Cross(transform.up, playerCamera.transform.forward).normalized;
			Vector3 targetVelocity = (forwardDir * Input.GetAxis("Vertical") + rightDir * Input.GetAxis("Horizontal")) * speed;

			Vector3 velocity = transform.InverseTransformDirection(r.velocity);
			velocity.y = 0;
			velocity = transform.TransformDirection(velocity);
			Vector3 velocityChange = transform.InverseTransformDirection(targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			velocityChange = transform.TransformDirection(velocityChange);

			r.AddForce(velocityChange, ForceMode.VelocityChange);

			catAnim.SetFloat("MoveSpeed", r.velocity.sqrMagnitude/10f); //Magic number 20f to make velocity reasonably small

			/* if (grounded && velocity.sqrMagnitude > 1f)
			{
				StepDustEmit(); 
			} */
		}
	}

	void Jump()
	{
			r.velocity = r.transform.up * jumpHeight;
			Instantiate(starJump, transform.position + -transform.up, transform.rotation * Quaternion.Euler(0, 0, 0));
			catAnim.SetTrigger("Jump"); // Do jump animation
			catAudio.PlayOneShot(hyppyClip);
			canJump = false;
			jumps = jumps - 1;
			StartCoroutine(JumpCooldown());
	}
	public void StepDustEmit() //Event called by KissaAnimationEvents
	{
		if(r.velocity.sqrMagnitude > 2f && grounded) // If moving full speed aka magic number 2f or faster and there's ground below
		{
			dust.Play();
			catAudio.PlayOneShot(stepClips[Random.Range(0, stepClips.Length)], 0.4f); //Play random clip in array, magic number volume
		}
	}

	IEnumerator JumpCooldown()
	{
		yield return new WaitForSeconds(jumpCooldown);
		canJump = true;
	}

	

	private void OnTriggerEnter(Collider other)
	{
		if (isColliding) return;
		isColliding = true;
		if (other.tag == "DamageZone" && isColliding != invincible)
		{
			PlayerProperties.Instance.RemoveHealth();
			invincible = true;
			StartCoroutine(InvincibleTime());
		}

		if(other.tag == "EnemyAttack" && isColliding != invincible)
		{
			PlayerProperties.Instance.RemoveHealth();
			catAudio.PlayOneShot(zappedClip);
			invincible = true;
			StartCoroutine(InvincibleTime());
		}

		//if (other.CompareTag("Stamp"))
		//{
			//stamps++;
		//}
		StartCoroutine(Reset());
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "DamageZone")
		{
			r.AddForce(transform.up * damageJumpHeight, ForceMode.Impulse);
			//PlayerProperties.Instance.RemoveHealth();
			Debug.Log("You Entered A Damage Zone!");
		}

		if(other.tag == "EnemyAttack")
		{
			Debug.Log("You Got Damaged By An Enemy!");
			frozen = true;
			StartCoroutine(IsFrozen());
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("EnemyAttack"))
		{
			Debug.Log("BZZZT!");
			frozen = true;
			StartCoroutine(IsFrozen());
		}
	}

	
	//private void OnCollisionEnter(Collision other)
	//{
	//if (other.gameObject.CompareTag("EnemyAttack"))
	//{
	//Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();

	//}
	//}

	IEnumerator IsFrozen()
	{
		yield return new WaitForSeconds(frozenTime);
		frozen = false;
	}

	private IEnumerator Reset()
	{
		yield return new WaitForEndOfFrame();
		isColliding = false;
	}

	IEnumerator InvincibleTime()
	{
		yield return new WaitForSeconds(iFrames);
		invincible = false;
	}
}