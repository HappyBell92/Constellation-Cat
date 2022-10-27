using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishEvents : MonoBehaviour
{
    [SerializeField] EnemyAttack enemyAttackScript;
    public void ShockAttack()
    {
        enemyAttackScript.ShockAttackEvent();
    }
}
