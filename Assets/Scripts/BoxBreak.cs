using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreak : MonoBehaviour
{
    [SerializeField] GameObject itemSpawn;
    [SerializeField] GameObject breakBoxAudioPrefab;
    public ParticleSystem breakBox;
    private float knockBackStrength = 2.0f;
    //public AudioClip breakSound;
    //public Collider collider;
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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("I have broken!");
            GameObject newStamp = Instantiate(itemSpawn, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
            Instantiate(breakBox, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));

            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = other.gameObject.transform.position - transform.position;

            playerRigidbody.velocity = playerRigidbody.transform.up * knockBackStrength;
            //AudioSource.PlayClipAtPoint(breakSound, transform.position);
			Instantiate(breakBoxAudioPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
            //collider.enabled = mesh.enabled = false;
            Destroy(this.gameObject);
        }
    }
}
