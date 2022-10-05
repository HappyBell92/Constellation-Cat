using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCtrl : MonoBehaviour
{
    // attch to all rigidbodies to orbit the world
    public GravityOrbit gravity;
    private Rigidbody rb;

    public float rotationSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (gravity)     // if there is a set planet to orbit
        {
            Vector3 gravityUp = Vector3.zero;

            if (gravity.fixedDirection)
            {
                gravityUp = gravity.transform.up;
            }
            else
            {
                gravityUp = (transform.position - gravity.transform.position).normalized;
            }

            Vector3 localUp = transform.up;

            Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
            rb.GetComponent<Rigidbody>().rotation = targetRotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.GetComponent<Rigidbody>().AddForce((-gravityUp * gravity.gravity) * rb.mass);
        }
    }
}






//transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed * Time.deltaTime);

// push down for gravity
//rb.AddForce((-gravityUp * gravity.gravity) * rb.mass);