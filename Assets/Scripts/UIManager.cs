using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text starText;
    public Text stampText;
    public Text healthText;

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.Log("UI Manager is null");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void UpdateStarText(int stars)
    {
        starText.text = "Stars: " + stars;
    }

    public void UpdateStampText(int stamps)
    {
        stampText.text = "Stamps: " + stamps;
    }

    public void UpdateHealthText(int health)
    {
        healthText.text = "Health: " + health;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
