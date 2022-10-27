using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField] public int stars;
    [SerializeField] private int stamps;
    [SerializeField] private int health;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject WinScreen;

    private float starTimer = 0.2f;
    public bool canCollectStar = true;

    public bool winCondition = false;

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
        if (canCollectStar = true)
        {
            stars++;
            canCollectStar = false;
            StartCoroutine(StarCoolDown());
        }
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

        if(health <= 0)
        {
            gameOverMenu.SetActive(true);
            mainUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("You Died");
            Destroy(this.gameObject);

        }

        if(stars >= 12)
        {
            winCondition = true;
            WinScreen.SetActive(true);
        }
    }

    IEnumerator StarCoolDown()
    {
        yield return new WaitForSeconds(starTimer);
        canCollectStar = true;
    }

    
}
