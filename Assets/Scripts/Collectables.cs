using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private bool isColliding = false;
    public GameObject starGetSoundPrefab;

    

    public bool includeChildren = true;
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
        if (isColliding) return;
        isColliding = true;
        if(other.tag == "Player" && isColliding)
        {
            PlayerProperties.Instance.AddStars();
            Debug.Log("You Got A Star!");
            //AudioSource.PlayClipAtPoint(starGet, transform.position);
			Instantiate(starGetSoundPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
            
            Destroy(this.gameObject);
        }
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }
}
