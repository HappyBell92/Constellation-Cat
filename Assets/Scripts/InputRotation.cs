using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotation : MonoBehaviour
{
    public Camera playerCamera;

    public float speed;

    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        
    }

    private void FixedUpdate()
    {
        Vector3 forwardDir = Vector3.Cross(transform.up, -playerCamera.transform.right).normalized;
        Vector3 rightDir = Vector3.Cross(transform.up, playerCamera.transform.forward).normalized;
        Vector3 targetVelocity = (forwardDir * Input.GetAxis("Vertical") + rightDir * Input.GetAxis("Horizontal")) * speed;

        if(targetVelocity != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(targetVelocity, transform.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
