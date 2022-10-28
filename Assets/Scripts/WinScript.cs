using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    [SerializeField] PlayerProperties collectablesScript;
    [SerializeField] GameObject winScreen;
    public ParticleSystem winActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(collectablesScript.winCondition == true)
        {
            winActive.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("winCondtition = " + collectablesScript.winCondition+", stars = "+ collectablesScript.stars);
        if (collectablesScript.winCondition == true && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("YOU WIN!");
            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
