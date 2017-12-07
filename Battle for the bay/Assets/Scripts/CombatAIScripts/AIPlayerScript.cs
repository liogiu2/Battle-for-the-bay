using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerScript : AIRootScript
{

    protected override void OnUpdate()
    {

    }

    protected override void ChooseWhoShot()
    {
        StartCoroutine(Fire(TargetEnemy));
    }


    public void ActivateTarget()
    {
    }

    public void DeActivateTarget()
    {
    }

}
