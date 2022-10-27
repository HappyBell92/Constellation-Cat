using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampCollectables : MonoBehaviour
{
    private bool isColliding = false;
    public AudioSource stampCollect;

    public Collider collider;
    public MeshRenderer mesh;
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
            stampCollect.Play();
            collider.enabled = mesh.enabled = false;
            Debug.Log("You Got A Stamp!");
            //Destroy(this.gameObject);
        }
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }
}
