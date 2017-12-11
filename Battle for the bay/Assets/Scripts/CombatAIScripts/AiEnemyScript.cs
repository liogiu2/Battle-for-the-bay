using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEnemyScript : AIRootScript
{
    private GameObject NewTargetEnemy;
    public void ActivateTarget()
    {
        transform.Find("Target").gameObject.SetActive(true);
    }

    public void DeActivateTarget()
    {
        transform.Find("Target").gameObject.SetActive(false);
    }

    protected override void OnUpdate()
    {
        NewTargetEnemy = null;
        if (enemies.Count == 1)
        {
            TargetEnemy = enemies[0].gameObject;
        }
        else
        {
            NewTargetEnemy = enemies.Find(ai => ai.gameObject.tag == TagCostants.Player).gameObject;
            if (NewTargetEnemy == null)
            {
                if (gameObject.tag == TagCostants.PlayerMinion)
                {
                    NewTargetEnemy = enemies.FindAll(ai => ai.gameObject.tag == TagCostants.EnemyMinion)[0].gameObject;
                }
                if (gameObject.tag == TagCostants.EnemyMinion)
                {
                    NewTargetEnemy = enemies.FindAll(ai => ai.gameObject.tag == TagCostants.PlayerMinion)[0].gameObject;
                }
            }
        }

        if (NewTargetEnemy != TargetEnemy)
        {
            StopCorutineFire();
            TargetEnemy = NewTargetEnemy;
        }
    }

    protected override void ChooseWhoShot()
    {
        StartCoroutine(Fire(TargetEnemy));
    }

}
