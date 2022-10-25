using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreak : MonoBehaviour
{
    [SerializeField] GameObject star;
    [SerializeField] GameObject stamp;
    private float knockBackStrength = 2.0f;
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
            Debug.Log("I have broken!");
            GameObject newStamp = Instantiate(stamp, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));

            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = other.gameObject.transform.position - transform.position;

            playerRigidbody.velocity = playerRigidbody.transform.up * knockBackStrength;
            Destroy(this.gameObject);
        }
    }
}
