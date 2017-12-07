using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEnemyScript : AIRootScript
{

    public List<GameObject> Targets;
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
        if (enemies.Count == 1)
        {
            TargetEnemy = enemies[0].gameObject;
        }
    }

    protected override void ChooseWhoShot()
    {
        if (Base && TargetEnemy)
        {
            StartCoroutine(Fire(TargetEnemy));
        }
        else if (TargetEnemy == null)
        {
            StartCoroutine(Fire(Base));
        }
        else
        {
            StartCoroutine(Fire(TargetEnemy));
        }
    }
}
