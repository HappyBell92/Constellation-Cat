using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RigidbodyWalker : MonoBehaviour
{
    public float speed = 5.0f;
    public bool canJump = true;
    public float jumpCooldown = 1;
    public float jumpHeight = 2.0f;
    public float damageJumpHeight = 2.0f;
    public float enemyDamageUp = 1.0f;
    public float enemyDamageBack = 2.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;

    public float rotationSpeed;
    private float frozenTime = 1;

    public bool frozen = false;
    bool grounded = false;
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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
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

            //r.transform.rotation = Quaternion.LookRotation(r.velocity, transform.up);

            if (Input.GetButton("Jump") && canJump && grounded)
            {
                r.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
                canJump = false;
                StartCoroutine(JumpCooldown());
            }

        }

            
        

        grounded = false;
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DamageZone")
        {
            PlayerProperties.Instance.RemoveHealth();
            //r.AddForce(transform.up * damageJumpHeight, ForceMode.Impulse);
            //Debug.Log("You Entered A Damage Zone!");
        }

        if(other.tag == "EnemyAttack")
        {
            //PlayerProperties.Instance.RemoveHealth();
        }
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
}