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
            var playerInRange = enemies.Find(ai => ai.gameObject.tag == TagCostants.Player);
            if (playerInRange == null)
            {
                List<GameObject> minionInRange = new List<GameObject>();
                if (gameObject.tag == TagCostants.PlayerMinion)
                {
                    minionInRange = enemies.FindAll(ai => ai.gameObject.tag == TagCostants.EnemyMinion);
                }
                if (gameObject.tag == TagCostants.EnemyMinion)
                {
                    minionInRange = enemies.FindAll(ai => ai.gameObject.tag == TagCostants.PlayerMinion);
                }
                if (minionInRange.Count > 0)
                {
                    NewTargetEnemy = minionInRange[0].gameObject;
                }
                else if(minionInRange.Count == 0)
                {
                    List<GameObject> towerOrBaseInRange = new List<GameObject>();
                    if (gameObject.tag == TagCostants.PlayerMinion)
                    {
                        towerOrBaseInRange = enemies.FindAll(ai => ai.gameObject.tag == TagCostants.EnemyTower || ai.gameObject.tag == TagCostants.EnemyBase);
                    }
                    if (gameObject.tag == TagCostants.EnemyMinion)
                    {
                        towerOrBaseInRange = enemies.FindAll(ai => ai.gameObject.tag == TagCostants.PlayerTower || ai.gameObject.tag == TagCostants.PlayerBase);
                    }
                    if (towerOrBaseInRange.Count > 0)
                    {
                        NewTargetEnemy = towerOrBaseInRange[0].gameObject.transform.parent.gameObject;
                        Debug.Log("Try shoot base: "+NewTargetEnemy.name);                                                
                    }
                }
            }
            else
            {
                NewTargetEnemy = playerInRange.gameObject;
            }
        }
        if (NewTargetEnemy != TargetEnemy)
        {
            StopCorutineFire();
        }
        if (NewTargetEnemy != null)
        {
            TargetEnemy = NewTargetEnemy;
        }

    }

    protected override void ChooseWhoShot()
    {
        StartCoroutine(Fire(TargetEnemy));
    }

}
