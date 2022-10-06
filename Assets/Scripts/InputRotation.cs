using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotation : MonoBehaviour
{

    public float rotationSpeed;
    public Transform orientation;
    public Transform playerObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        

        
    }
}
