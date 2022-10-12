using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDuration : MonoBehaviour
{
    private float duration = 3;
    private float growSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AttackDuration());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= new Vector3(-1, 0, -1) * Time.deltaTime * growSpeed;
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
