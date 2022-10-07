using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField] private int stars;
    [SerializeField] private int stamps;
    [SerializeField] private int health;

    private static PlayerProperties instance;
    public static PlayerProperties Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("Player Is Null");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void AddStars()
    {
        stars++;
    }

    public void AddStamps()
    {
        stamps++;
    }

    public void AddHealth()
    {
        health++;
    }

    public void RemoveHealth()
    {
        health--;
    }

    // Start is called before the first frame update
    void Start()
    {
        stars = 0;
        stamps = 0;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.UpdateStarText(stars);
        UIManager.Instance.UpdateStampText(stamps);
        UIManager.Instance.UpdateHealthText(health);

        if(health > 3)
        {
            health = 3;
        }

        if(health == 0)
        {
            Debug.Log("You Died");
            Destroy(this.gameObject);
        }
    }
}
