using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    [SerializeField] PlayerProperties collectablesScript;

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
        //Debug.Log("winCondtition = " + collectablesScript.winCondition+", stars = "+ collectablesScript.stars);
        if (collectablesScript.winCondition = true && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("YOU WIN!");
        }
    }
}
