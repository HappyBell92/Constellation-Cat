using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreak : MonoBehaviour
{
    [SerializeField] GameObject star;
    [SerializeField] GameObject stamp;
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
            GameObject newStamp = Instantiate(stamp, transform.position + transform.up, transform.rotation * Quaternion.Euler(0, 0, 0));
            Destroy(this.gameObject);
        }
    }
}
