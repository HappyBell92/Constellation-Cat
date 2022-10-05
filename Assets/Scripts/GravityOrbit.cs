using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    // the center that all object orbit
    public float gravity;

    public bool fixedDirection; // the gravity of this section is pulling the player down

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
        if (other.GetComponent<GravityCtrl>())
        {
            // if this object has a gravity script, set this as the planet
            other.GetComponent<GravityCtrl>().gravity = this.GetComponent<GravityOrbit>();
        }
    }
}
