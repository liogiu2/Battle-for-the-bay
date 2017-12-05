using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerScript : AIRootScript
{

    protected override void OnUpdate()
    {
        UpdateLists();
    }
    private void UpdateLists()
    {
        if (_updateEnemyList.GetDestroyingItem())
        {
            detected.Remove(_updateEnemyList.GetDestroyingItem());
            enemies.Remove(_updateEnemyList.GetDestroyingItem());
            if (TargetEnemy.GetComponent<AIRootScript>() == _updateEnemyList.GetDestroyingItem())
            {
                TargetEnemy = null;
            }
            _updateEnemyList.RemoveAiRootScript();
        }
    }

    public void ActivateTarget()
    {
    }

    public void DeActivateTarget()
    {
    }

}
