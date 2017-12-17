using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttack : MonoBehaviour
{
    public Animator sharkAnimator;
    public float attackTime;
    private float attackTimer;
    private bool enemyInAttackRange;
    public bool canDealDamage;

    void Update()
    {
        if (enemyInAttackRange && (attackTimer >= attackTime))
        {
            sharkAnimator.SetBool("isJumping", true);
            attackTimer = 0f;
        }
        else
        {
            sharkAnimator.SetBool("isJumping", false);
        }
        attackTimer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerMinion" || other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyMinion")
        {
            attackTimer = 0f;
            enemyInAttackRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerMinion" || other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyMinion")
        {
            attackTimer = 0f;
            enemyInAttackRange = false;
        }
    }
}
