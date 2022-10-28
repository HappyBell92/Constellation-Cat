using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float attackCooldown = 3;
    //private float attackDuration = 2;
    public bool canAttack = true;
    public GameObject attackObject;
    [SerializeField] GameObject attackSoundPrefab;

    [SerializeField] Animator m_Animator;
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
            canAttack = false;
            m_Animator.SetTrigger("ShockAttack");
            StartCoroutine(AttackCooldown());
        }
    }

    public void ShockAttackEvent() //Called from animation clip
    {
        GameObject newAttack = Instantiate(attackObject, transform.position + -transform.up, transform.rotation * Quaternion.Euler(0, 0, 0));
        Instantiate(attackSoundPrefab, transform.position + -transform.up, transform.rotation * Quaternion.Euler(0, 0, 0));
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
