using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampCollectables : MonoBehaviour
{
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
        if(other.tag == "Player")
        {
            PlayerProperties.Instance.AddStamps();
            PlayerProperties.Instance.AddHealth();
            Debug.Log("You Got A Stamp!");
            Destroy(this.gameObject);
        }
    }
}
