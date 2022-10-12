using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float attackCooldown = 3;
    //private float attackDuration = 2;
    public bool canAttack = true;
    public GameObject attackObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canAttack)
        {
            Debug.Log("I ATTACK!");
            GameObject newAttack = Instantiate(attackObject, transform.position + -transform.up, transform.rotation);
            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        Debug.Log("I Can Attack Again");
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
