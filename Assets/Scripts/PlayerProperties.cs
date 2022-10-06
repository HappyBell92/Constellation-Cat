using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField] private int stars;

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

    // Start is called before the first frame update
    void Start()
    {
        stars = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.UpdateStarText(stars);
    }
}
