using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampCollectables : MonoBehaviour
{
    private bool isColliding = false;
    public GameObject stampCollectSoundPrefab;

    
    
    

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
        if (other.tag == "Player" && isColliding)
        {
            PlayerProperties.Instance.AddStamps();
            PlayerProperties.Instance.AddHealth();
            //AudioSource.PlayClipAtPoint(stampCollect, transform.position);
			Instantiate(stampCollectSoundPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
            
            Debug.Log("You Got A Stamp!");
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
