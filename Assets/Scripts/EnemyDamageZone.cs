using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageZone : MonoBehaviour
{

    public float knockBackStrength = 10.0f; // how hard the player gets knocked back
    public float knockBackUp = 2.0f; // how hard the player gets knocked upwards
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
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = other.gameObject.transform.position - transform.position;

            playerRigidbody.AddForce(awayFromEnemy * knockBackStrength, ForceMode.Impulse);
            playerRigidbody.AddForce(transform.up * knockBackUp, ForceMode.Impulse);
        }
    }
}
